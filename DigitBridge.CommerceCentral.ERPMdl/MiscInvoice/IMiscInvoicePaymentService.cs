using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface IMiscInvoicePaymentService : IService<MiscInvoiceTransactionService, MiscInvoiceTransactionData, MiscInvoiceTransactionDataDto>
    {
        Task<bool> AddMiscPayment(string miscInvoiceUuid, string invoiceUuid, string invoiceNumber, decimal amount);
    }
}
