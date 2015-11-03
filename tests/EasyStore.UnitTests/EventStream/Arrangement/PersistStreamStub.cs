namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using System.Collections.Generic;

    using Moq;

    public class PersistStreamStub
    {
        private readonly Mock<ICommitEvents> _mock = new Mock<ICommitEvents>();

        public PersistStreamStub()
        {
            this.CommitAttempts = new List<CommitAttempt>();
        }

        public ICollection<CommitAttempt> CommitAttempts { get; private set; }

        public ICommitEvents Create()
        {
            this.MonitorCommits();
            return this._mock.Object;
        }


        private void MonitorCommits()
        {
            this._mock.Setup(x => x.Commit(It.IsAny<CommitAttempt>()))
                .Callback<CommitAttempt>(attempt => this.CommitAttempts.Add(attempt));
        }
    }
}
