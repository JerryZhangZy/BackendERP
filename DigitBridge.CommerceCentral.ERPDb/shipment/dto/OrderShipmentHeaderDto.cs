              
    

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
    /// Represents a OrderShipmentHeader Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class OrderShipmentHeaderDto
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
		/// (Readonly) Shipment Unique Number. Required, <br> Title: Shipment Number Display: true, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Shipment Unique Number. Required, <br> Title: Shipment Number Display: true, Editable: false.")]
        public long? OrderShipmentNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderShipmentNum => OrderShipmentNum != null;

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
		/// (Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false")]
        public int? ChannelNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChannelNum => ChannelNum != null;

		/// <summary>
		/// (Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false")]
        public int? ChannelAccountNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChannelAccountNum => ChannelAccountNum != null;

		/// <summary>
		/// (Readonly) The unique number of Order DC Assignment. <br> Title: Assignment Number: Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) The unique number of Order DC Assignment. <br> Title: Assignment Number: Display: true, Editable: false")]
        public long? OrderDCAssignmentNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderDCAssignmentNum => OrderDCAssignmentNum != null;

		/// <summary>
		/// (Readonly) DC number. <br> Title: DC Number: Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) DC number. <br> Title: DC Number: Display: true, Editable: false")]
        public int? DistributionCenterNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDistributionCenterNum => DistributionCenterNum != null;

		/// <summary>
		/// (Readonly) CentralOrderNum. <br> Title: Central Order: Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) CentralOrderNum. <br> Title: Central Order: Display: true, Editable: false")]
        public long? CentralOrderNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCentralOrderNum => CentralOrderNum != null;

		/// <summary>
		/// (Readonly) This usually is the marketplace order ID, or merchant PO Number. <br> Title: Channel Order: Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) This usually is the marketplace order ID, or merchant PO Number. <br> Title: Channel Order: Display: true, Editable: false")]
        [StringLength(130, ErrorMessage = "The ChannelOrderID value cannot exceed 130 characters. ")]
        public string ChannelOrderID { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChannelOrderID => ChannelOrderID != null;

		/// <summary>
		/// (Readonly) Shipment ID. <br> Title: Shipment Id, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Shipment ID. <br> Title: Shipment Id, Display: true, Editable: false")]
        [StringLength(50, ErrorMessage = "The ShipmentID value cannot exceed 50 characters. ")]
        public string ShipmentID { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShipmentID => ShipmentID != null;

		/// <summary>
		/// Warehouse Code. <br> Title: Warehouse Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Warehouse Code. <br> Title: Warehouse Code, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The WarehouseCode value cannot exceed 50 characters. ")]
        public string WarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseCode => WarehouseCode != null;

		/// <summary>
		/// Shipment Type. <br> Title: Shipment Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Shipment Type. <br> Title: Shipment Type, Display: true, Editable: true")]
        public int? ShipmentType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShipmentType => ShipmentType != null;

		/// <summary>
		/// Ref Id. <br> Title: Reference, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Ref Id. <br> Title: Reference, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The ShipmentReferenceID value cannot exceed 50 characters. ")]
        public string ShipmentReferenceID { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShipmentReferenceID => ShipmentReferenceID != null;

		/// <summary>
		/// Ship Date. <br> Title: Ship Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Ship Date. <br> Title: Ship Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? ShipmentDateUtc { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShipmentDateUtc => ShipmentDateUtc != null;

		/// <summary>
		/// Shipping Carrier. <br> Title: Shipping Carrier: Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Shipping Carrier. <br> Title: Shipping Carrier: Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The ShippingCarrier value cannot exceed 50 characters. ")]
        public string ShippingCarrier { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingCarrier => ShippingCarrier != null;

		/// <summary>
		/// Shipping Method. <br> Title: Shipping Method: Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Shipping Method. <br> Title: Shipping Method: Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The ShippingClass value cannot exceed 50 characters. ")]
        public string ShippingClass { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingClass => ShippingClass != null;

		/// <summary>
		/// Shipping fee. <br> Title: Shipping Fee, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Shipping fee. <br> Title: Shipping Fee, Display: true, Editable: true")]
        public decimal? ShippingCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingCost => ShippingCost != null;

		/// <summary>
		/// Total handling fee. <br> Title: Total handling fee, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Total handling fee. <br> Title: Total handling fee, Display: true, Editable: true")]
        public decimal? TotalHandlingFee { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTotalHandlingFee => TotalHandlingFee != null;

		/// <summary>
		/// Master TrackingNumber. <br> Title: Tracking Number, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Master TrackingNumber. <br> Title: Tracking Number, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The MainTrackingNumber value cannot exceed 50 characters. ")]
        public string MainTrackingNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMainTrackingNumber => MainTrackingNumber != null;

		/// <summary>
		/// Master Return TrackingNumber. <br> Title: Return Tracking Number, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Master Return TrackingNumber. <br> Title: Return Tracking Number, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The MainReturnTrackingNumber value cannot exceed 50 characters. ")]
        public string MainReturnTrackingNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMainReturnTrackingNumber => MainReturnTrackingNumber != null;

		/// <summary>
		/// Bill Of Lading ID. <br> Title: BOL Id, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Bill Of Lading ID. <br> Title: BOL Id, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The BillOfLadingID value cannot exceed 50 characters. ")]
        public string BillOfLadingID { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBillOfLadingID => BillOfLadingID != null;

		/// <summary>
		/// Total Packages. <br> Title: Number of Package, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Total Packages. <br> Title: Number of Package, Display: true, Editable: true")]
        public int? TotalPackages { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTotalPackages => TotalPackages != null;

		/// <summary>
		/// Total Shipped Qty. <br> Title: Shipped Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Total Shipped Qty. <br> Title: Shipped Qty, Display: true, Editable: true")]
        public decimal? TotalShippedQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTotalShippedQty => TotalShippedQty != null;

		/// <summary>
		/// Total Cancelled Qty. <br> Title: Cancelled Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Total Cancelled Qty. <br> Title: Cancelled Qty, Display: true, Editable: true")]
        public decimal? TotalCanceledQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTotalCanceledQty => TotalCanceledQty != null;

		/// <summary>
		/// Total Weight. <br> Title: Weight, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Total Weight. <br> Title: Weight, Display: true, Editable: true")]
        public decimal? TotalWeight { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTotalWeight => TotalWeight != null;

		/// <summary>
		/// Total Volume. <br> Title: Volume, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Total Volume. <br> Title: Volume, Display: true, Editable: true")]
        public decimal? TotalVolume { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTotalVolume => TotalVolume != null;

		/// <summary>
		/// Weight Unit. <br> Title: Weight Unit, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Weight Unit. <br> Title: Weight Unit, Display: true, Editable: true")]
        public int? WeightUnit { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWeightUnit => WeightUnit != null;

		/// <summary>
		/// Length Unit. <br> Title: Length Unit, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Length Unit. <br> Title: Length Unit, Display: true, Editable: true")]
        public int? LengthUnit { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLengthUnit => LengthUnit != null;

		/// <summary>
		/// Volume Unit. <br> Title: Volume Unit, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Volume Unit. <br> Title: Volume Unit, Display: true, Editable: true")]
        public int? VolumeUnit { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVolumeUnit => VolumeUnit != null;

		/// <summary>
		/// Shipment Status. <br> Title: Shipment Status, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Shipment Status. <br> Title: Shipment Status, Display: true, Editable: true")]
        public int? ShipmentStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShipmentStatus => ShipmentStatus != null;

		/// <summary>
		/// (Ignore) DBChannelOrderHeaderRowID. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) DBChannelOrderHeaderRowID. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The DBChannelOrderHeaderRowID value cannot exceed 50 characters. ")]
        public string DBChannelOrderHeaderRowID { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDBChannelOrderHeaderRowID => DBChannelOrderHeaderRowID != null;

		/// <summary>
		/// Process Status. <br> Title: Process Status, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Process Status. <br> Title: Process Status, Display: true, Editable: false")]
        public int? ProcessStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProcessStatus => ProcessStatus != null;

		/// <summary>
		/// Process Date. <br> Title: Process Date, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Process Date. <br> Title: Process Date, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? ProcessDateUtc { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProcessDateUtc => ProcessDateUtc != null;

		/// <summary>
		/// Shipment uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Shipment uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The OrderShipmentUuid value cannot exceed 50 characters. ")]
        public string OrderShipmentUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderShipmentUuid => OrderShipmentUuid != null;

		/// <summary>
		/// InvoiceNumber. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("InvoiceNumber. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The InvoiceNumber value cannot exceed 50 characters. ")]
        public string InvoiceNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceNumber => InvoiceNumber != null;

		/// <summary>
		/// Invoice uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Invoice uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The InvoiceUuid value cannot exceed 50 characters. ")]
        public string InvoiceUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceUuid => InvoiceUuid != null;

		/// <summary>
		/// Sales Order uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Sales Order uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The SalesOrderUuid value cannot exceed 50 characters. ")]
        public string SalesOrderUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSalesOrderUuid => SalesOrderUuid != null;

		/// <summary>
		/// Readable Sales Order number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true
        /// </summary>
		[OpenApiPropertyDescription("Readable Sales Order number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The OrderNumber value cannot exceed 50 characters. ")]
        public string OrderNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderNumber => OrderNumber != null;



        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}



