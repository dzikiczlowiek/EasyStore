namespace EasyStore.UnitTests.EventStore
{
    using EasyStore.Tests.Common;
    using EasyStore.UnitTests.EventStore.Arrangement;

    using FluentAssertions;

    using Xunit;

    public class CreateStreamTests : TestBase
    {
        [Fact]
        public void create_stream_should_return_new_event_stream()
        {
            var streamId = A.RandomStreamId();
            var fixture = CreateStreamFixture.WithStreamId(streamId);

            var act = fixture.CreateStream();
            act();

            fixture.CreatedEventStream.StreamId.Should().Be(streamId);
            fixture.CreatedEventStream.UncommittedEvents.Should().BeEmpty();
            fixture.CreatedEventStream.CommittedEvents.Should().BeEmpty();
            fixture.CreatedEventStream.CommitSequence.Should().Be(0);
        }
    }
}
