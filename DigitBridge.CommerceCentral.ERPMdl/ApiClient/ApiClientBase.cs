using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json.Linq;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public abstract class ApiClientBase
    {
        #region Member Variables

        // Cast & Crew-specific Data Service JWT client
        protected readonly string userName;
        protected readonly string password;
        protected readonly TimeSpan timeOut;
        protected readonly string endPoint;
        protected readonly bool useProxy;
        protected readonly CookieContainer cookieContainer = null;

        // Request to Payroll timeout settings - In minutes
        private const int requestTimeoutInMins = 5;

        #endregion Member Variables

        #region Constructor

        public ApiClientBase(string username, string pwd, TimeSpan timeout, string endpoint, bool useproxy = false)
        {
            userName = username;
            password = pwd;
            timeOut = timeout;
            endPoint = endpoint;
            useProxy = useproxy;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        #endregion Constructor

        #region Properties

        protected string ClientType => this.GetType().Name;
        protected long TokenTTL => (long)TimeSpan.FromMinutes(requestTimeoutInMins).TotalMilliseconds;

        #endregion Properties

        #region Abstract Methods and Properties

        protected abstract string UriLogin { get; }

        protected abstract Task<string> GetTokenAsync();

        #endregion Abstract Methods and Properties

        #region Virtual Methods - Public

        /// <summary>
        /// Sends HTTP post request
        /// </summary>
        /// <param name="function">Partial url path</param>
        /// <param name="data">data request</param>
        /// <param name="requiresToken">Requires token ?</param>
        /// <param name="bLogin">Is login request?</param>
        /// <returns>Returns HTTP response</returns>
        public virtual async Task<ApiReturnData> PostAsync(string function, JObject data, bool requiresToken = true, bool bLogin = false)
        {
            using var httpClient = await CreateHttpClientAsync(requiresToken);
            var content = GetHttpContent(data);
            var response = await httpClient.PostAsync(BuildRequestUri(function), content);

            // Log it to elastic search
            (LogDetails logDetails, bool isSuccess) = await GenerateLogMessageAsync(RequestType.Post, response, data, bLogin);
            if (!isSuccess)
                return new ApiReturnData(default, logDetails, false, response);

            JObject res = null;
            try
            {
                // Deserialize response
                res = Deserialize<JObject>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    FloatParseHandling = FloatParseHandling.Decimal
                });
            }
            catch (Exception ex)
            {
                logDetails.Message = $"HTG response > PostAsync > Could not deserialize the response. {ex.Message}";
                isSuccess = false;
            }

            return new ApiReturnData(res, logDetails, (!response.IsSuccessStatusCode || !isSuccess), response);
        }
        public virtual async Task<ApiReturnData> PostAsync(string function, JArray data, bool requiresToken = true, bool bLogin = false)
        {//TODO: HTG-7286
            using var httpClient = await CreateHttpClientAsync(requiresToken);
            var content = GetHttpContent(data);
            var response = await httpClient.PostAsync(BuildRequestUri(function), content);

            // Log it to elastic search
            (LogDetails logDetails, bool isSuccess) = await GenerateLogMessageAsync(RequestType.Post, response, data, bLogin);
            if (!isSuccess)
                return new ApiReturnData(default, logDetails, false, response);

            JObject res = null;
            try
            {
                // Deserialize response
                res = Deserialize<JObject>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    FloatParseHandling = FloatParseHandling.Decimal
                });
            }
            catch (Exception ex)
            {
                logDetails.Message = $"HTG response > PostAsync > Could not deserialize the response. {ex.Message}";
                isSuccess = false;
            }

            return new ApiReturnData(res, logDetails, (!response.IsSuccessStatusCode || !isSuccess), response);
        }

        /// <summary>
        /// Sends HTTP patch request
        /// </summary>
        /// <param name="function">Partial url path</param>
        /// <param name="data">data request</param>
        /// <param name="requiresToken">Requires token ?</param>
        /// <param name="bLogin">Is login request?</param>
        /// <returns>Returns HTTP response</returns>
        public virtual async Task<ApiReturnData> PatchAsync(string function, JObject data, bool requiresToken = true, bool bLogin = false)
        {
            using var httpClient = await CreateHttpClientAsync(requiresToken);
            var content = GetHttpContent(data);
            var response = await httpClient.PatchAsync(BuildRequestUri(function), content);

            // Log it to elastic search
            (LogDetails logDetails, bool isSuccess) = await GenerateLogMessageAsync(RequestType.Patch, response, data, bLogin);
            if (!isSuccess)
                return new ApiReturnData(default, logDetails, false, response);

            JObject res = null;

            try
            {
                // Deserialize response
                res = Deserialize<JObject>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                logDetails.Message = $"HTG response > PatchAsync > Could not deserialize the response. {ex.Message}";
                isSuccess = false;
            }

            return new ApiReturnData(res, logDetails, (!response.IsSuccessStatusCode || !isSuccess), response);
        }

        public virtual async Task<ApiReturnData> DeleteAsync(string function, JObject data, bool requiresToken = true, bool bLogin = false)
        {
            using var httpClient = await CreateHttpClientAsync(requiresToken);
            var request = new HttpRequestMessage(HttpMethod.Delete, BuildRequestUri(function));
            request.Content = GetHttpContent(data);
            var response = await httpClient.SendAsync(request);

            // Log it to elastic search
            (LogDetails logDetails, bool isSuccess) = await GenerateLogMessageAsync(RequestType.Delete, response, data, bLogin);
            if (!isSuccess)
                return new ApiReturnData(default, logDetails, false, response);
            JObject res = null;

            try
            {
                // Deserialize response
                res = Deserialize<JObject>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                logDetails.Message = $"HTG response > SendAsync > Could not deserialize the response. {ex.Message}";
                isSuccess = false;
            }

            return new ApiReturnData(res, logDetails, (!response.IsSuccessStatusCode || !isSuccess), response);
        }

        public virtual void LogAndThrow(LogDetails logDetails, string title)
        {
            Logger.Logger.Instance.Error(li =>
            {
                li.Type = title;
                li.Message = $"{title}. {logDetails.Message}.";
                li.Data.request = logDetails.Request;
                li.Data.response = logDetails.Response;
            });

            throw new ErrorPrompt(logDetails?.Message);
        }

        #endregion Virtual Methods - Public

        #region Virtual Methods - Protected

        /// <summary>
        /// Creates HTTP content
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">data request</param>
        /// <returns></returns>
        protected virtual HttpContent GetHttpContent<T>(T data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Retrieves token after successful logon
        /// </summary>
        /// <returns>Token</returns>
        protected virtual async Task<string> LoginAsync()
        {
            var data = new JObject { { "username", userName }, { "password", password } };
            var result = await PostAsync(UriLogin, data, false, true);
            if (result.IsError)
                LogAndThrow(result.Log, $"{ClientType} login failed.");

            string token = result.Data?.Value<string>("token");
            if (string.IsNullOrEmpty(token))
                LogAndThrow(result.Log, $"{ClientType} login failed. Failed to get token.");

            //Logger.Logger.Instance.Info(li =>
            //{
            //    li.Type = $"Login to {ClientType}";
            //    li.Message = $"{result.Log?.Message} Payroll Token";
            //    li.Data.request = result.Log?.Request;
            //    li.Data.response = result.Log?.Response;
            //});

            return token;
        }

        /// <summary>
        /// Deserialize string to T object
        /// </summary>
        /// <typeparam name="T">Data type to be de-serialized into</typeparam>
        /// <param name="content">string content</param>
        /// <returns>data object</returns>
        protected virtual T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        protected virtual T Deserialize<T>(string content, JsonSerializerSettings setting)
        {
            return JsonConvert.DeserializeObject<T>(content, setting);
        }

        /// <summary>
        /// Builds URI for HTTP request
        /// </summary>
        /// <param name="function">Partial URI</param>
        /// <returns>Returns full URI</returns>
        protected virtual string BuildRequestUri(string function)
        {
            return string.Format(endPoint, function);
        }

        /// <summary>
        /// Creates HTTP client
        /// </summary>
        /// <param name="requiresToken">Require token ?</param>
        /// <returns>Returns HTTP Client</returns>
        protected virtual async Task<HttpClient> CreateHttpClientAsync(bool requiresToken)
        {
            if (string.IsNullOrEmpty(endPoint))
                throw new ErrorPrompt($"{ClientType} not configured with endpoint");

            var httpClientHandler = new HttpClientHandler();
            if (cookieContainer != null)
            {
                httpClientHandler.CookieContainer = cookieContainer;
                httpClientHandler.UseCookies = true;
            }

            var httpClient = new HttpClient(httpClientHandler) { Timeout = timeOut };
            if (useProxy)
            {
                httpClientHandler.UseDefaultCredentials = true;
                httpClientHandler.UseProxy = true;
            }

            if (requiresToken)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync());

            return httpClient;
        }


        #endregion Virtual Methods - Protected
    }
}