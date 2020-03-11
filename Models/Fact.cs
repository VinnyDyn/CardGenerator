using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace VinnyB.CardGenerator.Models
{
    [DataContract]
    public class Fact
    {
        [DataMember(Name = "title", Order = 0)]
        public string title { get; set; }

        [DataMember(Name = "value", Order = 1)]
        public string value { get; set; }

        public Fact(AttributeCardModel attributeCardModel, object value)
        {
            this.title = attributeCardModel.DisplayName;

            if (value != null)
            {
                if (value is String
                || value is Int32
                || value is Decimal
                || value is Double
                || value is DateTime)
                {
                    this.value = value.ToString();
                }
                else if (value is Boolean)
                {
                    if (attributeCardModel.Properties != null)
                        this.value = ((List<OptionAttributeModel>)attributeCardModel.Properties).Where(w => w.TwoOptionValue == (bool)value).Select(s=>s.DisplayName).FirstOrDefault();
                    else
                        this.value = "";
                }
                else if (value is OptionSetValue)
                {
                    if (attributeCardModel.Properties != null)
                        this.value = ((List<OptionAttributeModel>)attributeCardModel.Properties).Where(w => w.OptionSetValue == ((OptionSetValue)value).Value).Select(s => s.DisplayName).FirstOrDefault();
                    else
                        this.value = "";
                }
                else if (value is Money)
                {
                    this.value = ((Money)value).Value.ToString();
                }
                else if (value is EntityReference)
                {
                    this.value = ((EntityReference)value).Name;
                }
            }
        }
    }
}
