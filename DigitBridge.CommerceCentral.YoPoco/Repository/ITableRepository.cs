using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public interface ITableRepository<TEntity, TId> : IEquatable<TEntity>
        where TEntity : TableRepository<TEntity, TId>, new()
    {
        void Register();
        PocoData GetPocoData();

        IDatabase db { get; }
        long RowNum { get; }
        bool AllowNull { get; }
        bool IsNew { get; }
        bool IsEmpty { get; }

        void ClearMetaData();
        TEntity SetAllowNull(bool allowNull);
        TEntity SetDataBaseFactory(IDataBaseFactory dbFactory);
        ITransaction GetTransaction();

        void CheckUniqueId();
        TEntity CheckIntegrity();
        void CheckIntegrityOthers();

        bool Add(IDataBaseFactory DbFactory);
        bool Add();
        int Put(IDataBaseFactory DbFactory);
        int Put();
        int Patch(IDataBaseFactory DbFactory, IEnumerable<string> columns);
        int Patch(IEnumerable<string> columns);
        int Patch(IDataBaseFactory DbFactory);
        int Patch();
        int Delete(IDataBaseFactory DbFactory);
        int Delete();
        int Delete(IDataBaseFactory DbFactory, string sql, params object[] args);
        int Delete(string sql, params object[] args);
        bool Save(IDataBaseFactory DbFactory);
        bool Save();

        Task<bool> AddAsync(IDataBaseFactory DbFactory);
        Task<bool> AddAsync();
        Task<int> PutAsync(IDataBaseFactory DbFactory);
        Task<int> PutAsync();
        Task<int> PatchAsync(IDataBaseFactory DbFactory, IEnumerable<string> columns);
        Task<int> PatchAsync(IEnumerable<string> columns);
        Task<int> DeleteAsync(IDataBaseFactory DbFactory);
        Task<int> DeleteAsync();
        Task<int> DeleteAsync(IDataBaseFactory DbFactory, string sql, params object[] args);
        Task<int> DeleteAsync(string sql, params object[] args);
        Task<bool> SaveAsync(IDataBaseFactory DbFactory);
        Task<bool> SaveAsync();


        bool PropertyEquals(TEntity other);
        TEntity CopyFrom(TEntity other);
        TEntity CopyFrom(TEntity other, IEnumerable<string> ignoreColumns);
        TEntity Clear();
        TEntity Clone();

        TEntity ConvertDbFieldsToData();
        TEntity ConvertDataFieldsToDb();
        void OnPropertyChanged(string name, object value);

    }
}