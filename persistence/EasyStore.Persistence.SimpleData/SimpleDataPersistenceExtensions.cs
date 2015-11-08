namespace EasyStore.Persistence.SimpleData
{
    using System;

    using EasyStore.DI;

    public static class SimpleDataPersistenceExtensions
    {
        public static Wireup UserSimpleDataPersistenceEngine(this Wireup wireup, string connectionString)
        {
            Func<IContainer, IPersistStreams> resolver =
                x => new SimpleDataPersistenceEngine(connectionString, x.Resolve<ISerialize>());
            wireup.With(resolver);
            return wireup;
        }
    }
}
