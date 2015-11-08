namespace EasySample
{
    using System;
    using System.Diagnostics;

    using EasySample.Domain;

    using EasyStore;
    using EasyStore.CommonDomain;
    using EasyStore.Persistence;
    using EasyStore.Persistence.SimpleData;
    using EasyStore.Serialization.Json;

    using Newtonsoft.Json;

    class Program
    {
        private static IStoreEvents store;

        static void Main(string[] args)
        {
            store = Wireup.Init()
                .With<ISerialize>(new JsonPayloadSerializer())
                .UserSimpleDataPersistenceEngine("SD_ES")
                .Build();

            SaveTest();
        }

        private static void LoadTest()
        {
            var id = Guid.Parse("451D80EF-6598-4041-B779-BFC5496BD3C9");
            using (var stream = store.CreateStream("ST1"))
            {
                try
                {
                    var note = stream.LoadAggregate<Note>(id);
                    note.ChangeTitle(string.Format("JUPIKAJEJ: {0}", DateTime.Now));
                    stream.CommitChanges(Guid.NewGuid());
                }
                catch (Exception ex)
                {
                    stream.Dispose();
                }
            }
        }

        private static void SaveTest()
        {
            var note = Note.CreateNew(Guid.NewGuid(), "Be Da Best", "Do Or Die");

            note.ChangeTitle("Terefere");
            note.ChangeTitle("JABADABADU");

            var jsonSerializer = new JsonPayloadSerializer();
            var simpleDataPersistence = new SimpleDataPersistenceEngine("SD_ES", jsonSerializer);
            var store = new EventStore(simpleDataPersistence);
            using (var stream = store.CreateStream("ST1"))
            {
                stream.AttachAggregate(note);

                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}
