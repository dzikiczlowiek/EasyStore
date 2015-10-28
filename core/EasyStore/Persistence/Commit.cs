namespace EasyStore.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Commit : ICommit
    {
        public Commit(
            Guid commitId, 
            int commitSequence, 
            int streamRevision,
            IEnumerable<EventMessage> events)
        {
            this.Events = events.ToList();
            this.CommitSequence = commitSequence;
            this.StreamRevision = streamRevision;
            this.CommitId = commitId;
        }

        public Guid CommitId { get; private set; }

        public virtual ICollection<EventMessage> Events { get; private set; }

        public virtual int CommitSequence { get; private set; }

        public int StreamRevision { get; private set; }
    }
}
