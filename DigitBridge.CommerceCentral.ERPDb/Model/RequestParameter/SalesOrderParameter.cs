using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request paging information
    /// </summary>
    public class SalesOrderParameter : RequestParameter
    {
        public IList<string> SalesOrderUuids { get; set; } = new List<string>();

        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "salesOrderUuids", val => SalesOrderUuids = val.Split(",").ToList() }
            };
        }

    }
}
