
              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class SalesOrderItems
    {
        public override IList<string> IgnoreUpdateColumns() => new List<string>()
            {
                "ShipPack",
                "ShipQty"
            };
    }
}



