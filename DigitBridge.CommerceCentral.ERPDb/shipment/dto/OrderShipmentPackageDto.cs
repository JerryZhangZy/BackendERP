
              
    

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
using System.Collections.Generic;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a OrderShipmentPackage Dto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class OrderShipmentPackageDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

        public long? OrderShipmentPackageNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderShipmentPackageNum => OrderShipmentPackageNum != null;

        public int? DatabaseNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDatabaseNum => DatabaseNum != null;

        public int? MasterAccountNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMasterAccountNum => MasterAccountNum != null;

        public int? ProfileNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasProfileNum => ProfileNum != null;

        public int? ChannelNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasChannelNum => ChannelNum != null;

        public int? ChannelAccountNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasChannelAccountNum => ChannelAccountNum != null;

        public long? OrderShipmentNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderShipmentNum => OrderShipmentNum != null;

        [StringLength(50, ErrorMessage = "The PackageID value cannot exceed 50 characters. ")]
        public string PackageID { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackageID => PackageID != null;

        public int? PackageType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackageType => PackageType != null;

        public int? PackagePatternNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackagePatternNum => PackagePatternNum != null;

        [StringLength(50, ErrorMessage = "The PackageTrackingNumber value cannot exceed 50 characters. ")]
        public string PackageTrackingNumber { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackageTrackingNumber => PackageTrackingNumber != null;

        [StringLength(50, ErrorMessage = "The PackageReturnTrackingNumber value cannot exceed 50 characters. ")]
        public string PackageReturnTrackingNumber { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackageReturnTrackingNumber => PackageReturnTrackingNumber != null;

        public decimal? PackageWeight { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackageWeight => PackageWeight != null;

        public decimal? PackageLength { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackageLength => PackageLength != null;

        public decimal? PackageWidth { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackageWidth => PackageWidth != null;

        public decimal? PackageHeight { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackageHeight => PackageHeight != null;

        public decimal? PackageVolume { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackageVolume => PackageVolume != null;

        public decimal? PackageQty { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackageQty => PackageQty != null;

        public long? ParentPackageNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasParentPackageNum => ParentPackageNum != null;

        public bool? HasChildPackage { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasHasChildPackage => HasChildPackage != null;

        [StringLength(50, ErrorMessage = "The OrderShipmentUuid value cannot exceed 50 characters. ")]
        public string OrderShipmentUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderShipmentUuid => OrderShipmentUuid != null;

        [StringLength(50, ErrorMessage = "The OrderShipmentPackageUuid value cannot exceed 50 characters. ")]
        public string OrderShipmentPackageUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderShipmentPackageUuid => OrderShipmentPackageUuid != null;



        #endregion Properties - Generated 

        #region Children - Generated 

		public IList<OrderShipmentShippedItemDto> OrderShipmentShippedItem { get; set; }
		[XmlIgnore, JsonIgnore, IgnoreCompare]
		public bool HasOrderShipmentShippedItem => OrderShipmentShippedItem != null;
		
        #endregion Children - Generated 

    }
}



