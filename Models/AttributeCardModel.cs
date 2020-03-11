namespace VinnyB.CardGenerator.Models
{
    public class AttributeCardModel
    {
        public string DisplayName;
        public string LogicalName;
        public string Value;
        public object Properties;

        public AttributeCardModel(string displayName, string logicalName)
        {
            this.DisplayName = displayName;
            this.LogicalName = logicalName;
            this.Value = string.Empty;
            this.Properties = null;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
