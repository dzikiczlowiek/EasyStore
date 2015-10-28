namespace EasyStore.UnitTests.EventStream
{
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.Domain.Dummy;
    using EasyStore.UnitTests.EventStream.Arrangement;

    using Xunit;

    public class LoadAggregateTests : TestBase
    {
        [Fact]
        public void ASASA()
        {
            var fixture = LoadAggregateFixture.Create();
            var aggregateId = A.RandomGuid();
            
            var act = fixture.LoadAggregate<DummyAggregate>(aggregateId);
            act();
        }
    }
}
