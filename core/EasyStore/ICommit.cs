namespace EasyStore
{
    using System;
    using System.Collections.Generic;

    public interface ICommit
    {
        Guid CommitId { get; }

        ICollection<EventMessage> Events { get; }

        int CommitSequence { get; }

        int StreamRevision { get; }
    }
}
