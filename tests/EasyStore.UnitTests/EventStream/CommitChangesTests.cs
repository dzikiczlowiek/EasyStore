namespace EasyStore.UnitTests.EventStream
{
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Person;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Product;
    using EasyStore.Tests.Common.Builders;
    using EasyStore.UnitTests.EventStream.Arrangement;

    using Xunit;

    public class CommitChangesTests : TestBase
    {
        [Fact]
        public void commit_should_send_all_uncommited_events_from_stream_to_peristence()
        {
            var streamId = A.RandomStreamId();
            
            var fixture = CommitChangesFixture.Create().WithStreamId(streamId);
            var personAggreagte = PersonAggregate.CreateNew(A.RandomGuid());
            var productAggregate = ProductAggregate.CreateNew(A.RandomGuid());

            var commitId = A.RandomGuid();

            fixture.attach_aggregates_to_stream(personAggreagte, productAggregate);

            var act = fixture.CommitChangesWithCommitId(commitId);
            act();

            fixture.there_should_be_one_commit_with_id(commitId);
        }

        [Fact]
        public void commit_should_clear_all_uncommited_events()
        {
            var streamId = A.RandomStreamId();

            var fixture = CommitChangesFixture.Create().WithStreamId(streamId);
            var personAggreagte = PersonAggregate.CreateNew(A.RandomGuid());
            var productAggregate = ProductAggregate.CreateNew(A.RandomGuid());

            var commitId = A.RandomGuid();

            fixture.attach_aggregates_to_stream(personAggreagte, productAggregate);

            var act = fixture.CommitChangesWithCommitId(commitId);
            act();

            fixture.there_is_no_uncommited_events_in_stream();
        }

        [Fact]
        public void commit_should_have_stream_id_same_as_one_assigned_to_stream()
        {
            var streamId = A.RandomStreamId();

            var fixture = CommitChangesFixture.Create().WithStreamId(streamId);
            var personAggreagte = PersonAggregate.CreateNew(A.RandomGuid());
            var productAggregate = ProductAggregate.CreateNew(A.RandomGuid());

            var commitId = A.RandomGuid();

            fixture.attach_aggregates_to_stream(personAggreagte, productAggregate);

            var act = fixture.CommitChangesWithCommitId(commitId);
            act();

            fixture.all_commit_should_have_stream_id_same_as_one_assigned_to_stream(streamId);
        }

        [Fact]
        public void commit_should_have_all_uncommited_events_from_stream()
        {
            var streamId = A.RandomStreamId();

            var fixture = CommitChangesFixture.Create().WithStreamId(streamId);
            var personAggreagte = PersonAggregate.CreateNew(A.RandomGuid());
            var productAggregate = ProductAggregate.CreateNew(A.RandomGuid());

            var commitId = A.RandomGuid();

            fixture.attach_aggregates_to_stream(personAggreagte, productAggregate);

            var act = fixture.CommitChangesWithCommitId(commitId);
            act();

            var events = Collections.Join(
                personAggreagte.GetUncommittedEvents(),
                productAggregate.GetUncommittedEvents());
            fixture.given_commit_should_have_all_aggregates_uncommited_events_from_stream(commitId, events);
        }
    }
}
