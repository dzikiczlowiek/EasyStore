namespace EasyStore.DI
{
    using System;

    public interface IContainerRegistration
    {
        IContainerRegistration InstancePerCall();

        object Resolve(EasyContainer container);

        IContainerRegistration Use<TConcrete>();

        IContainerRegistration Use(object instance);

        IContainerRegistration Use(Func<EasyContainer, object> resolve);
    }
}
