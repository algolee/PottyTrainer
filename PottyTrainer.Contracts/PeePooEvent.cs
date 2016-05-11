using System;

namespace PottyTrainer.Contracts
{
    public class PeePooEvent
    {
        public string Id { get; set; }
        public DateTime EventWhen { get; set; }

        public EventType EventType { get; set; }

        public bool InToilet { get; set; }

    }
}