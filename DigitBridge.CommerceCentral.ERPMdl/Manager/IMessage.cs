using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl.Manager
{
    public interface IMessage<TEntity, TDto>
        where TEntity : StructureRepository<TEntity>, new()
        where TDto : class, new()
    {
        TDto DtoData { get; }

        IDtoMapper<DtoData> DtoMapper { get; }
        void SetDtoMapper(IDtoMapper<DtoData> mapper);

    }
}
