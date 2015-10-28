namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using System;

    public class CommitChangesFixture : EventStreamFixtureBase
    {
        public Action CommitChanges(Guid commitId)
        {
            var sut = this.CreateSut();
            return () => sut.CommitChanges(commitId);
        }
    }
}