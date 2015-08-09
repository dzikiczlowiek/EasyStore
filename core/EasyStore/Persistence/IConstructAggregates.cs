namespace EasyStore.Persistence
{
    using System;

    using EasyStore.CommonDomain;

    public interface IConstructAggregates
    {
        IAggregate Build(Type type, Guid aggregateId);

        TAggregate Build<TAggregate>(Guid aggregateId);
    }
}
