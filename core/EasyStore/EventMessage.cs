namespace EasyStore
{
    using System;
    using System.Runtime.Serialization;

    public class EventMessage
    {
        public Guid AggregateId { get; set; }

        [DataMember]
        public object Body { get; set; }

        public string BodyType { get; set; }
    }
}