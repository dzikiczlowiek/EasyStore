namespace EasyStore.DI
{
    using System;
    using System.Collections.Generic;

    using EasyStore;

    public class Container : IContainer
    {
        private readonly IDictionary<Type, IContainerRegistration> _registrations =
            new Dictionary<Type, IContainerRegistration>();

        public virtual IContainerRegistration Register<TService>()
        {
            if (!typeof(TService).IsValueType && !typeof(TService).IsInterface)
            {
                throw new ArgumentException(ExceptionMessages.TypeMustBeInterface, "instance");
            }

            var registration = new ContainerRegistration();
            this._registrations[typeof(TService)] = registration;
            return registration;
        }

        public virtual TService Resolve<TService>()
        {
            IContainerRegistration registration;
            if (this._registrations.TryGetValue(typeof(TService), out registration))
            {
                return (TService)registration.Resolve(this);
            }

            return default(TService);
        }

        public virtual object Resolve(Type type)
        {
            IContainerRegistration registration;
            if (this._registrations.TryGetValue(type, out registration))
            {
                return registration.Resolve(this);
            }

            return null;
        }

        public IEnumerable<Type> RegisteredTypes()
        {
            return this._registrations.Keys;
        }
    }
}
