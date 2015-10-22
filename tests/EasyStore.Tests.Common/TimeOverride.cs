namespace EasyStore.Tests.Common
{
    using System;

    using EasyStore.Infrastructure;

    public class TimeOverride : IDisposable
    {
        public TimeOverride(DateTime utcNow)
        {
            CoreTime.MockUtcTime(() => utcNow);
        }

        public static TimeOverride LockOn(DateTime utcNow)
        {
            return new TimeOverride(utcNow);
        }

        public static TimeOverride Lock()
        {
            return LockOn(DateTime.UtcNow);
        }

        public void Dispose()
        {
            CoreTime.Reset();
        }
    }
}
