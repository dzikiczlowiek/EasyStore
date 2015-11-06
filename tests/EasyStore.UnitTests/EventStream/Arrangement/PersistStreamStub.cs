namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EasyStore.CommonDomain;
    using EasyStore.Tests.Common;

    using Moq;

    public class PersistStreamStub
    {
        private readonly Mock<ICommitEvents> _mock = new Mock<ICommitEvents>();

        private readonly ICollection<EventMessage> _eventMessages = new List<EventMessage>();

        public PersistStreamStub()
        {
            this.CommitAttempts = new List<CommitAttempt>();
        }

        public ICollection<CommitAttempt> CommitAttempts { get; private set; }

        public ICommitEvents Create()
        {
            this.MockGetAggregateEvents();
            this.MonitorCommits();
            return this._mock.Object;
        }

        public void AddAggregate(AggregateRoot aggregate)
        {
            foreach (var domainEvent in aggregate.GetUncommittedEvents())
            {
                var @event = new EventMessage(aggregate.Id, aggregate.Version, domainEvent);
                this._eventMessages.Add(@event);
            }
        }

        private void MockGetAggregateEvents()
        {
            this._mock.Setup(x => x.GetAggregateEvents(It.IsAny<Guid>()))
                .Returns<Guid>(aggregateId => this._eventMessages.Where(x => x.AggregateId == aggregateId));
        }

        private void MonitorCommits()
        {
            this._mock.Setup(x => x.Commit(It.IsAny<CommitAttempt>()))
                .Callback<CommitAttempt>(attempt => this.CommitAttempts.Add(attempt));
        }
    }
}
