namespace EasyStore.Persistence
{
    using System;

    public interface IAccessSnapshots
    {
        Snapshot GetSnapshotOfAggregate(Guid aggregateId);

        Snapshot GetSnapshotOfAggregateUpToVersion(Guid aggregateId, int maxVersion);
    }
}
