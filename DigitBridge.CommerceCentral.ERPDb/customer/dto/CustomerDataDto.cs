
              
    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a CustomerDataDto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public partial class CustomerDataDto
    {
        public CustomerDto Customer { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCustomer => Customer != null;

        public CustomerAddressDto CustomerAddress { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCustomerAddress => CustomerAddress != null;

        public IEnumerable<CustomerAttributesDto> CustomerAttributes { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCustomerAttributes => CustomerAttributes != null;

    }
}



