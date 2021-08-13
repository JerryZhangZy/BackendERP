
    
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
    public class InvoicePaymentPayloadAdd
    {
        /// <summary>
        /// (Request Data) InvoiceTransaction object to add.
        /// (Response Data) InvoiceTransaction object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InvoiceTransaction object to add.")]
        public InvoiceTransactionDto InvoiceTransaction { get; set; }

        public static InvoicePaymentPayloadAdd GetSampleData()
        {
            var data = new InvoicePaymentPayloadAdd();
            data.InvoiceTransaction = new InvoiceTransactionDto().GetFaker().Generate();
            return data;
        }
    } 

    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class InvoicePaymentPayloadUpdate
    {
        /// <summary>
        /// (Request Data) InvoiceTransaction object to update.
        /// (Response Data) InvoiceTransaction object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InvoiceTransaction object to update.")]
        public InvoiceTransactionDto InvoiceTransaction { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class InvoicePaymentPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) InvoiceTransaction object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) InvoiceTransaction object to get.")]
        public InvoiceTransactionDto InvoiceTransaction { get; set; }

        /// <summary>
        /// (Response Data) InvoiceHeader object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) InvoiceHeader object to get.")]
        public InvoiceHeaderDto InvoiceHeader { get; set; }
    } 
    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class InvoicePaymentPayloadDelete
    {
    }

    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class InvoicePaymentPayloadFind : FilterPayloadBase<InvoicePaymentFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> InvoiceTransactionList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int InvoiceTransactionListCount { get; set; }

        public static InvoicePaymentPayloadFind GetSampleData()
        {
            var data = new InvoicePaymentPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = InvoicePaymentFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class InvoicePaymentFilter
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

        public static Faker<InvoicePaymentFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<InvoicePaymentFilter>()
                .RuleFor(u => u.InvoiceNumber, f => string.Empty)
                .RuleFor(u => u.InvoiceType, f =>string.Empty)
                .RuleFor(u => u.InvoiceStatus, f =>string.Empty)
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

