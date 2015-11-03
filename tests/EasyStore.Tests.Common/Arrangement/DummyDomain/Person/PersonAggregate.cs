namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Person
{
    using System;

    using EasyStore.CommonDomain;

    public class PersonAggregate : AggregateRoot
    {
        private PersonAggregate(Guid aggregateId)
            : base(aggregateId)
        {
        }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public static PersonAggregate CreateNew(Guid aggregateId)
        {
            return new PersonAggregate(aggregateId);
        }

        public PersonAggregate ChangeName(string name)
        {
            this.RaiseEvent(new ChangedNameEvent(name));
            return this;
        }

        public PersonAggregate ChangeAge(int age)
        {
            this.RaiseEvent(new ChangedAgeEvent(age));
            return this;
        }

        private void Apply(ChangedNameEvent @event)
        {
            this.Name = @event.Name;
        }

        private void Apply(ChangedAgeEvent @event)
        {
            this.Age = @event.Age;
        }
    }
}
