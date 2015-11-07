namespace EasyStore.Persistence.SimpleData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        }

        public IEnumerable<EventMessage> GetAggregateEvents(Guid aggregateId)
        {
            var events = new List<EventMessage>();
            var rawEvents = this._db.EventsLog.FindAllByAggregateId(aggregateId).OrderBySequence();

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
            using (var transaction = this._db.BeginTransaction())
            {
                this.CreateNewAggregates(attempt);

                foreach (var eventMessage in attempt.Events)
                {
                    this.InsertEventMessage(attempt, eventMessage);
                }

                this.UpdateAggregatesVersion(attempt);

                transaction.Commit();
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

        private void UpdateAggregatesVersion(CommitAttempt attempt)
        {
            var aggregatesVersion =
                attempt.Events.GroupBy(x => x.AggregateId, x => x.AggregateVersion)
                    .Select(x => new { AggregateId = x.Key, ActualVersion = x.Max() });

            foreach (var aggregateVersion in aggregatesVersion)
            {
                dynamic record = new SimpleRecord();
                record.AggregateId = aggregateVersion.AggregateId;
                record.Version = aggregateVersion.ActualVersion;
                this._db.Aggregates.Update(record);
            }
        }

        private void InsertEventMessage(CommitAttempt attempt, EventMessage eventMessage)
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

        private void CreateNewAggregates(CommitAttempt attempt)
        {
            var createdEventTypeName = typeof(CreatedEvent).AssemblyQualifiedName;
            var createdEvents = attempt.Events.Where(x => x.BodyType == createdEventTypeName);
            foreach (var createdEvent in createdEvents)
            {
                var aggregate = this._db.Aggregates.Get(createdEvent.AggregateId);
                if (aggregate == null)
                {
                    this._db.Aggregates.Insert(
                        AggregateId: createdEvent.AggregateId,
                        @Type: ((CreatedEvent)createdEvent.Body).AggregateType,
                        @Version: 1,
                        Archived: 0,
                        CreatedAt: CoreTime.UtcNow);
                }
            }
        }
    }
}
