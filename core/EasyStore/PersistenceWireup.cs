namespace EasyStore
{
    using System;

    using EasyStore.Persistence;

    public class PersistenceWireup : Wireup
    {
        private bool _initialize;
        private bool _tracking;
        private string _trackingInstanceName;

        public PersistenceWireup(Wireup inner)
            : base(inner)
        {
        }

        public virtual PersistenceWireup WithPersistence(IPersistStreams instance)
        {
            With(instance);
            return this;
        }

        protected virtual SerializationWireup WithSerializer(ISerialize serializer)
        {
            return new SerializationWireup(this, serializer);
        }

        public virtual PersistenceWireup InitializeStorageEngine()
        {
            _initialize = true;
            return this;
        }

        public virtual PersistenceWireup TrackPerformanceInstance(string instanceName)
        {
            if (instanceName == null)
            {
                throw new ArgumentNullException("instanceName", ExceptionMessages.InstanceCannotBeNull);
            }

            _tracking = true;
            _trackingInstanceName = instanceName;
            return this;
        }

        public override IStoreEvents Build()
        {
            var engine = Container.Resolve<IPersistStreams>();

            if (_initialize)
            {
                engine.Initialize();
            }

            if (_tracking)
            {
              //  Container.Register<IPersistStreams>(new PerformanceCounterPersistenceEngine(engine, _trackingInstanceName));
            }

            return base.Build();
        }
    }

    public class SerializationWireup : Wireup
    {
        public SerializationWireup(Wireup inner, ISerialize serializer)
            : base(inner)
        {
            Container.Register(serializer);
        }

        //public SerializationWireup Compress()
        //{
        //    var wrapped = Container.Resolve<ISerialize>();

        //    Container.Register<ISerialize>(new GzipSerializer(wrapped));
        //    return this;
        //}

        //public SerializationWireup EncryptWith(byte[] encryptionKey)
        //{
        //    var wrapped = Container.Resolve<ISerialize>();

        //    Container.Register<ISerialize>(new RijndaelSerializer(wrapped, encryptionKey));
        //    return this;
        //}
    }
}
