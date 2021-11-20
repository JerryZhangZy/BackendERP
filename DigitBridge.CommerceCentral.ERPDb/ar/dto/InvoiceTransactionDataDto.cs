              
    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InvoiceTransactionDataDto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class InvoiceTransactionDataDto
    {
        public InvoiceTransactionDto InvoiceTransaction { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public bool HasInvoiceTransaction => InvoiceTransaction != null;

        public IList<InvoiceReturnItemsDto> InvoiceReturnItems { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public bool HasInvoiceReturnItems => InvoiceReturnItems != null;


        public InvoiceDataDto InvoiceDataDto { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public bool HasInvoiceDataDto => InvoiceDataDto != null;
    }
}



