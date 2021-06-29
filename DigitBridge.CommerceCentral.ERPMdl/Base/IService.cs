using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface IService<TService, TEntity>
        where TService : ServiceBase<TService, TEntity>
        where TEntity : StructureRepository<TEntity>, new()

    {
        IDataBaseFactory dbFactory { get; }
        TService SetDataBaseFactory(IDataBaseFactory dbFactory);

        TEntity Data { get; }
        ProcessingMode ProcessMode { get; set; }

        TService Clear();
        TService AttachData(TEntity data);
        TService DetachData(TEntity data);
        TService NewData();
        TService ClearData();
        TEntity CloneData();

        bool Get(long RowNum);

        bool GetById(string id);
        bool Save();
        bool Delete();
        Task<bool> GetAsync(long RowNum);
        Task<bool> GetByIdAsync(string id);
        Task<bool> SaveAsync();
        Task<bool> DeleteAsync();

    }
}
