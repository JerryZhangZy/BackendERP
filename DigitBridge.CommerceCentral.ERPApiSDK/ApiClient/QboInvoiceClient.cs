using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Add event and add message to queue to create qbo invoice
    /// </summary>
    public class QboInvoiceClient:ErpEventClient
    {

        public QboInvoiceClient() : base()
        { }
        public QboInvoiceClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> SendAddQboInvoiceAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, FunctionUrl.QuickBooksInvoice);
        }

        public async Task<bool> SendVoidQboInvoiceAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, FunctionUrl.QuickBooksInvoiceVoid);
        }
    }
}
