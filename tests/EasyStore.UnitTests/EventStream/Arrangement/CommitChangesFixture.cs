namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EasyStore.CommonDomain;
    using EasyStore.Tests.Common;

    using FluentAssertions;

    public class CommitChangesFixture : EventStreamFixtureBase
    {
        private readonly ICollection<Action<IEventStream>> _eventStreamAssignActions = new List<Action<IEventStream>>(); 
        
        public static CommitChangesFixture Create()
        {
            var fixture = new CommitChangesFixture();
            return fixture;
        }

        public CommitChangesFixture WithStreamId(string streamId)
        {
            this.StreamId = streamId;
            return this;
        }

        public void attach_aggregates_to_stream(params AggregateRoot[] aggregates)
        {
            foreach (var aggregateRoot in aggregates)
            {
                Action<IEventStream> attach = (eventStream) => eventStream.AttachAggregate(aggregateRoot);
                this._eventStreamAssignActions.Add(attach);
            }
        }

        public Action CommitChangesWithCommitId(Guid commitId)
        {
            var sut = this.CreateSut();
            this.PlayActions(sut);
            return () => sut.CommitChanges(commitId);
        }

        public void there_should_be_one_commit_with_id(Guid commitId)
        {
            this.CommitEventsStub.CommitAttempts.Count(x => x.CommitId == commitId).Should().Be(1);
        }

        public void there_is_no_uncommited_events_in_stream()
        {
            this.Stream.UncommittedEvents.Should().BeEmpty();
        }

        public void all_commit_should_have_stream_id_same_as_one_assigned_to_stream(string streamId)
        {
            this.CommitEventsStub.CommitAttempts.All(x => x.StreamId == streamId).Should().BeTrue();
        }

        public void given_commit_should_have_all_aggregates_uncommited_events_from_stream(Guid commitId, ICollection<IDomainEvent> events)
        {
            var commit = this.CommitEventsStub.CommitAttempts.Single(x => x.CommitId == commitId);
            foreach (var aggregateEvent in events)
            {
                commit.Events.Any(x => x.Body == aggregateEvent).Should().BeTrue();
            }
        }

        private void PlayActions(IEventStream eventStream)
        {
            foreach (var eventStreamAssignAction in this._eventStreamAssignActions)
            {
                eventStreamAssignAction(eventStream);
            }
        }
    }
}