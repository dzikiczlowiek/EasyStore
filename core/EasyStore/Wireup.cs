namespace EasyStore
{
    using EasyStore.Persistence;

    public class Wireup
    {
        private NanoContainer _container;

        private Wireup _inner;
      
        protected Wireup(NanoContainer container)
        {
            _container = container;
        }

        protected Wireup(Wireup inner)
        {
            _inner = inner;
        }

        protected NanoContainer Container
        {
            get { return _container ?? _inner.Container; }
        }

        public static Wireup Init()
        {
            var container = new NanoContainer();
            container.Register(BuildEventStore);

            return new Wireup(container);
        }

        public virtual Wireup With<T>(T instance) where T : class
        {
            Container.Register(instance);
            return this;
        }

        public virtual IStoreEvents Build()
        {
            if (_inner != null)
            {
                return _inner.Build();
            }

            return Container.Resolve<IStoreEvents>();
        }

        private static IStoreEvents BuildEventStore(NanoContainer context)
        {
            return new EventStore(context.Resolve<IPersistStreams>());
        }
    }
}
