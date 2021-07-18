
              
    

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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a OrderShipmentHeader Dto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class OrderShipmentHeaderDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public long? OrderShipmentNum { get; set; }
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        [JsonIgnore]
        [XmlIgnore, IgnoreCompare]
        public bool HasOrderShipmentNum => OrderShipmentNum != null;

        /// <summary>
        /// 
        /// </summary>
        [OpenApiPropertyDescription("")]
        public int? DatabaseNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDatabaseNum => DatabaseNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? MasterAccountNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMasterAccountNum => MasterAccountNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? ProfileNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasProfileNum => ProfileNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? ChannelNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasChannelNum => ChannelNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? ChannelAccountNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasChannelAccountNum => ChannelAccountNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public long? OrderDCAssignmentNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderDCAssignmentNum => OrderDCAssignmentNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? DistributionCenterNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDistributionCenterNum => DistributionCenterNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public long? CentralOrderNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCentralOrderNum => CentralOrderNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(130, ErrorMessage = "The ChannelOrderID value cannot exceed 130 characters. ")]
        public string ChannelOrderID { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasChannelOrderID => ChannelOrderID != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(50, ErrorMessage = "The ShipmentID value cannot exceed 50 characters. ")]
        public string ShipmentID { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShipmentID => ShipmentID != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(50, ErrorMessage = "The WarehouseID value cannot exceed 50 characters. ")]
        public string WarehouseID { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasWarehouseID => WarehouseID != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? ShipmentType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShipmentType => ShipmentType != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(50, ErrorMessage = "The ShipmentReferenceID value cannot exceed 50 characters. ")]
        public string ShipmentReferenceID { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShipmentReferenceID => ShipmentReferenceID != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [DataType(DataType.DateTime)]
        public DateTime? ShipmentDateUtc { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShipmentDateUtc => ShipmentDateUtc != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(50, ErrorMessage = "The ShippingCarrier value cannot exceed 50 characters. ")]
        public string ShippingCarrier { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShippingCarrier => ShippingCarrier != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(50, ErrorMessage = "The ShippingClass value cannot exceed 50 characters. ")]
        public string ShippingClass { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShippingClass => ShippingClass != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public decimal? ShippingCost { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShippingCost => ShippingCost != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(50, ErrorMessage = "The MainTrackingNumber value cannot exceed 50 characters. ")]
        public string MainTrackingNumber { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMainTrackingNumber => MainTrackingNumber != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(50, ErrorMessage = "The MainReturnTrackingNumber value cannot exceed 50 characters. ")]
        public string MainReturnTrackingNumber { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMainReturnTrackingNumber => MainReturnTrackingNumber != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(50, ErrorMessage = "The BillOfLadingID value cannot exceed 50 characters. ")]
        public string BillOfLadingID { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasBillOfLadingID => BillOfLadingID != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? TotalPackages { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTotalPackages => TotalPackages != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public decimal? TotalShippedQty { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTotalShippedQty => TotalShippedQty != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public decimal? TotalCanceledQty { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTotalCanceledQty => TotalCanceledQty != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public decimal? TotalWeight { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTotalWeight => TotalWeight != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public decimal? TotalVolume { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTotalVolume => TotalVolume != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? WeightUnit { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasWeightUnit => WeightUnit != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? LengthUnit { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasLengthUnit => LengthUnit != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? VolumeUnit { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasVolumeUnit => VolumeUnit != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? ShipmentStatus { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShipmentStatus => ShipmentStatus != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(50, ErrorMessage = "The DBChannelOrderHeaderRowID value cannot exceed 50 characters. ")]
        public string DBChannelOrderHeaderRowID { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDBChannelOrderHeaderRowID => DBChannelOrderHeaderRowID != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        public int? ProcessStatus { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasProcessStatus => ProcessStatus != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [DataType(DataType.DateTime)]
        public DateTime? ProcessDateUtc { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasProcessDateUtc => ProcessDateUtc != null;

		/// <summary>
		/// Global Unique Guid for one OrderShipment
		/// </summary>
		[OpenApiPropertyDescription("Global Unique Guid for one OrderShipment")]
        [StringLength(50, ErrorMessage = "The OrderShipmentUuid value cannot exceed 50 characters. ")]
        public string OrderShipmentUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderShipmentUuid => OrderShipmentUuid != null;



        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}



