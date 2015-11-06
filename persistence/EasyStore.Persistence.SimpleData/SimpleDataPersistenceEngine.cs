namespace EasyStore.Persistence.SimpleData
{
    using System;
    using System.Collections.Generic;

    using EasyStore.CommonDomain;
    using EasyStore.Infrastructure;

    using Serialization;
    using Simple.Data;

    public class SimpleDataPersistenceEngine : IPersistStreams
    {
        private readonly dynamic _db;

        private readonly ISerialize _serializer;

        public SimpleDataPersistenceEngine(string connectionName, ISerialize serializer)
        {
            this._db = Database.OpenNamedConnection(connectionName);
            this._serializer = serializer;
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<EventMessage> GetAggregateEvents(Guid aggregateId)
        {
            var events = new List<EventMessage>();
            var rawEvents = this._db.EventsLog.FindAllByAggregateId(aggregateId);

            foreach (var rawEvent in rawEvents)
            {
                Type type = Type.GetType(rawEvent.Type);
                var data = (byte[])rawEvent.Data;
                IDomainEvent @event = this._serializer.Deserialize(type, data) as IDomainEvent;
                var eventMessage = new EventMessage(aggregateId, rawEvent.Version, @event);
                events.Add(eventMessage);
            }

            return events;
        }

        public IEnumerable<EventMessage> GetAggregateEventsToVersion(Guid aggregateId, int version)
        {
            var events = new List<EventMessage>();
            var rawEvents =
                this._db.EventsLog.FindAllByAggregateId(aggregateId).Where(this._db.EventsLog.Version <= version);

            foreach (var rawEvent in rawEvents)
            {
                Type type = Type.GetType(rawEvent.Type);
                var data = (byte[])rawEvent.Data;
                IDomainEvent @event = this._serializer.Deserialize(type, data) as IDomainEvent;
                var eventMessage = new EventMessage(aggregateId, rawEvent.Version, @event);
                events.Add(eventMessage);
            }

            return events;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICommit> GetFrom(string streamId, int minRevision, int maxRevision)
        {
            throw new System.NotImplementedException();
        }

        public ICommit Commit(CommitAttempt attempt)
        {
            foreach (var eventMessage in attempt.Events)
            {
                this._db.EventsLog.Insert(
                    AggregateId: eventMessage.AggregateId,
                    Data: this._serializer.Serialize(eventMessage.Body),
                    StreamId: attempt.StreamId,
                    @Version: eventMessage.AggregateVersion,
                    @Type: eventMessage.BodyType,
                    CreatedAt: CoreTime.UtcNow,
                    CommitId: attempt.CommitId);
            }

            return null;
        }

        public Snapshot GetSnapshotOfAggregate(Guid aggregateId)
        {
            throw new System.NotImplementedException();
        }

        public Snapshot GetSnapshotOfAggregateUpToVersion(Guid aggregateId, int maxVersion)
        {
            throw new System.NotImplementedException();
        }
    }
}
