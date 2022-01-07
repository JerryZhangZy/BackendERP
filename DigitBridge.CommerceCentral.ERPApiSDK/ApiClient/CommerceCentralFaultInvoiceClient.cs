using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class CommerceCentralFaultInvoiceClient : ApiClientBase<List<FaultInvoiceResponsePayload>>
    {
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were not config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        public CommerceCentralFaultInvoiceClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        {

        }
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="authCode"></param>
        public CommerceCentralFaultInvoiceClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        /// <summary>
        /// update single fault invoice
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<bool> UpdateFaultInvoiceAsync(int masterAccountNum, int profileNum, FaultInvoiceRequestPayload faultInvoice)
        {
            if (faultInvoice is null)
            {
                AddError("faultInvoice cann't be empty.");
                return false;
            }
            var faultInvoiceList = new List<FaultInvoiceRequestPayload>() { faultInvoice };
            return await UpdateFaultInvoiceListAsync(masterAccountNum, profileNum, faultInvoiceList);
        }

        /// <summary>
        /// update multiple fault invoice
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<bool> UpdateFaultInvoiceListAsync(int masterAccountNum, int profileNum, IList<FaultInvoiceRequestPayload> faultInvoiceList)
        {
            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }

            if (faultInvoiceList is null || faultInvoiceList.Count == 0)
            {
                AddError("FaultInvoiceRequestPayload cann't be empty.");
                return false;
            }

            return await PatchAsync(faultInvoiceList, FunctionUrl.UpdateFaultInvoiceList);
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
