using System.Collections.Generic;
using System;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class DataBaseFactory : IDataBaseFactory
    {
        public static IDataBaseFactory CreateDefault(string connectionString = null)
        {
            return new DataBaseFactory(connectionString ?? ConfigurationManager.AppSettings["dsn"]);
        }

        private TransactionalCache Cache { get; } = new TransactionalCache();
        private IList<IDatabase> DbList { get; set; } = new List<IDatabase>();
        private readonly string _ConnectionString;
        public string ConnectionString => _ConnectionString ?? ConfigurationManager.AppSettings["dsn"];
        public IDatabase Db => GetDb();

        public DataBaseFactory() {}
        public DataBaseFactory(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        #region Create and Get Database

        protected static IDatabase CreateDb(string connectionString = null)
        {
            var db = DatabaseConfiguration
                .Build()
                .UsingConnectionString(connectionString)
                .UsingProvider<SqlSererMsDataDatabaseProvider>()
                .UsingCommandTimeout(300)
                .WithAutoSelect()
                .WithoutNamedParams()
                //.UsingIsolationLevel(IsolationLevel.Chaos)
                .Create();
            return db;
        }

        public IDatabase GetDb(string connectionString = null)
        {
            connectionString ??= this.ConnectionString;
            var db = DbList?.FirstOrDefault(item => item.ConnectionString == connectionString);
            if (db == null) {
                db = CreateDb(ConnectionString);
                DbList.Add(db);
            }
            return db;
        }
        #endregion

        #region Transaction Methods
        public virtual ITransaction GetTransaction(string connectionString = null)
        {
            var db = GetDb(connectionString);
            return db.GetTransaction();
        }

        public virtual void Begin(string connectionString = null)
        {
            var db = GetDb(connectionString);
            db.BeginTransaction();
            return;
        }

        public virtual bool Commit(string connectionString = null)
        {
            bool rtn = true;
            var db = GetDb(connectionString);
            try
            {
                db.CompleteTransaction();
            }
            catch (Exception he)
            {
                db.AbortTransaction();
                throw he;
            }
            return rtn;
        }

        public virtual void Abort(string connectionString = null)
        {
            var db = GetDb(connectionString);
            try
            {
                db.AbortTransaction();
            }
            finally
            {
            }
        }

        #endregion

        void IDisposable.Dispose()
        {
            foreach (var db in DbList)
            {
                db?.AbortTransaction();
                db?.Dispose();
            }
            DbList.Clear();
        }

        #region Exist and get Scalar value

        public virtual bool Exists<TEntity>(long id) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.Exists(this, id);
        public virtual bool ExistUniqueId<TEntity>(string uniqueKey) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.ExistUniqueId(this, uniqueKey);
        public virtual bool Exists<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.Exists(this, sql, args); // True or False
        public virtual TResult GetValue<TEntity, TResult>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.GetValue<TResult>(this, sql, args);
        public virtual long Count<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.Count(this, sql, args);


        public virtual async Task<bool> ExistsAsync<TEntity>(long id) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.ExistsAsync(this, id);

        public virtual async Task<bool> ExistUniqueIdAsync<TEntity>(string uniqueKey) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.ExistUniqueIdAsync(this, uniqueKey);

        public virtual async Task<bool> ExistsAsync<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.ExistsAsync(this, sql, args); // True or False

        public virtual async Task<T> GetValueAsync<TEntity, T>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.GetValueAsync<T>(this, sql, args);

        public virtual async Task<long> CountAsync<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.CountAsync(this, sql, args);

        #endregion

        #region Query - get single record

        public virtual TEntity Get<TEntity>(long id) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.Get(this, id)?.SetDataBaseFactory(this);

        public virtual TEntity Get<TEntity>(long id, IEnumerable<string> columns) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.Get(this, id, columns)?.SetDataBaseFactory(this);

        public virtual TEntity GetById<TEntity>(string uid) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.GetById(this, uid)?.SetDataBaseFactory(this);

        public virtual TEntity GetById<TEntity>(string uid, IEnumerable<string> columns) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.GetById(this, uid, columns)?.SetDataBaseFactory(this);

        public virtual TEntity GetBy<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.GetBy(this, sql, args)?.SetDataBaseFactory(this);


        public virtual async Task<TEntity> GetAsync<TEntity>(long id) where TEntity : TableRepository<TEntity, long>, new()
            => (await TableRepository<TEntity, long>.GetAsync(this, id))?.SetDataBaseFactory(this);

        public virtual async Task<TEntity> GetAsync<TEntity>(long id, IEnumerable<string> columns) where TEntity : TableRepository<TEntity, long>, new()
            => (await TableRepository<TEntity, long>.GetAsync(this, id, columns))?.SetDataBaseFactory(this);

        public virtual async Task<TEntity> GetByIdAsync<TEntity>(string uid) where TEntity : TableRepository<TEntity, long>, new()
            => (await TableRepository<TEntity, long>.GetByIdAsync(this, uid))?.SetDataBaseFactory(this);

        public virtual async Task<TEntity> GetByIdAsync<TEntity>(string uid, IEnumerable<string> columns) where TEntity : TableRepository<TEntity, long>, new()
            => (await TableRepository<TEntity, long>.GetByIdAsync(this, uid, columns))?.SetDataBaseFactory(this);

        public virtual async Task<TEntity> GetByAsync<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => (await TableRepository<TEntity, long>.GetByAsync(this, sql, args))?.SetDataBaseFactory(this);

        #endregion Query - get single record

        #region Query - find multiple records

        public virtual IEnumerable<TEntity> Find<TEntity>() where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.Find(this);

        public virtual IEnumerable<TEntity> FindByOrder<TEntity>(params string[] orderBy) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.FindByOrder(this, orderBy);

        public virtual IEnumerable<TEntity> Find<TEntity>(IEnumerable<string> columns, params string[] orderBy) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.Find(this, columns, orderBy);

        public virtual IEnumerable<TEntity> Find<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.Find(this, sql, args);

        public virtual IEnumerable<TEntity> Find<TEntity>(string sql, IEnumerable<string> columns, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => TableRepository<TEntity, long>.Find(this, sql, columns, args);


        public virtual async Task<IEnumerable<TEntity>> FindAsync<TEntity>() where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.FindAsync(this);

        public virtual async Task<IEnumerable<TEntity>> FindByOrderAsync<TEntity>(params string[] orderBy) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.FindByOrderAsync(this, orderBy);

        public virtual async Task<IEnumerable<TEntity>> FindAsync<TEntity>(IEnumerable<string> columns, params string[] orderBy) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.FindAsync(this, columns, orderBy);

        public virtual async Task<IEnumerable<TEntity>> FindAsync<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.FindAsync(this, sql, args);

        public virtual async Task<IEnumerable<TEntity>> FindAsync<TEntity>(string sql, IEnumerable<string> columns, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.FindAsync(this, sql, columns, args);

        #endregion

        #region Query - get single record from Cache

        public virtual TEntity GetFromCache<TEntity>(long id) where TEntity : TableRepository<TEntity, long>, new()
            => Cache.FromCache<TEntity>($"{typeof(TEntity).ToString()}:RowNum:{id}", () => Get<TEntity>(id));

        public virtual TEntity GetFromCacheById<TEntity>(string uid) where TEntity : TableRepository<TEntity, long>, new()
            => Cache.FromCache<TEntity>($"{typeof(TEntity).ToString()}:Id:{uid}", () => GetById<TEntity>(uid));


        public virtual async Task<TEntity> GetFromCacheAsync<TEntity>(long id) where TEntity : TableRepository<TEntity, long>, new()
            => await Cache.FromCache<Task<TEntity>>($"{typeof(TEntity).ToString()}:RowNum:{id}:async", 
                async () => await GetAsync<TEntity>(id));

        public virtual async Task<TEntity> GetFromCacheByIdAsync<TEntity>(string uid) where TEntity : TableRepository<TEntity, long>, new()
            => await Cache.FromCache<Task<TEntity>>($"{typeof(TEntity).ToString()}:Id:{uid}:async",
                async () => await GetByIdAsync<TEntity>(uid));

        #endregion Query - get single record


    }
}
