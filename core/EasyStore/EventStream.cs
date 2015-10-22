namespace EasyStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EasyStore.CommonDomain;
    using EasyStore.Infrastructure;

    public class EventStream : IEventStream
    {
        private readonly ICollection<EventMessage> _commitedEvents = new LinkedList<EventMessage>();

        private readonly ICollection<EventMessage> _uncommitedEvents = new LinkedList<EventMessage>();

        private readonly ICommitEvents _persistence;

        public EventStream(string streamId, ICommitEvents persistence)
        {
            this.StreamId = streamId;
            this._persistence = persistence;
        }

        public EventStream(string streamId, int minRevision, int maxRevision, ICommitEvents persistence)
            : this(streamId, persistence)
        {
            IEnumerable<ICommit> commits = persistence.GetFrom(streamId, minRevision, maxRevision);
            this.PopulateStream(minRevision, maxRevision, commits);
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

        public TAggregate LoadAggregate<TAggregate>(Guid aggregateId) where TAggregate : class, IAggregate
        {
            var persistedEvents = this._persistence.GetAggregateEvents(aggregateId);

            return default(TAggregate);
        }

        private void PopulateStream(int minRevision, int maxRevision, IEnumerable<ICommit> commits)
        {
            foreach (var commit in commits ?? Enumerable.Empty<ICommit>())
            {
                this.CommitSequence = commit.CommitSequence;
            }
        }
    }
}
