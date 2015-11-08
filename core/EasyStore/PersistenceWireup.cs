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
            this.With(instance);
            return this;
        }

        protected virtual SerializationWireup WithSerializer(ISerialize serializer)
        {
            return new SerializationWireup(this, serializer);
        }

        public virtual PersistenceWireup InitializeStorageEngine()
        {
            this._initialize = true;
            return this;
        }

        public virtual PersistenceWireup TrackPerformanceInstance(string instanceName)
        {
            if (instanceName == null)
            {
                throw new ArgumentNullException("instanceName", ExceptionMessages.InstanceCannotBeNull);
            }

            this._tracking = true;
            this._trackingInstanceName = instanceName;
            return this;
        }

        public override IStoreEvents Build()
        {
            var engine = this.Container.Resolve<IPersistStreams>();

            if (this._initialize)
            {
                engine.Initialize();
            }

            if (this._tracking)
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
            this.Container.Register<ISerialize>().Use(serializer);
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
