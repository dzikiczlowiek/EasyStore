namespace EasyStore.Persistence
{
    using System;

    public interface IPersistStreams : IDisposable, ICommitEvents, IAccessSnapshots
    {
        void Initialize();
    }
}
