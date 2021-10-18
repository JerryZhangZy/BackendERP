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
    public class ApiReturnData
    {
        public JObject Data { get; set; }
        public LogDetails Log { get; set; }
        public bool IsError { get; set; }

        public ApiReturnData(JObject data, LogDetails log, bool isError, HttpResponseMessage httpResponse)
        {
            Data = data;
            Log = log;
            IsError = isError;

            // Any errors in response??
            string errorMessage = ErrorInResponse(httpResponse);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                IsError = true;
                log.Message = errorMessage;
            }
        }

        /// <summary>
        /// Checks if there is any error in response.
        ///
        /// Response may or may not have an ImportResult in its Contents
        /// Even if it does, response will have a StatusCode of 200 so will succeed in the next call
        /// </summary>
        /// <param name="response">HTTP response message</param>
        /// <returns>Returns error message, if any</returns>
        private string ErrorInResponse(HttpResponseMessage response)
        {
            try
            {
                var responseResult = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
                var statusResult = responseResult?.GetValue<JObject>("status");
                if (statusResult == null)
                    return null;

                return GetErrorMessage(statusResult);
            }
            catch (Exception)
            {
            }
            return null;
        }

        private string GetErrorMessage(JObject statusResult)
        {
            JArray validationMessages = statusResult?.GetValue<JArray>("validationMessages");
            if (validationMessages?.Count() > 0)
            {
                StringBuilder builder = new StringBuilder();
                validationMessages.Each(message =>
                {
                    message["messages"]?.Each(validation =>
                    {
                        builder.Append(validation);
                    });
                });
                return builder.ToString();
            }
            return statusResult?.GetValue<string>("message");
        }
    }
}