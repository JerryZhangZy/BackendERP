using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Utility
{
    public static class HttpRequestExtensions
    {
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
    }
}
