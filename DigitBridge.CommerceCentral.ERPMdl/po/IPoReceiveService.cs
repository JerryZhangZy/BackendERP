using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface IPoReceiveService : IService<PoTransactionService, PoTransactionData, PoTransactionDataDto>
    {
        bool Add(PoReceivePayload payload);
        Task<bool> AddAsync(PoReceivePayload payload);

        bool Update(PoReceivePayload payload);
        Task<bool> UpdateAsync(PoReceivePayload payload);

        IList<MessageClass> Messages { get; set; }

        Task<bool> NewReceiveForVendorAsync(PoReceivePayload payload);
    }
}
