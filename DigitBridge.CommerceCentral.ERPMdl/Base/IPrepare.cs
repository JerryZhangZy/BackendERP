using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface IPrepare<TService, TEntity, TDto>
        where TService : ServiceBase<TService, TEntity, TDto>
        where TEntity : StructureRepository<TEntity>, new()
        where TDto : class, new()
    {
        Task<bool> PrepareDtoAsync(TDto dto);
    }
}
