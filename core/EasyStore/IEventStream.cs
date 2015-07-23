namespace EasyStore
{
    using System;

    public interface IEventStream : IDisposable
    {
        string StreamId { get; }

        void CommitChanges(Guid commitId);
    }
}
