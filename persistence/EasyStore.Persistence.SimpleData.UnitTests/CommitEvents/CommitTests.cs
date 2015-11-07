namespace EasyStore.Persistence.SimpleData.UnitTests.CommitEvents
{
    using System.Linq;

    using EasyStore.Persistence.SimpleData.UnitTests.Arrangement;
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Person;
    using EasyStore.Tests.Common.Builders;

    using FluentAssertions;

    using Xunit;

    public class CommitTests : TestBase
    {
        [Fact]
        public void should_persist_all_aggregate_events()
        {
            var commitId = A.RandomGuid();
            var streamId = A.RandomStreamId();
            var personAggregate =
                Person.CreateNew(A.RandomGuid()).ChangeAge(A.RandomNumber()).ChangeName(A.RandomShortString());

            var events = personAggregate.ConvertUncommitedMessagesToEventMessages();
            // commit sequence?? 
            var commitAttempt = new CommitAttempt(streamId, commitId, events);

            var fixture = new CommitFixture();

            var act = fixture.Commit(commitAttempt);
            act();

            var eventsFromDb = fixture.GetEvents();
            eventsFromDb.Should().HaveCount(events.Count());
           
            foreach (var eventLogRecord in eventsFromDb)
            {
                events.Should()
                    .Contain(
                        x =>
                            x.BodyType == eventLogRecord.EventType
                            && ReflectionHelper.PublicInstancePropertiesEqual(
                                x.BodyType,
                                x.Body,
                                eventLogRecord.DomainEvent));
            }
        }

        [Fact]
        public void if_aggregate_dont_exist_should_create_aggregate_record()
        {
            var commitId = A.RandomGuid();
            var streamId = A.RandomStreamId();
            var aggregateId = A.RandomGuid();
            var personAggregate =
                Person.CreateNew(aggregateId).ChangeAge(A.RandomNumber()).ChangeName(A.RandomShortString());

            var events = personAggregate.ConvertUncommitedMessagesToEventMessages();
        
            var commitAttempt = new CommitAttempt(streamId, commitId, events);

            var fixture = new CommitFixture();

            var act = fixture.Commit(commitAttempt);
            act();

            var aggregateFromDb = fixture.GetAggregateRecord(aggregateId);
            aggregateFromDb.Should().NotBeNull();
        }

        [Fact]
        public void aggregates_should_have_updated_version()
        {
            var commitId = A.RandomGuid();
            var streamId = A.RandomStreamId();
            var aggregateId = A.RandomGuid();
            var personAggregate =
                Person.CreateNew(aggregateId).ChangeAge(A.RandomNumber()).ChangeName(A.RandomShortString());

            var events = personAggregate.ConvertUncommitedMessagesToEventMessages();

            var commitAttempt = new CommitAttempt(streamId, commitId, events);

            var fixture = new CommitFixture();

            var act = fixture.Commit(commitAttempt);
            act();

            var aggregateFromDb = fixture.GetAggregateRecord(aggregateId);
            aggregateFromDb.Version.Should().Be(personAggregate.Version);
        }
    }
}
