using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using VinnyB.CardGenerator.Models;

namespace VinnyB.CardGenerator.Extends
{
    public static class Extends
    {
        public static AttributeCardModel ToCard(this AttributeMetadata attribute)
        {
            AttributeCardModel attributeCard = new AttributeCardModel(attribute.DisplayName.UserLocalizedLabel.Label, attribute.LogicalName);
            if(attribute is PicklistAttributeMetadata)
            {
                PicklistAttributeMetadata picklistAttributeMetadata = (PicklistAttributeMetadata)attribute;
                if (picklistAttributeMetadata != null)
                {
                    var options = new List<OptionAttributeModel>();
                    foreach (var option_ in picklistAttributeMetadata?.OptionSet?.Options)
                    {
                        var optionAttributeModel = new OptionAttributeModel(option_.Label.UserLocalizedLabel.Label, option_.Value.Value);
                        options.Add(optionAttributeModel);
                    }
                    attributeCard.Properties = options;
                }
            }
            else if(attribute is BooleanAttributeMetadata)
            {
                BooleanAttributeMetadata booleanOptionSetMetadata = (BooleanAttributeMetadata)attribute;
                if (booleanOptionSetMetadata != null)
                {
                    var options = new List<OptionAttributeModel>();
                    options.Add(new OptionAttributeModel(booleanOptionSetMetadata?.OptionSet.TrueOption.Label.UserLocalizedLabel.Label, true));
                    options.Add(new OptionAttributeModel(booleanOptionSetMetadata?.OptionSet.FalseOption.Label.UserLocalizedLabel.Label, false));
                    attributeCard.Properties = options;
                }
            }
            return attributeCard;
        }

        public static string CardToJson(this AdaptativeCard adaptativeCard)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(adaptativeCard.GetType(), new DataContractJsonSerializerSettings());
                serializer.WriteObject(memoryStream, adaptativeCard);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public static string JSONWorkaround(this StringBuilder th)
        {
            string json = th.ToString();
            json = json.TrimEnd();
            json = json.TrimStart();
            json = json.ToString().Replace("\"key\":", string.Empty);
            json = json.Replace(",\"value\"", string.Empty);
            json = json.Replace("},{", ",");
            json = json.Replace("[{", "{");
            json = json.Replace("}]", "}");
            return json;
        }
    }
}
