using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VinnyB.CardGenerator.Models
{
    [DataContract]
    [KnownType(typeof(BodyFact))]
    public class BodyFact
    {
        [DataMember(Name = "type", Order = 0)]
        public string type { get; set; }

        [DataMember(Name = "facts", Order = 1)]
        public List<Fact> facts { get; set; }

        public BodyFact()
        {
            type = "FactSet";
            facts = new List<Fact>();
        }
    }
}
