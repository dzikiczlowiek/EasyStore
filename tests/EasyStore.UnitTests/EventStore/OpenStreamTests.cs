namespace EasyStore.UnitTests.EventStore
{
    using EasyStore.Tests.Common;
    using EasyStore.UnitTests.EventStore.Arrangement;

    using FluentAssertions;

    using Xunit;

    public class OpenStreamTests : TestBase
    {
        [Fact(DisplayName = "Open stream with new StreamId should create new EventStream")]
        public void open_stream_with_new_streamId_should_create_new_event_stream()
        {
            var streamId = A.RandomStreamId();
            var fixture = OpenStreamFixture.WithStreamId(streamId);
            fixture.ThereIsNoCommitsForGivenStreamId();

            var act = fixture.OpenStream();
            act();

            fixture.ReturnedEventStream.StreamId.Should().Be(streamId);
            fixture.ReturnedEventStream.UncommittedEvents.Should().BeEmpty();
            fixture.ReturnedEventStream.CommittedEvents.Should().BeEmpty();
            fixture.ReturnedEventStream.CommitSequence.Should().Be(0);
        }

        [Fact]
        public void open_stream_without_providing_revision_range_should_return_event_stream_with_all_commits_in_stream()
        {
            var streamId = A.RandomStreamId();
            var fixture = OpenStreamFixture.WithStreamId(streamId);
            fixture.ThereIsNoCommitsForGivenStreamId();

            var act = fixture.OpenStream();
            act();

            fixture.ReturnedEventStream.StreamId.Should().Be(streamId);
            fixture.ReturnedEventStream.UncommittedEvents.Should().BeEmpty();
            fixture.ReturnedEventStream.CommittedEvents.Should().BeEmpty();
            fixture.ReturnedEventStream.CommitSequence.Should().Be(0);
        }
    }
}
