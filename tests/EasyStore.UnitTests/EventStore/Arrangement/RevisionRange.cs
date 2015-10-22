namespace EasyStore.UnitTests.EventStore.Arrangement
{
    using System;

    public class RevisionRange
    {
        public RevisionRange()
        {
            this.Min = Int32.MinValue;
            this.Max = Int32.MaxValue;
        }

        public int Min { get; private set; }

        public int Max { get; private set; }

        public RevisionRange From(int minRevision)
        {
            this.Min = minRevision;
            return this;
        }

        public RevisionRange To(int maxRevision)
        {
            this.Min = maxRevision;
            return this;
        }
    }
}