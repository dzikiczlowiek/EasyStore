namespace EasySample.Domain
{
    using System;

    using EasyStore.CommonDomain;

    public class Note : AggregateRoot
    {
        public static Note CreateNew(Guid aggregateId, string title, string body)
        {
            return new Note(aggregateId, title, body);
        }

        private Note(Guid aggregateId, string title, string body)
            : this(aggregateId)
        {
        }

        private Note(Guid aggregateId)
            : base(aggregateId)
        {
           
        }
    }
}
