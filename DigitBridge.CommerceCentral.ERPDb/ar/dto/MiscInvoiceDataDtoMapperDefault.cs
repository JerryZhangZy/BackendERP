    

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
    /// Represents a MiscInvoiceDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class MiscInvoiceDataDtoMapperDefault : IDtoMapper<MiscInvoiceData, MiscInvoiceDataDto> 
    {
        #region read from dto to data

        public virtual MiscInvoiceData ReadDto(MiscInvoiceData data, MiscInvoiceDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new MiscInvoiceData();
                data.New();
            }

			if (dto.MiscInvoiceHeader != null)
			{
				if (data.MiscInvoiceHeader is null)
					data.MiscInvoiceHeader = data.NewMiscInvoiceHeader();
				ReadMiscInvoiceHeader(data.MiscInvoiceHeader, dto.MiscInvoiceHeader);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadMiscInvoiceHeader(MiscInvoiceHeader data, MiscInvoiceHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasMiscInvoiceUuid) data.MiscInvoiceUuid = dto.MiscInvoiceUuid;
			if (dto.HasMiscInvoiceNumber) data.MiscInvoiceNumber = dto.MiscInvoiceNumber;
			if (dto.HasQboDocNumber) data.QboDocNumber = dto.QboDocNumber;
			if (dto.HasMiscInvoiceType) data.MiscInvoiceType = dto.MiscInvoiceType.ToInt();
			if (dto.HasMiscInvoiceStatus) data.MiscInvoiceStatus = dto.MiscInvoiceStatus.ToInt();
			if (dto.HasMiscInvoiceDate) data.MiscInvoiceDate = dto.MiscInvoiceDate.ToDateTime();
			if (dto.HasMiscInvoiceTime) data.MiscInvoiceTime = dto.MiscInvoiceTime.ToTimeSpan();
			if (dto.HasCustomerUuid) data.CustomerUuid = dto.CustomerUuid;
			if (dto.HasCustomerCode) data.CustomerCode = dto.CustomerCode;
			if (dto.HasCustomerName) data.CustomerName = dto.CustomerName;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasPaidBy) data.PaidBy = dto.PaidBy.ToInt();
			if (dto.HasBankAccountUuid) data.BankAccountUuid = dto.BankAccountUuid;
			if (dto.HasBankAccountCode) data.BankAccountCode = dto.BankAccountCode;
			if (dto.HasCheckNum) data.CheckNum = dto.CheckNum;
			if (dto.HasAuthCode) data.AuthCode = dto.AuthCode;
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasTotalAmount) data.TotalAmount = dto.TotalAmount.ToDecimal();
			if (dto.HasPaidAmount) data.PaidAmount = dto.PaidAmount.ToDecimal();
			if (dto.HasCreditAmount) data.CreditAmount = dto.CreditAmount.ToDecimal();
			if (dto.HasBalance) data.Balance = dto.Balance.ToDecimal();
			if (dto.HasInvoiceSourceCode) data.InvoiceSourceCode = dto.InvoiceSourceCode;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}



        #endregion read from dto to data

        #region write to dto from data

        public virtual MiscInvoiceDataDto WriteDto(MiscInvoiceData data, MiscInvoiceDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new MiscInvoiceDataDto();

            data.CheckIntegrity();

			if (data.MiscInvoiceHeader != null)
			{
				dto.MiscInvoiceHeader = new MiscInvoiceHeaderDto();
				WriteMiscInvoiceHeader(data.MiscInvoiceHeader, dto.MiscInvoiceHeader);
			}
            return dto;
        }

		protected virtual void WriteMiscInvoiceHeader(MiscInvoiceHeader data, MiscInvoiceHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.MiscInvoiceUuid = data.MiscInvoiceUuid;
			dto.MiscInvoiceNumber = data.MiscInvoiceNumber;
			dto.QboDocNumber = data.QboDocNumber;
			dto.MiscInvoiceType = data.MiscInvoiceType;
			dto.MiscInvoiceStatus = data.MiscInvoiceStatus;
			dto.MiscInvoiceDate = data.MiscInvoiceDate;
			dto.MiscInvoiceTime = data.MiscInvoiceTime.ToDateTime();
			dto.CustomerUuid = data.CustomerUuid;
			dto.CustomerCode = data.CustomerCode;
			dto.CustomerName = data.CustomerName;
			dto.Notes = data.Notes;
			dto.PaidBy = data.PaidBy;
			dto.BankAccountUuid = data.BankAccountUuid;
			dto.BankAccountCode = data.BankAccountCode;
			dto.CheckNum = data.CheckNum;
			dto.AuthCode = data.AuthCode;
			dto.Currency = data.Currency;
			dto.TotalAmount = data.TotalAmount;
			dto.PaidAmount = data.PaidAmount;
			dto.CreditAmount = data.CreditAmount;
			dto.Balance = data.Balance;
			dto.InvoiceSourceCode = data.InvoiceSourceCode;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}


        #endregion write to dto from data

    }
}



