using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace PottyTrainer.DataModel
{
    public class PeePooEvent
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public DateTime EventWhen { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EventType EventType { get; set; }

        public bool InToilet { get; set; }

    }
}