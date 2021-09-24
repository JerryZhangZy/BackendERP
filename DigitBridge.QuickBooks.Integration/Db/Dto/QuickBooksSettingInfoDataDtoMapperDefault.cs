    

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
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Represents a QuickBooksSettingInfoDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class QuickBooksSettingInfoDataDtoMapperDefault : IDtoMapper<QuickBooksSettingInfoData, QuickBooksSettingInfoDataDto> 
    {
        #region read from dto to data

        public virtual QuickBooksSettingInfoData ReadDto(QuickBooksSettingInfoData data, QuickBooksSettingInfoDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new QuickBooksSettingInfoData();
                data.New();
            }

			if (dto.QuickBooksChnlAccSetting != null)
			{
				if (data.QuickBooksChnlAccSetting is null)
					data.QuickBooksChnlAccSetting = data.NewQuickBooksChnlAccSetting();
				ReadQuickBooksChnlAccSetting(data.QuickBooksChnlAccSetting, dto.QuickBooksChnlAccSetting);
			}
			if (dto.QuickBooksIntegrationSetting != null)
			{
				if (data.QuickBooksIntegrationSetting is null)
					data.QuickBooksIntegrationSetting = data.NewQuickBooksIntegrationSetting();
				ReadQuickBooksIntegrationSetting(data.QuickBooksIntegrationSetting, dto.QuickBooksIntegrationSetting);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadQuickBooksChnlAccSetting(QuickBooksChnlAccSetting data, QuickBooksChnlAccSettingDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasSettingUuid) data.SettingUuid = dto.SettingUuid;
			if (dto.HasChannelAccountName) data.ChannelAccountName = dto.ChannelAccountName;
			if (dto.HasChannelAccountNum) data.ChannelAccountNum = dto.ChannelAccountNum.ToInt();
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);
			if (dto.HasLastUpdate) data.LastUpdate = dto.LastUpdate;
			if (dto.HasDailySummaryLastExport) data.DailySummaryLastExport = dto.DailySummaryLastExport;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadQuickBooksIntegrationSetting(QuickBooksIntegrationSetting data, QuickBooksIntegrationSettingDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasSettingUuid) data.SettingUuid = dto.SettingUuid;
			if (dto.HasChannelAccountName) data.ChannelAccountName = dto.ChannelAccountName;
			if (dto.HasChannelAccountNum) data.ChannelAccountNum = dto.ChannelAccountNum.ToInt();
			if (dto.HasExportByOrderStatus) data.ExportByOrderStatus = dto.ExportByOrderStatus.ToInt();
			if (dto.HasExportOrderAs) data.ExportOrderAs = dto.ExportOrderAs.ToInt();
			if (dto.HasExportOrderDateType) data.ExportOrderDateType = dto.ExportOrderDateType.ToInt();
			if (dto.HasExportOrderFromDate) data.ExportOrderFromDate = dto.ExportOrderFromDate.ToDateTime();
			if (dto.HasQboSettingStatus) data.QboSettingStatus = dto.QboSettingStatus;
			if (dto.HasQboImportOrderAfterUpdateDate) data.QboImportOrderAfterUpdateDate = dto.QboImportOrderAfterUpdateDate.ToDateTime();
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);
			if (dto.HasEnterDate) data.EnterDate = dto.EnterDate;
			if (dto.HasLastUpdate) data.LastUpdate = dto.LastUpdate;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}



        #endregion read from dto to data

        #region write to dto from data

        public virtual QuickBooksSettingInfoDataDto WriteDto(QuickBooksSettingInfoData data, QuickBooksSettingInfoDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new QuickBooksSettingInfoDataDto();

            data.CheckIntegrity();

			if (data.QuickBooksChnlAccSetting != null)
			{
				dto.QuickBooksChnlAccSetting = new QuickBooksChnlAccSettingDto();
				WriteQuickBooksChnlAccSetting(data.QuickBooksChnlAccSetting, dto.QuickBooksChnlAccSetting);
			}
			if (data.QuickBooksIntegrationSetting != null)
			{
				dto.QuickBooksIntegrationSetting = new QuickBooksIntegrationSettingDto();
				WriteQuickBooksIntegrationSetting(data.QuickBooksIntegrationSetting, dto.QuickBooksIntegrationSetting);
			}
            return dto;
        }

		protected virtual void WriteQuickBooksChnlAccSetting(QuickBooksChnlAccSetting data, QuickBooksChnlAccSettingDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.SettingUuid = data.SettingUuid;
			dto.ChannelAccountName = data.ChannelAccountName;
			dto.ChannelAccountNum = data.ChannelAccountNum;
			dto.Fields = data.Fields.ToJson();
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.LastUpdate = data.LastUpdate;
			dto.DailySummaryLastExport = data.DailySummaryLastExport;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteQuickBooksIntegrationSetting(QuickBooksIntegrationSetting data, QuickBooksIntegrationSettingDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.SettingUuid = data.SettingUuid;
			dto.ChannelAccountName = data.ChannelAccountName;
			dto.ChannelAccountNum = data.ChannelAccountNum;
			dto.ExportByOrderStatus = data.ExportByOrderStatus;
			dto.ExportOrderAs = data.ExportOrderAs;
			dto.ExportOrderDateType = data.ExportOrderDateType;
			dto.ExportOrderFromDate = data.ExportOrderFromDate;
			dto.QboSettingStatus = data.QboSettingStatus;
			dto.QboImportOrderAfterUpdateDate = data.QboImportOrderAfterUpdateDate;
			dto.Fields = data.Fields.ToJson();
			dto.EnterDate = data.EnterDate;
			dto.LastUpdate = data.LastUpdate;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}


        #endregion write to dto from data

    }
}



