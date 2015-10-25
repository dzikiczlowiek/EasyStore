namespace EasyStore
{
    using System;
    using System.Collections.Generic;

    public interface ICommit
    {
        Guid CommitId { get; }

        IEnumerable<EventMessage> Events { get; }

        int CommitSequence { get; }
    }
}
