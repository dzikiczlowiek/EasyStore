namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Product
{
    using System;

    using EasyStore.CommonDomain;

    [Serializable]
    public class ChangedCategoryEvent : IDomainEvent
    {
        public ChangedCategoryEvent(string category)
        {
            this.Category = category;
        }

        public string Category { get; private set; }
    }
}
