namespace EasyStore.Persistence
{
    using System;
    using System.Collections.Generic;

    public class Commit : ICommit
    {
        public Commit(Guid commitId, int commitSequence, IEnumerable<EventMessage> events)
        {
            this.Events = events;
            this.CommitSequence = commitSequence;
            this.CommitId = commitId;
        }

        public Guid CommitId { get; private set; }

        public virtual IEnumerable<EventMessage> Events { get; private set; }

        public virtual int CommitSequence { get; private set; }
    }
}
