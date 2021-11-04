using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class QboPaymentClient : ErpEventClient
    {

        public QboPaymentClient() : base()
        { }
        public QboPaymentClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> SendAddQboPaymentAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, ERPEventFunctionUrl.QuickBooksPayment);
        }

        public async Task<bool> SendDeleteQboPaymentAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, ERPEventFunctionUrl.QuickBooksPaymentDelete);
        }
    }
}
