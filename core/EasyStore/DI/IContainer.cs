namespace EasyStore.DI
{
    using System;
    using System.Collections.Generic;

    public interface IContainer
    {
        IContainerRegistration Register<TService>();

        TService Resolve<TService>();

        object Resolve(Type type);

        IEnumerable<Type> RegisteredTypes();
    }
}