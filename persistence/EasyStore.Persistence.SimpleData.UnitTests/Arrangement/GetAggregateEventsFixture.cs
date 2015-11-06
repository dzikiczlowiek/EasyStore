namespace EasyStore.Persistence.SimpleData.UnitTests.Arrangement
{
    using System;

    public class GetAggregateEventsFixture : PersistStreamsFixtureBase
    {
        public Action GetAggregateEvents(Guid aggregateId)
        {
            var sut = this.CreateSut();
            return () => sut.GetAggregateEvents(aggregateId);
        }
    }
}
