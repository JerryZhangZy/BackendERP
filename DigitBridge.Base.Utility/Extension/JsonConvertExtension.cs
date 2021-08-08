using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace DigitBridge.Base.Utility
{
    
    public static class JsonConvertExtension
    {
        public static JsonSerializerSettings DefaultJsonSerializerSettings(this bool ignoreNull)
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = ignoreNull ? NullValueHandling.Ignore : NullValueHandling.Include,
                Formatting = Formatting.None,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        OverrideSpecifiedNames = false
                    }
                }
            };
        }

        /// <summary>
        /// Use Json.Net convert string to object instance.
        /// Use default JsonSerializerSettings
        /// This function was created because it was reported that 
        /// calling JsonConvert.DeserializeObject in a timer trigger Azure directly will
        /// gernerate exception
        /// </summary>
        public static void PopulateToObject<T>(this string jsonInput, T obj, bool ignoreNull = true)
        {
            if (string.IsNullOrWhiteSpace(jsonInput)) return;
            var setting = ignoreNull.DefaultJsonSerializerSettings();
            jsonInput.PopulateToObject<T>(obj, setting);
        }
        /// <summary>
        /// Use Json.Net convert string to object instance.
        /// Use custom JsonSerializerSettings
        /// </summary>
        public static void PopulateToObject<T>(this string jsonInput, T obj, JsonSerializerSettings setting)
        {
            if (string.IsNullOrWhiteSpace(jsonInput)) return;
            try
            {
                JsonConvert.PopulateObject(jsonInput, obj, setting);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception. Location: JsonConvertExtension.PopulateToObject() ", ex);
            }
        }

        /// <summary>
        /// Use Json.Net convert string to object instance.
        /// Use default JsonSerializerSettings
        /// This function was created because it was reported that 
        /// calling JsonConvert.DeserializeObject in a timer trigger Azure directly will
        /// gernerate exception
        /// </summary>
        public static T JsonToObject<T>(this string jsonInput, bool ignoreNull = true, bool withType = false)
        {
            if (string.IsNullOrWhiteSpace(jsonInput)) return default(T);
            var setting = ignoreNull.DefaultJsonSerializerSettings();

            if (withType)
                setting.TypeNameHandling = TypeNameHandling.All;
            return jsonInput.JsonToObject<T>(setting);
        }
        /// <summary>
        /// Use Json.Net convert string to object instance.
        /// Use custom JsonSerializerSettings
        /// </summary>
        public static T JsonToObject<T>(this string jsonInput, JsonSerializerSettings setting)
        {
            if (string.IsNullOrWhiteSpace(jsonInput)) return default(T);
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonInput, setting);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception. Location: JsonConvertExtension.JsonToObject() ", ex);
            }
        }

        /// <summary>
        /// Use Json.Net convert bject instance to JSON string.
        /// Use default JsonSerializerSettings
        /// </summary>
        public static string ObjectToString<T>(this T obj, bool ignoreNull = true, string dateFormat = null, bool withType = false)
        {
            if (obj == null) return string.Empty;

            var setting = ignoreNull.DefaultJsonSerializerSettings();

            if (!string.IsNullOrEmpty(dateFormat))
                setting.DateFormatString = dateFormat;
            if (withType)
                setting.TypeNameHandling = TypeNameHandling.All;
            return obj.ObjectToString(setting);
        }
        /// <summary>
        /// Use Json.Net convert bject instance to JSON string.
        /// Use custom JsonSerializerSettings
        /// </summary>
        public static string ObjectToString<T>(this T obj, JsonSerializerSettings setting)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, setting);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception. Location: JsonConvertExtension.JsonToObject() ", ex);
            }
        }

        public static T StringToObject<T>(this string jsonString)
        {
            return jsonString.JsonToObject<T>();
        }

        public static Dictionary<string, T> ObjectToDicationary<T>(this object obj)
        {
            string jsonString = JsonConvert.SerializeObject(obj);

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonString);
            return dictionary;
        }

        public static T DicationaryToObject<T>(this Dictionary<string, string> dic)
        {
            string jsonString = JsonConvert.SerializeObject(dic);

            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static System.Data.DataTable StringToDataTable(this string dataString)
        {
            return (System.Data.DataTable)JsonConvert.DeserializeObject(dataString, (typeof(System.Data.DataTable)));
        }

        public static List<T> DataTableValueToList<T>(this System.Data.DataTable dt)
        {
            string serialized = JsonConvert.SerializeObject(dt);
            return JsonConvert.DeserializeObject<List<T>>(serialized);
        }


        public static JObject ToJObject(this Dictionary<string, object> dict) => JObject.FromObject(dict);

        public static string ToJsonString(this Dictionary<string, object> dict) => JObject.FromObject(dict).ToString(Formatting.None);

        public static JObject ToJObject(this string jsonString) => string.IsNullOrEmpty(jsonString) ? new JObject() : JObject.Parse(jsonString);

        public static Dictionary<string, object> ToDicationary(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return new Dictionary<string, object>();

            try
            {
                return JObject.Parse(jsonString).ToDicationary();
            }
            catch (JsonReaderException jex)
            {
                return new Dictionary<string, object>();
            }
        }

        public static Dictionary<string, object> ToDicationary(this JObject jObject) => DeserializeJObject(jObject);

        private static Dictionary<string, object> DeserializeJObject(JObject jObject)
        {
            var rtn = new Dictionary<string, object>();
            foreach (var kv in jObject)
            {
                switch (kv.Value)
                {
                    case JObject obj:
                        rtn.Add(kv.Key, DeserializeJObject(obj));
                        break;
                    case JArray arr:
                        rtn.Add(kv.Key, DeserializeJArray(arr));
                        break;
                    case JValue val:
                        rtn.Add(kv.Key, val.Value);
                        break;
                }
            }
            return rtn;
        }

        private static object DeserializeJArray(JArray jArray)
        {
            if (jArray == null)
                return new List<object>();
            if (jArray.Count == 0) return new List<object>();

            switch (jArray[0])
            {
                case JObject _:
                    return jArray.Select(o => DeserializeJObject(o as JObject)).ToList();
                case JArray _:
                    return jArray.Select(j => DeserializeJArray(j as JArray)).ToList();
                case JValue _:
                    return jArray.Select(v => v.ToString()).ToList();
            }
            return new List<object>();
        }

    }
}
