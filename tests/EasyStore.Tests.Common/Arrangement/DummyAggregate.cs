namespace EasyStore.Tests.Common.Arrangement
{
    using System;

    using EasyStore.CommonDomain;

    public class DummyAggregate : AggregateRoot
    {
        private DummyAggregate(Guid aggregateId)
            : base(aggregateId)
        {
        }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public static DummyAggregate CreateNew(Guid aggregateId)
        {
            return new DummyAggregate(aggregateId);
        }

        public void ChangeName(string name)
        {
            this.RaiseEvent(new DummyChangedNameEvent(name));
        }

        public void ChangeAge(int age)
        {
            this.RaiseEvent(new DummyChangedAgeEvent(age));
        }

        private void Apply(DummyChangedNameEvent @event)
        {
            this.Name = @event.Name;
        }

        private void Apply(DummyChangedAgeEvent @event)
        {
            this.Age = @event.Age;
        }
    }
}
