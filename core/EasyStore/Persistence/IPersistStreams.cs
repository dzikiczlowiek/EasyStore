namespace EasyStore.Persistence
{
    using System;
    using System.Collections.Generic;

    public interface IPersistStreams : IDisposable, ICommitEvents, IAccessSnapshots
    {
        void Initialize();

        IEnumerable<EventMessage> GetAggregateEvents(Guid aggregateId);

        IEnumerable<EventMessage> GetAggregateEventsToVersion(Guid aggregateId, int version);
    }
}
