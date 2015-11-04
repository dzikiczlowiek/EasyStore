namespace EasyStore.UnitTests.Aggregate
{
    using System.Linq;

    using EasyStore.CommonDomain;
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Person;

    using FluentAssertions;

    using Xunit;

    public class AggregateTests : TestBase
    {
        [Fact]
        public void newly_created_aggregate_should_have_one_uncommited_event_with_its_id()
        {
            var aggregateId = A.RandomGuid();
            var personAggregate = Person.CreateNew(aggregateId);
            
            var events = personAggregate.GetUncommittedEvents();
            events.Should().HaveCount(1);
            var createEvent = events.Single() as CreatedEvent;
            createEvent.AggregateId.Should().Be(aggregateId);
        }
    }
}
