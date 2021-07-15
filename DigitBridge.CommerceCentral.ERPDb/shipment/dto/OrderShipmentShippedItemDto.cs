
              
    

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
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a OrderShipmentShippedItem Dto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class OrderShipmentShippedItemDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

        public long? OrderShipmentShippedItemNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderShipmentShippedItemNum => OrderShipmentShippedItemNum != null;

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

        public long? OrderShipmentPackageNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderShipmentPackageNum => OrderShipmentPackageNum != null;

        [StringLength(130, ErrorMessage = "The ChannelOrderID value cannot exceed 130 characters. ")]
        public string ChannelOrderID { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasChannelOrderID => ChannelOrderID != null;

        public long? OrderDCAssignmentLineNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderDCAssignmentLineNum => OrderDCAssignmentLineNum != null;

        [StringLength(100, ErrorMessage = "The SKU value cannot exceed 100 characters. ")]
        public string SKU { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSKU => SKU != null;

        public decimal? ShippedQty { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShippedQty => ShippedQty != null;

        [StringLength(50, ErrorMessage = "The DBChannelOrderLineRowID value cannot exceed 50 characters. ")]
        public string DBChannelOrderLineRowID { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDBChannelOrderLineRowID => DBChannelOrderLineRowID != null;

        [StringLength(50, ErrorMessage = "The OrderShipmentUuid value cannot exceed 50 characters. ")]
        public string OrderShipmentUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderShipmentUuid => OrderShipmentUuid != null;

        [StringLength(50, ErrorMessage = "The OrderShipmentPackageUuid value cannot exceed 50 characters. ")]
        public string OrderShipmentPackageUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderShipmentPackageUuid => OrderShipmentPackageUuid != null;

        [StringLength(50, ErrorMessage = "The OrderShipmentShippedItemUuid value cannot exceed 50 characters. ")]
        public string OrderShipmentShippedItemUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderShipmentShippedItemUuid => OrderShipmentShippedItemUuid != null;



        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}



