using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    /// <summary>
    /// override HttpResponse
    /// </summary>
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// override HttpResponse
        /// </summary>
        /// <param name="response"></param>
        /// <param name="data">the information which will be displayed to requester</param>
        /// <param name="statusCode">HttpStatusCode</param>
        /// <returns></returns>
        public async static Task Output(this HttpResponse response, object data, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            response.ContentType = "application/json;charset=utf-8;";
            response.StatusCode = (int)statusCode;
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            response.ContentLength = bytes.Length;
            await response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
        public static async Task<T> GetBodyObjectAsync<T>(this JsonNetResponse<T> response) where T : class
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);

        }
    }
}
