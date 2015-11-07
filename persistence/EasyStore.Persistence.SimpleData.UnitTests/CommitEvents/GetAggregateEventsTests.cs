namespace EasyStore.Persistence.SimpleData.UnitTests.CommitEvents
{
    using System.Linq;

    using EasyStore.Persistence.SimpleData.UnitTests.Arrangement;
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Person;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Product;
    using EasyStore.Tests.Common.Builders;

    using FluentAssertions;

    using Xunit;

    public class GetAggregateEventsTests : TestBase
    {
        [Fact]
        public void should_return_all_desired_aggregate_events()
        {
            var fixture = new GetAggregateEventsFixture();
            var persionAggregateId = A.RandomGuid();
            var productAggregateId = A.RandomGuid();
            var personAggregate =
                Person.CreateNew(persionAggregateId).ChangeAge(A.RandomNumber()).ChangeName(A.RandomShortString());
            var productAggregate = Product.CreateNew(productAggregateId).ChangeName(A.RandomShortString());

            fixture.perists_aggregate_events(personAggregate, productAggregate);

            var act = fixture.GetAggregateEvents(persionAggregateId);
            act();

            var aggregateEvents = personAggregate.ConvertUncommitedMessagesToEventMessages();
            foreach (var @event in fixture.Events)
            {
                aggregateEvents.Should()
                    .Contain(
                        x =>
                            x.BodyType == @event.BodyType
                            && ReflectionHelper.PublicInstancePropertiesEqual(
                                x.BodyType,
                                x.Body,
                                @event.Body));
            }
        }

        [Fact]
        public void should_keep_event_order_apply()
        {
            var fixture = new GetAggregateEventsFixture();
            var productAggregateId = A.RandomGuid();
            var productAggregate = Product.CreateNew(productAggregateId).ChangeName(A.RandomShortString());

            var persionAggregateId = A.RandomGuid();
            var personAggregate =
                Person.CreateNew(persionAggregateId)
                    .ChangeAge(101)
                    .ChangeName("Name1")
                    .ChangeName("Name2")
                    .ChangeAge(102)
                    .ChangeName("Name3");

            fixture.perists_aggregate_events(personAggregate, productAggregate);

            var act = fixture.GetAggregateEvents(persionAggregateId);
            act();

            var aggregateEvents = personAggregate.ConvertUncommitedMessagesToEventMessages().ToList();
            var givenEvents = fixture.Events.ToList();
            for (int i = 0; i < aggregateEvents.Count; i++)
            {
                var givenEvent = givenEvents[i];
                var expectedEvent = aggregateEvents[i];
                ReflectionHelper.PublicInstancePropertiesEqual(
                    expectedEvent.BodyType,
                    expectedEvent.Body,
                    givenEvent.Body).Should().BeTrue();
            }
        }
    }
}
