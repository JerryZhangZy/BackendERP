
    

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
        public virtual async Task<bool> GetByOrderNumberAsync(string orderNumber)
        {
            var rowNum = await dbFactory.GetValueAsync<SalesOrderHeader, long>($"SELECT TOP 1 RowNum FROM SalesOrderHeader where OrderNumber='{orderNumber}'");
            return await GetAsync(rowNum);
        }
        public virtual bool GetByOrderNumber(string orderNumber)
        {
            var rowNum = dbFactory.GetValue<SalesOrderHeader, long>($"SELECT TOP 1 RowNum FROM SalesOrderHeader where OrderNumber='{orderNumber}'");
            return Get(rowNum);
        }
    }
}



