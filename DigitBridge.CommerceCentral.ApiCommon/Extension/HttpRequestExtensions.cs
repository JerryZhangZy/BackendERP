using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    public static class HttpRequestExtensions
    {

        /// <summary>
        /// Get all request parameter to RequestParameter object, include Header and Query string
        /// </summary>
        public static T GetRequestParameter<T>(this HttpRequest req) where T : PayloadBase, new()
        {
            //return (T)req.GetRequestParameter(typeof(T));
            //Activator.CreateInstance(instanceType);
            var instance = new T();
            instance.MasterAccountNum = req.GetHeaderValue("masterAccountNum").ToInt();
            instance.ProfileNum = req.GetHeaderValue("profileNum").ToInt();
            instance.Top = req.GetQueryStringValue("$top").ToInt();
            instance.Skip = req.GetQueryStringValue("$skip").ToInt();
            instance.IsQueryTotalCount = req.GetQueryStringValue("$Count").ToBool();
            instance.SortBy = req.GetQueryStringValue("$sortBy");
            instance.Filter = req.GetQueryStringValue("$filter").ToJObject();

            var moreParameterFunc = instance.GetOtherParameters();
            if (moreParameterFunc != null && moreParameterFunc.Count > 0)
            {
                foreach (var item in moreParameterFunc)
                {
                    if (string.IsNullOrEmpty(item.Key) || item.Value is null)
                        continue;
                    item.Value(req.GetQueryStringValue(item.Key));
                }
            }
            return instance;
        }


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
        /// Get nullable value type data from route datas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T? GetRouteData<T>(this HttpRequest req, string key) where T : struct
        {
            if (req.HttpContext.GetRouteData().Values.ContainsKey(key))
            {
                var value = req.HttpContext.GetRouteData().Values[key].ToString();
                return (T)Convert.ChangeType(value, typeof(T));
            }
            return null;
        }
        /// <summary>
        /// Get nullable reference type data from route datas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetRouteObject<T>(this HttpRequest req, string key) where T : class
        {
            if (req.HttpContext.GetRouteData().Values.ContainsKey(key))
            {
                var value = req.HttpContext.GetRouteData().Values[key].ToString();
                return (T)Convert.ChangeType(value, typeof(T));
            }
            else
            {
                return null;
            }
        }

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

        /// <summary>
        /// Get nullable reference type data from headers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetHeaderObject<T>(this HttpRequest req, string key) where T : class
        {
            if (req.Headers.ContainsKey(key))
            {
                var value = req.Headers[key].ToString();
                return (T)Convert.ChangeType(value, typeof(T));
            }
            return null;
        }

        public static async Task<T> GetBodyObjectAsync<T>(this HttpRequest req) where T : class
        {
            using (var reader = new StreamReader(req.Body))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(json);

            }
        }
        public static async Task<object> GetBodyObjectAsync(this HttpRequest req, Type type)
        {
            using (var reader = new StreamReader(req.Body))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject(json, type);
            }
        }
        /// <summary>
        /// Get data from Headers 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="key"></param> 
        /// <returns></returns>
        public static string GetData(this HttpRequest req, string key)
        {
            string value = null;
            if (req.Headers.ContainsKey(key))
            {
                value = req.Headers[key].ToString();
            }
            else if (req.HttpContext.GetRouteData().Values.ContainsKey(key))
            {
                value = req.HttpContext.GetRouteData().Values[key].ToString();
            }
            else if (req.Query.ContainsKey(key))
            {
                value = req.Query[key].ToString();
            }
            return value;
        }

        /// <summary>
        /// Get value string from context headers
        /// </summary>
        /// <returns></returns>
        public static string GetHeaderValue(this HttpRequest req, string key) =>
            req.Headers.TryGetValue(key, out var val)
                ? val.ToString()
                : string.Empty;

        /// <summary>
        /// Get value string from context headers
        /// </summary>
        /// <returns></returns>
        public static string GetQueryStringValue(this HttpRequest req, string key) =>
            req.Query.TryGetValue(key, out var val)
                ? val.ToString()
                : string.Empty;


    }
}
