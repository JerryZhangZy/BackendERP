using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface IValidator<TEntity>
        where TEntity : StructureRepository<TEntity>, new()
    {
        bool IsValid { get; set; }
        IList<string> Messages { get; set; }
        void Clear();

        bool ValidatePayload(TEntity data, IPayload payload, ProcessingMode processingMode = ProcessingMode.Edit);

        bool Validate(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
        Task<bool> ValidateAsync(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
    }
}
