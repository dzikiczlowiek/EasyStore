namespace EasyStore.UnitTests.EventStream
{
    using System.Linq;

    using EasyStore.CommonDomain;
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.Domain.Dummy;
    using EasyStore.UnitTests.EventStream.Arrangement;

    using FluentAssertions;

    using Xunit;

    public class AttachAggregateTests : TestBase
    {
        [Fact]
        public void attached_aggregate_with_already_two_events_should_add_those_events_to_stream_events()
        {
            var fixture = new AttachAggregateFixture();

            var dummyAggregate = DummyAggregate.CreateNew(A.RandomGuid());
            dummyAggregate.ChangeAge(A.RandomNumber());
            dummyAggregate.ChangeName(A.RandomShortString());

            var act = fixture.AttachAggregate(dummyAggregate);
            act();

            var aggregateEvents = ((IAggregate)dummyAggregate).GetUncommittedEvents();
            fixture.Stream.UncommittedEvents.Where(x => x.AggregateId == dummyAggregate.Id)
                .Should()
                .HaveCount(aggregateEvents.Count);
            foreach (var aggregateEvent in aggregateEvents)
            {
                fixture.Stream.UncommittedEvents.Any(x => x.Body == aggregateEvent).Should().BeTrue();
            }
        }

        [Fact]
        public void adding_events_to_attached_aggregate_should_add_events_to_his_stream()
        {
            var fixture = new AttachAggregateFixture();

            var dummyAggregate = DummyAggregate.CreateNew(A.RandomGuid());

            var act = fixture.AttachAggregate(dummyAggregate);
            act();

            dummyAggregate.ChangeAge(A.RandomNumber());
            dummyAggregate.ChangeName(A.RandomShortString());

            var aggregateEvents = ((IAggregate)dummyAggregate).GetUncommittedEvents();
            fixture.Stream.UncommittedEvents.Where(x => x.AggregateId == dummyAggregate.Id)
                .Should()
                .HaveCount(aggregateEvents.Count);
            foreach (var aggregateEvent in aggregateEvents)
            {
                fixture.Stream.UncommittedEvents.Any(x => x.Body == aggregateEvent).Should().BeTrue();
            }
        }

        [Fact]
        public void
            aggregate_with_some_events_attached_to_stream_ans_then_added_some_events_should_have_all_these_events_in_stream
            ()
        {
            var fixture = new AttachAggregateFixture();
           
            var dummyAggregate = DummyAggregate.CreateNew(A.RandomGuid());
            dummyAggregate.ChangeName(A.RandomShortString());

            var act = fixture.AttachAggregate(dummyAggregate);
            act();

            dummyAggregate.ChangeAge(A.RandomNumber());

            var aggregateEvents = ((IAggregate)dummyAggregate).GetUncommittedEvents();
            fixture.Stream.UncommittedEvents.Where(x => x.AggregateId == dummyAggregate.Id)
                .Should()
                .HaveCount(2);

            foreach (var aggregateEvent in aggregateEvents)
            {
                fixture.Stream.UncommittedEvents.Any(x => x.Body == aggregateEvent).Should().BeTrue();
            }
        }
    }
}
