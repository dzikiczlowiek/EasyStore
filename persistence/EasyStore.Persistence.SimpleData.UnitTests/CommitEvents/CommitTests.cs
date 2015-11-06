namespace EasyStore.Persistence.SimpleData.UnitTests.CommitEvents
{
    using System.Collections.Generic;

    using EasyStore.Persistence.SimpleData.UnitTests.Arrangement;
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Product;
    using EasyStore.Tests.Common.Builders;

    using FluentAssertions;

    using Xunit;

    public class CommitTests : TestBase
    {
        [Fact]
        public void should_work()
        {
            IEnumerable<EventMessage> events = new List<EventMessage>()
                                               {
                                                   new EventMessage(
                                                       A.RandomGuid(),
                                                       A.RandomNumber(),
                                                       new ChangedNameEvent("AA"))
                                               };

            var commitId = A.RandomGuid();
            var streamId = A.RandomStreamId();
            var commitAttempt = new CommitAttempt(streamId, commitId, events);

            var fixture = new CommitFixture();

            var act = fixture.Commit(commitAttempt);
            act();

            var eventsFromDb = fixture.GetEvents();
            eventsFromDb.Should().HaveCount(1);
        }
    }
}
