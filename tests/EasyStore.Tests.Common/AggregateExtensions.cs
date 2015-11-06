namespace EasyStore.Tests.Common
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyStore.CommonDomain;

    public static class AggregateExtensions
    {
        public static ICollection<IDomainEvent> GetUncommittedEvents(this AggregateRoot aggregate)
        {
            return ((IAggregate)aggregate).GetUncommittedEvents();
        }

        public static IEnumerable<EventMessage> ConvertUncommitedMessagesToEventMessages(this AggregateRoot aggregate)
        {
            return aggregate.GetUncommittedEvents().Select(x => new EventMessage(aggregate.Id, aggregate.Version, x));
        }
    }
}
