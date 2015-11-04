namespace EasyStore.Persistence
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.Serialization;

    using EasyStore.CommonDomain;

    public class DefaultAggregatorConstructor : IConstructAggregates
    {
        public AggregateRoot Build(Type type)
        {
            // TODO: TOO SLOOOOOOOOW AND BAD!!!!!!!!!
            return FormatterServices.GetUninitializedObject(type) as AggregateRoot;
        }


        public TAggregate Build<TAggregate>()
            where TAggregate : AggregateRoot
        {
            return (TAggregate)this.Build(typeof(TAggregate));
        }
    }
}
