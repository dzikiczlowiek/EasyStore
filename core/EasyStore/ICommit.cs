namespace EasyStore
{
    using System.Collections.Generic;

    public interface ICommit
    {
        IEnumerable<EventMessage> Events { get; }

        int CommitSequence { get; }
    }
}
