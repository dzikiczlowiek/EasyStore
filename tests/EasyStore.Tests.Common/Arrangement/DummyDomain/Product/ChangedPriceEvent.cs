namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Product
{
    using System;

    using EasyStore.CommonDomain;

    [Serializable]
    public class ChangedPriceEvent : IDomainEvent
    {
        public ChangedPriceEvent(decimal price)
        {
            this.Price = price;
        }

        public decimal Price { get; private set; }
    }
}
