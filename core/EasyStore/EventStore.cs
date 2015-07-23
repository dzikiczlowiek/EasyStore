namespace EasyStore
{
    using System;

    using EasyStore.Persistence;

    public class EventStore : IStoreEvents
    {
        private readonly IPersistStreams _persistence;

        public IEventStream OpenStream(string streamId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
