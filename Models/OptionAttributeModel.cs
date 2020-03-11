namespace VinnyB.CardGenerator.Models
{
    public class OptionAttributeModel
    {
        public string DisplayName;
        public int OptionSetValue;
        public bool TwoOptionValue;

        public OptionAttributeModel(string displayName, int value)
        {
            DisplayName = displayName;
            OptionSetValue = value;
        }

        public OptionAttributeModel(string displayName, bool value)
        {
            DisplayName = displayName;
            TwoOptionValue = value;
        }
    }
}
