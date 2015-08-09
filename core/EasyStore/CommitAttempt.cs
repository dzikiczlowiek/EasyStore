namespace EasyStore
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using EasyStore.Infrastructure;

    public class CommitAttempt
    {
        public CommitAttempt(string streamId, Guid commitId, IEnumerable<EventMessage> events)
        {
            Guard.NotNull(() => streamId);
            
            this.StreamId = streamId;
            this.CommitId = commitId;
            this.Events = events == null
                              ? new ReadOnlyCollection<EventMessage>(new List<EventMessage>())
                              : new ReadOnlyCollection<EventMessage>(events.ToList());
        }

        public Guid CommitId { get; private set; }

        public string StreamId { get; private set; }

        public ICollection<EventMessage> Events { get; private set; }
    }
}