﻿using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// WMS process downloaded purchase order, then send the process result back to erp
    /// </summary>
    public class WMSAckProcessPurchaseOrderClient : ApiClientBase<AcknowledgeProcessPayload>
    {
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were not config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        public WMSAckProcessPurchaseOrderClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        {

        }
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="authCode"></param>
        public WMSAckProcessPurchaseOrderClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        /// <summary>
        /// Wms transfer PurchaseOrder then ack process result to erp.
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="requestPayload"></param>
        /// <returns></returns>
        public async Task<bool> AckProcessPurchaseOrdersAsync(int masterAccountNum, int profileNum, IList<ProcessResult> processResults)
        {
            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }

            var requestPayload = new AcknowledgeProcessPayload()
            {
                ProcessResults = processResults
            };

            return await PostAsync(requestPayload, FunctionUrl.AckProcessPurchaseOrders);
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
