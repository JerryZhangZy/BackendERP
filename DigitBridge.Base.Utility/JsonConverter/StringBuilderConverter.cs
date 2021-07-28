using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Utility
{

    public class StringBuilderConverter :  JsonConverter<StringBuilder>
    {
        public override void WriteJson(JsonWriter writer, StringBuilder sb, JsonSerializer serializer)
        {
            if (sb == null || sb.Length == 0)
                return;
            writer.WriteRawValue(sb.ToString());
        }

        public override StringBuilder ReadJson(JsonReader reader, Type objectType, StringBuilder existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return new StringBuilder();
        }
    }
}
