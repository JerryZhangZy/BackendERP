

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
    public partial class InvoiceReturnServiceValidatorDefault : InvoiceTransactionServiceValidatorDefault
    {
        //public override bool Validate(InvoiceTransactionDataDto dto, IDataBaseFactory dbFactory, ProcessingMode processingMode = ProcessingMode.Edit)
        //{
        //    if (processingMode == ProcessingMode.Add)
        //    {
        //        dto.InvoiceTransaction.TransType = (int)TransTypeEnum.Return;
        //    }
        //    return base.Validate(dto, dbFactory, processingMode);
        //}
    }
}


