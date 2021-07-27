using Newtonsoft.Json;
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
    [Serializable()]
    public class CustomerPayload : PayloadBase
    {
        public IList<string> CustomerCodes { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasCustomerCodes => CustomerCodes != null && CustomerCodes.Count > 0;
        public bool ShouldSerializeCustomerCodes() => HasCustomerCodes;

        public IList<CustomerDataDto> Customers { get; set; }
        [JsonIgnore] public virtual bool HasCustomers => Customers != null && Customers.Count > 0;
        public bool ShouldSerializeSalesOrders() => HasCustomers;

        public CustomerDataDto Customer { get; set; }
        [JsonIgnore] public virtual bool HasCustomer => Customer != null;
        public bool ShouldSerializeSalesOrder() => HasCustomer;


        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "customerCodes", val =>CustomerCodes = val.Split(",").ToList() }
            };
        }

    }
}
