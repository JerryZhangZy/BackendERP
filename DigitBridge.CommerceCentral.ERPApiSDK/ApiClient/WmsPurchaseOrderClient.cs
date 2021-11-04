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
    public class WmsPurchaseOrderClient : ApiClientBase<WmsPurchaseOrderPayload>
    {
        public WmsPurchaseOrderClient() : base(ConfigUtil.Api_BaseUrl, ConfigUtil.Api_AuthCode)
        { }
        public WmsPurchaseOrderClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        public IList<PoHeader> Data { get; set; }
        
        public int ResultTotalCount { get; set; }

        public async Task<bool> QueryWmsPurchaseOrderListAsync(WmsQueryModel query)
        {
            var payload = new WmsPurchaseOrderPayload()
            {
                Filter = new Dictionary<string, object>()
                {
                    { "UpdateDateFrom", query.UpdateDateFrom },
                    { "UpdateDateTo", query.UpdateDateTo }
                }
            };
            headers["MasterAccountNum"] = query.MasterAccountNum.ToString();
            headers["ProfileNum"] = query.ProfileNum.ToString();
            return await PostAsync(payload);
        }

        protected override async Task<bool> AnalysisResponseAsync(string responseData)
        {
            if (ResopneData == null)
            {
                AddError("Call event api has no resopne.");
                return false;
            }
            if (ResopneData.PurchaseOrderList == null)
            {
                //Maybe the api throw exception.
                var exception = JsonConvert.DeserializeObject<Exception>(responseData, jsonSerializerSettings);
                if (exception != null)
                    AddError(exception.ObjectToString());
                return false;
            }
            Data = ResopneData.PurchaseOrderList;
            ResultTotalCount = ResopneData.PurchaseOrderListCount;
            
            var success = ResopneData.Success;
            if (!success)
            {
                this.Messages = this.Messages.Concat(ResopneData.Messages).ToList();
            }
            return success;
        }
    }
}
