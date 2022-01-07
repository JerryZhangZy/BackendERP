using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Add event and add message to queue to create qbo refund
    /// </summary>
    public class QboReturnClient : ErpEventClient
    {

        public QboReturnClient() : base()
        { }
        public QboReturnClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> SendAddQboReturnAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, FunctionUrl.QuickBooksReturn);
        }

        public async Task<bool> SendDeleteQboReturnAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, FunctionUrl.QuickBooksReturnDelete);
        }
    }
}
