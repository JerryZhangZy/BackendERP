
              
    

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
    /// Represents a InvoiceDataDto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public partial class InvoiceDataDto
    {
        public InvoiceHeaderDto InvoiceHeader { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasInvoiceHeader => InvoiceHeader != null;

        public InvoiceHeaderInfoDto InvoiceHeaderInfo { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasInvoiceHeaderInfo => InvoiceHeaderInfo != null;

        public InvoiceHeaderAttributesDto InvoiceHeaderAttributes { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasInvoiceHeaderAttributes => InvoiceHeaderAttributes != null;

        public IList<InvoiceItemsDto> InvoiceItems { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasInvoiceItems => InvoiceItems != null;

    }
}



