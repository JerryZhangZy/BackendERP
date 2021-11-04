using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class QboInvoiceClient:ErpEventClient
    {

        public QboInvoiceClient() : base()
        { }
        public QboInvoiceClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> SendAddQboInvoiceAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, ERPEventFunctionUrl.QuickBooksInvoice);
        }

        public async Task<bool> SendVoidQboInvoiceAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, ERPEventFunctionUrl.QuickBooksInvoiceVoid);
        }
    }
}
