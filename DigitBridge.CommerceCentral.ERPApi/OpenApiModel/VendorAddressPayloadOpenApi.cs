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
    public class VendorAddressPayloadAdd
    {
        /// <summary>
        /// (Request Data) Vendor object to add.
        /// (Response Data) Vendor object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) Vendor object to add.")]
        public VendorAddressDataDto VendorAddress { get; set; }

        public static VendorAddressPayloadAdd GetSampleData()
        {
            var data = new VendorAddressPayloadAdd();
            var dto = new VendorAddressDataDto();
            dto.VendorAddress = new VendorAddressDto().GetFaker().Generate(3)[0];
            data.VendorAddress = dto;

            return data;
        }

 
    }

    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class VendorAddressPayloadUpdate
    {
        /// <summary>
        /// (Request Data) Vendor object to add.
        /// (Response Data) Vendor object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) Vendor object to add.")]
        public VendorAddressDataDto VendorAddress { get; set; }

        public static VendorAddressPayloadUpdate GetSampleData()
        {
            var data = new VendorAddressPayloadUpdate();
            var dto = new VendorAddressDataDto();
            dto.VendorAddress = new VendorAddressDto().GetFaker().Generate(3)[0];
            data.VendorAddress = dto;

            return data;
        }
      

    }
    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class VendorAddressPayloadDelete
    {
    }

}
