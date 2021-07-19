    

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
    /// Represents a InventoryLogDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class InventoryLogDataDtoMapperDefault : IDtoMapper<InventoryLogData, InventoryLogDataDto> 
    {
        #region read from dto to data

        public virtual InventoryLogData ReadDto(InventoryLogData data, InventoryLogDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new InventoryLogData();
                data.New();
            }

			if (dto.InventoryLog != null)
			{
				if (data.InventoryLog is null)
					data.InventoryLog = data.NewInventoryLog();
				ReadInventoryLog(data.InventoryLog, dto.InventoryLog);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadInventoryLog(InventoryLog data, InventoryLogDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasInventoryLogUuid) data.InventoryLogUuid = dto.InventoryLogUuid;
			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;
			if (dto.HasInventoryUuid) data.InventoryUuid = dto.InventoryUuid;
			if (dto.HasBatchNum) data.BatchNum = dto.BatchNum;
			if (dto.HasLogType) data.LogType = dto.LogType;
			if (dto.HasLogUuid) data.LogUuid = dto.LogUuid;
			if (dto.HasLogNumber) data.LogNumber = dto.LogNumber;
			if (dto.HasLogItemUuid) data.LogItemUuid = dto.LogItemUuid;
			if (dto.HasLogStatus) data.LogStatus = dto.LogStatus;
			if (dto.HasLogDate) data.LogDate = dto.LogDate.ToDateTime();
			if (dto.HasLogTime) data.LogTime = dto.LogTime.ToTimeSpan();
			if (dto.HasLogBy) data.LogBy = dto.LogBy;
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasDescription) data.Description = dto.Description;
			if (dto.HasWarehouseCode) data.WarehouseCode = dto.WarehouseCode;
			if (dto.HasLotNum) data.LotNum = dto.LotNum;
			if (dto.HasLotInDate) data.LotInDate = dto.LotInDate;
			if (dto.HasLotExpDate) data.LotExpDate = dto.LotExpDate;
			if (dto.HasLpnNum) data.LpnNum = dto.LpnNum;
			if (dto.HasStyleCode) data.StyleCode = dto.StyleCode;
			if (dto.HasColorPatternCode) data.ColorPatternCode = dto.ColorPatternCode;
			if (dto.HasSizeCode) data.SizeCode = dto.SizeCode;
			if (dto.HasWidthCode) data.WidthCode = dto.WidthCode;
			if (dto.HasLengthCode) data.LengthCode = dto.LengthCode;
			if (dto.HasUOM) data.UOM = dto.UOM;
			if (dto.HasLogQty) data.LogQty = dto.LogQty.ToDecimal();
			if (dto.HasBeforeInstock) data.BeforeInstock = dto.BeforeInstock.ToDecimal();
			if (dto.HasBeforeBaseCost) data.BeforeBaseCost = dto.BeforeBaseCost.ToDecimal();
			if (dto.HasBeforeUnitCost) data.BeforeUnitCost = dto.BeforeUnitCost.ToDecimal();
			if (dto.HasBeforeAvgCost) data.BeforeAvgCost = dto.BeforeAvgCost.ToDecimal();
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;

			#endregion read properties

			return;
		}



        #endregion read from dto to data

        #region write to dto from data

        public virtual InventoryLogDataDto WriteDto(InventoryLogData data, InventoryLogDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new InventoryLogDataDto();

            data.CheckIntegrity();

			if (data.InventoryLog != null)
			{
				dto.InventoryLog = new InventoryLogDto();
				WriteInventoryLog(data.InventoryLog, dto.InventoryLog);
			}
            return dto;
        }

		protected virtual void WriteInventoryLog(InventoryLog data, InventoryLogDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.InventoryLogUuid = data.InventoryLogUuid;
			dto.ProductUuid = data.ProductUuid;
			dto.InventoryUuid = data.InventoryUuid;
			dto.BatchNum = data.BatchNum;
			dto.LogType = data.LogType;
			dto.LogUuid = data.LogUuid;
			dto.LogNumber = data.LogNumber;
			dto.LogItemUuid = data.LogItemUuid;
			dto.LogStatus = data.LogStatus;
			dto.LogDate = data.LogDate;
			dto.LogTime = data.LogTime.ToDateTime();
			dto.LogBy = data.LogBy;
			dto.SKU = data.SKU;
			dto.Description = data.Description;
			dto.WarehouseCode = data.WarehouseCode;
			dto.LotNum = data.LotNum;
			dto.LotInDate = data.LotInDate;
			dto.LotExpDate = data.LotExpDate;
			dto.LpnNum = data.LpnNum;
			dto.StyleCode = data.StyleCode;
			dto.ColorPatternCode = data.ColorPatternCode;
			dto.SizeCode = data.SizeCode;
			dto.WidthCode = data.WidthCode;
			dto.LengthCode = data.LengthCode;
			dto.UOM = data.UOM;
			dto.LogQty = data.LogQty;
			dto.BeforeInstock = data.BeforeInstock;
			dto.BeforeBaseCost = data.BeforeBaseCost;
			dto.BeforeUnitCost = data.BeforeUnitCost;
			dto.BeforeAvgCost = data.BeforeAvgCost;
			dto.EnterBy = data.EnterBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}


        #endregion write to dto from data

    }
}


