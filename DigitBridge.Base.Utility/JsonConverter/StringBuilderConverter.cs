using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Utility
{

    public class StringBuilderConverter : JsonConverter<StringBuilder>
    {
        public override void WriteJson(JsonWriter writer, StringBuilder sb, JsonSerializer serializer)
        {
            if (sb == null || sb.Length == 0)
                return;
            writer.WriteRawValue(sb.ToString());
        }

        public override StringBuilder ReadJson(JsonReader reader, Type objectType, StringBuilder existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JArray.Load(reader);
            var sb = new StringBuilder();
            sb.Append(jsonObject.ToString());
            return sb;
        }
    }
}
