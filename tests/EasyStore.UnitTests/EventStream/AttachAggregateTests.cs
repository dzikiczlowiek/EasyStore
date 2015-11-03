namespace EasyStore.UnitTests.EventStream
{
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Person;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Product;
    using EasyStore.UnitTests.EventStream.Arrangement;

    using Xunit;

    public class AttachAggregateTests : TestBase
    {
        [Fact]
        public void attached_aggregate_with_already_two_events_should_add_those_events_to_stream_events()
        {
            var fixture = AttachAggregateFixture.Create();

            var personAggregate = PersonAggregate.CreateNew(A.RandomGuid());
            personAggregate.ChangeAge(A.RandomNumber());
            personAggregate.ChangeName(A.RandomShortString());

            var act = fixture.AttachAggregatesToStream(personAggregate);
            act();

            fixture.stream_should_have_all_events_for_aggregate(personAggregate);
        }

        [Fact]
        public void adding_events_to_attached_aggregate_should_add_events_to_his_stream()
        {
            var fixture = AttachAggregateFixture.Create();

            var personAggregate = PersonAggregate.CreateNew(A.RandomGuid());

            var act = fixture.AttachAggregatesToStream(personAggregate);
            act();

            personAggregate.ChangeAge(A.RandomNumber());
            personAggregate.ChangeName(A.RandomShortString());

            fixture.stream_should_have_all_events_for_aggregate(personAggregate);
        }

        [Fact]
        public void
            aggregate_with_some_events_attached_to_stream_and_then_added_some_events_should_have_all_these_events_in_stream
            ()
        {
            var fixture = AttachAggregateFixture.Create();

            var personAggregate = PersonAggregate.CreateNew(A.RandomGuid());
            personAggregate.ChangeName(A.RandomShortString());

            var act = fixture.AttachAggregatesToStream(personAggregate);
            act();
            personAggregate.ChangeAge(A.RandomNumber());
           
            fixture.stream_should_have_all_events_for_aggregate(personAggregate);
        }

        [Fact]
        public void attaching_to_stream_few_aggregates_with_events_should_add_those_events_to_stream()
        {
            var fixture = AttachAggregateFixture.Create();

            var personAggregate = PersonAggregate.CreateNew(A.RandomGuid());
            personAggregate.ChangeName(A.RandomShortString());
            
            var productAggregate = ProductAggregate.CreateNew(A.RandomGuid());
            productAggregate.ChangePrice(A.RandomNumber());
            productAggregate.ChangeName(A.RandomShortString());

            var act = fixture.AttachAggregatesToStream(personAggregate, productAggregate);
            act();

            fixture.stream_should_have_all_events_for_aggregate(personAggregate);
            fixture.stream_should_have_all_events_for_aggregate(productAggregate);
        }

        [Fact]
        public void stream_should_track_events_of_multiple_attached_aggregates()
        {
            var fixture = AttachAggregateFixture.Create();

            var personAggregate = PersonAggregate.CreateNew(A.RandomGuid());
            personAggregate.ChangeName(A.RandomShortString());

            var productAggregate = ProductAggregate.CreateNew(A.RandomGuid());
            productAggregate.ChangePrice(A.RandomNumber());
            productAggregate.ChangeName(A.RandomShortString());

            var act = fixture.AttachAggregatesToStream(personAggregate, productAggregate);
            act();
            personAggregate.ChangeAge(A.RandomNumber());
            productAggregate.ChangeCategory(A.RandomShortString());

            fixture.stream_should_have_all_events_for_aggregate(personAggregate);
            fixture.stream_should_have_all_events_for_aggregate(productAggregate);
        }
    }
}
