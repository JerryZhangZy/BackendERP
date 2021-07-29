﻿using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request paging information
    /// </summary>
    [Serializable()]
    public class SalesOrderPayloadGetMultiple
    {
        public IList<string> SalesOrderUuids { get; set; } = new List<string>();

        public IList<SalesOrderDataDto> SalesOrders { get; set; }

    }
}
