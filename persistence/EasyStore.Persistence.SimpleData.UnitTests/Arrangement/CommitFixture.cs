namespace EasyStore.Persistence.SimpleData.UnitTests.Arrangement
{
    using System;

    public class CommitFixture : PersistStreamsFixtureBase
    {
        public ICommit CommitResult { get; private set; }

        public Action Commit(CommitAttempt attempt)
        {
            var sut = this.CreateSut();
            return () => this.CommitResult = sut.Commit(attempt);
        }
    }
}