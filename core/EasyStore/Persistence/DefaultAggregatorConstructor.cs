namespace EasyStore.Persistence
{
    using System;
    using System.Reflection;

    using EasyStore.CommonDomain;

    public class DefaultAggregatorConstructor : IConstructAggregates
    {
        public AggregateRoot Build(Type type, Guid aggregateId)
        {
            var constructor = type.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(Guid) }, null);

            return constructor.Invoke(new object[] { aggregateId }) as AggregateRoot;
        }


        public TAggregate Build<TAggregate>(Guid aggregateId)
            where TAggregate : AggregateRoot
        {
            return (TAggregate)this.Build(typeof(TAggregate), aggregateId);
        }
    }
}
