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
    /// WSM upload purchase order received items to erp.
    /// </summary>
    public class WMSPoReceiveClient : ApiClientBase<IList<WMSPoReceivePayload>>
    {
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were not config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        public WMSPoReceiveClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        {

        }
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="authCode"></param>
        public WMSPoReceiveClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        /// <summary>
        /// WMS post po receive items back to erp.
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="PurchaseOrderUuids"></param>
        /// <returns></returns>
        public async Task<bool> ReceiveAsync(int masterAccountNum, int profileNum, IList<WMSPoReceiveItem> receiveItems)
        {
            if (receiveItems == null || receiveItems.Count == 0)
            {
                AddError("receiveItems cann't  be empty");
            }
            //When PoItemUuid is zero means the item is new one.
            //if (receiveItems.Count(i => i.PoItemUuid.IsZero()) > 0)
            //{
            //    AddError("PoItemUuid cann't  be empty");
            //}

            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }

            return await PostAsync(receiveItems, FunctionUrl.PoReceive);
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

            var errorResult = ResopneData.Where(i => !i.Success);
            var success = errorResult.Count() == 0;
            if (!success)
                this.Messages.Add(errorResult.SelectMany(j => j.Messages).ToList());

            return success;
        }
    }
}
