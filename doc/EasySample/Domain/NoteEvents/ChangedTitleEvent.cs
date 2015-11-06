namespace EasySample.Domain.NoteEvents
{
    using System;

    using EasySample.Domain.CommonEvents;

    [Serializable]
    public class ChangedTitleEvent : WithModifiedDateEvent
    {
        public ChangedTitleEvent(string title)
        {
            this.Title = title;
        }

        public string Title { get; protected set; }
    }
}
