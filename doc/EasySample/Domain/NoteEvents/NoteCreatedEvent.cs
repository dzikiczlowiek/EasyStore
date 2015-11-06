namespace EasySample.Domain.NoteEvents
{
    using System;

    using EasySample.Domain.CommonEvents;

    [Serializable]
    public class NoteCreatedEvent : CreatedAggregateEvent
    {
        public NoteCreatedEvent(Guid aggregateId, string title, string body)
            : base(aggregateId)
        {
            this.Title = title;
            this.Body = body;
        }

        public string Title { get; private set; }

        public string Body { get; private set; }
    }
}
