using System;

namespace PottyTrainer.Core.Models
{
    public class PeePooEvent
    {
        public long Id { get; set; }
        public DateTime EventWhen { get; set; }

        public EventType EventType { get; set; }

        public bool InToilet { get; set; }

    }

    public enum EventType
    {
        Pee = 1,
        Poo = 2,
        Both = 3
    }
}
