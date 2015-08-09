namespace EasyStore.Persistence
{
    using System;
    using System.Collections.Generic;

    public interface IPersistStreams : IDisposable, ICommitEvents, IAccessSnapshots
    {
        void Initialize();
    }
}
