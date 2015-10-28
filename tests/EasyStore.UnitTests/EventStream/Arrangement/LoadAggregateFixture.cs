namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using System;

    using EasyStore.CommonDomain;

    public class LoadAggregateFixture : EventStreamFixtureBase
    {
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
    }
}
