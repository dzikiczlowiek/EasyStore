namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using System;

    using EasyStore.CommonDomain;

    public class LoadAggregateFixture : EventStreamFixtureBase
    {
        public AggregateRoot Aggregate { get; private set; }

        public static LoadAggregateFixture Create()
        {
            var fixture = new LoadAggregateFixture();
            return fixture;
        }

        public void persist_aggregate_events(AggregateRoot aggregate)
        {
            this.CommitEventsStub.AddAggregate(aggregate);
        }

        public Action LoadAggregate<T>(Guid aggregateId)
            where T : AggregateRoot
        {
            var sut = this.CreateSut();
            return () => this.Aggregate = sut.LoadAggregate<T>(aggregateId);
        }
    }
}
