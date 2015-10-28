namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Product
{
    using EasyStore.CommonDomain;

    public class ChangedCategoryEvent : IDomainEvent
    {
        public ChangedCategoryEvent(string category)
        {
            this.Category = category;
        }

        public string Category { get; private set; }
    }
}
