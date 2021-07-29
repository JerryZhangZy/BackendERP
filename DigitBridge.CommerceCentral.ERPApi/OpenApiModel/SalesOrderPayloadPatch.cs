using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Request paging information
    /// </summary>
    [Serializable()]
    public class SalesOrderPayloadPatch
    {
        public SalesOrderDataDto SalesOrder { get; set; }
    }
}
