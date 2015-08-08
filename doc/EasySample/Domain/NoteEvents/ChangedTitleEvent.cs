namespace EasySample.Domain.NoteEvents
{
    using EasySample.Domain.CommonEvents;

    public class ChangedTitleEvent : WithModifiedDateEvent
    {
        public ChangedTitleEvent(string title)
        {
            this.Title = title;
        }

        public string Title { get; protected set; }
    }
}
