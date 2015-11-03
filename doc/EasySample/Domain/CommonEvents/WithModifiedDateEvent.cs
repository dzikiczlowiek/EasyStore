namespace EasySample.Domain.CommonEvents
{
    using System;

    using EasyStore.CommonDomain;
    using EasyStore.Infrastructure;

    public abstract class WithModifiedDateEvent : IDomainEvent
    {
        protected WithModifiedDateEvent()
        {
            this.ModifiedDate = CoreTime.UtcNow;
        }

        public DateTime ModifiedDate { get; protected set; }
    }
}
