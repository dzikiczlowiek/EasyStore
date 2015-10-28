namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using System;

    using EasyStore.CommonDomain;

    using EventStream = EasyStore.EventStream;

    public class LoadAggregateFixture
    {
        private readonly PersistStreamStub _commitEventsStub = new PersistStreamStub();

        public string StreamId { get; protected set; }

        public static LoadAggregateFixture Create()
        {
            var fixture = new LoadAggregateFixture();
            return fixture;
        }

        public Action LoadAggregate<T>(Guid aggregateId)
            where T : AggregateRoot
        {
            var sut = this.CreateSut();
            return () => sut.LoadAggregate<T>(aggregateId);
        }

        private IEventStream CreateSut()
        {
            var stream = new EventStream(this.StreamId, this._commitEventsStub.Create());
            return stream;
        }
    }
}
