namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Person
{
    using System;

    using EasyStore.CommonDomain;

    [Serializable]
    public class ChangedNameEvent : IDomainEvent
    {
        public ChangedNameEvent(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
