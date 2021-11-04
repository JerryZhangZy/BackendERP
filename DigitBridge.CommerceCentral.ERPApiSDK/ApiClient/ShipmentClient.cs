using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class ShipmentClient : ErpEventClient
    {

        public ShipmentClient() : base()
        { }
        public ShipmentClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> CreateShipmentAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, "TODO");
        }
    }
}
