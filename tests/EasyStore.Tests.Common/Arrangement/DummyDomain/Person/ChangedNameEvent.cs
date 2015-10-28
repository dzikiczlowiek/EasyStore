namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Person
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
