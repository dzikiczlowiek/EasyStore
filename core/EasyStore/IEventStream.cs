namespace EasyStore
{
    using System;
    using System.Collections.Generic;

    using EasyStore.CommonDomain;

    public interface IEventStream : IDisposable
    {
        string StreamId { get; }

        ICollection<EventMessage> CommittedEvents { get; }

        ICollection<EventMessage> UncommittedEvents { get; }

        TAggregate LoadAggregate<TAggregate>(Guid aggregateId) where TAggregate : class, IAggregate;

        void Add(EventMessage uncommittedEvent);

        void CommitChanges(Guid commitId);

        void ClearChanges();
    }
}
