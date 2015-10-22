namespace EasyStore
{
    using System;

    using EasyStore.Infrastructure;

    public static class EventStoreExtensions
    {
        public static IEventStream CreateStream(this IStoreEvents storeEvents, Guid streamId)
        {
            Guard.NotNull(() => storeEvents);
            return storeEvents.CreateStream(streamId.ToString());
        }

        public static IEventStream OpenStream(
            this IStoreEvents storeEvents,
            string streamId,
            int minRevision = int.MinValue,
            int maxRevision = int.MaxValue)
        {
            Guard.NotNull(() => storeEvents);
            return storeEvents.OpenStream(streamId, minRevision, maxRevision);
        }

        public static IEventStream OpenStream(
            this IStoreEvents storeEvents,
            Guid streamId,
            int minRevision = int.MinValue,
            int maxRevision = int.MaxValue)
        {
            return storeEvents.OpenStream(streamId.ToString(), minRevision, maxRevision);
        }
    }
}
