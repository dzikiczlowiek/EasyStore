namespace EasyStore.Persistence.SimpleData.UnitTests.Arrangement
{
    using System;
    using System.Collections.Generic;

    using EasyStore.CommonDomain;
    using EasyStore.Serialization;

    using Simple.Data;

    public abstract class PersistStreamsFixtureBase
    {
        protected readonly ISerialize Serializer = new BinarySerializer();

        protected ICollection<Action<dynamic>> AssignActions = new List<Action<dynamic>>();

        public IEnumerable<EventLogRecord> GetEvents()
        {
            var db = Database.Open();
            foreach (var eventLog in db.EventsLog.All())
            {
                Type type = Type.GetType(eventLog.Type);
                var data = (byte[])eventLog.Data;
                var record = new EventLogRecord();
                record.AggregateId = eventLog.AggregateId;
                record.CommitId = eventLog.CommitId;
                record.CreatedAt = eventLog.CreatedAt;
                record.DomainEvent = this.Serializer.Deserialize(type, data) as IDomainEvent;
                record.EventType = eventLog.Type;
                record.StreamId = eventLog.StreamId;
                record.Version = eventLog.Version;
                yield return record;
            }
        }

        protected SimpleDataPersistenceEngine CreateSut()
        {
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
            var binarySerializer = new BinarySerializer();
            var sut = new SimpleDataPersistenceEngine(string.Empty, binarySerializer);
            return sut;
        }
    }
}
