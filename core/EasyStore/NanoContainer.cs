namespace EasyStore
{
    using System;
    using System.Collections.Generic;

    public class NanoContainer
    {
        private readonly IDictionary<Type, ContainerRegistration> _registrations =
            new Dictionary<Type, ContainerRegistration>();

        public virtual ContainerRegistration Register<TService>(Func<NanoContainer, TService> resolve)
            where TService : class
        {
            var registration = new ContainerRegistration(c => (object)resolve(c));
            this._registrations[typeof(TService)] = registration;
            return registration;
        }

        public virtual ContainerRegistration Register<TService>(TService instance)
        {
            if (Equals(instance, null))
            {
                throw new ArgumentNullException("instance", ExceptionMessages.InstanceCannotBeNull);
            }

            if (!typeof(TService).IsValueType && !typeof(TService).IsInterface)
            {
                throw new ArgumentException(ExceptionMessages.TypeMustBeInterface, "instance");
            }

            var registration = new ContainerRegistration(instance);
            this._registrations[typeof(TService)] = registration;
            return registration;
        }

        public virtual TService Resolve<TService>()
        {
            ContainerRegistration registration;
            if (this._registrations.TryGetValue(typeof(TService), out registration))
            {
                return (TService)registration.Resolve(this);
            }

            return default(TService);
        }

        public class ContainerRegistration
        {
            private readonly Func<NanoContainer, object> _resolve;

            private object _instance;

            private bool _instancePerCall;

            public ContainerRegistration(Func<NanoContainer, object> resolve)
            {
                this._resolve = resolve;
            }

            public ContainerRegistration(object instance)
            {
                this._instance = instance;
            }

            public virtual ContainerRegistration InstancePerCall()
            {
                this._instancePerCall = true;
                return this;
            }

            public virtual object Resolve(NanoContainer container)
            {
                if (this._instancePerCall)
                {
                    return this._resolve(container);
                }

                if (this._instance != null)
                {
                    return this._instance;
                }

                return this._instance = this._resolve(container);
            }
        }
    }
}