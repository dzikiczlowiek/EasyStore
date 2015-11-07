namespace EasyStore.Persistence.SimpleData.UnitTests.Arrangement
{
    using System;

    public class AggregateRecord
    {
        public Guid AggregateId { get; set; }

        public int Version { get; set; }

        public string Type { get; set; }

        public bool Archived { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
