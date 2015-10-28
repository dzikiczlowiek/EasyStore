namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Product
{
    using EasyStore.CommonDomain;

    public class ChangedNameEvent : IDomainEvent
    {
        public ChangedNameEvent(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
