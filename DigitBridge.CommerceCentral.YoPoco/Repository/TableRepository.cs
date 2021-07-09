using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DigitBridge.Base.Utility;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.YoPoco
{
    [ExplicitColumns]
    [Serializable]
    public class TableRepository<TEntity, TId> : ITableRepository<TEntity, TId>
        where TEntity : TableRepository<TEntity, TId>, new()
    {
        #region Public Static Methods

        public void Register()
        {
            PocoData.ForType(typeof(TEntity), new ConventionMapper());
        }

        public PocoData GetPocoData()
        {
            return PocoData.ForType(typeof(TEntity), new ConventionMapper());
        }

        #region Static Methods for Exist and get Scalar value 

        public static bool Exists(IDataBaseFactory dbFactory, TId id) => dbFactory.Db.Exists<TEntity>((object)id);
        public static bool ExistUniqueId(IDataBaseFactory dbFactory, string uniqueKey) => dbFactory.Db.ExistsUniqueId<TEntity>((object)uniqueKey);
        public static bool Exists(IDataBaseFactory dbFactory, string sql, params object[] args) => dbFactory.Db.Exists<TEntity>(sql, args); // True or False
        public static T GetValue<T>(IDataBaseFactory dbFactory, string sql, params object[] args) => dbFactory.Db.ExecuteScalar<T>(sql, args);
        public static long Count(IDataBaseFactory dbFactory, string sql, params object[] args)
            => dbFactory.Db.ExecuteScalar<long>(AutoSelectHelper.AddCountClause<TEntity>(sql), args);


        public static async Task<bool> ExistsAsync(IDataBaseFactory dbFactory, TId id)
            => await dbFactory.Db.ExistsAsync<TEntity>((object)id).ConfigureAwait(false);

        public static async Task<bool> ExistUniqueIdAsync(IDataBaseFactory dbFactory, string uniqueKey)
            => await dbFactory.Db.ExistUniqueIdAsync<TEntity>((object)uniqueKey).ConfigureAwait(false);

        public static async Task<bool> ExistsAsync(IDataBaseFactory dbFactory, string sql, params object[] args)
            => await dbFactory.Db.ExistsAsync<TEntity>(sql, args).ConfigureAwait(false); // True or False

        public static async Task<T> GetValueAsync<T>(IDataBaseFactory dbFactory, string sql, params object[] args)
            => await dbFactory.Db.ExecuteScalarAsync<T>(sql, args).ConfigureAwait(false);

        public static async Task<long> CountAsync(IDataBaseFactory dbFactory, string sql, params object[] args)
            => await dbFactory.Db.ExecuteScalarAsync<long>(AutoSelectHelper.AddCountClause<TEntity>(sql), args).ConfigureAwait(false);

        #endregion

        #region Query - Static Methods for get single record

        public static TEntity Get(IDataBaseFactory dbFactory, TId id)
            => dbFactory.Db.SingleOrDefault<TEntity>((object)id)?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory)?.ConvertDbFieldsToData();

        public static TEntity Get(IDataBaseFactory dbFactory, TId id, IEnumerable<string> columns)
            => dbFactory.Db.SingleOrDefault<TEntity>(columns, (object)id)?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory)?.ConvertDbFieldsToData();

        public static TEntity GetById(IDataBaseFactory dbFactory, string uid)
            => dbFactory.Db.SingleOrDefault<TEntity>((object)uid, true)?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory)?.ConvertDbFieldsToData();

        public static TEntity GetById(IDataBaseFactory dbFactory, string uid, IEnumerable<string> columns)
            => dbFactory.Db.SingleOrDefault<TEntity>(columns, (object)uid, true)?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory)?.ConvertDbFieldsToData();

        public static TEntity GetBy(IDataBaseFactory dbFactory, string sql, params object[] args)
            => dbFactory.Db.FirstOrDefault<TEntity>(sql, args)?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory)?.ConvertDbFieldsToData();


        public static async Task<TEntity> GetAsync(IDataBaseFactory dbFactory, TId id)
            => (await dbFactory.Db.SingleOrDefaultAsync<TEntity>((object)id).ConfigureAwait(false))?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory)?.ConvertDbFieldsToData();

        public static async Task<TEntity> GetAsync(IDataBaseFactory dbFactory, TId id, IEnumerable<string> columns)
            => (await dbFactory.Db.SingleOrDefaultAsync<TEntity>(columns, (object)id).ConfigureAwait(false))?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory)?.ConvertDbFieldsToData();

        public static async Task<TEntity> GetByIdAsync(IDataBaseFactory dbFactory, string uid)
            => (await dbFactory.Db.SingleOrDefaultAsync<TEntity>((object)uid, true).ConfigureAwait(false))?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory)?.ConvertDbFieldsToData();

        public static async Task<TEntity> GetByIdAsync(IDataBaseFactory dbFactory, string uid, IEnumerable<string> columns)
            => (await dbFactory.Db.SingleOrDefaultAsync<TEntity>(columns, (object)uid, true).ConfigureAwait(false))?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory)?.ConvertDbFieldsToData();

        public static async Task<TEntity> GetByAsync(IDataBaseFactory dbFactory, string sql, params object[] args)
            => (await dbFactory.Db.FirstOrDefaultAsync<TEntity>(sql, args).ConfigureAwait(false))?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory)?.ConvertDbFieldsToData();

        #endregion Query - get single record

        #region Query - Static Methods for find multiple records

        public static IEnumerable<TEntity> Find(IDataBaseFactory dbFactory)
            => dbFactory.Db.Query<TEntity>().ToList().SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory).ConvertDbFieldsToData<TEntity, TId>();

        public static IEnumerable<TEntity> FindByOrder(IDataBaseFactory dbFactory, params string[] orderBy)
            => dbFactory.Db.Query<TEntity>($"ORDER BY {orderBy.JoinToString(",")}").ToList().SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory).ConvertDbFieldsToData<TEntity, TId>();
        public static IEnumerable<TEntity> Find(IDataBaseFactory dbFactory, IEnumerable<string> columns, params string[] orderBy)
            => dbFactory.Db.Query<TEntity>(columns, $"ORDER BY {orderBy.JoinToString(",")}").ToList().SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory).ConvertDbFieldsToData<TEntity, TId>();

        public static IEnumerable<TEntity> Find(IDataBaseFactory dbFactory, string sql, params object[] args)
            => dbFactory.Db.Query<TEntity>(sql, args).ToList().SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory).ConvertDbFieldsToData<TEntity, TId>();

        public static IEnumerable<TEntity> Find(IDataBaseFactory dbFactory, string sql, IEnumerable<string> columns, params object[] args)
            => dbFactory.Db.Query<TEntity>(columns, sql, args).ToList().SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory).ConvertDbFieldsToData<TEntity, TId>();


        public static async Task<IEnumerable<TEntity>> FindAsync(IDataBaseFactory dbFactory)
            => (await dbFactory.Db.FetchAsync<TEntity>().ConfigureAwait(false)).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory).ConvertDbFieldsToData<TEntity, TId>();

        public static async Task<IEnumerable<TEntity>> FindByOrderAsync(IDataBaseFactory dbFactory, params string[] orderBy)
            => (await dbFactory.Db.FetchAsync<TEntity>($"ORDER BY {orderBy.JoinToString(",")}").ConfigureAwait(false)).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory).ConvertDbFieldsToData<TEntity, TId>();

        public static async Task<IEnumerable<TEntity>> FindAsync(IDataBaseFactory dbFactory, IEnumerable<string> columns, params string[] orderBy)
            => (await dbFactory.Db.FetchAsync<TEntity>(columns, $"ORDER BY {orderBy.JoinToString(",")}").ConfigureAwait(false)).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory).ConvertDbFieldsToData<TEntity, TId>();

        public static async Task<IEnumerable<TEntity>> FindAsync(IDataBaseFactory dbFactory, string sql, params object[] args)
            => (await dbFactory.Db.FetchAsync<TEntity>(sql, args).ConfigureAwait(false)).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory).ConvertDbFieldsToData<TEntity, TId>();

        public static async Task<IEnumerable<TEntity>> FindAsync(IDataBaseFactory dbFactory, string sql, IEnumerable<string> columns, params object[] args)
            => (await dbFactory.Db.FetchAsync<TEntity>(columns, sql, args).ConfigureAwait(false)).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory).ConvertDbFieldsToData<TEntity, TId>();

        #endregion

        #endregion Public Static Methods

        public TableRepository() { }

        public TableRepository(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }

        #region DataBase
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        protected IDataBaseFactory _dbFactory;

        [XmlIgnore, JsonIgnore, IgnoreCompare]
        protected IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public TEntity SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
            return (TEntity)this;
        }

        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public IDatabase db => dbFactory?.Db;

        public virtual ITransaction GetTransaction()
            => db.GetTransaction();

        #endregion DataBase

        #region Fields Variables

        [ResultColumn(Name = "RowNum", IncludeInAutoSelect = IncludeInAutoSelect.Yes)]
        [XmlIgnore]
        protected long _rowNum;

        [ResultColumn(Name = "EnterDateUtc", IncludeInAutoSelect = IncludeInAutoSelect.Yes)]
        [XmlIgnore]
        protected DateTime? _enterDateUtc;

        [ResultColumn(Name = "DigitBridgeGuid", IncludeInAutoSelect = IncludeInAutoSelect.Yes)]
        [XmlIgnore]
        protected Guid _digitBridgeGuid;

        #endregion Fields Variables

        #region Properties

        public virtual long RowNum => _rowNum;
        [IgnoreCompare]
        public virtual string UniqueId => string.Empty;
        [IgnoreCompare]
        public DateTime? EnterDateUtc => _enterDateUtc;
        [IgnoreCompare]
        public Guid DigitBridgeGuid => _digitBridgeGuid;

        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool AllowNull { get; private set; } = true;
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool IsNew => RowNum <= 0;

        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public virtual bool IsEmpty => false;

        public TEntity SetAllowNull(bool allowNull)
        {
            AllowNull = allowNull;
            return (TEntity)this;
        }

        //TODO Add method to identify entity changed
        private bool NeedsUpdate
        {
            get
            {
                //ObjectCacheData<T> cachedData = GetCacheData();
                //return (cachedData._UpdatedObjects.Count > 0) || (cachedData._InsertedObjects.Count > 0);
                return true;
            }
        }

        public virtual void ClearMetaData()
        {
            _rowNum = 0;
            _enterDateUtc = null;
            _digitBridgeGuid = Guid.NewGuid();
            return;
        }

        #endregion Properties

        #region Property Changed
        protected IList<string> _changedProperties;
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public virtual IList<string> ChangedProperties
        {
            get
            {
                if (_changedProperties is null)
                    _changedProperties = new List<string>();
                return _changedProperties;
            }
        }
        public virtual void OnPropertyChanged(string name, object value)
        {
            if (!string.IsNullOrEmpty(name) && !ChangedProperties.Contains(name))
                ChangedProperties.Add(name);
        }
        public virtual void ClearChangedProperties()
        {
            ChangedProperties.Clear();
        }

        #endregion Property Changed

        #region CRUD Methods

        public virtual bool Add(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return Add();
        }

        public virtual bool Add()
        {
            if (!IsNew) return false;
            object rtn;
            this.ConvertDataFieldsToDb();
            if (db.IsInTransaction)
                rtn = db.Insert(this.SetAllowNull(false));
            else
            {
                db.BeginTransaction();
                rtn = db.Insert(this.SetAllowNull(false));
                db.CompleteTransaction();
            }
            return (rtn != null);
        }

        public virtual int Put(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return Put();
        }

        public virtual int Put()
        {
            if (IsNew) return 0;
            int rtn;
            this.ConvertDataFieldsToDb();
            if (db.IsInTransaction)
                rtn = db.Update(this.SetAllowNull(false));
            else
            {
                db.BeginTransaction();
                rtn = db.Update(this.SetAllowNull(false));
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual int Patch(IDataBaseFactory DbFactory)
        {
            return Patch(DbFactory, ChangedProperties);
        }

        public virtual int Patch(IDataBaseFactory DbFactory, IEnumerable<string> columns)
        {
            SetDataBaseFactory(DbFactory);
            return Patch(columns);
        }
        public virtual int Patch()
        {
            return Patch(ChangedProperties);
        }

        public virtual int Patch(IEnumerable<string> columns)
        {
            if (IsNew) return 0;
            int rtn;
            this.ConvertDataFieldsToDb();
            if (db.IsInTransaction)
                rtn = db.Update(this.SetAllowNull(false), columns);
            else
            {
                db.BeginTransaction();
                rtn = db.Update(this.SetAllowNull(false), columns);
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual int Delete(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return Delete();
        }

        public virtual int Delete()
        {
            if (IsNew) return 0;
            int rtn;
            this.ConvertDataFieldsToDb();
            if (db.IsInTransaction)
                rtn = db.Delete(this);
            else
            {
                db.BeginTransaction();
                rtn = db.Delete(this);
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual int Delete(IDataBaseFactory DbFactory, string sql, params object[] args)
        {
            SetDataBaseFactory(DbFactory);
            return Delete(sql, args);
        }

        public virtual int Delete(string sql, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(sql)) return 0;
            int rtn;
            if (db.IsInTransaction)
                rtn = db.Delete<TEntity>(sql, args);
            else
            {
                db.BeginTransaction();
                rtn = db.Delete<TEntity>(sql, args);
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual bool Save(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return Save();
        }

        public virtual bool Save()
        {
            return (IsNew)
                ? Add()
                : Put() > 0;
        }


        public virtual async Task<bool> AddAsync(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return await AddAsync().ConfigureAwait(false);
        }

        public virtual async Task<bool> AddAsync()
        {
            if (!IsNew) return false;
            object rtn;
            this.ConvertDataFieldsToDb();
            if (db.IsInTransaction)
                rtn = await db.InsertAsync(this.SetAllowNull(false)).ConfigureAwait(false);
            else
            {
                await db.BeginTransactionAsync().ConfigureAwait(false);
                rtn = await db.InsertAsync(this.SetAllowNull(false)).ConfigureAwait(false);
                db.CompleteTransaction();
            }
            return (rtn != null);
        }

        public virtual async Task<int> PutAsync(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return await PutAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> PutAsync()
        {
            if (IsNew) return 0;
            int rtn;
            this.ConvertDataFieldsToDb();
            if (db.IsInTransaction)
                rtn = await db.UpdateAsync(this.SetAllowNull(false)).ConfigureAwait(false);
            else
            {
                await db.BeginTransactionAsync().ConfigureAwait(false);
                rtn = await db.UpdateAsync(this.SetAllowNull(false)).ConfigureAwait(false);
                db.CompleteTransaction();
            }
            return rtn;
        }
        public virtual async Task<int> PatchAsync(IDataBaseFactory DbFactory)
        {
            return await PatchAsync(DbFactory, this.ChangedProperties).ConfigureAwait(false);
        }
        public virtual async Task<int> PatchAsync(IDataBaseFactory DbFactory, IEnumerable<string> columns)
        {
            SetDataBaseFactory(DbFactory);
            return await PatchAsync(columns).ConfigureAwait(false);
        }
        public virtual async Task<int> PatchAsync()
        {
            return await PatchAsync(this.ChangedProperties).ConfigureAwait(false);
        }

        public virtual async Task<int> PatchAsync(IEnumerable<string> columns)
        {
            if (IsNew) return 0;
            int rtn;
            this.ConvertDataFieldsToDb();
            if (db.IsInTransaction)
                rtn = await db.UpdateAsync(this.SetAllowNull(false), columns).ConfigureAwait(false);
            else
            {
                await db.BeginTransactionAsync().ConfigureAwait(false);
                rtn = await db.UpdateAsync(this.SetAllowNull(false), columns).ConfigureAwait(false);
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual async Task<int> DeleteAsync(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return await DeleteAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> DeleteAsync()
        {
            if (IsNew) return 0;
            int rtn;
            this.ConvertDataFieldsToDb();
            if (db.IsInTransaction)
                rtn = await db.DeleteAsync(this).ConfigureAwait(false);
            else
            {
                await db.BeginTransactionAsync().ConfigureAwait(false);
                rtn = await db.DeleteAsync(this).ConfigureAwait(false);
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual async Task<int> DeleteAsync(IDataBaseFactory DbFactory, string sql, params object[] args)
        {
            SetDataBaseFactory(DbFactory);
            return await DeleteAsync(sql, args).ConfigureAwait(false);
        }

        public virtual async Task<int> DeleteAsync(string sql, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(sql)) return 0;
            int rtn;
            if (db.IsInTransaction)
                rtn = await db.DeleteAsync<TEntity>(sql, args).ConfigureAwait(false);
            else
            {
                await db.BeginTransactionAsync().ConfigureAwait(false);
                rtn = await db.DeleteAsync<TEntity>(sql, args).ConfigureAwait(false);
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual async Task<bool> SaveAsync(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return await SaveAsync().ConfigureAwait(false);
        }

        public virtual async Task<bool> SaveAsync()
        {
            return (IsNew)
                ? await AddAsync().ConfigureAwait(false)
                : (await PutAsync().ConfigureAwait(false)) > 0;
        }

        #endregion

        public virtual TEntity ConvertDbFieldsToData() { return (TEntity)this; }
        public virtual TEntity ConvertDataFieldsToDb() { return (TEntity)this; }

        #region Interface Definition

        public bool Equals(TEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!RowNum.Equals(other.RowNum)) return false;
            return PropertyEquals(other);
        }
        public virtual bool PropertyEquals(TEntity other)
        {
            var unequalProperties = GetPocoData().GetChangedProperties(this, other);
            return !unequalProperties.Any();
        }
        public virtual TEntity CopyFrom(TEntity other)
        {
            return CopyFrom(other, new List<string>() { "RowNum", "AllowNull", "IsNew" });
        }
        public virtual TEntity CopyFrom(TEntity other, IEnumerable<string> ignoreColumns)
        {
            GetPocoData().CopyProperties(other, this, true, ignoreColumns);
            return (TEntity)this;
        }
        public virtual TEntity Clear()
        {
            ClearChangedProperties();
            return (TEntity)this;
        }

        #endregion Interface Definition

    }

} 