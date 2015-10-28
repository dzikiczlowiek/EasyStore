namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Product
{
    using System;

    using EasyStore.CommonDomain;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Person;

    public class ProductAggregate : AggregateRoot
    {
        public ProductAggregate(Guid aggregateId)
            : base(aggregateId)
        {
        }

        public ProductAggregate(Guid aggregateId, IRouteEvents eventRouter)
            : base(aggregateId, eventRouter)
        {
        }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public string Category { get; private set; }

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

        private void Apply(ChangedAgeEvent @event)
        {
            this.Price = @event.Age;
        }

        private void Apply(ChangedCategoryEvent @event)
        {
            this.Category = @event.Category;
        }
    }
}
