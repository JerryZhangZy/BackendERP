


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
    public partial class SalesOrderData
    {
        /// <summary>
        /// Get row num by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<long> GetRowNumAsync(string orderNumber)
        {
            return await dbFactory.GetValueAsync<SalesOrderHeader, long>($"SELECT TOP 1 RowNum FROM SalesOrderHeader where OrderNumber='{orderNumber}'");
        }
        /// <summary>
        /// Get row num by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual long GetRowNum(string orderNumber)
        {
            return dbFactory.GetValue<SalesOrderHeader, long>($"SELECT TOP 1 RowNum FROM SalesOrderHeader where OrderNumber='{orderNumber}'");
        }
    }
}



