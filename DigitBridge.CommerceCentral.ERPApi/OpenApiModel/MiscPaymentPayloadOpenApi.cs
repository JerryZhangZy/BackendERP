

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Bogus;

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class MiscPaymentPayloadAdd
    {
        /// <summary>
        /// (Request Data) MiscInvoiceTransaction object to add.
        /// (Response Data) MiscInvoiceTransaction object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) MiscInvoiceTransaction object to add.")]
        public ApTransactionDataDto ApTransaction { get; set; }

        public static MiscPaymentPayloadAdd GetSampleData()
        {
            var data = new MiscPaymentPayloadAdd();
            data.ApTransaction = new ApTransactionDataDto().GetFakerData();
            data.ApTransaction.ApInvoiceTransaction.TransType = (int)TransTypeEnum.Payment;
            return data;
        }
    }

    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class MiscPaymentPayloadUpdate
    {
        /// <summary>
        /// (Request Data) MiscInvoiceTransaction object to update.
        /// (Response Data) MiscInvoiceTransaction object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) MiscInvoiceTransaction object to update.")]
        public MiscInvoiceTransactionDataDto MiscInvoiceTransaction { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class MiscPaymentPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) MiscInvoiceTransaction object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) MiscInvoiceTransaction object to get.")]
        public MiscInvoiceTransactionDataDto MiscInvoiceTransaction { get; set; }

        /// <summary>
        /// (Response Data) InvoiceHeader object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) InvoiceHeader object to get.")]
        public InvoiceHeaderDto InvoiceHeader { get; set; }
    }
    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class MiscPaymentPayloadGetMulti
    {
        /// <summary>
        /// (Response Data) MiscInvoiceDataDto object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) MiscInvoiceDataDto object to get.")]
        public MiscInvoiceDataDto MiscInvoiceDataDto { get; set; }

        /// <summary>
        /// (Response Data) InvoiceHeader object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) MiscInvoiceTransactionDto list.")]
        public List<MiscInvoiceTransactionDto> MiscInvoiceTransactionDtoList { get; set; }
    }
    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class MiscPaymentPayloadDelete
    {
    }

    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class MiscPaymentPayloadFind : FilterPayloadBase<MiscPaymentFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> MiscInvoiceTransactionList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int MiscInvoiceTransactionListCount { get; set; }

        public static MiscPaymentPayloadFind GetSampleData()
        {
            var data = new MiscPaymentPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = MiscPaymentFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class MiscPaymentFilter
    {
        public string InvoiceNumber { get; set; }

        public string InvoiceType { get; set; }

        public string InvoiceStatus { get; set; }

        public DateTime InvoiceDateFrom { get; set; }

        public DateTime InvoiceDateTo { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string ShippingCarrier { get; set; }

        public string WarehouseCode { get; set; }

        public static Faker<MiscPaymentFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<MiscPaymentFilter>()
                .RuleFor(u => u.InvoiceNumber, f => string.Empty)
                .RuleFor(u => u.InvoiceType, f => string.Empty)
                .RuleFor(u => u.InvoiceStatus, f => string.Empty)
                .RuleFor(u => u.CustomerCode, f => string.Empty)
                .RuleFor(u => u.CustomerName, f => string.Empty)
                .RuleFor(u => u.ShippingCarrier, f => string.Empty)
                .RuleFor(u => u.WarehouseCode, f => string.Empty)
                .RuleFor(u => u.InvoiceDateFrom, f => f.Date.Past(0).Date.Date.AddDays(-30))
                .RuleFor(u => u.InvoiceDateTo, f => f.Date.Past(0).Date.Date)
                ;
            #endregion faker data rules
        }
    }
}

