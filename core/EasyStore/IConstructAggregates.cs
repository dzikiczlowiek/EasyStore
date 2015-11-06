namespace EasyStore
{
    using System;

    using EasyStore.CommonDomain;

    public interface IConstructAggregates
    {
        AggregateRoot Build(Type type);

        TAggregate Build<TAggregate>() where TAggregate : AggregateRoot;
    }
}
