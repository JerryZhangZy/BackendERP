using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface IInvoicePaymentService : IService<InvoiceTransactionService, InvoiceTransactionData, InvoiceTransactionDataDto>
    {
        bool Add(InvoicePaymentPayload payload);
        Task<bool> AddAsync(InvoicePaymentPayload payload);

        bool Update(InvoicePaymentPayload payload);
        Task<bool> UpdateAsync(InvoicePaymentPayload payload);

        IList<MessageClass> Messages { get; set; }
    }
}
