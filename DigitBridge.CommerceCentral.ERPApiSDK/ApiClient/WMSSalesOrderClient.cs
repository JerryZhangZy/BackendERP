using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class WMSSalesOrderClient : ApiClientBase<WMSSalesOrderResponsePayload>
    {

        public WMSSalesOrderClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_BaseUrl)
        {

        }
        public WMSSalesOrderClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> GetSalesOrdersOpenList(WMSSalesOrderRequestPayload requestPayload)
        {
            if (!requestPayload.HasMasterAccountNum)
            {
                AddError("MasterAccountNum is invalid.");
                return false;
            }
            if (!requestPayload.HasProfileNum)
            {
                AddError("ProfileNum is invalid.");
                return false;
            }
            MasterAccountNum = requestPayload.MasterAccountNum;
            ProfileNum = requestPayload.ProfileNum;
            return await PostAsync(requestPayload, FunctionUrl.GetSalesOrderOpenList);
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

            if (!ResopneData.Success)
            {
                this.Messages = this.Messages.Concat(ResopneData.Messages).ToList();
            }

            return ResopneData.Success;
        }
    }
}
