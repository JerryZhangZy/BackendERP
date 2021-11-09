using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class WMSShipmentClient : ApiClientBase<List<WmsOrderShipmentPayload>>
    {
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were not config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        public WMSShipmentClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        {

        }
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="authCode"></param>
        public WMSShipmentClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        /// <summary>
        /// Add single shipment
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<bool> AddShipmentAsync(int masterAccountNum, int profileNum, InputOrderShipmentType shipment)
        {
            if (shipment is null)
            {
                AddError("shipment cann't be empty.");
                return false;
            }
            var shipments = new List<InputOrderShipmentType>() { shipment };
            return await AddShipmentListAsync(masterAccountNum, profileNum, shipments);
        }

        /// <summary>
        /// Add multiple shipment
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<bool> AddShipmentListAsync(int masterAccountNum, int profileNum, IList<InputOrderShipmentType> shipments)
        {
            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }

            if (shipments is null || shipments.Count == 0)
            {
                AddError("shipments cann't be empty.");
                return false;
            }

            return await PostAsync(shipments, FunctionUrl.AddShipments);
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
