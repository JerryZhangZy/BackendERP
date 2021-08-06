    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a OrderShipmentDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class OrderShipmentDataDtoMapperDefault : IDtoMapper<OrderShipmentData, OrderShipmentDataDto> 
    {
        #region read from dto to data

        public virtual OrderShipmentData ReadDto(OrderShipmentData data, OrderShipmentDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new OrderShipmentData();
                data.New();
            }

			if (dto.OrderShipmentHeader != null)
			{
				if (data.OrderShipmentHeader is null)
					data.OrderShipmentHeader = data.NewOrderShipmentHeader();
				ReadOrderShipmentHeader(data.OrderShipmentHeader, dto.OrderShipmentHeader);
			}
			if (dto.OrderShipmentCanceledItem != null)
			{
				if (data.OrderShipmentCanceledItem is null)
					data.OrderShipmentCanceledItem = new List<OrderShipmentCanceledItem>();
				var deleted = ReadOrderShipmentCanceledItem(data.OrderShipmentCanceledItem, dto.OrderShipmentCanceledItem);
				data.SetOrderShipmentCanceledItemDeleted(deleted);
			}
			if (dto.OrderShipmentPackage != null)
			{
				if (data.OrderShipmentPackage is null)
					data.OrderShipmentPackage = new List<OrderShipmentPackage>();
				var deleted = ReadOrderShipmentPackage(data.OrderShipmentPackage, dto.OrderShipmentPackage);
				data.SetOrderShipmentPackageDeleted(deleted);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadOrderShipmentHeader(OrderShipmentHeader data, OrderShipmentHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			//if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			//if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			//if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasChannelNum) data.ChannelNum = dto.ChannelNum.ToInt();
			if (dto.HasChannelAccountNum) data.ChannelAccountNum = dto.ChannelAccountNum.ToInt();
			if (dto.HasOrderDCAssignmentNum) data.OrderDCAssignmentNum = dto.OrderDCAssignmentNum;
			if (dto.HasDistributionCenterNum) data.DistributionCenterNum = dto.DistributionCenterNum;
			if (dto.HasCentralOrderNum) data.CentralOrderNum = dto.CentralOrderNum;
			if (dto.HasChannelOrderID) data.ChannelOrderID = dto.ChannelOrderID;
			if (dto.HasShipmentID) data.ShipmentID = dto.ShipmentID;
			if (dto.HasWarehouseID) data.WarehouseID = dto.WarehouseID;
			if (dto.HasShipmentType) data.ShipmentType = dto.ShipmentType;
			if (dto.HasShipmentReferenceID) data.ShipmentReferenceID = dto.ShipmentReferenceID;
			if (dto.HasShipmentDateUtc) data.ShipmentDateUtc = dto.ShipmentDateUtc;
			if (dto.HasShippingCarrier) data.ShippingCarrier = dto.ShippingCarrier;
			if (dto.HasShippingClass) data.ShippingClass = dto.ShippingClass;
			if (dto.HasShippingCost) data.ShippingCost = dto.ShippingCost;
			if (dto.HasMainTrackingNumber) data.MainTrackingNumber = dto.MainTrackingNumber;
			if (dto.HasMainReturnTrackingNumber) data.MainReturnTrackingNumber = dto.MainReturnTrackingNumber;
			if (dto.HasBillOfLadingID) data.BillOfLadingID = dto.BillOfLadingID;
			if (dto.HasTotalPackages) data.TotalPackages = dto.TotalPackages;
			if (dto.HasTotalShippedQty) data.TotalShippedQty = dto.TotalShippedQty;
			if (dto.HasTotalCanceledQty) data.TotalCanceledQty = dto.TotalCanceledQty;
			if (dto.HasTotalWeight) data.TotalWeight = dto.TotalWeight;
			if (dto.HasTotalVolume) data.TotalVolume = dto.TotalVolume;
			if (dto.HasWeightUnit) data.WeightUnit = dto.WeightUnit;
			if (dto.HasLengthUnit) data.LengthUnit = dto.LengthUnit;
			if (dto.HasVolumeUnit) data.VolumeUnit = dto.VolumeUnit;
			if (dto.HasShipmentStatus) data.ShipmentStatus = dto.ShipmentStatus;
			if (dto.HasDBChannelOrderHeaderRowID) data.DBChannelOrderHeaderRowID = dto.DBChannelOrderHeaderRowID;
			if (dto.HasProcessStatus) data.ProcessStatus = dto.ProcessStatus;
			if (dto.HasProcessDateUtc) data.ProcessDateUtc = dto.ProcessDateUtc;
			if (dto.HasOrderShipmentUuid) data.OrderShipmentUuid = dto.OrderShipmentUuid;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadOrderShipmentCanceledItem(OrderShipmentCanceledItem data, OrderShipmentCanceledItemDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			//if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			//if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			//if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasChannelNum) data.ChannelNum = dto.ChannelNum.ToInt();
			if (dto.HasChannelAccountNum) data.ChannelAccountNum = dto.ChannelAccountNum.ToInt();
			if (dto.HasOrderShipmentNum) data.OrderShipmentNum = dto.OrderShipmentNum;
			if (dto.HasChannelOrderID) data.ChannelOrderID = dto.ChannelOrderID;
			if (dto.HasOrderDCAssignmentLineNum) data.OrderDCAssignmentLineNum = dto.OrderDCAssignmentLineNum;
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasCanceledQty) data.CanceledQty = dto.CanceledQty.ToDecimal();
			if (dto.HasCancelCode) data.CancelCode = dto.CancelCode;
			if (dto.HasCancelOtherReason) data.CancelOtherReason = dto.CancelOtherReason;
			if (dto.HasDBChannelOrderLineRowID) data.DBChannelOrderLineRowID = dto.DBChannelOrderLineRowID;
			if (dto.HasOrderShipmentUuid) data.OrderShipmentUuid = dto.OrderShipmentUuid;
			if (dto.HasOrderShipmentCanceledItemUuid) data.OrderShipmentCanceledItemUuid = dto.OrderShipmentCanceledItemUuid;
			if (dto.HasOrderShipmentCanceledItemNum) data.OrderShipmentCanceledItemNum = dto.OrderShipmentCanceledItemNum.Value;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}

		protected virtual IList<OrderShipmentCanceledItem> ReadOrderShipmentCanceledItem(IList<OrderShipmentCanceledItem> data, IList<OrderShipmentCanceledItemDto> dto)
		{
			if (data is null || dto is null)
				return null;
			var lstOrig = new List<OrderShipmentCanceledItem>(data.Where(x => x != null).ToList());
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = itemDto.RowNum > 0
					? lstOrig.Find(x => x.RowNum == itemDto.RowNum)
					: lstOrig.Find(x => x.OrderShipmentCanceledItemUuid == itemDto.OrderShipmentCanceledItemUuid);
				if (obj is null)
					obj = new OrderShipmentCanceledItem().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadOrderShipmentCanceledItem(obj, itemDto);

			}
			return lstOrig;
		}


		protected virtual void ReadOrderShipmentPackage(OrderShipmentPackage data, OrderShipmentPackageDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			//if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			//if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			//if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasChannelNum) data.ChannelNum = dto.ChannelNum.ToInt();
			if (dto.HasChannelAccountNum) data.ChannelAccountNum = dto.ChannelAccountNum.ToInt();
			if (dto.HasOrderShipmentNum) data.OrderShipmentNum = dto.OrderShipmentNum;
			if (dto.HasPackageID) data.PackageID = dto.PackageID;
			if (dto.HasPackageType) data.PackageType = dto.PackageType;
			if (dto.HasPackagePatternNum) data.PackagePatternNum = dto.PackagePatternNum;
			if (dto.HasPackageTrackingNumber) data.PackageTrackingNumber = dto.PackageTrackingNumber;
			if (dto.HasPackageReturnTrackingNumber) data.PackageReturnTrackingNumber = dto.PackageReturnTrackingNumber;
			if (dto.HasPackageWeight) data.PackageWeight = dto.PackageWeight;
			if (dto.HasPackageLength) data.PackageLength = dto.PackageLength;
			if (dto.HasPackageWidth) data.PackageWidth = dto.PackageWidth;
			if (dto.HasPackageHeight) data.PackageHeight = dto.PackageHeight;
			if (dto.HasPackageVolume) data.PackageVolume = dto.PackageVolume;
			if (dto.HasPackageQty) data.PackageQty = dto.PackageQty;
			if (dto.HasParentPackageNum) data.ParentPackageNum = dto.ParentPackageNum;
			if (dto.HasOrderShipmentPackageNum) data.OrderShipmentPackageNum = dto.OrderShipmentPackageNum.Value;
			if (dto.HasHasChildPackage) data.HasChildPackage = dto.HasChildPackage;
			if (dto.HasOrderShipmentUuid) data.OrderShipmentUuid = dto.OrderShipmentUuid;
			if (dto.HasOrderShipmentPackageUuid) data.OrderShipmentPackageUuid = dto.OrderShipmentPackageUuid;

			#endregion read properties

			#region read all grand children object

			if (dto.OrderShipmentShippedItem != null)
			{
				if (data.OrderShipmentShippedItem is null)
					data.OrderShipmentShippedItem = new List<OrderShipmentShippedItem>();
				ReadOrderShipmentShippedItem(data.OrderShipmentShippedItem, dto.OrderShipmentShippedItem);
			}

			#endregion read all grand children object

			data.CheckIntegrity();
			return;
		}

		protected virtual IList<OrderShipmentPackage> ReadOrderShipmentPackage(IList<OrderShipmentPackage> data, IList<OrderShipmentPackageDto> dto)
		{
			if (data is null || dto is null)
				return null;
			var lstOrig = new List<OrderShipmentPackage>(data.Where(x => x != null).ToList());
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = itemDto.RowNum > 0
					? lstOrig.Find(x => x.RowNum == itemDto.RowNum)
					: lstOrig.Find(x => x.OrderShipmentPackageUuid == itemDto.OrderShipmentPackageUuid);
				if (obj is null)
					obj = new OrderShipmentPackage().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadOrderShipmentPackage(obj, itemDto);

			}
			return lstOrig;
		}

		protected virtual void ReadOrderShipmentShippedItem(OrderShipmentShippedItem data, OrderShipmentShippedItemDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			//if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			//if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			//if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasChannelNum) data.ChannelNum = dto.ChannelNum.ToInt();
			if (dto.HasChannelAccountNum) data.ChannelAccountNum = dto.ChannelAccountNum.ToInt();
			if (dto.HasOrderShipmentNum) data.OrderShipmentNum = dto.OrderShipmentNum;
			if (dto.HasOrderShipmentPackageNum) data.OrderShipmentPackageNum = dto.OrderShipmentPackageNum;
			if (dto.HasChannelOrderID) data.ChannelOrderID = dto.ChannelOrderID;
			if (dto.HasOrderDCAssignmentLineNum) data.OrderDCAssignmentLineNum = dto.OrderDCAssignmentLineNum;
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasShippedQty) data.ShippedQty = dto.ShippedQty.ToDecimal();
			if (dto.HasDBChannelOrderLineRowID) data.DBChannelOrderLineRowID = dto.DBChannelOrderLineRowID;
			if (dto.HasOrderShipmentUuid) data.OrderShipmentUuid = dto.OrderShipmentUuid;
			if (dto.HasOrderShipmentPackageUuid) data.OrderShipmentPackageUuid = dto.OrderShipmentPackageUuid;
			if (dto.HasOrderShipmentShippedItemUuid) data.OrderShipmentShippedItemUuid = dto.OrderShipmentShippedItemUuid;
			if (dto.HasOrderShipmentShippedItemNum) data.OrderShipmentShippedItemNum = dto.OrderShipmentShippedItemNum.Value;
			#endregion read properties

			return;
		}
		protected virtual IList<OrderShipmentShippedItem> ReadOrderShipmentShippedItem(IList<OrderShipmentShippedItem> data, IList<OrderShipmentShippedItemDto> dto)
		{
			if (data is null || dto is null)
				return data;

			var lstOrig = new List<OrderShipmentShippedItem>(data.Where(x => x != null));
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = itemDto.RowNum > 0
					? lstOrig.FirstOrDefault(x => x.RowNum == itemDto.RowNum)
					: lstOrig.FirstOrDefault(x => x.OrderShipmentPackageUuid == itemDto.OrderShipmentPackageUuid);
				if (obj is null)
					obj = new OrderShipmentShippedItem().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadOrderShipmentShippedItem(obj, itemDto);

			}

			return lstOrig;
		}


        #endregion read from dto to data

        #region write to dto from data

        public virtual OrderShipmentDataDto WriteDto(OrderShipmentData data, OrderShipmentDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new OrderShipmentDataDto();

            data.CheckIntegrity();

			if (data.OrderShipmentHeader != null)
			{
				dto.OrderShipmentHeader = new OrderShipmentHeaderDto();
				WriteOrderShipmentHeader(data.OrderShipmentHeader, dto.OrderShipmentHeader);
			}
			if (data.OrderShipmentCanceledItem != null)
			{
				dto.OrderShipmentCanceledItem = new List<OrderShipmentCanceledItemDto>();
				WriteOrderShipmentCanceledItem(data.OrderShipmentCanceledItem, dto.OrderShipmentCanceledItem);
			}
			if (data.OrderShipmentPackage != null)
			{
				dto.OrderShipmentPackage = new List<OrderShipmentPackageDto>();
				WriteOrderShipmentPackage(data.OrderShipmentPackage, dto.OrderShipmentPackage);
			}
            return dto;
        }

		protected virtual void WriteOrderShipmentHeader(OrderShipmentHeader data, OrderShipmentHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.OrderShipmentNum = data.OrderShipmentNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.ChannelNum = data.ChannelNum;
			dto.ChannelAccountNum = data.ChannelAccountNum;
			dto.OrderDCAssignmentNum = data.OrderDCAssignmentNum;
			dto.DistributionCenterNum = data.DistributionCenterNum;
			dto.CentralOrderNum = data.CentralOrderNum;
			dto.ChannelOrderID = data.ChannelOrderID;
			dto.ShipmentID = data.ShipmentID;
			dto.WarehouseID = data.WarehouseID;
			dto.ShipmentType = data.ShipmentType;
			dto.ShipmentReferenceID = data.ShipmentReferenceID;
			dto.ShipmentDateUtc = data.ShipmentDateUtc;
			dto.ShippingCarrier = data.ShippingCarrier;
			dto.ShippingClass = data.ShippingClass;
			dto.ShippingCost = data.ShippingCost;
			dto.MainTrackingNumber = data.MainTrackingNumber;
			dto.MainReturnTrackingNumber = data.MainReturnTrackingNumber;
			dto.BillOfLadingID = data.BillOfLadingID;
			dto.TotalPackages = data.TotalPackages;
			dto.TotalShippedQty = data.TotalShippedQty;
			dto.TotalCanceledQty = data.TotalCanceledQty;
			dto.TotalWeight = data.TotalWeight;
			dto.TotalVolume = data.TotalVolume;
			dto.WeightUnit = data.WeightUnit;
			dto.LengthUnit = data.LengthUnit;
			dto.VolumeUnit = data.VolumeUnit;
			dto.ShipmentStatus = data.ShipmentStatus;
			dto.DBChannelOrderHeaderRowID = data.DBChannelOrderHeaderRowID;
			dto.ProcessStatus = data.ProcessStatus;
			dto.ProcessDateUtc = data.ProcessDateUtc;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.OrderShipmentUuid = data.OrderShipmentUuid;
			dto.RowNum = data.RowNum;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteOrderShipmentCanceledItem(OrderShipmentCanceledItem data, OrderShipmentCanceledItemDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.OrderShipmentCanceledItemNum = data.OrderShipmentCanceledItemNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.ChannelNum = data.ChannelNum;
			dto.ChannelAccountNum = data.ChannelAccountNum;
			dto.OrderShipmentNum = data.OrderShipmentNum;
			dto.ChannelOrderID = data.ChannelOrderID;
			dto.OrderDCAssignmentLineNum = data.OrderDCAssignmentLineNum;
			dto.SKU = data.SKU;
			dto.CanceledQty = data.CanceledQty;
			dto.CancelCode = data.CancelCode;
			dto.CancelOtherReason = data.CancelOtherReason;
			dto.DBChannelOrderLineRowID = data.DBChannelOrderLineRowID;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.OrderShipmentUuid = data.OrderShipmentUuid;
			dto.OrderShipmentCanceledItemUuid = data.OrderShipmentCanceledItemUuid;
			dto.RowNum = data.RowNum;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}
		protected virtual void WriteOrderShipmentCanceledItem(IList<OrderShipmentCanceledItem> data, IList<OrderShipmentCanceledItemDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			#region write all list items and properties with null

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new OrderShipmentCanceledItemDto();
				dto.Add(obj);
				WriteOrderShipmentCanceledItem(itemData, obj);
			}

			#endregion write all list items and properties with null
			return;
		}


		protected virtual void WriteOrderShipmentPackage(OrderShipmentPackage data, OrderShipmentPackageDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.OrderShipmentPackageNum = data.OrderShipmentPackageNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.ChannelNum = data.ChannelNum;
			dto.ChannelAccountNum = data.ChannelAccountNum;
			dto.OrderShipmentNum = data.OrderShipmentNum;
			dto.PackageID = data.PackageID;
			dto.PackageType = data.PackageType;
			dto.PackagePatternNum = data.PackagePatternNum;
			dto.PackageTrackingNumber = data.PackageTrackingNumber;
			dto.PackageReturnTrackingNumber = data.PackageReturnTrackingNumber;
			dto.PackageWeight = data.PackageWeight;
			dto.PackageLength = data.PackageLength;
			dto.PackageWidth = data.PackageWidth;
			dto.PackageHeight = data.PackageHeight;
			dto.PackageVolume = data.PackageVolume;
			dto.PackageQty = data.PackageQty;
			dto.ParentPackageNum = data.ParentPackageNum;
			dto.HasChildPackage = data.HasChildPackage;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.OrderShipmentUuid = data.OrderShipmentUuid;
			dto.OrderShipmentPackageUuid = data.OrderShipmentPackageUuid;
			dto.RowNum = data.RowNum;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			#region write all grand children object

			if (data.OrderShipmentShippedItem != null)
			{
				dto.OrderShipmentShippedItem = new List<OrderShipmentShippedItemDto>();
				WriteOrderShipmentShippedItem(data.OrderShipmentShippedItem, dto.OrderShipmentShippedItem);
			}

			#endregion write all grand children object

			return;
		}
		protected virtual void WriteOrderShipmentPackage(IList<OrderShipmentPackage> data, IList<OrderShipmentPackageDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			#region write all list items and properties with null

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new OrderShipmentPackageDto();
				dto.Add(obj);
				WriteOrderShipmentPackage(itemData, obj);
			}

			#endregion write all list items and properties with null
			return;
		}


		protected virtual void WriteOrderShipmentShippedItem(OrderShipmentShippedItem data, OrderShipmentShippedItemDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.OrderShipmentShippedItemNum = data.OrderShipmentShippedItemNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.ChannelNum = data.ChannelNum;
			dto.ChannelAccountNum = data.ChannelAccountNum;
			dto.OrderShipmentNum = data.OrderShipmentNum;
			dto.OrderShipmentPackageNum = data.OrderShipmentPackageNum;
			dto.ChannelOrderID = data.ChannelOrderID;
			dto.OrderDCAssignmentLineNum = data.OrderDCAssignmentLineNum;
			dto.SKU = data.SKU;
			dto.ShippedQty = data.ShippedQty;
			dto.DBChannelOrderLineRowID = data.DBChannelOrderLineRowID;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.OrderShipmentUuid = data.OrderShipmentUuid;
			dto.OrderShipmentPackageUuid = data.OrderShipmentPackageUuid;
			dto.OrderShipmentShippedItemUuid = data.OrderShipmentShippedItemUuid;
			dto.RowNum = data.RowNum;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}
		protected virtual void WriteOrderShipmentShippedItem(IList<OrderShipmentShippedItem> data, IList<OrderShipmentShippedItemDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new OrderShipmentShippedItemDto();
				dto.Add(obj);
				WriteOrderShipmentShippedItem(itemData, obj);
			}
			return;
		}

        #endregion write to dto from data

    }
}



