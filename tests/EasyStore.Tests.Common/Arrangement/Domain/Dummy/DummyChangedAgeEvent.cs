namespace EasyStore.Tests.Common.Arrangement.Domain.Dummy
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
