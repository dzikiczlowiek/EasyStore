namespace EasyStore
{
    using System;

    public interface IStoreEvents : IDisposable
    {
        IEventStream OpenStream(string streamId);
    }
}
