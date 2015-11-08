namespace EasyStore.DI
{
    using System;

    public interface IContainerRegistration
    {
        IContainerRegistration InstancePerCall();

        object Resolve(Container container);

        IContainerRegistration Use<TConcrete>();

        IContainerRegistration Use(object instance);

        IContainerRegistration Use(Func<Container, object> resolve);
    }
}
