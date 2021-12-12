    
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
using Bogus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class MiscInvoicePayloadAdd
    {
        /// <summary>
        /// (Request Data) MiscInvoice object to add.
        /// (Response Data) MiscInvoice object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) MiscInvoice object to add.")]
        public MiscInvoiceDataDto MiscInvoice { get; set; }
        
        public static MiscInvoicePayloadAdd GetSampleData()
        {
            var data = new MiscInvoicePayloadAdd();
            data.MiscInvoice = new MiscInvoiceDataDto().GetFakerData();
            return data;
        }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class MiscInvoicePayloadUpdate
    {
        /// <summary>
        /// (Request Data) MiscInvoice object to update.
        /// (Response Data) MiscInvoice object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) MiscInvoice object to update.")]
        public MiscInvoiceDataDto MiscInvoice { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class MiscInvoicePayloadGetSingle
    {
        /// <summary>
        /// (Response Data) MiscInvoice object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) MiscInvoice object to get.")]
        public MiscInvoiceDataDto MiscInvoice { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class MiscInvoicePayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple MiscInvoices.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple MiscInvoices.")]
        public IList<string> MiscInvoiceUuids { get; set; }

        /// <summary>
        /// (Response) Array of MiscInvoice which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of MiscInvoice which get by uuid array.")]
        public IList<MiscInvoiceDataDto> MiscInvoices { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class MiscInvoicePayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class MiscInvoicePayloadFind : FilterPayloadBase<MiscInvoiceFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> MiscInvoiceList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int MiscInvoiceListCount { get; set; }
        
        public static MiscInvoicePayloadFind GetSampleData()
        {
            var data = new MiscInvoicePayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = MiscInvoiceFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class MiscInvoiceFilter
    {
        public string MiscInvoiceUuid { get; set; }
        public string QboDocNumber { get; set; }
        public string MiscInvoiceNumber { get; set; }
        public string MiscInvoiceNumberFrom { get; set; }
        public string MiscInvoiceNumberTo { get; set; }
        public DateTime MiscInvoiceDateFrom { get; set; }
        public DateTime MiscInvoiceDateTo { get; set; }
        public DateTime DueDateFrom { get; set; }
        public DateTime DueDateTo { get; set; }
        public int MiscInvoiceType { get; set; }
        public int MiscInvoiceStatus { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string BankAccountUuid { get; set; }
        public string BankAccountCode { get; set; }
        public int PaidBy { get; set; }
        public string CheckNum { get; set; }
        public string AuthCode { get; set; }

        public static Faker<MiscInvoiceFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<MiscInvoiceFilter>()
                .RuleFor(u => u.MiscInvoiceNumber, f => string.Empty)
                .RuleFor(u => u.MiscInvoiceType, f => f.Random.Number())
                .RuleFor(u => u.MiscInvoiceStatus, f => f.Random.Number())
                .RuleFor(u => u.MiscInvoiceDateFrom, f => f.Date.Past(0).Date.Date.AddDays(-30))
                .RuleFor(u => u.MiscInvoiceDateTo, f => f.Date.Past(0).Date.Date)
                .RuleFor(u => u.CustomerCode, f => string.Empty)
                .RuleFor(u => u.CustomerName, f => string.Empty)
                ;
            #endregion faker data rules
        }
    }

}

