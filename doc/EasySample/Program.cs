namespace EasySample
{
    using System;

    using EasySample.Domain;

    class Program
    {
        static void Main(string[] args)
        {
            var note = Note.CreateNew(Guid.NewGuid(), "Be Da Best", "Do Or Die");

        }
    }
}
