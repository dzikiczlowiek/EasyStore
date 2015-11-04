namespace EasyStore
{
    using System;
    using System.Runtime.Serialization;

    using EasyStore.CommonDomain;

    public class EventMessage
    {
        public EventMessage(Guid aggregateId, IDomainEvent @event)
        {
            this.AggregateId = aggregateId;
            this.Body = @event;
            this.BodyType = @event.GetType().AssemblyQualifiedName;
        }

        public Guid AggregateId { get; private set; }

        [DataMember]
        public object Body { get; private set; }

        public string BodyType { get; private set; }
    }
}