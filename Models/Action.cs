using System.Runtime.Serialization;

namespace VinnyB.CardGenerator.Models
{
    [DataContract]
    public class Action
    {
        [DataMember(Name = "type", Order = 0)]
        public string type { get; set; }

        [DataMember(Name = "title", Order = 1)]
        public string title { get; set; }

        [DataMember(Name = "url", Order = 2)]
        public string url { get; set; }

        public Action(string title, string url)
        {
            this.type = "Action.OpenUrl";
            this.title = title;
            this.url = url;
        }
    }
}
