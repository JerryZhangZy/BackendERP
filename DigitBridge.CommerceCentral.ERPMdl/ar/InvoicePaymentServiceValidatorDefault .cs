

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
        public virtual bool ValidatePayload(InvoiceTransactionData data, IPayload payload, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            Clear();
            var pl = payload as SalesOrderPayload;
            if (processingMode == ProcessingMode.Add)
            {
                //TODO set MasterAccountNum, ProfileNum and DatabaseNum from payload
                //data.InvoiceTransaction.MasterAccountNum = pl.MasterAccountNum;
                //data.InvoiceTransaction.ProfileNum = pl.ProfileNum;
                //data.InvoiceTransaction.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //TODO check MasterAccountNum, ProfileNum and DatabaseNum between data and payload
                //if (
                //    data.SalesOrderHeader.MasterAccountNum != pl.MasterAccountNum ||
                //    data.SalesOrderHeader.ProfileNum != pl.ProfileNum
                //)
                //    IsValid = false;
                //AddError($"Sales Order not found.");
                //return IsValid;
            }
            return true;
        }
    }
}


