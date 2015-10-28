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

        TAggregate LoadAggregate<TAggregate>(Guid aggregateId) where TAggregate : class, IAggregate;

        void AttachAggregate<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregate;
    }
}
