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

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEventStream CreateStream(string streamId)
        {
            return new EventStream(streamId, this._persistence);
        }

        public IEventStream OpenStream(string streamId, int minRevision, int maxRevision)
        {
            return new EventStream(streamId, minRevision, maxRevision, this._persistence);
        }
    }
}
