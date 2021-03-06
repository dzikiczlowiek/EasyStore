﻿namespace EasySample.Domain.CommonEvents
{
    using System;

    using EasyStore.CommonDomain;

    [Serializable]
    public abstract class CreatedAggregateEvent : IDomainEvent
    {
        protected CreatedAggregateEvent(Guid aggregateId)
        {
            this.AggregateId = aggregateId;
        }

        public Guid AggregateId { get; private set; }
    }
}
