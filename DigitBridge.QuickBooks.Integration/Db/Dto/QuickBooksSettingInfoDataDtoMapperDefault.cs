    

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

			if (dto.QuickBooksSettingInfo != null)
			{
				if (data.QuickBooksSettingInfo is null)
					data.QuickBooksSettingInfo = data.NewQuickBooksSettingInfo();
				ReadQuickBooksSettingInfo(data.QuickBooksSettingInfo, dto.QuickBooksSettingInfo);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadQuickBooksSettingInfo(QuickBooksSettingInfo data, QuickBooksSettingInfoDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasSettingUuid) data.SettingUuid = dto.SettingUuid;
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;

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

			if (data.QuickBooksSettingInfo != null)
			{
				dto.QuickBooksSettingInfo = new QuickBooksSettingInfoDto();
				WriteQuickBooksSettingInfo(data.QuickBooksSettingInfo, dto.QuickBooksSettingInfo);
			}
            return dto;
        }

		protected virtual void WriteQuickBooksSettingInfo(QuickBooksSettingInfo data, QuickBooksSettingInfoDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.SettingUuid = data.SettingUuid;
			dto.Fields = data.Fields.ToJson();
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}


        #endregion write to dto from data

    }
}



