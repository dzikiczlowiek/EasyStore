namespace EasyStore.Persistence
{
    using System;

    using EasyStore.CommonDomain;

    public interface IConstructAggregates
    {
        AggregateRoot Build(Type type, Guid aggregateId);

        TAggregate Build<TAggregate>(Guid aggregateId) where TAggregate : AggregateRoot;
    }
}
