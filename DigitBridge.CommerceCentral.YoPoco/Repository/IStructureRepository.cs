using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public interface IStructureRepository<TEntity> : IEquatable<TEntity>
        where TEntity : StructureRepository<TEntity>, new()
    {
        IDataBaseFactory dbFactory { get; }
        IDatabase db { get; }
        bool AllowNull { get; }
        bool IsNew { get; }

        TEntity SetAllowNull(bool allowNull);
        TEntity SetDataBaseFactory(IDataBaseFactory dbFactory);
        ITransaction GetTransaction();

        void New();
        void Clear();
        TEntity Clone() => null;

        bool Get(long RowNum);
        bool GetById(string InvoiceId);
        bool Save();
        bool Delete();

        Task<bool> GetAsync(long RowNum);
        Task<bool> GetByIdAsync(string id);
        Task<bool> SaveAsync();
        Task<bool> DeleteAsync();


    }
}