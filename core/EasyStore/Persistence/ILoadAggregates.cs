namespace EasyStore.Persistence
{
    using System;

    using EasyStore.CommonDomain;

    public interface ILoadAggregates
    {
        TAggregate LoadAggregate<TAggregate>(Guid aggregateId) where TAggregate : AggregateRoot;

        TAggregate LoadAggregate<TAggregate>(Guid aggregateId, int version) where TAggregate : AggregateRoot;
    }
}
