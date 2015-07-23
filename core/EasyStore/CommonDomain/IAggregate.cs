namespace EasyStore.CommonDomain
{
    using System;
    using System.Collections.Generic;

    public interface IAggregate
    {
        Guid Id { get; }

        int Version { get; }

        void ApplyEvent(IDomainEvent eventData);

        ICollection<IDomainEvent> GetUncommittedEvents();

        void ClearUncommittedEvents();
    }
}
