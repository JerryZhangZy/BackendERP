



using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class OrderShipmentShippedItem
    {
        //TODO : where to init uuid, same issue to others such as salesorderitem
        public OrderShipmentShippedItem() : base() { this.OrderShipmentShippedItemUuid = Guid.NewGuid().ToString(); }
    }
}



