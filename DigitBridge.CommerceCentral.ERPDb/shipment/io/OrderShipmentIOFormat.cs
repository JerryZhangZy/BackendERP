              
    

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
using CsvHelper;
using System.IO;
using DigitBridge.Base.Utility;
using System.Dynamic;
using System.Linq;
using CsvHelper.Configuration;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a OrderShipmentIOFormat Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class OrderShipmentIOFormat : CsvFormat
    {
        public OrderShipmentIOFormat() : base()
        {
            InitConfig();
			InitOrderShipmentHeader();
			InitOrderShipmentCanceledItem();
			InitOrderShipmentPackage();
        }

        protected virtual void InitConfig()
        {
            FormatNum = 1;
            FormatName = "OrderShipment Deafult Format";
            KeyName = "";
			DefaultKeyName = "OrderShipmentUuid";
            HasHeaderRecord = true;
        }

    
		protected virtual void InitOrderShipmentHeader()
		{
			var obj = this.InitParentObject<OrderShipmentHeaderDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("OrderShipmentNum", "", idx++, null, false),
				new CsvFormatColumn("DatabaseNum", "", idx++, null, false),
				new CsvFormatColumn("MasterAccountNum", "", idx++, null, false),
				new CsvFormatColumn("ProfileNum", "", idx++, null, false),
				new CsvFormatColumn("ChannelNum", "", idx++, null, false),
				new CsvFormatColumn("ChannelAccountNum", "", idx++, null, false),
				new CsvFormatColumn("OrderDCAssignmentNum", "", idx++, null, false),
				new CsvFormatColumn("DistributionCenterNum", "", idx++, null, false),
				new CsvFormatColumn("CentralOrderNum", "", idx++, null, false),
				new CsvFormatColumn("ChannelOrderID", "", idx++, null, false),
				new CsvFormatColumn("ShipmentID", "", idx++, null, false),
				new CsvFormatColumn("ShipmentType", "", idx++, null, false),
				new CsvFormatColumn("ShipmentReferenceID", "", idx++, null, false),
				new CsvFormatColumn("ShipmentDateUtc", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("ShippingCarrier", "", idx++, null, false),
				new CsvFormatColumn("ShippingClass", "", idx++, null, false),
				new CsvFormatColumn("ShippingCost", "", idx++, FormatType.Cost, false),
				new CsvFormatColumn("MainTrackingNumber", "", idx++, null, false),
				new CsvFormatColumn("MainReturnTrackingNumber", "", idx++, null, false),
				new CsvFormatColumn("BillOfLadingID", "", idx++, null, false),
				new CsvFormatColumn("TotalPackages", "", idx++, null, false),
				new CsvFormatColumn("TotalShippedQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("TotalCanceledQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("TotalWeight", "", idx++, null, false),
				new CsvFormatColumn("TotalVolume", "", idx++, null, false),
				new CsvFormatColumn("WeightUnit", "", idx++, null, false),
				new CsvFormatColumn("LengthUnit", "", idx++, null, false),
				new CsvFormatColumn("VolumeUnit", "", idx++, null, false),
				new CsvFormatColumn("ShipmentStatus", "", idx++, null, false),
				new CsvFormatColumn("DBChannelOrderHeaderRowID", "", idx++, null, false),
				new CsvFormatColumn("ProcessStatus", "", idx++, null, false),
				new CsvFormatColumn("ProcessDateUtc", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("OrderShipmentUuid", "", idx++, null, false),
				new CsvFormatColumn("RowNum", "", idx++, null, false),
				new CsvFormatColumn("InvoiceNumber", "", idx++, null, false),
				new CsvFormatColumn("InvoiceUuid", "", idx++, null, false),
				new CsvFormatColumn("SalesOrderUuid", "", idx++, null, false),
				new CsvFormatColumn("OrderNumber", "", idx++, null, false),
			};
		}



    
		protected virtual void InitOrderShipmentCanceledItem()
		{
			var obj = this.InitParentObject<OrderShipmentCanceledItemDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("OrderShipmentCanceledItemNum", "", idx++, null, false),
				new CsvFormatColumn("OrderDCAssignmentLineNum", "", idx++, null, false),
				new CsvFormatColumn("SKU", "", idx++, null, false),
				new CsvFormatColumn("CanceledQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("CancelCode", "", idx++, null, false),
				new CsvFormatColumn("CancelOtherReason", "", idx++, null, false),
				new CsvFormatColumn("DBChannelOrderLineRowID", "", idx++, null, false),
				new CsvFormatColumn("OrderShipmentCanceledItemUuid", "", idx++, null, false),
				new CsvFormatColumn("SalesOrderItemsUuid", "", idx++, null, false),
			};
		}



    
		protected virtual void InitOrderShipmentPackage()
		{
			var obj = this.InitParentObject<OrderShipmentPackageDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("OrderShipmentPackageNum", "", idx++, null, false),
				new CsvFormatColumn("PackageID", "", idx++, null, false),
				new CsvFormatColumn("PackageType", "", idx++, null, false),
				new CsvFormatColumn("PackagePatternNum", "", idx++, null, false),
				new CsvFormatColumn("PackageTrackingNumber", "", idx++, null, false),
				new CsvFormatColumn("PackageReturnTrackingNumber", "", idx++, null, false),
				new CsvFormatColumn("PackageWeight", "", idx++, null, false),
				new CsvFormatColumn("PackageLength", "", idx++, null, false),
				new CsvFormatColumn("PackageWidth", "", idx++, null, false),
				new CsvFormatColumn("PackageHeight", "", idx++, null, false),
				new CsvFormatColumn("PackageVolume", "", idx++, null, false),
				new CsvFormatColumn("PackageQty", "", idx++, FormatType.Qty, false),
				new CsvFormatColumn("ParentPackageNum", "", idx++, null, false),
				new CsvFormatColumn("HasChildPackage", "", idx++, null, false),
				new CsvFormatColumn("OrderShipmentPackageUuid", "", idx++, null, false),
			};
		}



    }
}



