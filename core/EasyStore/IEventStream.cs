namespace EasyStore
{
    using System;
    using System.Collections.Generic;

    using EasyStore.CommonDomain;

    public interface IEventStream : IDisposable
    {
        string StreamId { get; }

        int CommitSequence { get; }

        ICollection<EventMessage> CommittedEvents { get; }

        ICollection<EventMessage> UncommittedEvents { get; }
      
        void Add(EventMessage uncommittedEvent);

        void CommitChanges(Guid commitId);

        void ClearChanges();

        TAggregate LoadAggregate<TAggregate>(Guid aggregateId) where TAggregate : AggregateRoot;

        void AttachAggregate<TAggregate>(TAggregate aggregate) where TAggregate : AggregateRoot;

        void ForwardEvent(Guid id, IDomainEvent @event);
    }
}
