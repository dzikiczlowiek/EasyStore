namespace EasyStore
{
    using System;
    using System.Collections.Generic;

    public interface IEventStream : IDisposable
    {
        string StreamId { get; }

        ICollection<EventMessage> CommittedEvents { get; }

        ICollection<EventMessage> UncommittedEvents { get; }

        void Add(EventMessage uncommittedEvent);

        void CommitChanges(Guid commitId);

        void ClearChanges();
    }
}
