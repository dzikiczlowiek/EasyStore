﻿namespace EasyStore.Serialization.Json.Tests
{
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement;
    using EasyStore.Tests.Common.Arrangement.DummyDomain.Person;

    using FluentAssertions;
    using Xunit;

    public class DeserializeTests : TestBase
    {
        [Fact]
        public void should_serialize_deserialize_aggregate_root_from_stream()
        {
            var id = A.RandomGuid();
            var aggregate = Person.CreateNew(id);
            aggregate.ChangeAge(29);
            aggregate.ChangeName("John Snow");
            var serializer = new JsonPayloadSerializer();

            var bytes = serializer.Serialize(aggregate);

            var deserializedPayload = serializer.Deserialize<Person>(bytes);

            deserializedPayload.Age.Should().Be(aggregate.Age);
            deserializedPayload.Name.Should().Be(aggregate.Name);
        }

        [Fact]
        public void should_serialize_and_deserialize_event_message()
        {
            var changedNameEvent = new ChangedNameEvent("John Snow");
            var serializer = new JsonPayloadSerializer();

            var serializedPayload = serializer.Serialize(changedNameEvent);

            var deserializedPayload = serializer.Deserialize<ChangedNameEvent>(serializedPayload);

            deserializedPayload.Name.Should().Be(changedNameEvent.Name);
        }
    }
}
