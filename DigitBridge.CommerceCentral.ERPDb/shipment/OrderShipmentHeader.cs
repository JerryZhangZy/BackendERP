              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class OrderShipmentHeader
    {
        public override IList<string> IgnoreUpdateColumns() => new List<string>()
            {
                "MasterAccountNum",
                "ProfileNum",
                //"DatabaseNum",
                "OrderShipmentUuid",
                "OrderShipmentNum"
            };
    }
}



