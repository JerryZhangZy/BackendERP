using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface IInvoiceReturnService : IService<InvoiceTransactionService, InvoiceTransactionData, InvoiceTransactionDataDto>
    {
        bool Add(InvoiceReturnPayload payload);
        Task<bool> AddAsync(InvoiceReturnPayload payload);

        bool Update(InvoiceReturnPayload payload);
        Task<bool> UpdateAsync(InvoiceReturnPayload payload);

        IList<MessageClass> Messages { get; set; }
    }
}
