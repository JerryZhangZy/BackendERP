using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface IValidator<TEntity, TDto>
        where TEntity : StructureRepository<TEntity>, new()
        where TDto : class, new()
    {
        bool IsValid { get; set; }
        void Clear();

        bool ValidateAccount(IPayload payload, string number = null, ProcessingMode processingMode = ProcessingMode.Edit);
        Task<bool> ValidateAccountAsync(IPayload payload, string number = null, ProcessingMode processingMode = ProcessingMode.Edit);

        bool Validate(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
        Task<bool> ValidateAsync(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit); 

        bool Validate(TDto dto, ProcessingMode processingMode = ProcessingMode.Edit);
        Task<bool> ValidateAsync(TDto dto, ProcessingMode processingMode = ProcessingMode.Edit);
          
    }
}
