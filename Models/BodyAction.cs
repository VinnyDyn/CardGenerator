using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VinnyB.CardGenerator.Models
{
    [DataContract]
    [KnownType(typeof(BodyAction))]
    public class BodyAction
    { 
        [DataMember(Name = "type", Order = 0)]
        public string type { get; set; }

        [DataMember(Name = "actions", Order = 1)]
        public List<Action> actions { get; set; }

        public BodyAction()
        {
            this.type = "ActionSet";
            this.actions = new List<Action>();
        }
    }
}
