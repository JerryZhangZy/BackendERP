using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    public static partial class HttpRequestExtensions
    {
        /// <summary>
        /// Get all request parameter to RequestParameter object, include Header and Query string
        /// </summary>
        public static async Task<TPayload> GetParameters<TPayload>(this HttpRequest req, bool fromBody = false)
            where TPayload : PayloadBase, new()
        {
            if (fromBody)
                return await req.GetBodyParameters<TPayload>();
            else
                return req.GetNoneBodyParameters<TPayload>();
        }
        /// <summary>
        /// Get all request parameter to RequestParameter object, include Header and Query string
        /// </summary>
        public static async Task<TPayload> GetParameters<TEntity, TPayload>(this HttpRequest req)
            where TPayload : PayloadBase, new()
            where TEntity : class
        {
            var instance = new TPayload();
            instance.ReqeustData = await req.GetBodyObjectAsync<TEntity>();
            instance.MasterAccountNum = req.GetHeaderValue("masterAccountNum").ToInt();
            instance.ProfileNum = req.GetHeaderValue("profileNum").ToInt();
            return instance;
        }
        private static async Task<TPayload> GetBodyParameters<TPayload>(this HttpRequest req)
            where TPayload : PayloadBase, new()
        {
            var instance = await req.GetBodyObjectAsync<TPayload>();
            instance.MasterAccountNum = req.GetHeaderValue("masterAccountNum").ToInt();
            instance.ProfileNum = req.GetHeaderValue("profileNum").ToInt();
            return instance;
        }
        /// <summary>
        /// Get all request parameter to RequestParameter object, include Header and Query string
        /// </summary>
        private static T GetNoneBodyParameters<T>(this HttpRequest req) where T : PayloadBase, new()
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
                    var param = req.GetQueryStringValue(item.Key);
                    if (!string.IsNullOrEmpty(param))
                        item.Value(param);
                }
            }
            return instance;
        }


        public static async Task<T> GetBodyObjectAsync<T>(this HttpRequest req) where T : class
        {
            using (var reader = new StreamReader(req.Body))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(json);

            }
        }

        /// <summary>
        /// Get value string from context headers
        /// </summary>
        /// <returns></returns>
        private static string GetHeaderValue(this HttpRequest req, string key) =>
            req.Headers.TryGetValue(key, out var val)
                ? val.ToString()
                : string.Empty;

        /// <summary>
        /// Get value string from context headers
        /// </summary>
        /// <returns></returns>
        private static string GetQueryStringValue(this HttpRequest req, string key) =>
            req.Query.TryGetValue(key, out var val)
                ? val.ToString()
                : string.Empty;

        /// <summary>
        /// Get data from Headers 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="key"></param> 
        /// <returns></returns>
        public static string GetData(this HttpRequest req, string key, ParameterLocation location)
        {
            string value = null;
            if (req.Headers.ContainsKey(key) && location == ParameterLocation.Header)
            {
                value = req.Headers[key].ToString();
            }
            else if (req.Query.ContainsKey(key) && location == ParameterLocation.Query)
            {
                value = req.Query[key].ToString();
            }
            else if (req.HttpContext.GetRouteData().Values.ContainsKey(key) && location == ParameterLocation.Path)
            {
                value = req.HttpContext.GetRouteData().Values[key].ToString();
            }
            else if (req.Cookies.ContainsKey(key) && location == ParameterLocation.Cookie)
            {
                value = req.Cookies[key].ToString();
            }
            return value;
        }
    }
}
