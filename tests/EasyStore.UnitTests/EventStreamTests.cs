namespace EasyStore.UnitTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using FluentAssertions;

    using NSubstitute;

    using Xunit;

    public class EventStreamTests : TestBase
    {
        public void adding_event_message_should_be_stored_in_uncommitted_events()
        {
            var streamId = A.RandomShortString();
            var eventStream = CreateStream(streamId);

            var message = new EventMessage
            {
                Body = new { }
            };

            eventStream.Add(message);

            eventStream.UncommittedEvents.Count.Should().Be(1);
        }

        [Fact]
        public void adding_null_event_message_should_throw_ArgumentNullException_exception()
        {
            var streamId = A.RandomShortString();
            var eventStream = CreateStream(streamId);

            Assert.Throws<ArgumentNullException>(() => eventStream.Add(null));
        }

        [Fact]
        public void adding_event_message_with_empty_Body_should_throw_ArgumentNullException_exception()
        {
            var streamId = A.RandomShortString();
            var eventStream = CreateStream(streamId);

            var message = new EventMessage();

            Assert.Throws<ArgumentNullException>(() => eventStream.Add(message));
        }

        [Fact]
        public void clearing_changes_should_clear_all_uncommited_events()
        {
            var streamId = A.RandomShortString();
            var eventStream = CreateStream(streamId);

            eventStream.Add(new EventMessage() { Body = new { } });
            eventStream.Add(new EventMessage() { Body = new { } });

            eventStream.ClearChanges();

            eventStream.UncommittedEvents.Should().BeEmpty();
        }

        private EventStream CreateStream(string streamId)
        {
            commitEvents = Substitute.For<ICommitEvents>();
            return new EventStream(streamId, commitEvents);
        }

        private ICommitEvents commitEvents;
    }
}
