namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Product
{
    using EasyStore.CommonDomain;

    public class ChangedPriceEvent : IDomainEvent
    {
        public ChangedPriceEvent(decimal price)
        {
            this.Price = price;
        }

        public decimal Price { get; private set; }
    }
}
