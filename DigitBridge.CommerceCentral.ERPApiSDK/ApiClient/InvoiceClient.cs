using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class InvoiceClient:ErpEventClient
    {

        public InvoiceClient() : base()
        { }
        public InvoiceClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> SendCreateInvoiceByOrderShipmentAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, ERPEventFunctionUrl.CreateInvoiceByOrderShipment);
        }
    }
}
