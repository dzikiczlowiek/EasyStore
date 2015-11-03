namespace EasyStore.Tests.Common
{
    using System.Collections.Generic;

    using EasyStore.CommonDomain;

    public static class AggregateExtensions
    {
        public static ICollection<IDomainEvent> GetUncommittedEvents(this AggregateRoot aggregate)
        {
            return ((IAggregate)aggregate).GetUncommittedEvents();
        }
    }
}
