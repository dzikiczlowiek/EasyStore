namespace EasyStore.Tests.Common.Arrangement
{
    using EasyStore.CommonDomain;

    public class DummyChangedAgeEvent : IDomainEvent
    {
        public DummyChangedAgeEvent(int age)
        {
            this.Age = age;
        }

        public int Age { get; private set; }
    }
}
