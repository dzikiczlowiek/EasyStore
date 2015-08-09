namespace EasyStore.Persistence.SimpleData
{
    using System;
    using System.Collections.Generic;

    using Simple.Data;

    public class SimpleDataPersistenceEngine : IPersistStreams
    {
        private readonly dynamic _db;

        private readonly ISerialize _serializer;

        public SimpleDataPersistenceEngine(string connectionName, ISerialize serializer)
        {
            this._db = Database.OpenNamedConnection(connectionName);
            this._serializer = serializer;
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<EventMessage> GetAggregateEvents(Guid aggregateId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<EventMessage> GetAggregateEventsToVersion(Guid aggregateId, int version)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICommit> GetFrom(string streamId, int minRevision, int maxRevision)
        {
            throw new System.NotImplementedException();
        }

        public ICommit Commit(CommitAttempt attempt)
        {
            throw new System.NotImplementedException();
        }

        public Snapshot GetSnapshotOfAggregate(Guid aggregateId)
        {
            throw new System.NotImplementedException();
        }

        public Snapshot GetSnapshotOfAggregateUpToVersion(Guid aggregateId, int maxVersion)
        {
            throw new System.NotImplementedException();
        }
    }
}
