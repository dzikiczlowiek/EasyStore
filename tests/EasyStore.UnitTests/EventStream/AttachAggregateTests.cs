namespace EasyStore.UnitTests.EventStream
{
    using EasyStore.Tests.Common;
    using EasyStore.Tests.Common.Arrangement.Domain.Dummy;
    using EasyStore.UnitTests.EventStream.Arrangement;

    using Xunit;

    public class AttachAggregateTests : TestBase
    {
        [Fact]
        public void asdsadsadsad()
        {
            var dummyAggregate = DummyAggregate.CreateNew(A.RandomGuid());

            var fixture = new AttachAggregateFixture();

            var act = fixture.AttacheAggregate(dummyAggregate);
            act();

        }
    }
}
