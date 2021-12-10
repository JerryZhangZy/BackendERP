using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Commerce central download unprocess invoice from erp, then send succeed downloaded invoiceuuids back to erp.
    /// </summary>
    public class CommerceCentralAckReceiveInvoiceClient : ApiClientBase<AcknowledgePayload>
    {
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were not config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        public CommerceCentralAckReceiveInvoiceClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        {

        }
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="authCode"></param>
        public CommerceCentralAckReceiveInvoiceClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        /// <summary>
        /// Commerce central download unprocess invoice, then ack this batch invoice downloaded succeeded.
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="InvoiceUuids"></param>
        /// <returns></returns>
        public async Task<bool> AckReceiveInvoicesAsync(int masterAccountNum, int profileNum, IList<string> InvoiceUuids)
        {
            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }
            var payload = new AcknowledgePayload()
            {
                ProcessUuids = InvoiceUuids
            };
            return await PostAsync(payload, FunctionUrl.AckReceiveInvoices);
        }

        protected override async Task<bool> AnalysisResponseAsync(string responseData)
        {
            if (ResopneData == null)
            {
                AddError(responseData);

                //Maybe the api throw exception.
                //var exception = JsonConvert.DeserializeObject<Exception>(responseData, jsonSerializerSettings);
                //if (exception != null)
                //    AddError(exception.ObjectToString());
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
