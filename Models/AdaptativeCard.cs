using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VinnyB.CardGenerator.Models
{
    [DataContract]
    public class AdaptativeCard
    {
        [DataMember(Name = "$schema", Order = 0)]
        public string schema { get; set; }

        [DataMember(Name = "type", Order = 1)]
        public string type { get; set; }

        [DataMember(Name = "version", Order = 2)]
        public string version { get; set; }

        [DataMember(Name = "body", Order = 3)]
        public List<object> body { get; set; }

        public AdaptativeCard()
        {
            schema = "http://adaptivecards.io/schemas/adaptive-card.json";
            type = "AdaptiveCard";
            version = "1.0";
            body = new List<object>();
        }
    }
}
