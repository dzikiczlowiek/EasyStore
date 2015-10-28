namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using Moq;

    public class PersistStreamStub
    {
        private readonly Mock<ICommitEvents> _mock = new Mock<ICommitEvents>();

        public ICommitEvents Create()
        {
            return this._mock.Object;
        }
    }
}
