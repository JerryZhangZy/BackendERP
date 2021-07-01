using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.YoPoco;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public abstract partial class ServiceBase<TService, TEntity> : IService<TService, TEntity>
        where TService : ServiceBase<TService, TEntity> 
        where TEntity : StructureRepository<TEntity>, new()
    {
        public ServiceBase() { ProcessMode = ProcessingMode.List; }

        public ServiceBase(IDataBaseFactory dbFactory): this()
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

        public TService SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
            return (TService)this;
        }

        #endregion DataBase

        #region Properties
        protected TEntity _data;
        [XmlIgnore, JsonIgnore]
        public virtual TEntity Data
        {
            get => (TEntity)_data;
        }

        [XmlIgnore, JsonIgnore]
        public virtual ProcessingMode ProcessMode { get; set; }

        #endregion Properties

        #region Methods
        public virtual TService Clear()
        {
            ProcessMode = ProcessingMode.List;
            ClearData();
            return (TService)this;
        }

        public virtual TService AttachData(TEntity data)
        {
            _data = data;
            _data.SetDataBaseFactory(dbFactory);
            return (TService)this;
        }
        public virtual TService DetachData(TEntity data)
        {
            _data = null;
            return (TService)this;
        }
        public virtual TService NewData()
        {
            _data = new TEntity();
            _data.SetDataBaseFactory(dbFactory);
            _data.New();
            return (TService)this;
        }
        public virtual TService ClearData()
        {
            if (_data is null)
                NewData();
            _data.Clear();
            return (TService)this;
        }
        public virtual TEntity CloneData()
        {
            if (_data is null)
                return null;
            return _data.Clone();
        }
        #endregion Methods

        #region CRUD Methods
        public virtual bool Get(long RowNum)
        {
            ClearData();
            return _data.Get(RowNum);
        }

        public virtual bool GetById(string id)
        {
            ClearData();
            return _data.GetById(id);
        }

        public virtual bool Save()
        {
            if (_data is null)
                return false;
            return _data.Save();
        }

        public virtual bool Delete()
        {
            if (_data is null)
                return false;
            return _data.Delete();
        }

        public virtual async Task<bool> GetAsync(long RowNum)
        {
            if (_data is null)
                NewData();
            return await _data.GetAsync(RowNum).ConfigureAwait(false);
        }

        public virtual async Task<bool> GetByIdAsync(string id)
        {
            if (_data is null)
                NewData();
            return await _data.GetByIdAsync(id).ConfigureAwait(false);
        }

        public virtual async Task<bool> SaveAsync()
        {
            if (_data is null)
                return false;
            return await _data.SaveAsync().ConfigureAwait(false);
        }

        public virtual async Task<bool> DeleteAsync()
        {
            if (_data is null)
                return false;
            return await _data.DeleteAsync().ConfigureAwait(false);
        }

        #endregion CRUD Methods


    }
}
