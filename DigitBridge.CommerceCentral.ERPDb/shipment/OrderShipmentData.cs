


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class OrderShipmentData
    {
        public async Task<long?> GetOrderShipmentNumAsync(long orderShipmentNum, int profileNum, int masterAccountNum)
        {
            return await dbFactory.GetValueAsync<OrderShipmentHeader, long?>($"SELECT TOP 1 orderShipmentNum FROM OrderShipmentHeader where orderShipmentNum={orderShipmentNum} and profileNum={profileNum} and masterAccountNum={masterAccountNum}");
        }
    }
}



