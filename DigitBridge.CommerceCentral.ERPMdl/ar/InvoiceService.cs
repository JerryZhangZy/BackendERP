
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InvoiceService
    {

        public override InvoiceService Init()
        {
            base.Init();
            SetDtoMapper(new InvoiceDataDtoMapperDefault());
            SetCalculator(new InvoiceServiceCalculatorDefault());
            AddValidator(new InvoiceServiceValidatorDefault());
            return this;
        }
    }
}



