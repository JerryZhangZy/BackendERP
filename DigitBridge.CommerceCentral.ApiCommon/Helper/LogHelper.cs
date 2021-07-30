using Microsoft.AspNetCore.Http;
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
        public static async Task<Dictionary<string, object>> GetRequestInfo(HttpRequest req, string name)
        {
            var requestInfos = new Dictionary<string, object>();
            requestInfos.Add("method", req.Method);

            #region header  
            var headers = new List<object>();
            foreach (var itemkey in req.Headers.Keys)
            {
                headers.Add(new
                {
                    key = itemkey,
                    value = req.Headers[itemkey].ToString(),
                    type = "text"
                });
            }
            requestInfos.Add("header", headers);
            #endregion

            #region url and query
            var querys = new List<object>();
            foreach (var itemkey in req.Query.Keys)
            {
                querys.Add(new
                {
                    key = itemkey,
                    value = req.Query[itemkey].ToString()
                });
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

            #region body

            //if (bodyType != null)
            //    requestInfos.Add("body", new
            //    {
            //        mode = "raw",
            //        raw = await req.GetBodyObjectAsync(bodyType)
            //    }); 

            requestInfos.Add("body", new
            {
                mode = "raw",
                raw = "please put request body here."
            });

            #endregion

            #region final object which will be written to log center 
            var result = new Dictionary<string, object>();
            result.Add("name", name);
            result.Add("request", requestInfos);
            // due to the body may be too long to store in db
            var postman_request_no_body = JsonConvert.SerializeObject(result);
            result.Add("postman request", postman_request_no_body);
            result.Add("request body", await req.GetBodyStringAsync());
            #endregion
            return result;
        }

    }
}
