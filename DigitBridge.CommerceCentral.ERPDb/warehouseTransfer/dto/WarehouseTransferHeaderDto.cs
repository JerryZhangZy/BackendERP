              
    

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

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a WarehouseTransferHeader Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class WarehouseTransferHeaderDto
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
		/// WarehouseTransfer uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("WarehouseTransfer uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The WarehouseTransferUuid value cannot exceed 50 characters. ")]
        public string WarehouseTransferUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseTransferUuid => WarehouseTransferUuid != null;

		/// <summary>
		/// Readable WarehouseTransfer number, unique in same database and profile. <br> Parameter should pass ProfileNum-BatchNumber. <br> Title: WarehouseTransfer Number, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable WarehouseTransfer number, unique in same database and profile. <br> Parameter should pass ProfileNum-BatchNumber. <br> Title: WarehouseTransfer Number, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The BatchNumber value cannot exceed 50 characters. ")]
        public string BatchNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBatchNumber => BatchNumber != null;

		/// <summary>
		/// WarehouseTransfer type (Adjust/Damage/Cycle Count/Physical Count). <br> Title: Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("WarehouseTransfer type (Adjust/Damage/Cycle Count/Physical Count). <br> Title: Type, Display: true, Editable: true")]
        public int? WarehouseTransferType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseTransferType => WarehouseTransferType != null;

		/// <summary>
		/// WarehouseTransfer status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("WarehouseTransfer status. <br> Title: Status, Display: true, Editable: true")]
        public int? WarehouseTransferStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseTransferStatus => WarehouseTransferStatus != null;

		/// <summary>
		/// WarehouseTransfer date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("WarehouseTransfer date. <br> Title: Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? TransferDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransferDate => TransferDate != null;

		/// <summary>
		/// WarehouseTransfer time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("WarehouseTransfer time. <br> Title: Time, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? TransferTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransferTime => TransferTime != null;

		/// <summary>
		/// WarehouseTransfer processor account. <br> Title: Processor, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("WarehouseTransfer processor account. <br> Title: Processor, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The Processor value cannot exceed 50 characters. ")]
        public string Processor { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProcessor => Processor != null;

		/// <summary>
		/// WarehouseTransfer date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("WarehouseTransfer date. <br> Title: Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? ReceiveDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReceiveDate => ReceiveDate != null;

		/// <summary>
		/// WarehouseTransfer time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("WarehouseTransfer time. <br> Title: Time, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? ReceiveTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReceiveTime => ReceiveTime != null;

		/// <summary>
		/// WarehouseTransfer processor account. <br> Title: Processor, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("WarehouseTransfer processor account. <br> Title: Processor, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The ReceiveProcessor value cannot exceed 50 characters. ")]
        public string ReceiveProcessor { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReceiveProcessor => ReceiveProcessor != null;

		/// <summary>
		/// (Readonly) Warehouse uuid, transfer from warehouse. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Warehouse uuid, transfer from warehouse. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The FromWarehouseUuid value cannot exceed 50 characters. ")]
        public string FromWarehouseUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasFromWarehouseUuid => FromWarehouseUuid != null;

		/// <summary>
		/// Readable warehouse code, transfer from warehouse. <br> Title: Warehouse Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable warehouse code, transfer from warehouse. <br> Title: Warehouse Code, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The FromWarehouseCode value cannot exceed 50 characters. ")]
        public string FromWarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasFromWarehouseCode => FromWarehouseCode != null;

		/// <summary>
		/// (Readonly) Warehouse uuid, transfer to warehouse. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Warehouse uuid, transfer to warehouse. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The ToWarehouseUuid value cannot exceed 50 characters. ")]
        public string ToWarehouseUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasToWarehouseUuid => ToWarehouseUuid != null;

		/// <summary>
		/// Readable warehouse code, transfer to warehouse. <br> Title: Warehouse Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable warehouse code, transfer to warehouse. <br> Title: Warehouse Code, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The ToWarehouseCode value cannot exceed 50 characters. ")]
        public string ToWarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasToWarehouseCode => ToWarehouseCode != null;

		/// <summary>
		/// Readable InTransitToWarehouseCode code, transfer to warehouse. <br> Title: Warehouse Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable InTransitToWarehouseCode code, transfer to warehouse. <br> Title: Warehouse Code, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The InTransitToWarehouseCode value cannot exceed 50 characters. ")]
        public string InTransitToWarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInTransitToWarehouseCode => InTransitToWarehouseCode != null;

		/// <summary>
		/// Reference Transaction Type, reference to invoice, P/O. <br> Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Reference Transaction Type, reference to invoice, P/O. <br> Display: true, Editable: true")]
        public int? ReferenceType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReferenceType => ReferenceType != null;

		/// <summary>
		/// Reference Transaction uuid, reference to uuid of invoice, P/O#. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Reference Transaction uuid, reference to uuid of invoice, P/O#. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The ReferenceUuid value cannot exceed 50 characters. ")]
        public string ReferenceUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReferenceUuid => ReferenceUuid != null;

		/// <summary>
		/// Reference Transaction number, reference to invoice#, P/O#. <br> Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Reference Transaction number, reference to invoice#, P/O#. <br> Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The ReferenceNum value cannot exceed 50 characters. ")]
        public string ReferenceNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReferenceNum => ReferenceNum != null;

		/// <summary>
		/// (Readonly) WarehouseTransfer created from other entity number, use to prevent import duplicate WarehouseTransfer. <br> Title: Source Number, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) WarehouseTransfer created from other entity number, use to prevent import duplicate WarehouseTransfer. <br> Title: Source Number, Display: false, Editable: false")]
        [StringLength(100, ErrorMessage = "The WarehouseTransferSourceCode value cannot exceed 100 characters. ")]
        public string WarehouseTransferSourceCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseTransferSourceCode => WarehouseTransferSourceCode != null;

		/// <summary>
		/// (Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdateDateUtc { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateDateUtc => UpdateDateUtc != null;

		/// <summary>
		/// (Readonly) User who created this WarehouseTransfer. <br> Title: Created By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) User who created this WarehouseTransfer. <br> Title: Created By, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The EnterBy value cannot exceed 100 characters. ")]
        public string EnterBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEnterBy => EnterBy != null;

		/// <summary>
		/// (Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The UpdateBy value cannot exceed 100 characters. ")]
        public string UpdateBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateBy => UpdateBy != null;



        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}



