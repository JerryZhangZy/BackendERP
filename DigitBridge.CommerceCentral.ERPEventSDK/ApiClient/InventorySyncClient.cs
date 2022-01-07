using DigitBridge.CommerceCentral.ERPApiSDK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK.ApiClient
{
    public class InventorySyncClient  : ErpApiClient
    {

        public InventorySyncClient() : base()
        {
           
        }
        public InventorySyncClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        public async Task<bool> InventoryDataAsync(InventorySyncUpdateDataDto payload)
        {
            headers.Add("masterAccountNum",payload.MasterAccountNum.ToString());
            headers.Add("profileNum", payload.ProfileNum.ToString());
            return await PostAsync<InventorySyncUpdatePayload> (new InventorySyncUpdatePayload() { InventorySyncData=payload }, ERPApiFunctionUrl.InventorySync);
        }
        
    }
}
