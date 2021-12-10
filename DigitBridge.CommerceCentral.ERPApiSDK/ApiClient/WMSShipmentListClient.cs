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
    /// ERP providing shipment (which uploaded by WMS) transferred result for WMS to download.
    /// </summary>
    public class WMSShipmentListClient : ApiClientBase<WmsOrderShipmentListPayload>
    {
        public IList<WMSShipmentProcess> Data { get; set; }
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were not config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        public WMSShipmentListClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        {

        }
        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="authCode"></param>
        public WMSShipmentListClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        /// <summary>
        /// Get wms shipment list by array of shipmentid
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<bool> GetWMSOrderShipmentListAsync(int masterAccountNum, int profileNum, IList<string> shipmentIDs)
        {
            if (shipmentIDs is null || shipmentIDs.Count == 0)
            {
                AddError("shipmentIDs cann't be empty.");
                return false;
            }

            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }

            return await PostAsync(shipmentIDs, FunctionUrl.WMSShipmentList);
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

            if (!ResopneData.Success)
            {
                this.Messages.Add(ResopneData.Messages);
            }
            else
            {
                Data = ResopneData.WMSShipmentProcessesList;
            }
            return ResopneData.Success;
        }
    }
}
