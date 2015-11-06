namespace EasyStore.UnitTests.Aggregate
{
    using EasyStore.CommonDomain;
    using EasyStore.Persistence;
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Order;

    using FluentAssertions;

    using Xunit;

    public class DefaultAggregatorConstructorTests : TestBase
    {
        [Fact]
        public void should_build_any_AggregateRoot_inherited_aggregate()
        {
            var aggregateConstructor = new DefaultAggregateConstructor();

            var aggregate = aggregateConstructor.Build<Order>();

            aggregate.Should().BeOfType(typeof(Order));
        }

        [Fact]
        public void builded_aggregate_should_not_have_any_uncommitted_events()
        {
            var aggregateConstructor = new DefaultAggregateConstructor();

            var aggregate = aggregateConstructor.Build<Order>();

            aggregate.GetUncommittedEvents().Should().BeEmpty();
        }

        [Fact]
        public void builded_aggregate_should_have_any_event_router()
        {
            var aggregateConstructor = new DefaultAggregateConstructor();

            var aggregate = aggregateConstructor.Build<Order>();

            var eventRouter = aggregate.GetPrivateFieldValue<AggregateRoot, IRouteEvents>("_eventRouter");
            eventRouter.Should().NotBeNull();
        }
    }
}
