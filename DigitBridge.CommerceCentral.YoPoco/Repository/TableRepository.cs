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
    public class TableRepository<TEntity, TId> : ITableRepository<TEntity, TId>, IEquatable<TEntity>
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
            => await dbFactory.Db.ExistsAsync<TEntity>((object)id);

        public static async Task<bool> ExistUniqueIdAsync(IDataBaseFactory dbFactory, string uniqueKey)
            => await dbFactory.Db.ExistUniqueIdAsync<TEntity>((object)uniqueKey);

        public static async Task<bool> ExistsAsync(IDataBaseFactory dbFactory, string sql, params object[] args)
            => await dbFactory.Db.ExistsAsync<TEntity>(sql, args); // True or False

        public static async Task<T> GetValueAsync<T>(IDataBaseFactory dbFactory, string sql, params object[] args)
            => await dbFactory.Db.ExecuteScalarAsync<T>(sql, args);

        public static async Task<long> CountAsync(IDataBaseFactory dbFactory, string sql, params object[] args)
            => await dbFactory.Db.ExecuteScalarAsync<long>(AutoSelectHelper.AddCountClause<TEntity>(sql), args);

        #endregion

        #region Query - Static Methods for get single record

        public static TEntity Get(IDataBaseFactory dbFactory, TId id)
            => dbFactory.Db.SingleOrDefault<TEntity>((object)id)?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory);

        public static TEntity Get(IDataBaseFactory dbFactory, TId id, IEnumerable<string> columns)
            => dbFactory.Db.SingleOrDefault<TEntity>(columns, (object)id)?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory);

        public static TEntity GetById(IDataBaseFactory dbFactory, string uid)
            => dbFactory.Db.SingleOrDefault<TEntity>((object)uid, true)?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory);

        public static TEntity GetById(IDataBaseFactory dbFactory, string uid, IEnumerable<string> columns)
            => dbFactory.Db.SingleOrDefault<TEntity>(columns, (object)uid, true)?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory);

        public static TEntity GetBy(IDataBaseFactory dbFactory, string sql, params object[] args)
            => dbFactory.Db.FirstOrDefault<TEntity>(sql, args)?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory);


        public static async Task<TEntity> GetAsync(IDataBaseFactory dbFactory, TId id)
            => (await dbFactory.Db.SingleOrDefaultAsync<TEntity>((object)id))?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory);

        public static async Task<TEntity> GetAsync(IDataBaseFactory dbFactory, TId id, IEnumerable<string> columns)
            => (await dbFactory.Db.SingleOrDefaultAsync<TEntity>(columns, (object)id))?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory);

        public static async Task<TEntity> GetByIdAsync(IDataBaseFactory dbFactory, string uid)
            => (await dbFactory.Db.SingleOrDefaultAsync<TEntity>((object)uid, true))?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory);

        public static async Task<TEntity> GetByIdAsync(IDataBaseFactory dbFactory, string uid, IEnumerable<string> columns)
            => (await dbFactory.Db.SingleOrDefaultAsync<TEntity>(columns, (object)uid, true))?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory);

        public static async Task<TEntity> GetByAsync(IDataBaseFactory dbFactory, string sql, params object[] args)
            => (await dbFactory.Db.FirstOrDefaultAsync<TEntity>(sql, args))?.SetAllowNull(false)?.SetDataBaseFactory(dbFactory);

        #endregion Query - get single record

        #region Query - Static Methods for find multiple records

        public static IEnumerable<TEntity> Find(IDataBaseFactory dbFactory)
            => dbFactory.Db.Query<TEntity>().SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory);

        public static IEnumerable<TEntity> FindByOrder(IDataBaseFactory dbFactory, params string[] orderBy)
            => dbFactory.Db.Query<TEntity>($"ORDER BY {orderBy.JoinToString(",")}").SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory);
        public static IEnumerable<TEntity> Find(IDataBaseFactory dbFactory, IEnumerable<string> columns, params string[] orderBy)
            => dbFactory.Db.Query<TEntity>(columns, $"ORDER BY {orderBy.JoinToString(",")}").SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory);

        public static IEnumerable<TEntity> Find(IDataBaseFactory dbFactory, string sql, params object[] args)
            => dbFactory.Db.Query<TEntity>(sql, args).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory);

        public static IEnumerable<TEntity> Find(IDataBaseFactory dbFactory, string sql, IEnumerable<string> columns, params object[] args)
            => dbFactory.Db.Query<TEntity>(columns, sql, args).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory);


        public static async Task<IEnumerable<TEntity>> FindAsync(IDataBaseFactory dbFactory)
            => (await dbFactory.Db.FetchAsync<TEntity>()).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory);

        public static async Task<IEnumerable<TEntity>> FindByOrderAsync(IDataBaseFactory dbFactory, params string[] orderBy)
            => (await dbFactory.Db.FetchAsync<TEntity>($"ORDER BY {orderBy.JoinToString(",")}")).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory);

        public static async Task<IEnumerable<TEntity>> FindAsync(IDataBaseFactory dbFactory, IEnumerable<string> columns, params string[] orderBy)
            => (await dbFactory.Db.FetchAsync<TEntity>(columns, $"ORDER BY {orderBy.JoinToString(",")}")).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory);

        public static async Task<IEnumerable<TEntity>> FindAsync(IDataBaseFactory dbFactory, string sql, params object[] args)
            => (await dbFactory.Db.FetchAsync<TEntity>(sql, args)).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory);

        public static async Task<IEnumerable<TEntity>> FindAsync(IDataBaseFactory dbFactory, string sql, IEnumerable<string> columns, params object[] args)
            => (await dbFactory.Db.FetchAsync<TEntity>(columns, sql, args)).SetAllowNull<TEntity, TId>(false).SetDataBaseFactory<TEntity, TId>(dbFactory);

        #endregion

        #endregion Public Static Methods

        public TableRepository() {}

        public TableRepository(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }

        #region DataBase
        [XmlIgnore, JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [XmlIgnore, JsonIgnore]
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

        [XmlIgnore, JsonIgnore]
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

        public long RowNum => _rowNum;
        [IgnoreCompare]
        public virtual string UniqueId => string.Empty;
        [IgnoreCompare]
        public DateTime? EnterDateUtc => _enterDateUtc;
        [IgnoreCompare]
        public Guid DigitBridgeGuid => _digitBridgeGuid;

        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool AllowNull { get; private set; } = true;
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool IsNew => _rowNum <= 0;

        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public virtual bool IsEmpty => false;

        public TEntity SetAllowNull(bool allowNull)
        {
            AllowNull = allowNull;
            return (TEntity) this;
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

        #endregion Properties

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

        public virtual int Patch(IDataBaseFactory DbFactory, IEnumerable<string> columns)
        {
            SetDataBaseFactory(DbFactory);
            return Patch(columns);
        }

        public virtual int Patch(IEnumerable<string> columns)
        {
            if (IsNew) return 0;
            int rtn;
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
            return await AddAsync();
        }

        public virtual async Task<bool> AddAsync()
        {
            if (!IsNew) return false;
            object rtn;
            if (db.IsInTransaction)
                rtn = await db.InsertAsync(this.SetAllowNull(false));
            else
            {
                await db.BeginTransactionAsync();
                rtn = await db.InsertAsync(this.SetAllowNull(false));
                db.CompleteTransaction();
            }
            return (rtn != null);
        }

        public virtual async Task<int> PutAsync(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return await PutAsync();
        }

        public virtual async Task<int> PutAsync()
        {
            if (IsNew) return 0;
            int rtn;
            if (db.IsInTransaction)
                rtn = await db.UpdateAsync(this.SetAllowNull(false));
            else
            {
                await db.BeginTransactionAsync();
                rtn = await db.UpdateAsync(this.SetAllowNull(false));
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual async Task<int> PatchAsync(IDataBaseFactory DbFactory, IEnumerable<string> columns)
        {
            SetDataBaseFactory(DbFactory);
            return await PatchAsync(columns);
        }

        public virtual async Task<int> PatchAsync(IEnumerable<string> columns)
        {
            if (IsNew) return 0;
            int rtn;
            if (db.IsInTransaction)
                rtn = await db.UpdateAsync(this.SetAllowNull(false), columns);
            else
            {
                await db.BeginTransactionAsync();
                rtn = await db.UpdateAsync(this.SetAllowNull(false), columns);
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual async Task<int> DeleteAsync(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return await DeleteAsync();
        }

        public virtual async Task<int> DeleteAsync()
        {
            if (IsNew) return 0;
            int rtn;
            if (db.IsInTransaction)
                rtn = await db.DeleteAsync(this);
            else
            {
                await db.BeginTransactionAsync();
                rtn = await db.DeleteAsync(this);
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual async Task<int> DeleteAsync(IDataBaseFactory DbFactory, string sql, params object[] args)
        {
            SetDataBaseFactory(DbFactory);
            return await DeleteAsync(sql, args);
        }

        public virtual async Task<int> DeleteAsync(string sql, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(sql)) return 0;
            int rtn;
            if (db.IsInTransaction)
                rtn = await db.DeleteAsync<TEntity>(sql, args);
            else
            {
                await db.BeginTransactionAsync();
                rtn = await db.DeleteAsync<TEntity>(sql, args);
                db.CompleteTransaction();
            }
            return rtn;
        }

        public virtual async Task<bool> SaveAsync(IDataBaseFactory DbFactory)
        {
            SetDataBaseFactory(DbFactory);
            return await SaveAsync();
        }

        public virtual async Task<bool> SaveAsync()
        {
            return (IsNew)
                ? await AddAsync()
                : (await PutAsync()) > 0;
        }

        #endregion


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
            return (TEntity)this;
        }

        #endregion Interface Definition

    }


    public static class TableRepositoryExtensions
    {
        public static IEnumerable<TEntity> SetAllowNull<TEntity>(this IEnumerable<TEntity> lst, bool allowNull)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.SetAllowNull<TEntity, long>(allowNull);

        public static IEnumerable<TEntity> SetAllowNull<TEntity, TId>(this IEnumerable<TEntity> lst, bool allowNull) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var tableRepositories = lst.ToList();
            tableRepositories?.ForEach(i => i?.SetAllowNull(allowNull));
            return tableRepositories;
        }

        public static IEnumerable<TEntity> SetDataBaseFactory<TEntity>(this IEnumerable<TEntity> lst, IDataBaseFactory dbFactory)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.SetDataBaseFactory<TEntity, long>(dbFactory);

        public static IEnumerable<TEntity> SetDataBaseFactory<TEntity, TId>(this IEnumerable<TEntity> lst, IDataBaseFactory dbFactory) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var tableRepositories = lst.ToList();
            tableRepositories?.ForEach(i => i?.SetDataBaseFactory(dbFactory));
            return tableRepositories;
        }

        public static bool Save<TEntity>(this IEnumerable<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.Save<TEntity, long>();

        public static bool Save<TEntity, TId>(this IEnumerable<TEntity> lst) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var rtn = true;
            var tableRepositories = lst.ToList();
            foreach (var tableRepository in tableRepositories.Where(x => x != null))
            {
                var rtn1 = tableRepository.Save();
                rtn = (rtn1) && rtn;
            }
            return rtn;
        }

        public static int Delete<TEntity>(this IEnumerable<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.Delete<TEntity, long>();

        public static int Delete<TEntity, TId>(this IEnumerable<TEntity> lst) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var rtn = 0;
            var tableRepositories = lst.ToList();
            foreach (var tableRepository in tableRepositories.Where(x => x != null))
                rtn += tableRepository.Delete();
            return rtn;
        }

        public static async Task<bool> SaveAsync<TEntity>(this IEnumerable<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => await lst.SaveAsync<TEntity, long>();

        public static async Task<bool> SaveAsync<TEntity, TId>(this IEnumerable<TEntity> lst) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var rtn = true;
            var tableRepositories = lst.ToList();
            foreach (var tableRepository in tableRepositories.Where(x => x != null))
            {
                var rtn1 = await tableRepository.SaveAsync();
                rtn = (rtn1) && rtn;
            }
            return rtn;
        }

        public static async Task<int> DeleteAsync<TEntity>(this IEnumerable<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => await lst.DeleteAsync<TEntity, long>();

        public static async Task<int> DeleteAsync<TEntity, TId>(this IEnumerable<TEntity> lst) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var rtn = 0;
            var tableRepositories = lst.ToList();
            foreach (var tableRepository in tableRepositories.Where(x => x != null))
                rtn += await tableRepository.DeleteAsync();
            return rtn;
        }

        public static TEntity FindByRowNum<TEntity>(this IEnumerable<TEntity> lst, long rowNum)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.FindByRowNum<TEntity, long>(rowNum);

        public static TEntity FindByRowNum<TEntity, TId>(this IEnumerable<TEntity> lst, long rowNum) where TEntity : TableRepository<TEntity, TId>, new()
        {
            return (lst == null || rowNum <= 0) ? null : (TEntity)lst.FirstOrDefault(item => item.RowNum == rowNum);
        }

        public static TEntity FindById<TEntity>(this IEnumerable<TEntity> lst, string uniqueId)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.FindById<TEntity, long>(uniqueId);

        public static TEntity FindById<TEntity, TId>(this IEnumerable<TEntity> lst, string uniqueId) where TEntity : TableRepository<TEntity, TId>, new()
        {
            return (lst == null || string.IsNullOrEmpty(uniqueId)) ? null : (TEntity)lst.FirstOrDefault(item => item.UniqueId == uniqueId);
        }

        public static TEntity FindByObject<TEntity>(this IEnumerable<TEntity> lst, TEntity obj)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.FindByObject<TEntity, long>(obj);

        public static TEntity FindByObject<TEntity, TId>(this IEnumerable<TEntity> lst, TEntity obj) where TEntity : TableRepository<TEntity, TId>, new()
        {
            if (obj.RowNum <= 0 && string.IsNullOrEmpty(obj.UniqueId)) return null;

            var tableRepositories = lst.ToList();
            if (!tableRepositories.Any()) return null;

            var index = tableRepositories.IndexOf(obj);
            if (index >= 0)
                return tableRepositories[index];

            return tableRepositories.FirstOrDefault(item => 
                (obj.RowNum > 0 && item.RowNum == obj.RowNum) || 
                (!string.IsNullOrEmpty(obj.UniqueId) && item.UniqueId == obj.UniqueId));
        }

        public static IEnumerable<TEntity> CopyFrom<TEntity>(this IEnumerable<TEntity> lst, IEnumerable<TEntity> lstFrom, IEnumerable<string> ignoreColumns)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.CopyFrom<TEntity, long>(lstFrom, ignoreColumns);

        public static IEnumerable<TEntity> CopyFrom<TEntity, TId>(this IEnumerable<TEntity> lst, IEnumerable<TEntity> lstFrom, IEnumerable<string> ignoreColumns) 
            where TEntity : TableRepository<TEntity, TId>, new()
        {
            var repositories = lst.ToList();
            var fromRepositories = lstFrom.ToList();
            if (!fromRepositories.Any()) return repositories;

            if (fromRepositories.Count == 1)
            {
                repositories.CopyFrom<TEntity, TId>(fromRepositories[0], ignoreColumns);
                return repositories;
            }
            // if copy multiple items, need copy by same order of from list
            var lstOrig = new List<TEntity>(repositories);
            repositories.Clear();
            foreach (TEntity l in fromRepositories)
            {
                if (l == null) continue;
                var o = l.RowNum > 0
                    ? lstOrig.FindByRowNum<TEntity, TId>(l.RowNum)
                    : lstOrig.FindByObject<TEntity, TId>(l);
                if (o is null) 
                    o = l;

                o.SetAllowNull(false);
                repositories.Add(o);
                o.CopyFrom(l, ignoreColumns);
            }
            return repositories;
        }

        public static IEnumerable<TEntity> CopyFrom<TEntity, TId>(this IEnumerable<TEntity> lst, TEntity obj, IEnumerable<string> ignoreColumns) 
            where TEntity : TableRepository<TEntity, TId>, new()
        {
            if (obj == null) return lst;
            var repositories = lst.ToList();
            var o = obj.RowNum > 0
                ? repositories.FindByRowNum<TEntity, TId>(obj.RowNum)
                : repositories.FindByObject<TEntity, TId>(obj);
            if (o == null)
            {
                o = new TEntity().SetAllowNull(false);
                repositories.Add(o);
            }
            o.CopyFrom(obj, ignoreColumns);
            return repositories;
        }

        public static IEnumerable<TEntity> CopyFrom<TEntity>(this IEnumerable<TEntity> lst, IEnumerable<TEntity> lstFrom)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.CopyFrom<TEntity, long>(lstFrom);

        public static IEnumerable<TEntity> CopyFrom<TEntity, TId>(this IEnumerable<TEntity> lst, IEnumerable<TEntity> lstFrom) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var repositories = lst.ToList();
            var fromRepositories = lstFrom.ToList();
            if (!fromRepositories.Any()) return repositories;

            if (fromRepositories.Count == 1)
            {
                repositories.CopyFrom<TEntity, TId>(fromRepositories[0]);
                return repositories;
            }
            // if copy multiple items, need copy by same order of from list
            var lstOrig = new List<TEntity>(repositories);
            repositories.Clear();
            foreach (TEntity l in fromRepositories)
            {
                if (l == null) continue;
                var o = l.RowNum > 0
                    ? lstOrig.FindByRowNum<TEntity, TId>(l.RowNum)
                    : lstOrig.FindByObject<TEntity, TId>(l);
                if (o is null)
                    o = l;

                o.SetAllowNull(false);
                repositories.Add(o);
                o.CopyFrom(l);
            }
            return repositories;
        }

        public static IEnumerable<TEntity> CopyFrom<TEntity, TId>(this IEnumerable<TEntity> lst, TEntity obj) where TEntity : TableRepository<TEntity, TId>, new()
        {
            if (obj == null) return lst;
            var repositories = lst.ToList();
            var o = obj.RowNum > 0
                ? repositories.FindByRowNum<TEntity, TId>(obj.RowNum)
                : repositories.FindByObject<TEntity, TId>(obj);
            if (o == null)
            {
                o = new TEntity().SetAllowNull(false);
                repositories.Add(o);
            }
            o.CopyFrom(obj);
            return repositories;
        }

        public static TEntity AddOrReplace<TEntity>(this IEnumerable<TEntity> lst, TEntity obj)
            where TEntity : TableRepository<TEntity, long>, new()
        {
            var tableRepositories = lst.ToList();
            var exist = tableRepositories.FindByObject(obj);
            if (exist is null)
                tableRepositories.Add(obj);
            else
            {
                var index = tableRepositories.IndexOf(exist);
                if (index >= 0)
                    tableRepositories[index] = obj;
            }
            return obj;
        }

        public static IEnumerable<TEntity> FindNotExistsByRowNum<TEntity>(this IEnumerable<TEntity> lst,
            IEnumerable<TEntity> lstMatch)
            where TEntity : TableRepository<TEntity, long>, new()
        {
            var tableRepositories = lst.ToList();
            return tableRepositories.Where(x => x.RowNum > 0 && lstMatch.FirstOrDefault(m => m.RowNum == x.RowNum) is null);
        }

        public static TEntity FindBy<TEntity>(this IEnumerable<TEntity> lst, Func<TEntity, bool> predicate)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.FindBy<TEntity, long>(predicate);

        public static TEntity FindBy<TEntity, TId>(this IEnumerable<TEntity> lst, Func<TEntity, bool> predicate) where TEntity : TableRepository<TEntity, TId>, new()
        {
            return (lst == null) ? null : (TEntity)lst.FirstOrDefault(predicate);
        }

        public static TEntity AddOrReplaceBy<TEntity>(this IEnumerable<TEntity> lst, TEntity obj, Func<TEntity, bool> predicate)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.AddOrReplaceBy<TEntity, long>(obj, predicate);

        public static TEntity AddOrReplaceBy<TEntity, TId>(this IEnumerable<TEntity> lst, TEntity obj, Func<TEntity, bool> predicate) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var tableRepositories = lst.ToList();
            var exist = tableRepositories.FindBy<TEntity, TId>(predicate);
            if (exist is null)
                tableRepositories.Add(obj);
            else
            {
                var index = tableRepositories.IndexOf(exist);
                if (index >= 0)
                    tableRepositories[index] = obj;
            }
            return obj;
        }

        public static TEntity Remove<TEntity>(this IEnumerable<TEntity> lst, TEntity obj)
            where TEntity : TableRepository<TEntity, long>, new()
        {
            var tableRepositories = lst.ToList();
            var exist = tableRepositories.FindByObject(obj);
            if (exist != null)
                tableRepositories.Remove(exist);
            return exist;
        }
        public static IEnumerable<TEntity> RemoveBy<TEntity>(this IEnumerable<TEntity> lst, Func<TEntity, bool> predicate)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.RemoveBy<TEntity, long>(predicate);

        public static IEnumerable<TEntity> RemoveBy<TEntity, TId>(this IEnumerable<TEntity> lst, Func<TEntity, bool> predicate) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var tableRepositories = lst.ToList();
            var removeList = tableRepositories.Where(predicate).ToList();
            foreach (var remove in removeList)
                tableRepositories.Remove(remove);
            return removeList;
        }
        public static IEnumerable<TEntity> RemoveEmpty<TEntity>(this IEnumerable<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.RemoveBy<TEntity, long>(x => x.IsEmpty);

        public static bool EqualsList<TEntity>(this IEnumerable<TEntity> lst, IEnumerable<TEntity> listOther)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.EqualsList<TEntity, long>(listOther);

        public static bool EqualsList<TEntity, TId>(this IEnumerable<TEntity> lst, IEnumerable<TEntity> listOther) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var tableRepositories = lst.ToList();
            var tableRepositoriesOther = listOther.ToList();
            if (tableRepositories.Count != tableRepositoriesOther.Count) return false;
            for (var i = 0; i < tableRepositories.Count; i++)
            {
                if (!tableRepositories[i].Equals(tableRepositoriesOther[i]))
                    return false;
            }
            return true;
        }

    }
}