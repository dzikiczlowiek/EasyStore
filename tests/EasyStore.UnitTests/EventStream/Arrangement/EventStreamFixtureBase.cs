namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using EventStream = EasyStore.EventStream;

    public abstract class EventStreamFixtureBase
    {
        protected EventStreamFixtureBase()
        {
            this.CommitEventsStub = new PersistStreamStub();
        }

        public string StreamId { get; protected set; }

        public IEventStream Stream { get; protected set; }

        protected PersistStreamStub CommitEventsStub { get; private set; }

        protected virtual IEventStream CreateSut()
        {
            this.Stream = new EventStream(this.StreamId, this.CommitEventsStub.Create());
            return this.Stream;
        }
    }
}
