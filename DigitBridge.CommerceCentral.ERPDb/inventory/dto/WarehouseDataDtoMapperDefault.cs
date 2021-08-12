    

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
    /// Represents a WarehouseDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class WarehouseDataDtoMapperDefault : IDtoMapper<WarehouseData, WarehouseDataDto> 
    {
        #region read from dto to data

        public virtual WarehouseData ReadDto(WarehouseData data, WarehouseDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new WarehouseData();
                data.New();
            }

			if (dto.DistributionCenter != null)
			{
				if (data.DistributionCenter is null)
					data.DistributionCenter = data.NewDistributionCenter();
				ReadDistributionCenter(data.DistributionCenter, dto.DistributionCenter);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadDistributionCenter(DistributionCenter data, DistributionCenterDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasDistributionCenterName) data.DistributionCenterName = dto.DistributionCenterName;
			if (dto.HasDistributionCenterCode) data.DistributionCenterCode = dto.DistributionCenterCode;
			if (dto.HasDistributionCenterType) data.DistributionCenterType = dto.DistributionCenterType.ToInt();
			if (dto.HasStatus) data.Status = dto.Status.ToInt();
			if (dto.HasDefaultLevel) data.DefaultLevel = dto.DefaultLevel.ToBool();
			if (dto.HasAddressLine1) data.AddressLine1 = dto.AddressLine1;
			if (dto.HasAddressLine2) data.AddressLine2 = dto.AddressLine2;
			if (dto.HasCity) data.City = dto.City;
			if (dto.HasState) data.State = dto.State;
			if (dto.HasZipCode) data.ZipCode = dto.ZipCode;
			if (dto.HasCompanyName) data.CompanyName = dto.CompanyName;
			if (dto.HasContactName) data.ContactName = dto.ContactName;
			if (dto.HasContactEmail) data.ContactEmail = dto.ContactEmail;
			if (dto.HasContactPhone) data.ContactPhone = dto.ContactPhone;
			if (dto.HasMainPhone) data.MainPhone = dto.MainPhone;
			if (dto.HasFax) data.Fax = dto.Fax;
			if (dto.HasWebsite) data.Website = dto.Website;
			if (dto.HasEmail) data.Email = dto.Email;
			if (dto.HasBusinessHours) data.BusinessHours = dto.BusinessHours;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasPriority) data.Priority = dto.Priority.ToInt();
			if (dto.HasDistributionCenterUuid) data.DistributionCenterUuid = dto.DistributionCenterUuid;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}



        #endregion read from dto to data

        #region write to dto from data

        public virtual WarehouseDataDto WriteDto(WarehouseData data, WarehouseDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new WarehouseDataDto();

            data.CheckIntegrity();

			if (data.DistributionCenter != null)
			{
				dto.DistributionCenter = new DistributionCenterDto();
				WriteDistributionCenter(data.DistributionCenter, dto.DistributionCenter);
			}
            return dto;
        }

		protected virtual void WriteDistributionCenter(DistributionCenter data, DistributionCenterDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.DatabaseNum = data.DatabaseNum;
			dto.DistributionCenterNum = data.DistributionCenterNum.ToInt();
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.DistributionCenterName = data.DistributionCenterName;
			dto.DistributionCenterCode = data.DistributionCenterCode;
			dto.DistributionCenterType = data.DistributionCenterType;
			dto.Status = data.Status;
			dto.DefaultLevel = data.DefaultLevel;
			dto.AddressLine1 = data.AddressLine1;
			dto.AddressLine2 = data.AddressLine2;
			dto.City = data.City;
			dto.State = data.State;
			dto.ZipCode = data.ZipCode;
			dto.CompanyName = data.CompanyName;
			dto.ContactName = data.ContactName;
			dto.ContactEmail = data.ContactEmail;
			dto.ContactPhone = data.ContactPhone;
			dto.MainPhone = data.MainPhone;
			dto.Fax = data.Fax;
			dto.Website = data.Website;
			dto.Email = data.Email;
			dto.BusinessHours = data.BusinessHours;
			dto.Notes = data.Notes;
			dto.Priority = data.Priority;
			dto.RowNum = data.RowNum;
			dto.DistributionCenterUuid = data.DistributionCenterUuid;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}


        #endregion write to dto from data

    }
}


