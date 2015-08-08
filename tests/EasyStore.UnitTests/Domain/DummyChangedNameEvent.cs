﻿namespace EasyStore.UnitTests.Domain
{
    using EasyStore.CommonDomain;

    public class DummyChangedNameEvent : IDomainEvent
    {
        public DummyChangedNameEvent(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
