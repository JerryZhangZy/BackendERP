using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.YoPoco
{
    [Serializable]
    public class StructureRepository<TEntity> : IStructureRepository<TEntity>
        where TEntity : StructureRepository<TEntity>, new()
    {
        public StructureRepository() {}

        public StructureRepository(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }

        #region DataBase
        [XmlIgnore, JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [XmlIgnore, JsonIgnore]
        public IDataBaseFactory dbFactory
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

        #region Properties

        [XmlIgnore, JsonIgnore]
        public virtual bool AllowNull { get; private set; } = true;
        [XmlIgnore, JsonIgnore]
        public virtual bool IsNew => false;

        public TEntity SetAllowNull(bool allowNull)
        {
            AllowNull = allowNull;
            return (TEntity) this;
        }

        #endregion Properties

        #region CallBack function delegate

        protected Action<TEntity> _OnClear = null;

        protected Func<TEntity, bool> _OnAfterLoad = null;
        protected Func<TEntity, bool> _OnBeforeSave = null;
        protected Func<IDataBaseFactory, TEntity, bool> _OnSave = null;
        protected Func<TEntity, bool> _OnAfterSave = null;
        protected Func<TEntity, bool> _OnBeforeDelete = null;
        protected Func<IDataBaseFactory, TEntity, bool> _OnDelete = null;
        protected Func<TEntity, bool> _OnAfterDelete = null;

        protected Func<Exception, bool> _OnException = null;

        public void OnClear(Action<TEntity> onClear) => _OnClear = onClear;
        public void OnAfterLoad(Func<TEntity, bool> onAfterLoad) => _OnAfterLoad = onAfterLoad;
        public void OnBeforeSave(Func<TEntity, bool> onBeforeSave) => _OnBeforeSave = onBeforeSave;
        public void OnSave(Func<IDataBaseFactory, TEntity, bool> onSave) => _OnSave = onSave;
        public void OnAfterSave(Func<TEntity, bool> onBeforeDelete) => _OnBeforeDelete = onBeforeDelete;
        public void OnBeforeDelete(Func<IDataBaseFactory, TEntity, bool> onDelete) => _OnDelete = onDelete;
        public void OnDelete(Func<TEntity, bool> onAfterDelete) => _OnAfterDelete = onAfterDelete;
        public void OnAfterDelete(Func<TEntity, bool> onAfterDelete) => _OnAfterDelete = onAfterDelete;
        public void OnException(Func<Exception, bool> onException) => _OnException = onException;

        #endregion CallBack function delegate

    }
}