using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DigitBridge.CommerceCentral.ERPApi.OpenApiModel
{  
  

    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class CustomerAddressPayloadAdd
    {
        /// <summary>
        /// (Request Data) CustomerAddress object to add.
        /// (Response Data) CustomerAddress object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) CustomerAddress object to add.")]
        public CustomerAddressDataDto CustomerAddress { get; set; }

        public static CustomerAddressPayloadAdd GetSampleData()
        {
            var data = new CustomerAddressPayloadAdd();
            var dto = new CustomerAddressDataDto();
            dto.CustomerAddress = new CustomerAddressDto().GetFaker().Generate(3)[0];
            data.CustomerAddress = dto;

            return data;
        }

 
    }

    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class CustomerAddressPayloadUpdate
    {
        /// <summary>
        /// (Request Data) CustomerAddress object to add.
        /// (Response Data) CustomerAddress object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) CustomerAddress object to add.")]
        public CustomerAddressDataDto CustomerAddress { get; set; }

        public static CustomerAddressPayloadUpdate GetSampleData()
        {
            var data = new CustomerAddressPayloadUpdate();
            var dto = new CustomerAddressDataDto();
            dto.CustomerAddress = new CustomerAddressDto().GetFaker().Generate(3)[0];
            data.CustomerAddress = dto;

            return data;
        }
      

    }
    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class CustomerAddressPayloadDelete
    {
    }

}
