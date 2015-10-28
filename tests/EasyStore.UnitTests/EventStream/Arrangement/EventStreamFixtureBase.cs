namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using EventStream = EasyStore.EventStream;

    public abstract class EventStreamFixtureBase
    {
        private readonly PersistStreamStub _commitEventsStub = new PersistStreamStub();

        public string StreamId { get; protected set; }

        public IEventStream Stream { get; protected set; }

        protected virtual IEventStream CreateSut()
        {
            this.Stream = new EventStream(this.StreamId, this._commitEventsStub.Create());
            return this.Stream;
        }
    }
}
