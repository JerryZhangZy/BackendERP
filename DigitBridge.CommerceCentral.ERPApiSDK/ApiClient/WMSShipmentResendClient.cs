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
    /// Resend existing WMS shipment to queue by WMS shipmentID
    /// </summary>
    public class WMSShipmentResendClient : ApiClientBase<WMSShipmentResendResponsePayload>
    {

        /// <summary>
        /// "ERP_Integration_Api_BaseUrl" and "ERP_Integration_Api_AuthCode" were not config in config file
        /// Local config file is 'local.settings.json'
        /// </summary>
        public WMSShipmentResendClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        {

        }
        public WMSShipmentResendClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        /// <summary>
        /// Transfer channel order to erp
        /// </summary>
        /// <param name="MasterAccountNum"></param>
        /// <param name="ProfileNum"></param>
        /// <param name="processUuid"></param>
        /// <returns></returns>
        public async Task<bool> ResendWMSShipmentToErpAsync(int masterAccountNum, int profileNum, IList<string> shipmentIDs)
        {
            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }
            if (shipmentIDs is null || shipmentIDs.Count == 0)
            {
                AddError("shipmentIDs cann't be empty.");
                return false;
            }
            return await PostAsync(shipmentIDs, FunctionUrl.ResendWMSShipment);
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

            return ResopneData.Success;
        }
    }
}
