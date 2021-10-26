using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    [Serializable()]
    public partial class SelectListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public int CountNumber { get; set; }

        public string Description { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public bool With_Description { get { return (Description != null && !string.IsNullOrWhiteSpace(Description)); } }
        public bool ShouldSerializeDescription() { return (With_Description); }

        public IList<KeyValuePair<string, object>> More { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public bool With_More { get { return (More != null && More.Count > 0); } }
        public bool ShouldSerializeMore() { return (With_More); }
        public SelectListItem AddMore(string name, string value)
        {
            if (!With_More)
                More = new List<KeyValuePair<string, object>>();
            More.Add(new KeyValuePair<string, object>(name, value));
            return this;
        }

        public SelectListItem()
        {
            Value = string.Empty;
            Text = string.Empty;
            CountNumber = 0;
            Description = null;
            More = null;
        }
        public SelectListItem(string text, string value) : this()
        {
            Text = text;
            Value = value;
        }
        public SelectListItem(string text, string value, string desc, IList<KeyValuePair<string, object>> more) : this()
        {
            Text = text;
            Value = value;
            Description = desc;
            More = more;
        }
    }
}
