using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class ChannelOrderClient:ErpEventClient
    {

        public ChannelOrderClient() : base()
        { }
        public ChannelOrderClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> SendCreateSalesOrderByCentralOrderAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, FunctionUrl.CreateSalesOrderByCentralOrder);
        }
    }
}
