namespace EasyStore.UnitTests.EventStore.Arrangement
{
    using EasyStore.Tests.Common;

    public static class EventStreamValuesGenerator
    {
        public static string RandomStreamId(this ValuesGenerator a)
        {
            return a.RandomGuid().ToString();
        }
    }
}
