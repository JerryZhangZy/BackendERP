using System.Collections.Generic;
using System;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using DigitBridge.Base.Utility;
using Azure.Identity;
using Azure.Core;
using System.Threading;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class ThreadConnections
    {
        public Dictionary<string, object> Data { get; }
        public ThreadConnections()
        {
            Data = new Dictionary<string, object>();
        }

        //public TransactionalCache Data { get; }
        //public ThreadConnections()
        //{
        //    Data = new TransactionalCache();
        //}
    }

    public class DataBaseFactory : IDataBaseFactory
    {
        #region static 

        public static readonly DateTime _SqlMinDateTime = new DateTime(1753, 1, 1);
        public static readonly int DefaultTimeout = 180;
        public static readonly string TimestampFormat = "yyyy-MM-dd HH:mm:ss";
        public static readonly string DateFormat = "yyyy-MM-dd";
        //make it public, so the caller can override it.
        public static string AzureDatabaseTokenUrl = "https://database.windows.net/";
        public static string DefaultDataBaseFactoryKey = "_DefaultDataBaseFactory_";
        private static readonly string[] AzureDatabaseTokenScopes = { "https://database.windows.net/.default" };


        //private static ThreadContext<ThreadConnections> _connectionThreadContext =
        //    new ThreadContext<ThreadConnections>("DataBaseFactory._connectionThreadContext", new ThreadConnections());

        //public static Dictionary<string, object> dataBaseFactoryCache
        //{
        //    get
        //    {
        //        if (_connectionThreadContext.Value == null)
        //            _connectionThreadContext.Set(new ThreadConnections());
        //        return _connectionThreadContext.Value?.Data;
        //    }
        //}


        /// <summary>
        /// Cache DataBaseFactory object for current thread
        /// </summary>
        [ThreadStatic] static TransactionalCache _dataBaseFactoryCache = new TransactionalCache();
        private static TransactionalCache dataBaseFactoryCache
        {
            get
            {
                if (_dataBaseFactoryCache is null)
                    _dataBaseFactoryCache = new TransactionalCache();
                return _dataBaseFactoryCache;
            }
        }


        public static IDataBaseFactory SetDataBaseFactory(IDataBaseFactory dataBaseFactory) =>
            dataBaseFactoryCache.SetData(dataBaseFactory.ConnectionString, dataBaseFactory);
        public static IDataBaseFactory GetDataBaseFactory(string connectionString) =>
            dataBaseFactoryCache.GetData<IDataBaseFactory>(connectionString);
        public static IDataBaseFactory GetDefaultDataBaseFactory() =>
            dataBaseFactoryCache.GetData<IDataBaseFactory>(DefaultDataBaseFactoryKey);
        public static IDataBaseFactory SetDefaultDataBaseFactory(IDataBaseFactory dataBaseFactory)
        {
            if (GetDefaultDataBaseFactory() == dataBaseFactory)
                return dataBaseFactory;
            dataBaseFactoryCache.SetData(DefaultDataBaseFactoryKey, dataBaseFactory);
            dataBaseFactoryCache.SetData(dataBaseFactory.ConnectionString, dataBaseFactory);
            return dataBaseFactory;
        }
        public static void ClearDataBaseFactoryCache() => dataBaseFactoryCache.ClearAll();

        public static IDbConnection CreateConnection(string connectionString = null)
        {
            var dbFactory = (string.IsNullOrWhiteSpace(connectionString))
                ? GetDefaultDataBaseFactory()
                : GetDataBaseFactory(connectionString);

            if (dbFactory is null)
                dbFactory = CreateDefault();

            return dbFactory?.Db.Connection;
        }

        private static SqlCommand CreateCommandDefault(string strCommand, CommandType commandType, params IDataParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(strCommand)
            {
                CommandTimeout = DefaultTimeout,
                CommandType = commandType
            };

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                    sqlCommand.Parameters.Add(parameter);
            }

            return sqlCommand;
        }

        public static IDbCommand CreateCommand(string strCommand, CommandType commandType, params IDataParameter[] parameters)
        {
            SqlCommand sqlCommand = CreateCommandDefault(strCommand, commandType, parameters);

            var dbFactory = GetDefaultDataBaseFactory();
            if (dbFactory is null)
                return sqlCommand;

            if (!dbFactory.Db.IsInTransaction)
                dbFactory.Begin();

            sqlCommand.Transaction = (SqlTransaction)dbFactory.Db.CurrentTransaction;
            sqlCommand.Connection = (SqlConnection)dbFactory.Db.Connection;
            return sqlCommand;
        }


        public static IDataBaseFactory CreateDefault(string connectionString = null)
        {
            var dbFactory = string.IsNullOrWhiteSpace(connectionString)
                ? GetDefaultDataBaseFactory()
                : GetDataBaseFactory(connectionString);
            if (dbFactory != null)
                return dbFactory;

            dbFactory = new DataBaseFactory(connectionString ?? ConfigurationManager.AppSettings["dsn"]);
            if (string.IsNullOrWhiteSpace(connectionString) || GetDefaultDataBaseFactory() == null)
                SetDefaultDataBaseFactory(dbFactory);
            else
                SetDataBaseFactory(dbFactory);
            return dbFactory;
        }

        public static IDataBaseFactory CreateDefault(DbConnSetting config)
        {
            var dbFactory = GetDefaultDataBaseFactory();
            if (dbFactory != null && (string.IsNullOrEmpty(config.ConnString) || dbFactory.ConnectionString.Trim() == config.ConnString.Trim()))
                return dbFactory;

            dbFactory = new DataBaseFactory(config);
            SetDefaultDataBaseFactory(dbFactory);
            return dbFactory;
        }

        #endregion static 

        private TransactionalCache Cache { get; } = new TransactionalCache();
        private IList<IDatabase> DbList { get; set; } = new List<IDatabase>();
        
        private readonly string _ConnectionString;
        public string ConnectionString => _ConnectionString ?? ConfigurationManager.AppSettings["dsn"];

        public bool UseAzureManagedIdentity { get; set; } = false;
        public string AccessToken { get; set; }
        public string TokenProviderConnectionString { get; set; }
        public string TenantId { get; set; }
        public int DatabaseNum { get; set; }

        private AccessToken _accessToken;

        private readonly SqlConnection _Connection;
        public SqlConnection Connection => _Connection;

        public IDatabase Db => GetDb();

        public DataBaseFactory() {}
        public DataBaseFactory(string connectionString)
        {
            _ConnectionString = connectionString;
        }
        public DataBaseFactory(DbConnSetting config)
        {
            _ConnectionString = config.ConnString;
            UseAzureManagedIdentity = config.UseAzureManagedIdentity;
            AccessToken = config.AccessToken;
            TokenProviderConnectionString = config.TokenProviderConnectionString;
            TenantId = config.TenantId;
            DatabaseNum = config.DatabaseNum;
        }
        public DataBaseFactory(MsSqlUniversalDBConfig config)
        {
            _ConnectionString = config.DbConnectionString;
            UseAzureManagedIdentity = config.UseAzureManagedIdentity;
            AccessToken = config.AccessToken;
            TokenProviderConnectionString = config.TokenProviderConnectionString;
            TenantId = config.TenantId;
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
                AddConnectionInterceptor(db);
                DbList.Add(db);
            }
            return db;
        }

        protected void AddConnectionInterceptor(IDatabase db)
        {
            if (db is null)
                return;
            if (UseAzureManagedIdentity)
            {
                db.AddDbConnectionInterceptor(SetConnectionForAzureManagedIdentity);
                db.AddDbConnectionInterceptorAsync(SetConnectionForAzureManagedIdentityAsync);
            }
            return;
        }

        public SqlConnection SetConnectionForAzureManagedIdentity(IDbConnection conn)
        {
            var sqlConn = (SqlConnection)conn;
            if (sqlConn is null)
                return sqlConn;

            if (!UseAzureManagedIdentity)
                return sqlConn;

            sqlConn.AccessToken = GetAzureTokenAsync().Result;
            return sqlConn;
        }
        public async Task<SqlConnection> SetConnectionForAzureManagedIdentityAsync(IDbConnection conn)
        {
            var sqlConn = (SqlConnection)conn;
            if (sqlConn is null)
                return sqlConn;

            if (!UseAzureManagedIdentity)
                return sqlConn;

            sqlConn.AccessToken = await GetAzureTokenAsync();
            return sqlConn;
        }

        protected async Task<string> GetAzureTokenAsync()
        {
            var now = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(_accessToken.Token) && now < _accessToken.ExpiresOn)
                return _accessToken.Token;

            try
            {
                //var tokenProvider = new AzureServiceTokenProvider(TokenProviderConnectionString);
                //sqlConn.AccessToken = tokenProvider.GetAccessTokenAsync(AzureDatabaseTokenUrl, TenantId).Result;

                //var tokenCredential = new DefaultAzureCredential(
                //    new DefaultAzureCredentialOptions
                //    {
                //        ExcludeEnvironmentCredential = true,
                //        ExcludeManagedIdentityCredential = true,
                //        ExcludeSharedTokenCacheCredential = true,
                //        ExcludeInteractiveBrowserCredential = true,
                //        //ExcludeAzureCliCredential = true,
                //        //ExcludeVisualStudioCredential = true,
                //        ExcludeVisualStudioCodeCredential = true,
                //        ExcludeAzurePowerShellCredential = true

                //        //VisualStudioCodeTenantId = this.TenantId
                //    }
                //);

                var tokenCredential = new VisualStudioCredential(
                    new VisualStudioCredentialOptions()
                    {
                        TenantId = this.TenantId
                    }
                );

                //var tokenCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
                //{
                //    VisualStudioTenantId = this.TenantId,

                //    ExcludeEnvironmentCredential = true,
                //    ExcludeManagedIdentityCredential = true,
                //    ExcludeSharedTokenCacheCredential = true,
                //    ExcludeInteractiveBrowserCredential = true,
                //    //ExcludeAzureCliCredential = true,
                //    //ExcludeVisualStudioCredential = true,
                //    ExcludeVisualStudioCodeCredential = true,
                //    ExcludeAzurePowerShellCredential = true
                //});

                _accessToken = await tokenCredential.GetTokenAsync(new TokenRequestContext(AzureDatabaseTokenScopes), CancellationToken.None);
                return _accessToken.Token;
            }
            catch (AuthenticationFailedException ex)
            {
                throw;
            }
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
        public virtual async void BeginAsync(string connectionString = null)
        {
            var db = GetDb(connectionString);
            await db.BeginTransactionAsync();
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
            => await TableRepository<TEntity, long>.ExistsAsync(this, id).ConfigureAwait(false);

        public virtual async Task<bool> ExistUniqueIdAsync<TEntity>(string uniqueKey) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.ExistUniqueIdAsync(this, uniqueKey).ConfigureAwait(false);

        public virtual async Task<bool> ExistsAsync<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.ExistsAsync(this, sql, args).ConfigureAwait(false); // True or False

        public virtual async Task<T> GetValueAsync<TEntity, T>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.GetValueAsync<T>(this, sql, args).ConfigureAwait(false);

        public virtual async Task<long> CountAsync<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.CountAsync(this, sql, args).ConfigureAwait(false);

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
            => (await TableRepository<TEntity, long>.GetAsync(this, id).ConfigureAwait(false))?.SetDataBaseFactory(this);

        public virtual async Task<TEntity> GetAsync<TEntity>(long id, IEnumerable<string> columns) where TEntity : TableRepository<TEntity, long>, new()
            => (await TableRepository<TEntity, long>.GetAsync(this, id, columns).ConfigureAwait(false))?.SetDataBaseFactory(this);

        public virtual async Task<TEntity> GetByIdAsync<TEntity>(string uid) where TEntity : TableRepository<TEntity, long>, new()
            => (await TableRepository<TEntity, long>.GetByIdAsync(this, uid).ConfigureAwait(false))?.SetDataBaseFactory(this);

        public virtual async Task<TEntity> GetByIdAsync<TEntity>(string uid, IEnumerable<string> columns) where TEntity : TableRepository<TEntity, long>, new()
            => (await TableRepository<TEntity, long>.GetByIdAsync(this, uid, columns).ConfigureAwait(false))?.SetDataBaseFactory(this);

        public virtual async Task<TEntity> GetByAsync<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => (await TableRepository<TEntity, long>.GetByAsync(this, sql, args).ConfigureAwait(false))?.SetDataBaseFactory(this);

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
            => await TableRepository<TEntity, long>.FindAsync(this).ConfigureAwait(false);

        public virtual async Task<IEnumerable<TEntity>> FindByOrderAsync<TEntity>(params string[] orderBy) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.FindByOrderAsync(this, orderBy).ConfigureAwait(false);

        public virtual async Task<IEnumerable<TEntity>> FindAsync<TEntity>(IEnumerable<string> columns, params string[] orderBy) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.FindAsync(this, columns, orderBy).ConfigureAwait(false);

        public virtual async Task<IEnumerable<TEntity>> FindAsync<TEntity>(string sql, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.FindAsync(this, sql, args).ConfigureAwait(false);

        public virtual async Task<IEnumerable<TEntity>> FindAsync<TEntity>(string sql, IEnumerable<string> columns, params object[] args) where TEntity : TableRepository<TEntity, long>, new()
            => await TableRepository<TEntity, long>.FindAsync(this, sql, columns, args).ConfigureAwait(false);

        #endregion

        #region Query - get single record from Cache
        public virtual T GetFromCache<T>(string key, Func<T> create, bool reNew = false) => Cache.FromCache<T>(key, create, reNew);

        public virtual TEntity GetFromCache<TEntity>(long id, bool reNew = false) where TEntity : TableRepository<TEntity, long>, new()
            => Cache.FromCache<TEntity>($"{typeof(TEntity).ToString()}:RowNum:{id}", () => Get<TEntity>(id), reNew);

        public virtual TEntity GetFromCacheById<TEntity>(string uid, bool reNew = false) where TEntity : TableRepository<TEntity, long>, new()
            => Cache.FromCache<TEntity>($"{typeof(TEntity).ToString()}:Id:{uid}", () => GetById<TEntity>(uid), reNew);


        public virtual async Task<TEntity> GetFromCacheAsync<TEntity>(long id, bool reNew = false) where TEntity : TableRepository<TEntity, long>, new()
            => await Cache.FromCache<Task<TEntity>>($"{typeof(TEntity).ToString()}:RowNum:{id}:async",
                async () => await GetAsync<TEntity>(id).ConfigureAwait(false), reNew).ConfigureAwait(false);

        public virtual async Task<TEntity> GetFromCacheByIdAsync<TEntity>(string uid, bool reNew = false) where TEntity : TableRepository<TEntity, long>, new()
            => await Cache.FromCache<Task<TEntity>>($"{typeof(TEntity).ToString()}:Id:{uid}:async",
                async () => await GetByIdAsync<TEntity>(uid).ConfigureAwait(false), reNew).ConfigureAwait(false);

        #endregion Query - get single record


    }
}
