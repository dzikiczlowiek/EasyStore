namespace EasyStore.Persistence
{
    public class AggregateLoader : ILoadAggregates
    {
        private IPersistStreams persistence;

        public TAggregate LoadAggregate<TAggregate>(System.Guid aggregateId) where TAggregate : CommonDomain.IAggregate
        {
            throw new System.NotImplementedException();
        }
    }
}
