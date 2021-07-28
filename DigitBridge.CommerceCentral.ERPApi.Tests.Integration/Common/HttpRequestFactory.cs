using DigitBridge.Base.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace DigitBridge.CommerceCentral.ERPApi.Tests.Integration
{
    /// <summary>
    /// get a http request,include header, query, body,url
    /// </summary>
    public class HttpRequestFactory
    {
        //public static HttpRequest GetRequest(KeyValuePair<string, StringValues> headers)
        //{
        //    var context = new DefaultHttpContext();
        //    var req = context.Request;
        //    req.Headers.Add(headers);
        //    return req;
        //}
        //public static HttpRequest GetRequest(Dictionary<string, StringValues> query)
        //{
        //    var context = new DefaultHttpContext();
        //    var req = context.Request;
        //    req.Query = new QueryCollection(query);
        //    return req;
        //}
        //public static HttpRequest GetRequest(string url)
        //{
        //    var context = new DefaultHttpContext();
        //    var req = context.Request;
        //    req.Path = new PathString(url);
        //    return req;
        //}
        //public static HttpRequest GetRequest(object body)
        //{
        //    var context = new DefaultHttpContext();
        //    var req = context.Request;
        //    req.Body = body.ToStream();
        //    return req;
        //}
        private static void AddHeader<T>(RequestInfo<T> requestInfo, HttpRequest req)
        {
            if (requestInfo.RequestHeader == null) return;
            if (requestInfo.RequestHeader.MasterAccountNum.HasValue)
            {
                req.Headers.Add("masterAccountNum", requestInfo.RequestHeader.MasterAccountNum.Value.ToString());
            }
            if (requestInfo.RequestHeader.ProfileNum.HasValue)
            {
                req.Headers.Add("profileNum", requestInfo.RequestHeader.ProfileNum.Value.ToString());
            }
        }
        private static void AddQuery<T>(RequestInfo<T> requestInfo, HttpRequest req)
        {
            if (requestInfo.RequestQuery == null) return;
            var store = new Dictionary<string, StringValues>();
            if (requestInfo.RequestQuery.Top.HasValue)
            {
                store.Add("$top", requestInfo.RequestQuery.Top.Value.ToString());
            }
            if (requestInfo.RequestQuery.Skip.HasValue)
            {
                store.Add("$skip", requestInfo.RequestQuery.Skip.Value.ToString());
            }
            if (requestInfo.RequestQuery.IsQueryTotalCount.HasValue)
            {
                store.Add("$Count", requestInfo.RequestQuery.IsQueryTotalCount.Value.ToString());
            }
            if (!string.IsNullOrEmpty(requestInfo.RequestQuery.SortBy))
            {
                store.Add("$sortBy", requestInfo.RequestQuery.SortBy);
            }
            if (requestInfo.RequestQuery.Filter != null)
            {
                store.Add("$filter", requestInfo.RequestQuery.Filter.ToString());
            }
            if (requestInfo.RequestQuery.LoadAll.HasValue)
            {
                store.Add("$loadAll", requestInfo.RequestQuery.LoadAll.ToInt().ToString());
            }
            req.Query = new QueryCollection(store);
        }

        private static void AddBody<T>(RequestInfo<T> requestInfo, HttpRequest req)
        {
            if (requestInfo.RequestBody == null) return;
            req.Body = requestInfo.RequestBody.ToStream();
        }
        //public static HttpRequest GetRequest<T>(string pathString, string method, RequestInfo<T> requestInfo)
        public static HttpRequest GetRequest<T>( RequestInfo<T> requestInfo)
        {
            var context = new DefaultHttpContext();
            var req = context.Request;
            req.ContentType = "application/json;";
            //req.Method = method;
            //if (!string.IsNullOrEmpty(pathString))
            //{
            //    req.Path = new PathString(pathString);
            //}
            AddHeader(requestInfo, req);
            AddQuery(requestInfo, req);
            AddBody(requestInfo, req);
            return req;
        }
        //public static HttpRequest GetRequest(string pathString, object body)
        //{
        //    var context = new DefaultHttpContext();
        //    var req = context.Request;
        //    req.Path = new PathString(pathString);
        //    req.Body = body.ToStream();
        //    return req;
        //}
    }
}
