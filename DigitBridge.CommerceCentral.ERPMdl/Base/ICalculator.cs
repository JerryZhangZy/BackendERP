using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface ICalculator<TEntity>
        where TEntity : StructureRepository<TEntity>, new()
    {
        bool Calculate(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
        Task<bool> CalculateAsync(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);

        bool CalculateSummary(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
        Task<bool> CalculateSummaryAsync(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);

        bool CalculateDetail(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
        Task<bool> CalculateDetailAsync(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
    }
}
