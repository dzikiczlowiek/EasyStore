namespace EasyStore.UnitTests.EventStore.Arrangement
{
    using System;

    using EventStore = EasyStore.EventStore;

    public class OpenStreamFixture
    {
        private readonly PersistStreamStub _persistStreamStub;

        private OpenStreamFixture()
        {
            this._persistStreamStub = new PersistStreamStub();
            this.Revision = new RevisionRange();
        }

        public string StreamId { get; private set; }

        public RevisionRange Revision { get; private set; }

        public IEventStream ReturnedEventStream { get; private set; }

        public static OpenStreamFixture WithStreamId(string streamId)
        {
            var fixture = new OpenStreamFixture();
            fixture.StreamId = streamId;
            return fixture;
        }

        public Action OpenStream()
        {
            var sut = this.CreateSut();
            return () => this.ReturnedEventStream = sut.OpenStream(this.StreamId, this.Revision.Min, this.Revision.Max);
        }

        private EventStore CreateSut()
        {
            var sut = new EventStore(this._persistStreamStub.Create());
            return sut;
        }

        public void ThereIsNoCommitsForGivenStreamId()
        {
            this._persistStreamStub.NoCommitsFor(this.StreamId, this.Revision);
        }
    }
}