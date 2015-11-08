namespace EasyStore
{
    using System;

    using EasyStore.DI;
    using EasyStore.Persistence;

    public class Wireup
    {
        private readonly IContainer _container;

        private readonly Wireup _inner;

        protected Wireup(IContainer container)
        {
            this._container = container;
        }

        protected Wireup(Wireup inner)
        {
            this._inner = inner;
        }

        protected IContainer Container
        {
            get { return this._container ?? this._inner.Container; }
        }

        public static Wireup Init()
        {
            var container = new EasyContainer();
            container.Register<IStoreEvents>().Use(BuildEventStore);

            return new Wireup(container);
        }

        public virtual Wireup With<T>(T instance)
            where T : class
        {
            this.Container.Register<T>().Use(instance);
            return this;
        }

        public virtual Wireup With<T>(Func<IContainer, T> resolver)
            where T : class
        {
            this.Container.Register<T>().Use(resolver);
            return this;
        }

        public virtual Wireup With<TInterface, TConcrete>()
            where TConcrete : class
        {
            this.Container.Register<TInterface>().Use<TConcrete>();
            return this;
        }

        public virtual IStoreEvents Build()
        {
            if (this._inner != null)
            {
                return this._inner.Build();
            }

            return this.Container.Resolve<IStoreEvents>();
        }

        private static IStoreEvents BuildEventStore(IContainer context)
        {
            return new EventStore(context.Resolve<IPersistStreams>());
        }
    }
}
