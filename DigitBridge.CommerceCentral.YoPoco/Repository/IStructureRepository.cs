﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public interface IStructureRepository<TEntity> : IEquatable<TEntity>
        where TEntity : StructureRepository<TEntity>, new()
    {
        IDataBaseFactory dbFactory { get; }
        IDatabase db { get; }

        RepositoryCache Cache { get; }
        T GetCache<T>(string id) where T : TableRepository<T, long>, new();
        T GetCache<T>(string id, Func<T> create) where T : StructureRepository<T>, new();

        bool AllowNull { get; }
        bool IsNew { get; }

        TEntity SetAllowNull(bool allowNull);
        TEntity SetDataBaseFactory(IDataBaseFactory dbFactory);
        ITransaction GetTransaction();

        TEntity CheckIntegrity();
        void CheckIntegrityOthers();

        void New();
        void Clear();
        TEntity Clone() => null;

        bool Get(long RowNum);
        bool GetByNumber(int masterAccountNum, int profileNum, string number);
        bool GetById(string InvoiceId);
        bool Save();
        bool Delete();

        Task<bool> GetAsync(long RowNum);
        Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string number);
        Task<bool> GetByIdAsync(string id);
        Task<bool> SaveAsync();
        Task<bool> DeleteAsync();


    }
}