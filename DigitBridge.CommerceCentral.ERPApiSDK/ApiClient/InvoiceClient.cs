using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Create erp invoice by OrderShipmentUuid
    /// </summary>
    public class InvoiceClient:ErpEventClient
    {

        public InvoiceClient() : base()
        { }
        public InvoiceClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> CreateInvoiceByOrderShipmentAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, FunctionUrl.CreateInvoiceByOrderShipment);
        }
    }
}
