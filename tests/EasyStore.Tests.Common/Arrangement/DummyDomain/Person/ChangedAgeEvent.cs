namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Person
{
    using System;

    using EasyStore.CommonDomain;
   
    [Serializable]
    public class ChangedAgeEvent : IDomainEvent
    {
        public ChangedAgeEvent(int age)
        {
            this.Age = age;
        }

        public int Age { get; private set; }
    }
}
