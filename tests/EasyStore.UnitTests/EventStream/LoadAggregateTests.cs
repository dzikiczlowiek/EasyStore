namespace EasyStore.UnitTests.EventStream
{
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Person;
    using EasyStore.UnitTests.EventStream.Arrangement;

    using Xunit;

    public class LoadAggregateTests : TestBase
    {
        [Fact]
        public void load_aggregate_from_persistence()
        {
            var fixture = LoadAggregateFixture.Create();
            
            var aggregateId = A.RandomGuid();
            var personAggregate =
                Person.CreateNew(aggregateId).ChangeAge(A.RandomNumber()).ChangeName("Jon Snow");

            fixture.persist_aggregate_events(personAggregate);

            var act = fixture.LoadAggregate<Person>(aggregateId);
            act();
        }
    }
}
