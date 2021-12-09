using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Resend existing event to queue.
    /// </summary>
    public class ErpEventResendClient : ApiClientBase<ErpEventResendResponsePayload>
    {
        public ErpEventResendClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        { }
        public ErpEventResendClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }  

        /// <summary>
        /// resend array event uuids.
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="eventUuids"></param>
        /// <returns></returns>
        public async Task<bool> ResendEventAsync(int masterAccountNum, int profileNum, IList<string> eventUuids)
        {
            if (eventUuids == null || eventUuids.Count == 0)
            {
                AddError("EventUuids cann't be empty.");
                return false;
            }

            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }

            var data = new { EventUuids = eventUuids };

            return await PostAsync(data, FunctionUrl.ResendEvent);
        }

        protected override async Task<bool> AnalysisResponseAsync(string responseData)
        {
            if (ResopneData == null)
            {
                //Maybe the api throw exception.
                var exception = JsonConvert.DeserializeObject<Exception>(responseData, jsonSerializerSettings);
                if (exception != null)
                    AddError(exception.ObjectToString());
                return false;
            }

            if (!ResopneData.Success)
            {
                this.Messages.Add(ResopneData.Messages);
            }

            return ResopneData.Success;
        }
    }
}
