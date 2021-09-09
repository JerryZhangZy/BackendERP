

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a default InvoiceTransactionService Validator class.
    /// </summary>
    public partial class InvoicePaymentServiceValidatorDefault : InvoiceTransactionServiceValidatorDefault
    {
        public InvoicePaymentServiceValidatorDefault() : base() { }
        public InvoicePaymentServiceValidatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory) : base(serviceMessage, dbFactory) { }
        public override bool ValidateAccount(IPayload payload, string number = null, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var pl = (payload as InvoicePaymentPayload);

            if (pl is null || pl.InvoiceTransaction is null || pl.InvoiceTransaction.InvoiceTransaction is null)
            {
                AddError("InvoiceTransaction is require.");
                return false;
            }

            pl.InvoiceTransaction.InvoiceTransaction.TransType = (int)TransTypeEnum.Payment;
            pl.InvoiceTransaction.InvoiceReturnItems = null;
            return base.ValidateAccount(payload, number, processingMode);
        }
        public override async Task<bool> ValidateAccountAsync(IPayload payload, string number = null, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var pl = (payload as InvoicePaymentPayload);
            if (pl is null || pl.InvoiceTransaction is null || pl.InvoiceTransaction.InvoiceTransaction is null)
            {
                AddError("InvoiceTransaction is require.");
                return false;
            }

            pl.InvoiceTransaction.InvoiceTransaction.TransType = (int)TransTypeEnum.Payment;
            pl.InvoiceTransaction.InvoiceReturnItems = null;
            return await base.ValidateAccountAsync(payload, number, processingMode);
        }
        public override bool Validate(InvoiceTransactionDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (dto is null || dto.InvoiceTransaction is null)
            {
                AddError("InvoiceTransaction is require.");
                return false;
            }
            if (processingMode == ProcessingMode.Add)
            {
                dto.InvoiceTransaction.TransType = (int)TransTypeEnum.Payment; 
            }
            // payment shouldn't add any return item.
            dto.InvoiceReturnItems = null;
            return base.Validate(dto, processingMode);
        }
        public override async Task<bool> ValidateAsync(InvoiceTransactionDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (dto is null || dto.InvoiceTransaction is null)
            {
                AddError("InvoiceTransaction is require.");
                return false;
            }
            if (processingMode == ProcessingMode.Add)
            {
                dto.InvoiceTransaction.TransType = (int)TransTypeEnum.Payment;  
            }
            // payment shouldn't add any return item.
            dto.InvoiceReturnItems = null;
            return await base.ValidateAsync(dto, processingMode);
        }
    }
}



