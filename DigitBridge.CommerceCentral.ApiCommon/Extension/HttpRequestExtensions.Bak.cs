using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    public static partial class HttpRequestExtensions
    {

        ///// <summary>
        ///// Get all request parameter to RequestParameter object, include Header and Query string
        ///// </summary>
        //public static T GetRequestParameter<T>(this HttpRequest req) where T : RequestParameter, new()
        //{
        //    return (T)req.GetRequestParameter(typeof(T));
        //}
        ///// <summary>
        ///// Get all request parameter to RequestParameter object, include Header and Query string
        ///// </summary>
        //public static object GetRequestParameter(this HttpRequest req, Type instanceType)
        //{
        //    var instance = Activator.CreateInstance(instanceType);
        //    var properties = instanceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        //    foreach (var property in properties)
        //    {
        //        var required = property.GetCustomAttribute<RequiredAttribute>();
        //        var parameterName = property.GetCustomAttribute<DisplayAttribute>()?.Name;
        //        var parameterValue = req.GetData(parameterName);
        //        if (required != null && parameterName == null)
        //        {
        //            throw new Exception(required.ErrorMessage);
        //        }
        //        if (parameterValue == null)
        //        {
        //            continue;
        //        }

        //        var val = Convert.ChangeType(parameterValue, property.PropertyType);
        //        var range = property.GetCustomAttribute<RangeAttribute>();
        //        var valid = range?.IsValid(val);
        //        if (valid.HasValue && valid.Value)
        //        {
        //            throw new Exception(range.ErrorMessage);
        //        }
        //        property.SetValue(instance, val);

        //    }
        //    return instance;
        //}

        /// <summary>
        /// Get nullable value type data from context headers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T? GetHeaderData<T>(this HttpRequest req, string key) where T : struct
        {
            if (req.Headers.ContainsKey(key))
            {
                var value = req.Headers[key].ToString();
                return (T)Convert.ChangeType(value, typeof(T));
            }
            return null;
        }
        
        public static async Task<object> GetBodyObjectAsync(this HttpRequest req, Type type)
        {
            using (var reader = new StreamReader(req.Body))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject(json, type);
            }
        }
    }
}
