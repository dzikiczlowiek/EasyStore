namespace EasyStore.Persistence.SimpleData
{
    using System.Collections.Generic;

    using Simple.Data;

    public class SimpleDataPersistenceEngine : IPersistStreams
    {
        private readonly dynamic _db;

        public SimpleDataPersistenceEngine(string connectionName)
        {
            this._db = Database.OpenNamedConnection(connectionName);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICommit> GetFrom(string streamId, int minRevision, int maxRevision)
        {
            throw new System.NotImplementedException();
        }

        ICommit IPersistStreams.Commit(CommitAttempt commitAttempt)
        {

            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        ICommit ICommitEvents.Commit(CommitAttempt attempt)
        {
            throw new System.NotImplementedException();
        }

    }
}
