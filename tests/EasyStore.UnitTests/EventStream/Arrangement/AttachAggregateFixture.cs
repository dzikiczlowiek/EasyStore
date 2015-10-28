namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using System;

    using EasyStore.CommonDomain;

    using EventStream = EasyStore.EventStream;

    public class AttachAggregateFixture
    {
        private readonly PersistStreamStub _persistenceStub = new PersistStreamStub();

        public string StreamId { get; protected set; }

        public IEventStream Stream { get; protected set; }

        public Action AttacheAggregate(AggregateRoot aggregate)
        {
            this.Stream = this.CreateSut();
            return () => this.Stream.AttachAggregate(aggregate);
        }

        private IEventStream CreateSut()
        {
            var stream = new EventStream(this.StreamId, this._persistenceStub.Create());
            return stream;
        }
    }
}
