

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
    /// Represents a default PoTransactionService Validator class.
    /// </summary>
    public partial class PoReceiveServiceValidatorDefault : PoTransactionServiceValidatorDefault
    {
        public PoReceiveServiceValidatorDefault() : base() { }
        public PoReceiveServiceValidatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory) : base(serviceMessage, dbFactory) { }
        public override bool ValidateAccount(IPayload payload, string number = null, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var pl = (payload as PoReceivePayload);

            if (pl is null || pl.PoTransaction is null || pl.PoTransaction.PoTransaction is null)
            {
                AddError("PoTransaction is require.");
                return false;
            }
            return base.ValidateAccount(payload, number, processingMode);
        }
        public override async Task<bool> ValidateAccountAsync(IPayload payload, string number = null, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var pl = (payload as PoReceivePayload);
            if (pl is null || pl.PoTransaction is null || pl.PoTransaction.PoTransaction is null)
            {
                AddError("PoTransaction is require.");
                return false;
            }

            pl.PoTransaction.PoTransaction.TransType = (int)TransTypeEnum.Payment;
            return await base.ValidateAccountAsync(payload, number, processingMode);
        }
        public override bool Validate(PoTransactionDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (dto is null || dto.PoTransaction is null)
            {
                AddError("PoTransaction is require.");
                return false;
            }
            if (processingMode == ProcessingMode.Add)
            {
                // dto.PoTransaction.TransType = (int)TransTypeEnum.Payment; 
            }
            // payment shouldn't add any return item.
            return base.Validate(dto, processingMode);
        }
        public override async Task<bool> ValidateAsync(PoTransactionDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (dto is null || dto.PoTransaction is null)
            {
                AddError("PoTransaction is require.");
                return false;
            }
            if (processingMode == ProcessingMode.Add)
            {
                // dto.PoTransaction.TransType = (int)TransTypeEnum.Payment;  
            }
            // payment shouldn't add any return item.
            return await base.ValidateAsync(dto, processingMode);
        }
        protected override bool ValidateAdd(PoTransactionData data)
        {
            var isValid = base.ValidateAdd(data);
            isValid = isValid && ValidReceivedQty(data.PoTransactionItems);
            return isValid;
        }
        protected override async Task<bool> ValidateAddAsync(PoTransactionData data)
        {
            var isValid = await base.ValidateAddAsync(data);
            isValid = isValid && ValidReceivedQty(data.PoTransactionItems);
            return isValid;
        }
        protected override bool ValidateEdit(PoTransactionData data)
        {
            var isValid = base.ValidateEdit(data);
            isValid = isValid && ValidReceivedQty(data.PoTransactionItems);
            return isValid;
        }
        protected override async Task<bool> ValidateEditAsync(PoTransactionData data)
        {
            var isValid = await base.ValidateEditAsync(data);
            isValid = isValid && ValidReceivedQty(data.PoTransactionItems);
            return isValid;
        }

        private bool ValidReceivedQty(IList<PoTransactionItems> poReceiveItems)
        {
            var isValid = true;

            if (poReceiveItems == null || poReceiveItems.Count == 0)
                return isValid;

            foreach (var item in poReceiveItems)
            {
                //return qty cannot > open qty
                if (item.TransQty > item.OpenQty)
                {
                    isValid = false;
                    AddError($"Receive item TransQty cannot greater than OpenQty. [Sku:{item.SKU},PoItemUuid:{item.PoUuid},TransQty:{item.TransQty},OpenQty:{item.OpenQty}]");
                }
            }
            return isValid;
        }
    }
}



