﻿using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPApiSDK.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class ErpInventorySyncClient : ApiClientBase<ResponsePayloadBase>
    {

        public ErpInventorySyncClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        { }
        public ErpInventorySyncClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        public async Task<bool> InventorySyncAsync(int masterAccountNum, int profileNum, InventorySyncUpdatePayload payload)
        {
            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }
            return await PostAsync(payload, "commercecentral/inventory");
        }

        protected override async Task<bool> AnalysisResponseAsync(string responseData)
        {
            if (ResopneData == null)
            {
                AddError("Call event api has no resopne.");
                return false;
            }

            var success = ResopneData.Success;
            if (!success)
            {
                this.Messages = this.Messages.Concat(ResopneData.Messages).ToList();
            }
            return success;
        }

    }
}
