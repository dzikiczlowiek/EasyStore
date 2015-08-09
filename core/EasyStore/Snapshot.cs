namespace EasyStore
{
    using System;
    using System.Runtime.Serialization;

    public class Snapshot
    {
        public Guid AggregateId { get; set; }

        public int Version { get; set; }

        [DataMember]
        public byte[] Data { get; set; }
    }
}
