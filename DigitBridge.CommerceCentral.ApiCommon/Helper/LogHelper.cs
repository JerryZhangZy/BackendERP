using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DigitBridge.CommerceCentral.ApiCommon
{
    public class LogHelper
    {/// <summary>
     /// generate req infn to dictionary,include body,header,query,route,cookie,path
     /// </summary>
     /// <param name="req"></param>
     /// <returns></returns>
        public static async Task<Dictionary<string, object>> GetRequestInfo(HttpRequest req, string functionName, IEnumerable<OpenApiParameterAttribute> parameters)
        {
            var requestInfos = new Dictionary<string, object>();
            requestInfos.Add("method", req.Method);

            #region Append header to requestInfos，prepare all headers 
            var headers = new List<object>();
            var allHeaders = new List<object>();
            foreach (var itemkey in req.Headers.Keys)
            {
                var header = new
                {
                    key = itemkey,
                    value = req.Headers[itemkey].ToString(),
                    type = "text"
                };
                allHeaders.Add(header);
                if (parameters.Where(i => i.Name == itemkey && i.In == ParameterLocation.Header).Count() > 0)
                    headers.Add(header);
            }
            requestInfos.Add("header", headers);
            #endregion

            #region Append url and query to requestInfos 
            var querys = new List<object>();
            foreach (var itemkey in req.Query.Keys)
            {
                var query = new
                {
                    key = itemkey,
                    value = req.Query[itemkey].ToString()
                };
                querys.Add(query);
            }

            requestInfos.Add("url", new
            {
                raw = new StringBuilder()
                            .Append(req.Scheme)
                            .Append("://")
                            .Append(req.Host)
                            .Append(req.PathBase)
                            .Append(req.Path)
                            .Append(req.QueryString)
                            .ToString(),
                protocol = req.Scheme,
                host = new string[] { req.Host.Host },
                port = req.Host.Port,
                path = req.Path.ToString().Split('/').Where(i => !string.IsNullOrEmpty(i)),
                query = querys
            });
            #endregion

            #region prepare body and wrapped as postman format

            //if (bodyType != null)
            //    requestInfos.Add("body", new
            //    {
            //        mode = "raw",
            //        raw = await req.GetBodyObjectAsync(bodyType)
            //    }); 
            var bodyJsonString = await req.GetBodyStringAsync();
            var wrapObject_WithBody = new
            {
                mode = "raw",
                raw = bodyJsonString
            };
            var wrapObject_WithoutBody = new
            {
                mode = "raw",
                raw = "please put request body here."
            };
            //var body_limit_length = 5000;//reffer to db_varchar_max is 8000


            #endregion

            // due to the body may be too long to store in db
            // default put empty wrapped body to requestInfos
            requestInfos.Add("body", wrapObject_WithoutBody);
            var without_Body_Object = new
            {
                name = functionName,
                request = requestInfos
            };
            var without_Body_JsonString = JsonConvert.SerializeObject(without_Body_Object);

            requestInfos["body"] = wrapObject_WithBody;
            var with_Body_Object = new
            {
                name = functionName,
                request = requestInfos
            };
            var with_Body_JsonString = JsonConvert.SerializeObject(with_Body_Object);

            #region final object which will be written to log center 
            requestInfos["body"] = wrapObject_WithoutBody;
            var result = new Dictionary<string, object>();
            result.Add("request name", functionName);
            result.Add("request ", requestInfos);
            result.Add("request body", await req.GetBodyStringAsync());
            result.Add("request header", allHeaders);

            result.Add("postman request with body", with_Body_JsonString);
            result.Add("postman request without body", without_Body_JsonString);
            #endregion
            return result;
        }

    }
}
