namespace EasyStore.Persistence
{
    using System;
    using Serialization;
    using EasyStore.CommonDomain;

    public class AggregateLoader : ILoadAggregates
    {
        private readonly IPersistStreams _persistence;

        private readonly ISerialize _serializer;

        private readonly IConstructAggregates _aggregatorConstructor;

        public AggregateLoader(IPersistStreams persistence, ISerialize serializer, IConstructAggregates aggregatorConstructor)
        {
            this._persistence = persistence;
            this._serializer = serializer;
            this._aggregatorConstructor = aggregatorConstructor;
        }

        public TAggregate LoadAggregate<TAggregate>(Guid aggregateId) where TAggregate : AggregateRoot
        {
            TAggregate aggregate;

            var snapshot = this._persistence.GetSnapshotOfAggregate(aggregateId);

            if (snapshot != null)
            {
                aggregate = this._serializer.Deserialize<TAggregate>(snapshot.Data);
            }
            else
            {
                aggregate = this._aggregatorConstructor.Build<TAggregate>(aggregateId);
            }

            return aggregate;
        }

        public TAggregate LoadAggregate<TAggregate>(Guid aggregateId, int version) where TAggregate : AggregateRoot
        {
            throw new NotImplementedException();
        }
    }
}
