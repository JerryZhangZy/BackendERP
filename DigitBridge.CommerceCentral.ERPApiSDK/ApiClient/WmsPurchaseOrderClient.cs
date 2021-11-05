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
        public WmsPurchaseOrderClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        { }
        public WmsPurchaseOrderClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        public IList<PoHeader> Data { get; set; }
        
        public int ResultTotalCount { get; set; }

        public async Task<bool> QueryWmsPurchaseOrderListAsync(WmsQueryModel query)
        {
            if (query.MasterAccountNum.IsZero())
            {
                AddError("MasterAccountNum is invalid.");
                return false;
            }
            if (query.ProfileNum.IsZero())
            {
                AddError("ProfileNum is invalid.");
                return false;
            }
            var payload = new WmsPurchaseOrderPayload()
            {
                Filter = new Dictionary<string, object>()
                {
                    { "UpdateDateFrom", query.UpdateDateFrom },
                    { "UpdateDateTo", query.UpdateDateTo }
                }
            };
            MasterAccountNum = query.MasterAccountNum;
            ProfileNum = query.ProfileNum;
            return await PostAsync(payload,FunctionUrl.GetPurchaseOrderList);
        }
        
        public async Task<bool> CreatePoReceiveAsync(int masterAccountNum,int profileNum,PoHeader receive)
        {
            if (masterAccountNum.IsZero())
            {
                AddError("MasterAccountNum is invalid.");
                return false;
            }
            if (profileNum.IsZero())
            {
                AddError("ProfileNum is invalid.");
                return false;
            }
            var payload = new WmsPurchaseOrderPayload()
            {
                PoTransaction = new PoTransactionDataDto(receive)
            };
            MasterAccountNum = masterAccountNum;
            ProfileNum = profileNum;
            return await PostAsync(payload,FunctionUrl.CreatePoReceive);
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
