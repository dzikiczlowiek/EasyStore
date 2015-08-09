namespace EasyStore.Serialization.Json.Tests
{
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement;
    using FluentAssertions;
    using Xunit;

    public class DeserializeTests : TestBase
    {
        [Fact]
        public void should_serialize_deserialize_aggregate_root_from_stream()
        {
            var id = A.RandomGuid();
            var aggregate = DummyAggregate.CreateNew(id);
            aggregate.ChangeAge(29);
            aggregate.ChangeName("John Snow");
            var serializer = new JsonPayloadSerializer();

            var bytes = serializer.Serialize(aggregate);

            var deserializedPayload = serializer.Deserialize<DummyAggregate>(bytes);

            deserializedPayload.Age.Should().Be(aggregate.Age);
            deserializedPayload.Name.Should().Be(aggregate.Name);
        }

        [Fact]
        public void should_serialize_and_deserialize_event_message()
        {
            var changedNameEvent = new DummyChangedNameEvent("John Snow");
            var serializer = new JsonPayloadSerializer();

            var serializedPayload = serializer.Serialize(changedNameEvent);

            var deserializedPayload = serializer.Deserialize<DummyChangedNameEvent>(serializedPayload);

            deserializedPayload.Name.Should().Be(changedNameEvent.Name);
        }
    }
}
