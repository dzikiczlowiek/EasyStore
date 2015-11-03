namespace EasyStore.UnitTests.EventStream.Arrangement
{
    using System;
    using System.Linq;

    using EasyStore.CommonDomain;
    using EasyStore.Tests.Common;

    using FluentAssertions;

    public class AttachAggregateFixture : EventStreamFixtureBase
    {
        public static AttachAggregateFixture Create()
        {
            var fixture = new AttachAggregateFixture();
            return fixture;
        }

        public void stream_should_have_all_events_for_aggregate(AggregateRoot aggregate)
        {
            var aggregateEvents = aggregate.GetUncommittedEvents();
            this.Stream.UncommittedEvents.Where(x => x.AggregateId == aggregate.Id)
                .Should()
                .HaveCount(aggregateEvents.Count);

            foreach (var aggregateEvent in aggregateEvents)
            {
                this.Stream.UncommittedEvents.Any(x => x.Body == aggregateEvent).Should().BeTrue();
            }
        }

        public Action AttachAggregatesToStream(params AggregateRoot[] aggregates)
        {
            var sut = this.CreateSut();
            return () =>
            {
                foreach (var aggregate in aggregates)
                {
                    sut.AttachAggregate(aggregate); 
                }
            };
        }
    }
}
