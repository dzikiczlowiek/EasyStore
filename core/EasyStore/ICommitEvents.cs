namespace EasyStore
{
    using System.Collections.Generic;

    public interface ICommitEvents
    {
        IEnumerable<ICommit> GetFrom(string streamId, int minRevision, int maxRevision);

        ICommit Commit(CommitAttempt attempt);
    }
}
