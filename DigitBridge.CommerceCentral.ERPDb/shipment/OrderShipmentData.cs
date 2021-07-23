
    

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
        /// <summary>
        /// Get row num by order shipment number
        /// </summary>
        /// <param name="orderShipmentNum"></param>
        /// <returns></returns>
        public virtual async Task<long?> GetRowNumAsync(string orderShipmentNum)
        {
            return await dbFactory.GetValueAsync<OrderShipmentHeader, long?>($"SELECT TOP 1 RowNum FROM OrderShipmentHeader where OrderShipmentNum='{orderShipmentNum}'");
        } 
    }
}



