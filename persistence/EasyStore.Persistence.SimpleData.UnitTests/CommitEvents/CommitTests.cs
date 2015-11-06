namespace EasyStore.Persistence.SimpleData.UnitTests.CommitEvents
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyStore.Persistence.SimpleData.UnitTests.Arrangement;
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Person;
    using EasyStore.Tests.Common.Builders;

    using FluentAssertions;

    using Xunit;

    using ChangedNameEvent = EasyStore.Tests.Common.Arrangement.DummyDomain.Product.ChangedNameEvent;

    public class CommitTests : TestBase
    {
        [Fact]
        public void should_return_add_all_aggregate_events()
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
        }

        [Fact]
        public void if_aggregate_dont_exist_should_create_aggregate_record()
        {

        }
    }
}
