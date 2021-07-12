
              
    

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
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a CustomerAttributes Dto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class CustomerAttributesDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

        [StringLength(50, ErrorMessage = "The CustomerUuid value cannot exceed 50 characters. ")]
        public string CustomerUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCustomerUuid => CustomerUuid != null;


        [IgnoreCompare]
        public JObject Fields { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasFields => Fields != null;


        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}



