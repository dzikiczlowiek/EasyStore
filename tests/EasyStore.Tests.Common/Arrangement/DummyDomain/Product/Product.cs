namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Product
{
    using System;

    using EasyStore.CommonDomain;

    public class Product : AggregateRoot
    {
        public Product(Guid aggregateId)
            : base(aggregateId)
        {
        }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public string Category { get; private set; }

        public static Product CreateNew(Guid aggregateId)
        {
            return new Product(aggregateId);
        }

        public void ChangeName(string name)
        {
            this.RaiseEvent(new ChangedNameEvent(name));
        }

        public void ChangePrice(decimal price)
        {
            this.RaiseEvent(new ChangedPriceEvent(price));
        }

        public void ChangeCategory(string category)
        {
            this.RaiseEvent(new ChangedCategoryEvent(category));
        }

        private void Apply(ChangedNameEvent @event)
        {
            this.Name = @event.Name;
        }

        private void Apply(ChangedPriceEvent @event)
        {
            this.Price = @event.Price;
        }

        private void Apply(ChangedCategoryEvent @event)
        {
            this.Category = @event.Category;
        }
    }
}
