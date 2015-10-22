namespace EasyStore.UnitTests.EventStore.Arrangement
{
    using EasyStore.Persistence;

    using Moq;

    public class PersistStreamStub
    {
        private readonly Mock<IPersistStreams> _mock = new Mock<IPersistStreams>();

        public IPersistStreams Create()
        {
            return this._mock.Object;
        }

        public void NoCommitsFor(string streamId, RevisionRange revision)
        {
            this._mock.Setup(x => x.GetFrom(streamId, revision.Min, revision.Max)).Returns(() => null);
        }
    }
}
