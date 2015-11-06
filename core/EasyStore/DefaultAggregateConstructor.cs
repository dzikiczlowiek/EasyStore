namespace EasyStore
{
    using System;
    using System.Reflection;

    using EasyStore.CommonDomain;

    public class DefaultAggregateConstructor : IConstructAggregates
    {
        public AggregateRoot Build(Type type)
        {
            var constructor = type.GetConstructor(
               BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);

            return constructor.Invoke(null) as AggregateRoot;
        }

        public TAggregate Build<TAggregate>()
            where TAggregate : AggregateRoot
        {
            return (TAggregate)this.Build(typeof(TAggregate));
        }
    }
}
