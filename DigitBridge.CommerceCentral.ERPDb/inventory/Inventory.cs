
              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class Inventory
    {
        public void AddIgnoreStockRelatedColumns()
        {
            AddIgnoreUpdate("Instock");
            AddIgnoreUpdate("OnHand");
            AddIgnoreUpdate("OpenSoQty");
            AddIgnoreUpdate("OpenFulfillmentQty");
            AddIgnoreUpdate("AvaQty");
            AddIgnoreUpdate("OpenPoQty");
            AddIgnoreUpdate("OpenInTransitQty");
            AddIgnoreUpdate("OpenWipQty");
            AddIgnoreUpdate("ProjectedQty");
        }
    }
}



