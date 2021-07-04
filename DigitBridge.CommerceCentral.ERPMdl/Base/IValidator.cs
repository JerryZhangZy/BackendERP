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
        bool Validate(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
    }
}
