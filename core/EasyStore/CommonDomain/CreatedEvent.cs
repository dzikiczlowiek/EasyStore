namespace EasyStore.CommonDomain
{
    using System;

    [Serializable]
    public class CreatedEvent : IDomainEvent
    {
        public CreatedEvent(Guid aggregateId, string aggregateType)
        {
            this.AggregateId = aggregateId;
            this.AggregateType = aggregateType;
        }

        public Guid AggregateId { get; private set; }

        public string AggregateType { get; private set; }
    }
}
