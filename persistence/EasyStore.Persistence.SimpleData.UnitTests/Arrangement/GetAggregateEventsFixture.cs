namespace EasyStore.Persistence.SimpleData.UnitTests.Arrangement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EasyStore.CommonDomain;
    using EasyStore.Tests.Common;

    public class GetAggregateEventsFixture : PersistStreamsFixtureBase
    {
        public CommitAttempt CommitAttempt { get; private set; }

        public IEnumerable<EventMessage> Events { get; private set; }

        public void perists_aggregate_events(params AggregateRoot[] aggregates)
        {
            var events = aggregates.Select(x => x.ConvertUncommitedMessagesToEventMessages()).SelectMany(x => x);
            this.CommitAttempt = new CommitAttempt("ST1", Guid.Empty, events);
        }

        public Action GetAggregateEvents(Guid aggregateId)
        {
            var sut = this.CreateSut();
            sut.Commit(this.CommitAttempt);
            return () => this.Events = sut.GetAggregateEvents(aggregateId);
        }

    }
}
