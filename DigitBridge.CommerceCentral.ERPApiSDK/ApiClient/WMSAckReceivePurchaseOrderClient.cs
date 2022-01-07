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
    /// WMS download purchase order from erp, then send succeed downloaded purchaseOrderUuids back to erp.
    /// </summary>
    public class WMSAckReceivePurchaseOrderClient : ApiClientBase<AcknowledgePayload>
    {
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were not config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        public WMSAckReceivePurchaseOrderClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        {

        }
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="authCode"></param>
        public WMSAckReceivePurchaseOrderClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        /// <summary>
        /// Wms data downloaded then send ack to erp.
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="PurchaseOrderUuids"></param>
        /// <returns></returns>
        public async Task<bool> AckReceivePurchaseOrdersAsync(int masterAccountNum, int profileNum, IList<string> purchaseOrderUuids)
        {
            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }
            var payload = new AcknowledgePayload()
            {
                ProcessUuids = purchaseOrderUuids
            };
            return await PostAsync(payload, FunctionUrl.AckReceivePurchaseOrders);
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
