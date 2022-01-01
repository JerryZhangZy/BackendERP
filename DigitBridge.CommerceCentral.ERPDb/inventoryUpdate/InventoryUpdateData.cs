    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class InventoryUpdateData
    {

        public virtual bool WithInventoryInfo()
        {
            return InventoryUpdateItems.Any(x => (
                    string.IsNullOrEmpty(x.InventoryUuid) ||
                    string.IsNullOrEmpty(x.ProductUuid) ||
                    string.IsNullOrEmpty(x.WarehouseCode) ||
                    string.IsNullOrEmpty(x.WarehouseUuid)
                ));
        }


    }
}



