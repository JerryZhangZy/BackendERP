using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataBaseFactory : IDisposable
    {
        string ConnectionString { get; }
        IDatabase Db { get; }

        IDatabase GetDb(string connectionString = null);

        ITransaction GetTransaction(string connectionString = null);
        void Begin(string connectionString = null);
        bool Commit(string connectionString = null);
        void Abort(string connectionString = null);


        #region Exist and get Scalar value

        bool Exists<TEntity>(long id) where TEntity : TableRepository<TEntity, long>, new();
        bool ExistUniqueId<TEntity>(string uniqueKey) where TEntity : TableRepository<TEntity, long>, new();

        bool Exists<TEntity>(string sql, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();

        TResult GetValue<TEntity, TResult>(string sql, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();

        long Count<TEntity>(string sql, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();


        Task<bool> ExistsAsync<TEntity>(long id) where TEntity : TableRepository<TEntity, long>, new();

        Task<bool> ExistUniqueIdAsync<TEntity>(string uniqueKey)
            where TEntity : TableRepository<TEntity, long>, new();

        Task<bool> ExistsAsync<TEntity>(string sql, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();

        Task<T> GetValueAsync<TEntity, T>(string sql, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();

        Task<long> CountAsync<TEntity>(string sql, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();

        #endregion

        #region Query - get single record

        TEntity Get<TEntity>(long id) where TEntity : TableRepository<TEntity, long>, new();

        TEntity Get<TEntity>(long id, IEnumerable<string> columns)
            where TEntity : TableRepository<TEntity, long>, new();

        TEntity GetById<TEntity>(string uid) where TEntity : TableRepository<TEntity, long>, new();

        TEntity GetById<TEntity>(string uid, IEnumerable<string> columns)
            where TEntity : TableRepository<TEntity, long>, new();

        TEntity GetBy<TEntity>(string sql, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();


        Task<TEntity> GetAsync<TEntity>(long id) where TEntity : TableRepository<TEntity, long>, new();

        Task<TEntity> GetAsync<TEntity>(long id, IEnumerable<string> columns)
            where TEntity : TableRepository<TEntity, long>, new();

        Task<TEntity> GetByIdAsync<TEntity>(string uid) where TEntity : TableRepository<TEntity, long>, new();

        Task<TEntity> GetByIdAsync<TEntity>(string uid, IEnumerable<string> columns)
            where TEntity : TableRepository<TEntity, long>, new();

        Task<TEntity> GetByAsync<TEntity>(string sql, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();

        #endregion Query - get single record

        #region Query - find multiple records

        IEnumerable<TEntity> Find<TEntity>() where TEntity : TableRepository<TEntity, long>, new();

        IEnumerable<TEntity> FindByOrder<TEntity>(params string[] orderBy)
            where TEntity : TableRepository<TEntity, long>, new();

        IEnumerable<TEntity> Find<TEntity>(IEnumerable<string> columns, params string[] orderBy)
            where TEntity : TableRepository<TEntity, long>, new();

        IEnumerable<TEntity> Find<TEntity>(string sql, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();

        IEnumerable<TEntity> Find<TEntity>(string sql, IEnumerable<string> columns, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();

        Task<IEnumerable<TEntity>> FindAsync<TEntity>() where TEntity : TableRepository<TEntity, long>, new();

        Task<IEnumerable<TEntity>> FindByOrderAsync<TEntity>(params string[] orderBy)
            where TEntity : TableRepository<TEntity, long>, new();

        Task<IEnumerable<TEntity>> FindAsync<TEntity>(IEnumerable<string> columns, params string[] orderBy)
            where TEntity : TableRepository<TEntity, long>, new();

        Task<IEnumerable<TEntity>> FindAsync<TEntity>(string sql, params object[] args)
            where TEntity : TableRepository<TEntity, long>, new();

        Task<IEnumerable<TEntity>> FindAsync<TEntity>(string sql, IEnumerable<string> columns,
            params object[] args) where TEntity : TableRepository<TEntity, long>, new();

        #endregion

        #region Query - get record from Cache

        T GetFromCache<T>(string key, Func<T> create, bool reNew = false);
        TEntity GetFromCache<TEntity>(long id, bool reNew = false) where TEntity : TableRepository<TEntity, long>, new();
        TEntity GetFromCacheById<TEntity>(string uid, bool reNew = false) where TEntity : TableRepository<TEntity, long>, new();
        Task<TEntity> GetFromCacheAsync<TEntity>(long id, bool reNew = false) where TEntity : TableRepository<TEntity, long>, new();
        Task<TEntity> GetFromCacheByIdAsync<TEntity>(string uid, bool reNew = false) where TEntity : TableRepository<TEntity, long>, new();

        #endregion Query - get single record from Cache

    }
}
