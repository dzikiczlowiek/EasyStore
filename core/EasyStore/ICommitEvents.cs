namespace EasyStore
{
    using System;
    using System.Collections.Generic;

    public interface ICommitEvents
    {
        IEnumerable<EventMessage> GetAggregateEvents(Guid aggregateId);

        IEnumerable<EventMessage> GetAggregateEventsToVersion(Guid aggregateId, int version);

        IEnumerable<ICommit> GetFrom(string streamId, int minRevision, int maxRevision);

        ICommit Commit(CommitAttempt attempt);
    }
}
