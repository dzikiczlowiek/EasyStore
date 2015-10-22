namespace EasyStore
{
    using System;

    public interface IStoreEvents : IDisposable
    {
        IEventStream CreateStream(string streamId);

        IEventStream OpenStream(string streamId, int minRevision, int maxRevision);
    }
}
