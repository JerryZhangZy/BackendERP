    

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
    /// Represents a WarehouseTransferDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class WarehouseTransferDataDtoMapperDefault : IDtoMapper<WarehouseTransferData, WarehouseTransferDataDto> 
    {
        #region read from dto to data

        public virtual WarehouseTransferData ReadDto(WarehouseTransferData data, WarehouseTransferDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new WarehouseTransferData();
                data.New();
            }

			if (dto.WarehouseTransferHeader != null)
			{
				if (data.WarehouseTransferHeader is null)
					data.WarehouseTransferHeader = data.NewWarehouseTransferHeader();
				ReadWarehouseTransferHeader(data.WarehouseTransferHeader, dto.WarehouseTransferHeader);
			}
			if (dto.WarehouseTransferItems != null)
			{
				if (data.WarehouseTransferItems is null)
					data.WarehouseTransferItems = new List<WarehouseTransferItems>();
				var deleted = ReadWarehouseTransferItems(data.WarehouseTransferItems, dto.WarehouseTransferItems);
				data.SetWarehouseTransferItemsDeleted(deleted);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadWarehouseTransferHeader(WarehouseTransferHeader data, WarehouseTransferHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasWarehouseTransferUuid) data.WarehouseTransferUuid = dto.WarehouseTransferUuid;
			if (dto.HasBatchNumber) data.BatchNumber = dto.BatchNumber;
			if (dto.HasWarehouseTransferType) data.WarehouseTransferType = dto.WarehouseTransferType.ToInt();
			if (dto.HasWarehouseTransferStatus) data.WarehouseTransferStatus = dto.WarehouseTransferStatus.ToInt();
			if (dto.HasTransferDate) data.TransferDate = dto.TransferDate.ToDateTime();
			if (dto.HasTransferTime) data.TransferTime = dto.TransferTime.ToTimeSpan();
			if (dto.HasProcessor) data.Processor = dto.Processor;
			if (dto.HasReceiveDate) data.ReceiveDate = dto.ReceiveDate.ToDateTime();
			if (dto.HasReceiveTime) data.ReceiveTime = dto.ReceiveTime.ToTimeSpan();
			if (dto.HasReceiveProcessor) data.ReceiveProcessor = dto.ReceiveProcessor;
			if (dto.HasFromWarehouseUuid) data.FromWarehouseUuid = dto.FromWarehouseUuid;
			if (dto.HasFromWarehouseCode) data.FromWarehouseCode = dto.FromWarehouseCode;
			if (dto.HasToWarehouseUuid) data.ToWarehouseUuid = dto.ToWarehouseUuid;
			if (dto.HasToWarehouseCode) data.ToWarehouseCode = dto.ToWarehouseCode;
			if (dto.HasReferenceType) data.ReferenceType = dto.ReferenceType.ToInt();
			if (dto.HasReferenceUuid) data.ReferenceUuid = dto.ReferenceUuid;
			if (dto.HasReferenceNum) data.ReferenceNum = dto.ReferenceNum;
			if (dto.HasWarehouseTransferSourceCode) data.WarehouseTransferSourceCode = dto.WarehouseTransferSourceCode;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadWarehouseTransferItems(WarehouseTransferItems data, WarehouseTransferItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasWarehouseTransferItemsUuid) data.WarehouseTransferItemsUuid = dto.WarehouseTransferItemsUuid;
			if (dto.HasReferWarehouseTransferItemsUuid) data.ReferWarehouseTransferItemsUuid = dto.ReferWarehouseTransferItemsUuid;
			if (dto.HasWarehouseTransferUuid) data.WarehouseTransferUuid = dto.WarehouseTransferUuid;
			if (dto.HasSeq) data.Seq = dto.Seq.ToInt();
			if (dto.HasItemDate) data.ItemDate = dto.ItemDate.ToDateTime();
			if (dto.HasItemTime) data.ItemTime = dto.ItemTime.ToTimeSpan();
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;
			if (dto.HasFromInventoryUuid) data.FromInventoryUuid = dto.FromInventoryUuid;
			if (dto.HasFromWarehouseUuid) data.FromWarehouseUuid = dto.FromWarehouseUuid;
			if (dto.HasFromWarehouseCode) data.FromWarehouseCode = dto.FromWarehouseCode;
			if (dto.HasLotNum) data.LotNum = dto.LotNum;
			if (dto.HasToInventoryUuid) data.ToInventoryUuid = dto.ToInventoryUuid;
			if (dto.HasToWarehouseUuid) data.ToWarehouseUuid = dto.ToWarehouseUuid;
			if (dto.HasToWarehouseCode) data.ToWarehouseCode = dto.ToWarehouseCode;
			if (dto.HasToLotNum) data.ToLotNum = dto.ToLotNum;
			if (dto.HasDescription) data.Description = dto.Description;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasUOM) data.UOM = dto.UOM;
			if (dto.HasPackType) data.PackType = dto.PackType;
			if (dto.HasPackQty) data.PackQty = dto.PackQty.ToDecimal();
			if (dto.HasTransferPack) data.TransferPack = dto.TransferPack.ToDecimal();
			if (dto.HasTransferQty) data.TransferQty = dto.TransferQty.ToDecimal();
			if (dto.HasFromBeforeInstockPack) data.FromBeforeInstockPack = dto.FromBeforeInstockPack.ToDecimal();
			if (dto.HasFromBeforeInstockQty) data.FromBeforeInstockQty = dto.FromBeforeInstockQty.ToDecimal();
			if (dto.HasToBeforeInstockPack) data.ToBeforeInstockPack = dto.ToBeforeInstockPack.ToDecimal();
			if (dto.HasToBeforeInstockQty) data.ToBeforeInstockQty = dto.ToBeforeInstockQty.ToDecimal();
			if (dto.HasUnitCost) data.UnitCost = dto.UnitCost.ToDecimal();
			if (dto.HasAvgCost) data.AvgCost = dto.AvgCost.ToDecimal();
			if (dto.HasLotCost) data.LotCost = dto.LotCost.ToDecimal();
			if (dto.HasLotInDate) data.LotInDate = dto.LotInDate;
			if (dto.HasLotExpDate) data.LotExpDate = dto.LotExpDate;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}

		protected virtual IList<WarehouseTransferItems> ReadWarehouseTransferItems(IList<WarehouseTransferItems> data, IList<WarehouseTransferItemsDto> dto)
		{
			if (data is null || dto is null)
				return null;
			var lstOrig = new List<WarehouseTransferItems>(data.Where(x => x != null).ToList());
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = itemDto.RowNum > 0
					? lstOrig.Find(x => x.RowNum == itemDto.RowNum)
					: lstOrig.Find(x => x.WarehouseTransferItemsUuid == itemDto.WarehouseTransferItemsUuid);
				if (obj is null)
					obj = new WarehouseTransferItems().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadWarehouseTransferItems(obj, itemDto);

			}
			return lstOrig;
		}



        #endregion read from dto to data

        #region write to dto from data

        public virtual WarehouseTransferDataDto WriteDto(WarehouseTransferData data, WarehouseTransferDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new WarehouseTransferDataDto();

            data.CheckIntegrity();

			if (data.WarehouseTransferHeader != null)
			{
				dto.WarehouseTransferHeader = new WarehouseTransferHeaderDto();
				WriteWarehouseTransferHeader(data.WarehouseTransferHeader, dto.WarehouseTransferHeader);
			}
			if (data.WarehouseTransferItems != null)
			{
				dto.WarehouseTransferItems = new List<WarehouseTransferItemsDto>();
				WriteWarehouseTransferItems(data.WarehouseTransferItems, dto.WarehouseTransferItems);
			}
            return dto;
        }

		protected virtual void WriteWarehouseTransferHeader(WarehouseTransferHeader data, WarehouseTransferHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.WarehouseTransferUuid = data.WarehouseTransferUuid;
			dto.BatchNumber = data.BatchNumber;
			dto.WarehouseTransferType = data.WarehouseTransferType;
			dto.WarehouseTransferStatus = data.WarehouseTransferStatus;
			dto.TransferDate = data.TransferDate;
			dto.TransferTime = data.TransferTime.ToDateTime();
			dto.Processor = data.Processor;
			dto.ReceiveDate = data.ReceiveDate;
			dto.ReceiveTime = data.ReceiveTime.ToDateTime();
			dto.ReceiveProcessor = data.ReceiveProcessor;
			dto.FromWarehouseUuid = data.FromWarehouseUuid;
			dto.FromWarehouseCode = data.FromWarehouseCode;
			dto.ToWarehouseUuid = data.ToWarehouseUuid;
			dto.ToWarehouseCode = data.ToWarehouseCode;
			dto.ReferenceType = data.ReferenceType;
			dto.ReferenceUuid = data.ReferenceUuid;
			dto.ReferenceNum = data.ReferenceNum;
			dto.WarehouseTransferSourceCode = data.WarehouseTransferSourceCode;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteWarehouseTransferItems(WarehouseTransferItems data, WarehouseTransferItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.WarehouseTransferItemsUuid = data.WarehouseTransferItemsUuid;
			dto.ReferWarehouseTransferItemsUuid = data.ReferWarehouseTransferItemsUuid;
			dto.WarehouseTransferUuid = data.WarehouseTransferUuid;
			dto.Seq = data.Seq;
			dto.ItemDate = data.ItemDate;
			dto.ItemTime = data.ItemTime.ToDateTime();
			dto.SKU = data.SKU;
			dto.ProductUuid = data.ProductUuid;
			dto.FromInventoryUuid = data.FromInventoryUuid;
			dto.FromWarehouseUuid = data.FromWarehouseUuid;
			dto.FromWarehouseCode = data.FromWarehouseCode;
			dto.LotNum = data.LotNum;
			dto.ToInventoryUuid = data.ToInventoryUuid;
			dto.ToWarehouseUuid = data.ToWarehouseUuid;
			dto.ToWarehouseCode = data.ToWarehouseCode;
			dto.ToLotNum = data.ToLotNum;
			dto.Description = data.Description;
			dto.Notes = data.Notes;
			dto.UOM = data.UOM;
			dto.PackType = data.PackType;
			dto.PackQty = data.PackQty;
			dto.TransferPack = data.TransferPack;
			dto.TransferQty = data.TransferQty;
			dto.FromBeforeInstockPack = data.FromBeforeInstockPack;
			dto.FromBeforeInstockQty = data.FromBeforeInstockQty;
			dto.ToBeforeInstockPack = data.ToBeforeInstockPack;
			dto.ToBeforeInstockQty = data.ToBeforeInstockQty;
			dto.UnitCost = data.UnitCost;
			dto.AvgCost = data.AvgCost;
			dto.LotCost = data.LotCost;
			dto.LotInDate = data.LotInDate;
			dto.LotExpDate = data.LotExpDate;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}
		protected virtual void WriteWarehouseTransferItems(IList<WarehouseTransferItems> data, IList<WarehouseTransferItemsDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			#region write all list items and properties with null

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new WarehouseTransferItemsDto();
				dto.Add(obj);
				WriteWarehouseTransferItems(itemData, obj);
			}

			#endregion write all list items and properties with null
			return;
		}



        #endregion write to dto from data

    }
}


