namespace EasySample
{
    using System;
    using System.Diagnostics;

    using EasySample.Domain;

    using EasyStore.CommonDomain;

    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            var note = Note.CreateNew(Guid.NewGuid(), "Be Da Best", "Do Or Die");

            note.ChangeTitle("Terefere");
            note.ChangeTitle("JABADABADU");

            foreach (var @event in (note as IAggregate).GetUncommittedEvents())
            {
                Debug.WriteLine(JsonConvert.SerializeObject(@event));
            }
        }
    }
}
