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
    /// <summary>
    /// WMS upload inventory instock to ERP
    /// </summary>
    public class WMSInventorySyncClient   : ApiClientBase<ResponsePayloadBase>
    {
        /// <summary>
        /// WSM upload inventory instock to erp
        /// </summary>
        public WMSInventorySyncClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        {

        }
        public WMSInventorySyncClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        public async Task<bool> UpdateStockAsync(int masterAccountNum, int profileNum, IList<InventorySyncItemsModel> inventorySyncItems)
        {

            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }
          
            return await PostAsync(inventorySyncItems, FunctionUrl.InventorySync);
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
 
            var success = ResopneData.Success;
            if (!success)
            {
                this.Messages = this.Messages.Concat(ResopneData.Messages).ToList();
            }
            return success;
        }
    }
}
