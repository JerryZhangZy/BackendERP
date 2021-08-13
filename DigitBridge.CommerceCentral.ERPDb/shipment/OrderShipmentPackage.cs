
              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class OrderShipmentPackage
    {
        //TODO : where to init uuid, same issue to others such as salesorderitem
        public OrderShipmentPackage() : base() { this.OrderShipmentPackageUuid = Guid.NewGuid().ToString(); }
    }
}



