namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Order
{
    using System;

    using EasyStore.CommonDomain;

    [Serializable]
    public class ChangedOrderNumberEvent : IDomainEvent
    {
        public int OrderNumber { get; private set; }

        public ChangedOrderNumberEvent(int orderNumber)
        {
            this.OrderNumber = orderNumber;
        }
    }
}
