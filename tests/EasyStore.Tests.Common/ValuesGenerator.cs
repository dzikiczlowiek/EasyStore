namespace EasyStore.Tests.Common
{
    using System;
    using System.IO;
    using System.Text;

    public class ValuesGenerator
    {
        private static readonly Random Random = new Random(4567);

        public Guid RandomGuid()
        {
            return Guid.NewGuid();
        }

        public int RandomNumber()
        {
            return Random.Next();
        }

        public DateTime RandomDate()
        {
            var baseDate = new DateTime(1990, 1, 1);
            var randomOffset = TimeSpan.FromDays(Random.Next(0, 365 * 100));
            return baseDate + randomOffset;
        }

        public DateTime RandomFutureDate()
        {
            var baseDate = DateTime.Now;
            var randomOffset = TimeSpan.FromDays(Random.Next(0, 365 * 100));
            return baseDate + randomOffset;
        }

        public string RandomString()
        {
            var result = new StringBuilder();
            var wordCount = Random.Next(10);
            for (int i = 0; i < wordCount; i++)
            {
                var word = this.RandomShortString();
                result.Append(word);
                result.Append(" ");
            }

            return result.ToString();
        }

        public string RandomShortString()
        {
            var path = Path.GetRandomFileName();
            path = path.Replace(".", string.Empty);
            return path;
        }

        public Func<int> SequenceFrom(int start, int stepBy = 1)
        {
            var from = start;
            return () =>
            {
                var val = from;
                from += stepBy;
                return val;
            };
        }
    }
}
