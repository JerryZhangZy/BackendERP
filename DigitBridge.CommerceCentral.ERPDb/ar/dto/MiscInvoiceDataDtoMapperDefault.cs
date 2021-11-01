    

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
			if (dto.MiscInvoiceTransaction != null)
			{
				if (data.MiscInvoiceTransaction is null)
					data.MiscInvoiceTransaction = new List<MiscInvoiceTransaction>();
				var deleted = ReadMiscInvoiceTransaction(data.MiscInvoiceTransaction, dto.MiscInvoiceTransaction);
				data.SetMiscInvoiceTransactionDeleted(deleted);
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


		protected virtual void ReadMiscInvoiceTransaction(MiscInvoiceTransaction data, MiscInvoiceTransactionDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasTransUuid) data.TransUuid = dto.TransUuid;
			if (dto.HasTransNum) data.TransNum = dto.TransNum.ToInt();
			if (dto.HasMiscInvoiceUuid) data.MiscInvoiceUuid = dto.MiscInvoiceUuid;
			if (dto.HasMiscInvoiceNumber) data.MiscInvoiceNumber = dto.MiscInvoiceNumber;
			if (dto.HasTransType) data.TransType = dto.TransType.ToInt();
			if (dto.HasTransStatus) data.TransStatus = dto.TransStatus.ToInt();
			if (dto.HasTransDate) data.TransDate = dto.TransDate.ToDateTime();
			if (dto.HasTransTime) data.TransTime = dto.TransTime.ToTimeSpan();
			if (dto.HasDescription) data.Description = dto.Description;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasPaidBy) data.PaidBy = dto.PaidBy.ToInt();
			if (dto.HasBankAccountUuid) data.BankAccountUuid = dto.BankAccountUuid;
			if (dto.HasBankAccountCode) data.BankAccountCode = dto.BankAccountCode;
			if (dto.HasCheckNum) data.CheckNum = dto.CheckNum;
			if (dto.HasAuthCode) data.AuthCode = dto.AuthCode;
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasTotalAmount) data.TotalAmount = dto.TotalAmount.ToDecimal();
			if (dto.HasCreditAccount) data.CreditAccount = dto.CreditAccount.ToLong();
			if (dto.HasDebitAccount) data.DebitAccount = dto.DebitAccount.ToLong();
			if (dto.HasTransSourceCode) data.TransSourceCode = dto.TransSourceCode;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}

		protected virtual IList<MiscInvoiceTransaction> ReadMiscInvoiceTransaction(IList<MiscInvoiceTransaction> data, IList<MiscInvoiceTransactionDto> dto)
		{
			if (data is null || dto is null)
				return null;
			var lstOrig = new List<MiscInvoiceTransaction>(data.Where(x => x != null).ToList());
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = itemDto.RowNum > 0
					? lstOrig.Find(x => x.RowNum == itemDto.RowNum)
					: lstOrig.Find(x => x.TransUuid == itemDto.TransUuid);
				if (obj is null)
					obj = new MiscInvoiceTransaction().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadMiscInvoiceTransaction(obj, itemDto);

			}
			return lstOrig;
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
			if (data.MiscInvoiceTransaction != null)
			{
				dto.MiscInvoiceTransaction = new List<MiscInvoiceTransactionDto>();
				WriteMiscInvoiceTransaction(data.MiscInvoiceTransaction, dto.MiscInvoiceTransaction);
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

		protected virtual void WriteMiscInvoiceTransaction(MiscInvoiceTransaction data, MiscInvoiceTransactionDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.TransUuid = data.TransUuid;
			dto.TransNum = data.TransNum;
			dto.MiscInvoiceUuid = data.MiscInvoiceUuid;
			dto.MiscInvoiceNumber = data.MiscInvoiceNumber;
			dto.TransType = data.TransType;
			dto.TransStatus = data.TransStatus;
			dto.TransDate = data.TransDate;
			dto.TransTime = data.TransTime.ToDateTime();
			dto.Description = data.Description;
			dto.Notes = data.Notes;
			dto.PaidBy = data.PaidBy;
			dto.BankAccountUuid = data.BankAccountUuid;
			dto.BankAccountCode = data.BankAccountCode;
			dto.CheckNum = data.CheckNum;
			dto.AuthCode = data.AuthCode;
			dto.Currency = data.Currency;
			dto.TotalAmount = data.TotalAmount;
			dto.CreditAccount = data.CreditAccount;
			dto.DebitAccount = data.DebitAccount;
			dto.TransSourceCode = data.TransSourceCode;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}
		protected virtual void WriteMiscInvoiceTransaction(IList<MiscInvoiceTransaction> data, IList<MiscInvoiceTransactionDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			#region write all list items and properties with null

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new MiscInvoiceTransactionDto();
				dto.Add(obj);
				WriteMiscInvoiceTransaction(itemData, obj);
			}

			#endregion write all list items and properties with null
			return;
		}



        #endregion write to dto from data

    }
}



