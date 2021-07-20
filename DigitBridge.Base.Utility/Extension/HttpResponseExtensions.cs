using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.Base.Utility
{
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Get nullable value type data from route datas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async static Task Output(this HttpResponse response, object data, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            response.ContentType = "application/json;charset=utf-8;";
            response.StatusCode = (int)statusCode;
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            response.ContentLength = bytes.Length;
            await response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
