              
    

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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Represents a QuickBooksExportLog Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class QuickBooksExportLogDto
    {
        public long? RowNum { get; set; }
        [JsonIgnore,XmlIgnore]
        public string UniqueId { get; set; }
        [JsonIgnore,XmlIgnore]
        public DateTime? EnterDateUtc { get; set; }
        [JsonIgnore,XmlIgnore]
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

		/// <summary>
		/// (Readonly) Database Number. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Database Number. <br> Display: false, Editable: false.")]
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public int? DatabaseNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDatabaseNum => DatabaseNum != null;

		/// <summary>
		/// (Readonly) Login user account. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Login user account. <br> Display: false, Editable: false.")]
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public int? MasterAccountNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMasterAccountNum => MasterAccountNum != null;

		/// <summary>
		/// (Readonly) Login user profile. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Login user profile. <br> Display: false, Editable: false.")]
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public int? ProfileNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProfileNum => ProfileNum != null;

		/// <summary>
		/// (Readonly) QuickBooksExport log Line uuid. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) QuickBooksExport log Line uuid. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The QuickBooksExportLogUuid value cannot exceed 50 characters. ")]
        public string QuickBooksExportLogUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasQuickBooksExportLogUuid => QuickBooksExportLogUuid != null;

		/// <summary>
		/// Batch number for log update. <br> Title: Batch Number, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Batch number for log update. <br> Title: Batch Number, Display: true, Editable: false")]
        public long? BatchNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBatchNum => BatchNum != null;

		/// <summary>
		/// Log type. Which transaction to update QuickBooksExport. For Example: Shippment, P/O Receive, Adjust. <br> Title: Type, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Log type. Which transaction to update QuickBooksExport. For Example: Shippment, P/O Receive, Adjust. <br> Title: Type, Display: true, Editable: false")]
        [StringLength(50, ErrorMessage = "The LogType value cannot exceed 50 characters. ")]
        public string LogType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogType => LogType != null;

		/// <summary>
		/// Transaction ID (for example: PO receive, Shhipment). <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Transaction ID (for example: PO receive, Shhipment). <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The LogUuid value cannot exceed 50 characters. ")]
        public string LogUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogUuid => LogUuid != null;

		/// <summary>
		/// QuickBooks DocNumber <br> Title: DocNumber, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("QuickBooks DocNumber <br> Title: DocNumber, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The DocNumber value cannot exceed 100 characters. ")]
        public string DocNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDocNumber => DocNumber != null;

		/// <summary>
		/// Quickbook Return status. <br> Title: Status, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Quickbook Return status. <br> Title: Status, Display: true, Editable: false")]
        public int? DocStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDocStatus => DocStatus != null;

		/// <summary>
		/// Log date. <br> Title: Date, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Log date. <br> Title: Date, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? LogDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogDate => LogDate != null;

		/// <summary>
		/// Log time. <br> Title: Time, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Log time. <br> Title: Time, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? LogTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogTime => LogTime != null;

		/// <summary>
		/// Log create by. <br> Title: By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Log create by. <br> Title: By, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The LogBy value cannot exceed 100 characters. ")]
        public string LogBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogBy => LogBy != null;

		/// <summary>
		/// QuickBooks TxnId.<br> Title: TxnId, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("QuickBooks TxnId.<br> Title: TxnId, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The TxnId value cannot exceed 100 characters. ")]
        public string TxnId { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTxnId => TxnId != null;

		/// <summary>
		/// --log status. <br> Title: Status, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("--log status. <br> Title: Status, Display: true, Editable: false")]
        public int? LogStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogStatus => LogStatus != null;

		/// <summary>
		/// log error messages.<br> Title:ErrorMessage,Display:true,Editable:false
		/// </summary>
		[OpenApiPropertyDescription("log error messages.<br> Title:ErrorMessage,Display:true,Editable:false")]
        public string ErrorMessage { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasErrorMessage => ErrorMessage != null;

		/// <summary>
		/// log request info , qbo entity.<br> Title:RequestInfo,Display:true,Editable:false
		/// </summary>
		[OpenApiPropertyDescription("log request info , qbo entity.<br> Title:RequestInfo,Display:true,Editable:false")]
        [StringLength(2000, ErrorMessage = "The RequestInfo value cannot exceed 2000 characters. ")]
        public string RequestInfo { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasRequestInfo => RequestInfo != null;

		/// <summary>
		/// (Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The EnterBy value cannot exceed 100 characters. ")]
        public string EnterBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEnterBy => EnterBy != null;



        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}



