using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.Base.Utility
{
    public class HttpRequestUtil
    {
        public static string Call(string url, string authcode = null, object postData = null, Dictionary<string, string> headers = null, string method = "POST", string contentType = "application/json", int timeOut = 300000)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(authcode))
            {
                url += $"?code={authcode}";
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = contentType;
            request.Method = method;
            request.Timeout = timeOut;
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }

            if (postData != null)
            {
                var jsonData = JsonConvert.SerializeObject(postData);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8))
            {
                result = streamReader.ReadToEnd();
            }
            response.Close();
            return result;
        }

        public static async Task<string> CallAsync(string url, string authcode = null, object postData = null, Dictionary<string, string> headers = null, string method = "POST", string contentType = "application/json", int timeOut = 300000)
        {

            string result = string.Empty;

            if (!string.IsNullOrEmpty(authcode))
            {
                url += $"?code={authcode}";
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = contentType;
            request.Method = method;
            request.Timeout = timeOut;
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
            if (postData != null)
            {
                var jsonData = JsonConvert.SerializeObject(postData);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    await streamWriter.WriteAsync(jsonData);
                    await streamWriter.FlushAsync();
                    streamWriter.Close();
                }
            }

            var response = (HttpWebResponse)(await request.GetResponseAsync());
            using (var streamReader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8))
            {
                result = await streamReader.ReadToEndAsync();
            }
            response.Close();
            return result;
        }
    }
}
