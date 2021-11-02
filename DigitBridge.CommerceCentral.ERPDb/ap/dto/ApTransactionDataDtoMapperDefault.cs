    

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
    /// Represents a ApTransactionDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class ApTransactionDataDtoMapperDefault : IDtoMapper<ApTransactionData, ApTransactionDataDto> 
    {
        #region read from dto to data

        public virtual ApTransactionData ReadDto(ApTransactionData data, ApTransactionDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new ApTransactionData();
                data.New();
            }

			if (dto.ApInvoiceTransaction != null)
			{
				if (data.ApInvoiceTransaction is null)
					data.ApInvoiceTransaction = data.NewApInvoiceTransaction();
				ReadApInvoiceTransaction(data.ApInvoiceTransaction, dto.ApInvoiceTransaction);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadApInvoiceTransaction(ApInvoiceTransaction data, ApInvoiceTransactionDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasTransUuid) data.TransUuid = dto.TransUuid;
			if (dto.HasTransNum) data.TransNum = dto.TransNum.ToInt();
			if (dto.HasApInvoiceUuid) data.ApInvoiceUuid = dto.ApInvoiceUuid;
			if (dto.HasApInvoiceNum) data.ApInvoiceNum = dto.ApInvoiceNum;
			if (dto.HasTransType) data.TransType = dto.TransType;
			if (dto.HasTransStatus) data.TransStatus = dto.TransStatus;
			if (dto.HasTransDate) data.TransDate = dto.TransDate.ToDateTime();
			if (dto.HasTransTime) data.TransTime = dto.TransTime.ToTimeSpan();
			if (dto.HasDescription) data.Description = dto.Description;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasPaidBy) data.PaidBy = dto.PaidBy.ToInt();
			if (dto.HasBankAccountUuid) data.BankAccountUuid = dto.BankAccountUuid;
			if (dto.HasCheckNum) data.CheckNum = dto.CheckNum;
			if (dto.HasAuthCode) data.AuthCode = dto.AuthCode;
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasExchangeRate) data.ExchangeRate = dto.ExchangeRate.ToDecimal();
			if (dto.HasAmount) data.Amount = dto.Amount.ToDecimal();
			if (dto.HasCreditAccount) data.CreditAccount = dto.CreditAccount;
			if (dto.HasDebitAccount) data.DebitAccount = dto.DebitAccount;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}



        #endregion read from dto to data

        #region write to dto from data

        public virtual ApTransactionDataDto WriteDto(ApTransactionData data, ApTransactionDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new ApTransactionDataDto();

            data.CheckIntegrity();

			if (data.ApInvoiceTransaction != null)
			{
				dto.ApInvoiceTransaction = new ApInvoiceTransactionDto();
				WriteApInvoiceTransaction(data.ApInvoiceTransaction, dto.ApInvoiceTransaction);
			}
            return dto;
        }

		protected virtual void WriteApInvoiceTransaction(ApInvoiceTransaction data, ApInvoiceTransactionDto dto)
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
			dto.ApInvoiceUuid = data.ApInvoiceUuid;
			dto.ApInvoiceNum = data.ApInvoiceNum;
			dto.TransType = data.TransType;
			dto.TransStatus = data.TransStatus;
			dto.TransDate = data.TransDate;
			dto.TransTime = data.TransTime.ToDateTime();
			dto.Description = data.Description;
			dto.Notes = data.Notes;
			dto.PaidBy = data.PaidBy;
			dto.BankAccountUuid = data.BankAccountUuid;
			dto.CheckNum = data.CheckNum;
			dto.AuthCode = data.AuthCode;
			dto.Currency = data.Currency;
			dto.ExchangeRate = data.ExchangeRate;
			dto.Amount = data.Amount;
			dto.CreditAccount = data.CreditAccount;
			dto.DebitAccount = data.DebitAccount;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}


        #endregion write to dto from data

    }
}



