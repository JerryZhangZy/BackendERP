using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPApiSDK.Model;
using DigitBridge.CommerceCentral.ERPApiSDK.Model.Payload;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK.ApiClient
{
    public class WMSInventorySyncClient   : ApiClientBase<ResponsePayloadBase>
    {

        public WMSInventorySyncClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_BaseUrl)
        {

        }
        public WMSInventorySyncClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        public async Task<bool> InventoryDataAsync(WMSInventorySyncModel  wMSInventorySyncModel)
        {
            this.MasterAccountNum = wMSInventorySyncModel.MasterAccountNum;
            this.ProfileNum = wMSInventorySyncModel.ProfileNum;
          
            return await PostAsync(new WMSInventorySyncPayload() { InventorySyncData = wMSInventorySyncModel }, FunctionUrl.InventorySync);
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
