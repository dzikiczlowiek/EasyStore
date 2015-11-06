namespace EasyStore.UnitTests.EventStream
{
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Person;
    using EasyStore.UnitTests.EventStream.Arrangement;

    using FluentAssertions;

    using Xunit;

    public class LoadAggregateTests : TestBase
    {
        [Fact]
        public void load_aggregate_from_persistence()
        {
            var fixture = LoadAggregateFixture.Create();

            var aggregateId = A.RandomGuid();
            var persistedAggregate =
                Person.CreateNew(aggregateId).ChangeAge(A.RandomNumber()).ChangeName("Jon Snow");

            fixture.persist_aggregate_events(persistedAggregate);

            var act = fixture.LoadAggregate<Person>(aggregateId);
            act();

            fixture.Aggregate.Should().NotBeNull();
        }

        [Fact]
        public void should_be_same_as_persisted_aggregate()
        {
            var fixture = LoadAggregateFixture.Create();

            var aggregateId = A.RandomGuid();
            var persistedAggregate =
                Person.CreateNew(aggregateId).ChangeAge(A.RandomNumber()).ChangeName("Jon Snow");

            fixture.persist_aggregate_events(persistedAggregate);

            var act = fixture.LoadAggregate<Person>(aggregateId);
            act();

            var loadedAggregate = (Person)fixture.Aggregate;
            loadedAggregate.Age.Should().Be(persistedAggregate.Age);
            loadedAggregate.Name.Should().Be(persistedAggregate.Name);
        }
    }
}
