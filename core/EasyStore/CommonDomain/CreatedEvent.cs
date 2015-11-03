namespace EasyStore.CommonDomain
{
    using System;

    public class CreatedEvent : IDomainEvent
    {
        public CreatedEvent(Guid aggregateId)
        {
            this.AggregateId = aggregateId;
        }

        public Guid AggregateId { get; private set; }
    }
}
