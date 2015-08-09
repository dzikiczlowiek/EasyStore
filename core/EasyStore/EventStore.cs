namespace EasyStore
{
    using System;

    using EasyStore.CommonDomain;
    using EasyStore.Infrastructure;
    using EasyStore.Persistence;

    public class EventStore : IStoreEvents
    {
        private readonly IPersistStreams _persistence;

        public EventStore(IPersistStreams persistence)
        {
            Guard.NotNull(() => persistence);
            this._persistence = persistence;
        }

        public IEventStream OpenStream(string streamId)
        {
            return new EventStream(streamId, this._persistence);
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
