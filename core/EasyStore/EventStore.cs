namespace EasyStore
{
    using System;

    using EasyStore.CommonDomain;
    using EasyStore.Persistence;

    public class EventStore : IStoreEvents, ILoadAggregates
    {
        private readonly IPersistStreams _persistence;

        private readonly ILoadAggregates aggreagatesLoader;

        public IEventStream OpenStream(string streamId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TAggregate LoadAggregate<TAggregate>(Guid aggregateId) where TAggregate : IAggregate
        {
            throw new NotImplementedException();
        }
    }
}
