using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPApiSDK.Model;
using DigitBridge.CommerceCentral.ERPEventSDK;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK.ApiClient
{
 
    public class ErpApiClient : ApiClientBase<ERPApiPayload>
    {
        public ErpApiClient() : base(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode)
        { }
        public ErpApiClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }
 
        protected async Task<bool> PostAsync<T>(T dto, string functionUrl)
        {
           
            return await base.PostAsync(dto, functionUrl);
        }

 

        protected override async Task<bool> AnalysisResponseAsync(string responseData)
        {
            if (ResopneData == null)
            {
                AddError("Call event api has no resopne.");
                return false;
            }
            //if (ResopneData.EventERP == null)
            //{
                //Maybe the api throw exception.
                var exception = JsonConvert.DeserializeObject<Exception>(responseData, jsonSerializerSettings);
                if (exception != null)
                    AddError(exception.ObjectToString());
                return false;
            //}
            //Data = ResopneData.EventERP.Event_ERP;

            var success = ResopneData.Success;
            if (!success)
            {
                this.Messages = this.Messages.Concat(ResopneData.Messages).ToList();
            }
            return success;
        }
    }
}
