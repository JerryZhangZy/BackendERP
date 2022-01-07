
              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public static class InventoryExtensions
    {
        public static Inventory FindByWarehouseCode(this IList<Inventory> lst, string warehouseCode)
        {
            return (lst == null || string.IsNullOrEmpty(warehouseCode))
                ? null
                : lst.AsEnumerable().FirstOrDefault(x => x.WarehouseCode.EqualsIgnoreSpace(warehouseCode));
        }
    }
}



