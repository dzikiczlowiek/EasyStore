namespace EasySample.Domain
{
    using System;

    using EasySample.Domain.CommonEvents;
    using EasySample.Domain.NoteEvents;

    using EasyStore.CommonDomain;

    public class Note : AggregateRoot
    {
        private Note(Guid aggregateId, string title, string body)
            : this(aggregateId)
        {
            this.RaiseEvent(new NoteCreatedEvent(aggregateId, title, body));
        }

        private Note(Guid aggregateId)
            : base(aggregateId)
        {
            this.RegisteredRoutes.Register<NoteCreatedEvent>(this.ApplyCreateNew);
            this.RegisteredRoutes.Register<ChangedTitleEvent>(this.ApplyChangeTitle);
        }

        public string Title { get; private set; }

        public string Body { get; private set; }

        public DateTime? ModifyDate { get; private set; }

        public bool IsPrivate { get; private set; }

        public static Note CreateNew(Guid aggregateId, string title, string body)
        {
            return new Note(aggregateId, title, body);
        }

        public void ChangeTitle(string title)
        {
            this.RaiseEvent(new ChangedTitleEvent(title));
        }

        private void ApplyChangeTitle(ChangedTitleEvent @event)
        {
            this.ApplyModifiedDateChange(@event);
            this.Title = @event.Title;
        }

        private void ApplyCreateNew(NoteCreatedEvent @event)
        {
            this.Id = @event.AggregateId;
            this.Title = @event.Title;
            this.Body = @event.Body;
        }

        private void ApplyModifiedDateChange(WithModifiedDateEvent @event)
        {
            this.ModifyDate = @event.ModifiedDate;
        }
    }
}
