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


        /// <summary>
        /// Query open s/o.
        /// When requestPayload is null,the default setting will be applied to query.
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="requestPayload"></param>
        /// <returns></returns>
        public async Task<bool> GetSalesOrdersOpenListAsync(int masterAccountNum, int profileNum, WMSSalesOrderRequestPayload requestPayload = null)
        {
            if (!SetHeader(masterAccountNum, profileNum))
            {
                return false;
            }

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
                this.Messages.Add(ResopneData.Messages);
            }

            return ResopneData.Success;
        }
    }
}
