namespace EasyStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using EasyStore.CommonDomain;
    using EasyStore.Infrastructure;

    public class EventStream : IEventStream
    {
        private readonly ICollection<EventMessage> _commitedEvents = new LinkedList<EventMessage>();

        private readonly ICollection<EventMessage> _uncommitedEvents = new LinkedList<EventMessage>();

        private readonly ICollection<IAggregate> _aggregates = new LinkedList<IAggregate>(); 

        private readonly ICommitEvents _persistence;

        public EventStream(string streamId, ICommitEvents persistence)
        {
            this.StreamId = streamId;
            this._persistence = persistence;
        }

        public EventStream(string streamId, int minRevision, int maxRevision, ICommitEvents persistence)
            : this(streamId, persistence)
        {
            throw new NotImplementedException();
        }

        public int CommitSequence { get; private set; }

        public string StreamId { get; private set; }

        public ICollection<EventMessage> CommittedEvents
        {
            get
            {
                return new List<EventMessage>(this._commitedEvents);
            }
        }

        public ICollection<EventMessage> UncommittedEvents
        {
            get
            {
                return new List<EventMessage>(this._uncommitedEvents);
            }
        }

        public int StreamRevision { get; private set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Add(EventMessage uncommittedEvent)
        {
            Guard.NotNull(() => uncommittedEvent);
            Guard.NotNull(() => uncommittedEvent.Body);

            this._uncommitedEvents.Add(uncommittedEvent);
        }

        public void CommitChanges(Guid commitId)
        {
            var commiteAttempt = new CommitAttempt(this.StreamId, commitId, this._uncommitedEvents);
            this._persistence.Commit(commiteAttempt);
            this.ClearChanges();
        }

        public void ClearChanges()
        {
            this._uncommitedEvents.Clear();
        }

        public TAggregate LoadAggregate<TAggregate>(Guid aggregateId) where TAggregate : AggregateRoot
        {
            var persistedEvents = this._persistence.GetAggregateEvents(aggregateId);
            throw new NotImplementedException();
        }

        public void AttachAggregate<TAggregate>(TAggregate aggregate) where TAggregate : AggregateRoot
        {
            this._aggregates.Add(aggregate);
            foreach (var uncommittedEvent in ((IAggregate)aggregate).GetUncommittedEvents())
            {
                ((IEventStream)this).ForwardEvent(aggregate.Id, uncommittedEvent);
            }

            ((IAggregate)aggregate).AttachToStream(this);
        }

        void IEventStream.ForwardEvent(Guid aggregateId, IDomainEvent @event)
        {
            var eventMessage = new EventMessage();
            eventMessage.AggregateId = aggregateId;
            eventMessage.Body = @event;
            eventMessage.BodyType = @event.GetType().AssemblyQualifiedName;
            this.Add(eventMessage);
        }
    }
}
