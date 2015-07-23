namespace EasyStore
{
    using System;
    using System.Collections.Generic;

    using EasyStore.CommonDomain;

    public class EventStream : IEventStream, ICommitEvents
    {
        private readonly ICollection<IAggregate> _loadedAggregates = new List<IAggregate>();

        private readonly ICommitEvents _persistence;

        public EventStream(string streamId, ICommitEvents persistence)
        {
            this.StreamId = streamId;
            this._persistence = persistence;
        }

        public string StreamId { get; private set; }

        public TAggregate LoadAggregate<TAggregate>(Guid aggregateId) where TAggregate : IAggregate
        {
            throw new NotImplementedException();
        }

        public void CommitChanges(Guid commitId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
