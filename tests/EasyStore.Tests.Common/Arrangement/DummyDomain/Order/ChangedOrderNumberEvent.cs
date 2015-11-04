namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Order
{
    using EasyStore.CommonDomain;

    public class ChangedOrderNumberEvent : IDomainEvent
    {
        public int OrderNumber { get; private set; }

        public ChangedOrderNumberEvent(int orderNumber)
        {
            this.OrderNumber = orderNumber;
        }
    }
}
