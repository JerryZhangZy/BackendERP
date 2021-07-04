    

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
    /// Represents a default InvoiceService Validator class.
    /// </summary>
    public partial class InvoiceServiceValidatorDefault : IValidator<InvoiceData>
    {
        public virtual bool IsValid { get; set; }
        public virtual IList<string> Messages { get; set; }

        public virtual void Clear()
        {
            IsValid = true;
            Messages = new List<string>();
        }
        public virtual bool Validate(InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            Clear();
            return IsValid;
        }
    }
}



