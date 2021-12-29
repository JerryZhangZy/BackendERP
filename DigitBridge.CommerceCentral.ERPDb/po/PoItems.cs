


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
    public partial class PoItems
    {
        public override IList<string> IgnoreUpdateColumns() => new List<string>()
            {
                "ReceivedQty",
                "CancelledQty"
            };

        /// <summary>
        /// PoNum of the item.
        /// </summary>
        public string PoNum { get; set; }
    }
}



