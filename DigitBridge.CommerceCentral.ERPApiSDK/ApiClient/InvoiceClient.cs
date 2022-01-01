using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Create erp invoice by OrderShipmentUuid
    /// </summary>
    public class InvoiceClient : ErpEventClient
    {

        public InvoiceClient() : base()
        { }
        public InvoiceClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        /// <summary>
        /// Create invoice by order shipment uuid
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="orderShipmentUuid"></param>
        /// <returns></returns>
        public async Task<bool> CreateInvoiceByOrderShipmentAsync(int masterAccountNum, int profileNum, string orderShipmentUuid)
        {
            var eventDto = new AddErpEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = orderShipmentUuid,
            };
            return await AddEventERPAsync(eventDto, FunctionUrl.CreateInvoiceByOrderShipment);
        }
        public async Task<bool> CreateInvoiceByOrderShipmentAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, FunctionUrl.CreateInvoiceByOrderShipment);
        }
    }
}
