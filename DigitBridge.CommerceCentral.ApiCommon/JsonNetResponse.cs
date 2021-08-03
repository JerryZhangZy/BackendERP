using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    public class JsonNetResponse<T> : HttpResponseMessage
    {
        private const string ContentTypeApplicationJson = "application/json";

        /// <summary>
        /// success respone data 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param> 
        public JsonNetResponse(T data, HttpStatusCode statusCode = HttpStatusCode.OK, JsonSerializerSettings setting = null) 
            : base(statusCode)
        {
            if (typeof(T) is string)
                Content = new StringContent(data.ToString(), Encoding.UTF8, ContentTypeApplicationJson);
            else
            {
                var jsonString = JsonConvert.SerializeObject(data, setting ?? GetDefaultJsonSerializerSettings());
                Content = new StringContent(jsonString, Encoding.UTF8, ContentTypeApplicationJson);
            }
        }

        public JsonSerializerSettings GetDefaultJsonSerializerSettings()
        {
            var setting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                Converters = new List<JsonConverter> { new StringEnumConverter { CamelCaseText = false } }
            };
            return setting;
        }

    }
}
