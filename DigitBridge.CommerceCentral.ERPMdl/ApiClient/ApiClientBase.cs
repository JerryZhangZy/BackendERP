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
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public abstract class ApiClientBase<T> : IMessage
    {
        #region Member Variables

        // Cast & Crew-specific Data Service JWT client
        //protected readonly string userName;
        //protected readonly string password;
        protected readonly CookieContainer cookieContainer = null;

        protected readonly TimeSpan timeOut;
        protected readonly string endPoint;
        protected readonly bool useProxy;
        // Request to Payroll timeout settings - In minutes
        private const int requestTimeoutInMins = 5;
        protected readonly string authCode;

        protected readonly JsonSerializerSettings jsonSerializerSettings;

        protected Dictionary<string, string> headers;

        protected T ResopneData;

        #endregion Member Variables

        #region Constructor

        public ApiClientBase(string endpoint, string authCode = null, int timeoutInMins = requestTimeoutInMins, bool useproxy = false, JsonSerializerSettings jsonSerializerSettings = null, Dictionary<string, string> headers = null)
        {
            this.endPoint = endpoint;
            this.authCode = authCode;
            this.timeOut = TimeSpan.FromMinutes(timeoutInMins);
            this.useProxy = useproxy;

            this.jsonSerializerSettings = jsonSerializerSettings;
            if (this.jsonSerializerSettings == null)
            {
                this.jsonSerializerSettings = new JsonSerializerSettings
                {
                    FloatParseHandling = FloatParseHandling.Decimal
                };
            }

            this.headers = headers;
            if (this.headers == null)
                this.headers = new Dictionary<string, string>();
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        #endregion Constructor

        #region Properties

        //protected string ClientType => this.GetType().Name;
        //protected long TokenTTL => (long)TimeSpan.FromMinutes(requestTimeoutInMins).TotalMilliseconds;

        #endregion Properties

        #region Abstract Methods and Properties 

        //protected abstract Task<string> GetTokenAsync();

        protected abstract Task<bool> AnalysisResponseAsync(string responseData);

        #endregion Abstract Methods and Properties

        #region Virtual Methods - Public

        /// <summary>
        /// Sends HTTP post request
        /// </summary>
        /// <param name="function">Partial url path</param>
        /// <param name="data">data request</param>
        /// <param name="requiresToken">Requires token ?</param> 
        /// <returns>Returns HTTP response</returns>
        public virtual async Task<bool> PostAsync(object data, string function = null)
        {
            var success = true;
            try
            {
                using var httpClient = await CreateHttpClientAsync();

                var content = GetHttpContent(data);

                var response = await httpClient.PostAsync(BuildRequestUri(function), content);
                var responseData = await response.Content.ReadAsStringAsync();

                // Deserialize response
                ResopneData = JsonConvert.DeserializeObject<T>(responseData, jsonSerializerSettings);

                success = await AnalysisResponseAsync(responseData);
            }
            catch (Exception ex)
            {
                success = false;
                AddError($"ApiClientBase->PostAsync error: {ex.Message}");
            }

            return success;
        }

        /// <summary>
        /// Sends HTTP patch request
        /// </summary>
        /// <param name="function">Partial url path</param>
        /// <param name="data">data request</param>
        /// <param name="requiresToken">Requires token ?</param>
        /// <param name="bLogin">Is login request?</param>
        /// <returns>Returns HTTP response</returns>
        public virtual async Task<bool> PatchAsync(object data, string function = null)
        {
            var success = true;
            try
            {
                using var httpClient = await CreateHttpClientAsync();

                var content = GetHttpContent(data);

                var response = await httpClient.PatchAsync(BuildRequestUri(function), content);
                var responseData = await response.Content.ReadAsStringAsync();

                // Deserialize response
                ResopneData = JsonConvert.DeserializeObject<T>(responseData, jsonSerializerSettings);

                success = await AnalysisResponseAsync(responseData);
            }
            catch (Exception ex)
            {
                success = false;
                AddError($"ApiClientBase->PatchAsync error: {ex.Message}");
            }

            return success;
        }

        public virtual async Task<bool> DeleteAsync(object data, string function = null)
        {
            var success = true;
            try
            {
                using var httpClient = await CreateHttpClientAsync();

                var request = new HttpRequestMessage(HttpMethod.Delete, BuildRequestUri(function));
                request.Content = GetHttpContent(data);

                var response = await httpClient.SendAsync(request);
                var responseData = await response.Content.ReadAsStringAsync();

                // Deserialize response
                ResopneData = JsonConvert.DeserializeObject<T>(responseData, jsonSerializerSettings);

                success = await AnalysisResponseAsync(responseData);
            }
            catch (Exception ex)
            {
                success = false;
                AddError($"ApiClientBase->DeleteAsync error: {ex.Message}");
            }

            return success;
        }

        #endregion Virtual Methods - Public

        #region Virtual Methods - Protected

        /// <summary>
        /// Creates HTTP content
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">data request</param>
        /// <returns></returns>
        protected virtual HttpContent GetHttpContent(object data = null)
        {
            var content = data == null ? string.Empty : JsonConvert.SerializeObject(data);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
        /// <summary>
        /// Builds URI for HTTP request
        /// </summary>
        /// <param name="function">Partial URI</param>
        /// <returns>Returns full URI</returns>
        protected virtual string BuildRequestUri(string function)
        {
            var requestUrl = function.IsZero() ? endPoint : endPoint + function;
            return authCode.IsZero() ? requestUrl : requestUrl + $"?code={authCode}";
        }

        /// <summary>
        /// Creates HTTP client
        /// </summary>
        /// <param name="requiresToken">Require token ?</param>
        /// <returns>Returns HTTP Client</returns>
        protected virtual async Task<HttpClient> CreateHttpClientAsync()
        {
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
            //if (requiresToken)
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync());

            foreach (var header in headers)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            return httpClient;
        }
        #endregion Virtual Methods - Protected

        #region Messages
        protected IList<MessageClass> _messages;
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (_messages is null)
                    _messages = new List<MessageClass>();
                return _messages;
            }
            set { _messages = value; }
        }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             Messages.Add(message, MessageLevel.Info, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Warning, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Error, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Fatal, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Debug, code);

        #endregion Messages
    }
}