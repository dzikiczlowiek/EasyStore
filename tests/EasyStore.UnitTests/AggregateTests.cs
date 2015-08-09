namespace EasyStore.UnitTests
{
    using EasyStore.CommonDomain;
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement;

    using FluentAssertions;

    using Xunit;

    public class AggregateTests : TestBase
    {
        [Fact]
        public void applying_events_should_raise_aggregate_version()
        {
            var aggregateId = A.RandomGuid();
            var aggregate = DummyAggregate.CreateNew(aggregateId);

            aggregate.ChangeAge(A.RandomNumber());
            aggregate.ChangeName(A.RandomShortString());

            aggregate.Version.Should().Be(2);
        }

        [Fact]
        public void applying_events_should_return_this_events_with_method_GetUncommitedEvents()
        {
            var aggregateId = A.RandomGuid();
            var aggregate = DummyAggregate.CreateNew(aggregateId);

            aggregate.ChangeAge(A.RandomNumber());
            aggregate.ChangeName(A.RandomShortString());

            (aggregate as IAggregate).GetUncommittedEvents().Count.Should().Be(2);
        }
    }
}
