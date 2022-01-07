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
        void PrepareData(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
        bool SetDefault(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
        bool SetDefaultSummary(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
        bool SetDefaultDetail(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);

        bool Calculate(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
        bool CalculateSummary(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);
        bool CalculateDetail(TEntity data, ProcessingMode processingMode = ProcessingMode.Edit);

    }
}
