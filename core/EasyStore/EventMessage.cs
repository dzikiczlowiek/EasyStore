namespace EasyStore
{
    using System;
    using System.Runtime.Serialization;

    using EasyStore.CommonDomain;

    public class EventMessage
    {
        public EventMessage(Guid aggregateId, int aggregateVersion, IDomainEvent @event)
        {
            this.AggregateId = aggregateId;
            this.AggregateVersion = aggregateVersion;
            this.Body = @event;
            this.BodyType = @event.GetType().AssemblyQualifiedName;
        }

        public int AggregateVersion { get; private set; }

        public Guid AggregateId { get; private set; }

        [DataMember]
        public object Body { get; private set; }

        public string BodyType { get; private set; }
    }
}