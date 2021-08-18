using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl.Base;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl.Manager
{
    [Serializable()]
    public class CreateSalesOrderDto
    {
        public ChannelOrderDataDto ChannelOrder { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public bool HasChannelOrder => ChannelOrder != null;

        public SalesOrderDataDto SalesOrder { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public bool HasSalesOrder => SalesOrder != null;
    }

}
