
              
    

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
            var ignoreColumns = new List<string>{ "Instock", "OnHand", "OpenSoQty", "OpenFulfillmentQty", "AvaQty", "OpenPoQty", "OpenInTransitQty", "OpenWipQty", "ProjectedQty" };
            SetIgnoreUpdateColumns(ignoreColumns);
        }
        public override IList<string> IgnoreUpdateColumns() => new List<string>()
            {
                "Instock",
                "OnHand",
                "OpenSoQty",
                "OpenFulfillmentQty",
                "AvaQty",
                "OpenPoQty",
                "OpenInTransitQty",
                "OpenWipQty",
                "ProjectedQty"

            };


    }
}



