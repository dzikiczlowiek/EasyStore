namespace EasyStore.CommonDomain
{
    using System;

    public interface IRouteEvents
    {
        void Register<T>(Action<T> handler);

        void Register(AggregateRoot aggregate);

        void Dispatch(object eventMessage);
    }
}
