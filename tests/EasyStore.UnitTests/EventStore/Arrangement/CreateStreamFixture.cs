namespace EasyStore.UnitTests.EventStore.Arrangement
{
    using System;

    using EventStore = EasyStore.EventStore;

    public class CreateStreamFixture
    {
        private readonly PersistStreamStub _persistStreamStub;

        private CreateStreamFixture()
        {
            this._persistStreamStub = new PersistStreamStub();
        }

        public string StreamId { get; private set; }

        public IEventStream CreatedEventStream { get; private set; }

        public static CreateStreamFixture WithStreamId(string streamId)
        {
            var fixture = new CreateStreamFixture();
            fixture.StreamId = streamId;
            return fixture;
        }

        public Action CreateStream()
        {
            var sut = this.CreateSut();
            return () => this.CreatedEventStream = sut.CreateStream(this.StreamId);
        }

        private EventStore CreateSut()
        {
            var sut = new EventStore(this._persistStreamStub.Create());
            return sut;
        }
    }
}
