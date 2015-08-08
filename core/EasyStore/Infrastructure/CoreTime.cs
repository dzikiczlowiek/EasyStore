namespace EasyStore.Infrastructure
{
    using System;

    public static class CoreTime
    {
        private static Func<DateTime> fixedDateTime;

        public static DateTime Now
        {
            get
            {
                if (fixedDateTime != null)
                {
                    return fixedDateTime().ToLocalTime();
                }

                return DateTime.Now;
            }
        }

        public static DateTime UtcNow
        {
            get
            {
                if (fixedDateTime != null)
                {
                    return fixedDateTime();
                }

                return DateTime.UtcNow;
            }
        }

        public static void SetFixedUtcTime(Func<DateTime> fixedUtcTime)
        {
            fixedDateTime = fixedUtcTime;
        }
    }
}
