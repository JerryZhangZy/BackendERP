using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public interface IDtoMapper<TEntity, TDto>
        where TEntity: new()
        where TDto: new()
    {
        TEntity ReadDto(TEntity data, TDto dto);
        TDto WriteDto(TEntity data, TDto dto);
    }
}
