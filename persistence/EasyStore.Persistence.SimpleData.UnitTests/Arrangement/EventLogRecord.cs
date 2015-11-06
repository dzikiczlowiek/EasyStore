namespace EasyStore.Persistence.SimpleData.UnitTests.Arrangement
{
    using System;

    using EasyStore.CommonDomain;

    public class EventLogRecord
    {
        public Guid AggregateId { get; set; }

        public IDomainEvent DomainEvent { get; set; }

        public int Version { get; set; }

        public string EventType { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CommitId { get; set; }

        public string StreamId { get; set; }
    }
}
