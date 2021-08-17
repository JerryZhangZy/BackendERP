


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InvoiceReturnService : InvoiceTransactionService
    {
        public InvoiceReturnService(IDataBaseFactory dbFactory) : base(dbFactory)
        { 
        }
        public override InvoiceTransactionService Init()
        {
            SetDtoMapper(new InvoiceTransactionDataDtoMapperDefault());
            SetCalculator(new InvoiceTransactionServiceCalculatorDefault());
            AddValidator(new InvoiceReturnServiceValidatorDefault(this, this.dbFactory));
            return this;
        }
        public async Task<bool> GetDataAsync(string invoiceNumber, InvoiceReturnPayload payload)
        {
            var success = await base.GetDataAsync(invoiceNumber, payload.MasterAccountNum, payload.ProfileNum, true);
            if (success && Data.InvoiceTransaction.TransType != (int)TransTypeEnum.Return)
            {
                AddError($"{invoiceNumber} isn't a return invoice number");
                return false;
            }
            return success;
        }
    }
}



