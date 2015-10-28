namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using System;

    using EasyStore.CommonDomain;

    public class AttachAggregateFixture : EventStreamFixtureBase
    {
        public Action AttachAggregate(AggregateRoot aggregate)
        {
            var sut = this.CreateSut();
            return () => sut.AttachAggregate(aggregate);
        }
    }
}
