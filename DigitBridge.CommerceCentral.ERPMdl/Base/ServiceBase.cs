using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.YoPoco;
using System.Threading.Tasks;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public abstract partial class ServiceBase<TService, TEntity, TDto> : IService<TService, TEntity, TDto>
        where TService : ServiceBase<TService, TEntity, TDto>
        where TEntity : StructureRepository<TEntity>, new()
        where TDto : class, new()
    {
        public ServiceBase() 
        { 
            ProcessMode = ProcessingMode.List;
            Init();
        }
        public ServiceBase(
            IDataBaseFactory dbFactory) : this()
        {
            SetDataBaseFactory(dbFactory);
        }

        public ServiceBase(
            IDataBaseFactory dbFactory,
            IDtoMapper<TEntity, TDto> dtoMapper,
            ICalculator<TEntity> calculator,
            IList<IValidator<TEntity>> validators) : this(dbFactory)
        {
            _DtoMapper = dtoMapper;
            _Calculator = calculator;
            _Validators = validators;
        }

        public virtual TService Init()
        {
            return (TService)this;
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
        [XmlIgnore, JsonIgnore]
        public virtual ProcessingMode ProcessMode { get; set; }

        protected TEntity _data;
        [XmlIgnore, JsonIgnore]
        public virtual TEntity Data => (TEntity)_data;


        protected IDtoMapper<TEntity, TDto> _DtoMapper;
        [XmlIgnore, JsonIgnore]
        public virtual IDtoMapper<TEntity, TDto> DtoMapper => _DtoMapper;
        public virtual void SetDtoMapper(IDtoMapper<TEntity, TDto> mapper) => _DtoMapper = mapper;

        protected ICalculator<TEntity> _Calculator;
        [XmlIgnore, JsonIgnore]
        public virtual ICalculator<TEntity> Calculator => _Calculator;
        public virtual void SetCalculator(ICalculator<TEntity> calculator) => _Calculator = calculator;

        protected IList<IValidator<TEntity>> _Validators;
        [XmlIgnore, JsonIgnore]
        public virtual IList<IValidator<TEntity>> Validators => _Validators;
        public virtual void AddValidator(IValidator<TEntity> validator) => _Validators.Add(validator);

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
            _data?.SetDataBaseFactory(dbFactory);
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

        public virtual TDto ToDto()
        {
            return ToDto(Data);
        }
        public virtual TDto ToDto(TEntity data)
        {
            if (data is null || DtoMapper is null)
                return (TDto)null;
            return DtoMapper.WriteDto(data, null);
        }

        public virtual TEntity FromDto(TDto dto)
        {
            var data = FromDto(Data, dto);
            return data;
        }
        public virtual TEntity FromDto(TEntity data, TDto dto)
        {
            if (dto is null || DtoMapper is null)
                return (TEntity)null;
            return DtoMapper.ReadDto(data, dto);
        }

        public virtual bool Calculate()
        {
            if (Data is null || Calculator is null)
                return false;
            return Calculator.Calculate(Data, ProcessMode);
        }

        public virtual bool Validate()
        {
            if (Data is null || Validators is null || Validators.Count == 0)
                return false;
            foreach (var validator in Validators)
            {
                if (!validator.Validate(Data, ProcessMode))
                    return false;
            }
            return true;
        }

        #endregion Methods

        #region CRUD Methods
        public virtual bool GetData(long RowNum)
        {
            if (ProcessMode == ProcessingMode.Add || RowNum == 0)
                return false;
            ClearData();
            return _data.Get(RowNum);
        }

        public virtual bool GetDataById(string id)
        {
            if (ProcessMode == ProcessingMode.Add || string.IsNullOrWhiteSpace(id))
                return false;
            ClearData();
            return _data.GetById(id);
        }

        public virtual bool SaveData()
        {
            if (ProcessMode != ProcessingMode.Add && ProcessMode != ProcessingMode.Edit)
                return false;
            if (_data is null)
                return false;
            return _data.Save();
        }

        public virtual bool DeleteData()
        {
            if (ProcessMode != ProcessingMode.Delete && ProcessMode != ProcessingMode.Edit)
                return false;
            if (_data is null)
                return false;
            return _data.Delete();
        }

        public virtual async Task<bool> GetDataAsync(long RowNum)
        {
            if (ProcessMode == ProcessingMode.Add || RowNum == 0)
                return false;
            if (_data is null)
                NewData();
            return await _data.GetAsync(RowNum).ConfigureAwait(false);
        }

        public virtual async Task<bool> GetDataByIdAsync(string id)
        {
            if (ProcessMode == ProcessingMode.Add || string.IsNullOrWhiteSpace(id))
                return false;
            if (_data is null)
                NewData();
            return await _data.GetByIdAsync(id).ConfigureAwait(false);
        }

        public virtual async Task<bool> SaveDataAsync()
        {
            if (ProcessMode != ProcessingMode.Add && ProcessMode != ProcessingMode.Edit)
                return false;
            if (_data is null)
                return false;
            return await _data.SaveAsync().ConfigureAwait(false);
        }

        public virtual async Task<bool> DeleteDataAsync()
        {
            if (ProcessMode != ProcessingMode.Delete && ProcessMode != ProcessingMode.Edit)
                return false;
            if (_data is null)
                return false;
            return await _data.DeleteAsync().ConfigureAwait(false);
        }

        #endregion CRUD Methods

        #region Service Method - Add process
        /// <summary>
        /// Set ProcessMode to Add and add new data
        /// </summary>
        public virtual bool Add()
        {
            ProcessMode = ProcessingMode.Add;
            NewData();
            ClearData();
            return true;
        }
        #endregion Service API Method - Add process

        #region Service Method - Edit process
        /// <summary>
        /// Set ProcessMode to Edit and clear current data
        /// </summary>
        public virtual bool Edit()
        {
            ProcessMode = ProcessingMode.Edit;
            ClearData();
            return true;
        }
        /// <summary>
        /// Set ProcessMode to Edit and load data by RowNum to edit
        /// </summary>
        public virtual bool Edit(long RowNum)
        {
            if (!Edit())
                return false;
            return GetData(RowNum);
        }
        /// <summary>
        /// Set ProcessMode to Edit and load data by Unique Id to edit
        /// </summary>
        public virtual bool Edit(string id)
        {
            if (!Edit())
                return false;
            return GetDataById(id);
        }
        /// <summary>
        /// Set ProcessMode to Edit and load data by RowNum to edit
        /// </summary>
        public virtual async Task<bool> EditAsync(long RowNum)
        {
            if (!Edit())
                return false;
            return await GetDataAsync(RowNum).ConfigureAwait(false);
        }
        /// <summary>
        /// Set ProcessMode to Edit and load data by Unique Id to edit
        /// </summary>
        public virtual async Task<bool> EditAsync(string id)
        {
            if (!Edit())
                return false;
            return await GetDataByIdAsync(id).ConfigureAwait(false);
        }

        #endregion Service Method - Edit process

        #region Service Method - List process
        /// <summary>
        /// Set ProcessMode to List and clear current data
        /// </summary>
        public virtual bool List()
        {
            ProcessMode = ProcessingMode.List;
            ClearData();
            return true;
        }
        /// <summary>
        /// Set ProcessMode to List and load data by Unique Id to edit
        /// </summary>
        public virtual bool List(string id)
        {
            if (!List())
                return false;
            return GetDataById(id);
        }
        /// <summary>
        /// Set ProcessMode to List and load data by RowNum to edit
        /// </summary>
        public virtual async Task<bool> ListAsync(long RowNum)
        {
            if (!List())
                return false;
            return await GetDataAsync(RowNum).ConfigureAwait(false);
        }
        /// <summary>
        /// Set ProcessMode to List and load data by Unique Id to edit
        /// </summary>
        public virtual async Task<bool> ListAsync(string id)
        {
            if (!List())
                return false;
            return await GetDataByIdAsync(id).ConfigureAwait(false);
        }
        #endregion Service Method - List process

        #region Service Method - Delete process
        /// <summary>
        /// Set ProcessMode to Delete and clear current data
        /// </summary>
        public virtual bool Delete()
        {
            ProcessMode = ProcessingMode.Delete;
            ClearData();
            return true;
        }
        /// <summary>
        /// Set ProcessMode to Delete and delete data by RowNum
        /// </summary>
        public virtual bool Delete(long RowNum)
        {
            if (!Delete())
                return false;
            if (!GetData(RowNum))
                return false;
            return DeleteData();
        }
        /// <summary>
        /// Set ProcessMode to Delete and delete data by Unique Id
        /// </summary>
        public virtual bool Delete(string id)
        {
            if (!Delete())
                return false;
            if (!GetDataById(id))
                return false;
            return DeleteData();
        }
        /// <summary>
        /// Set ProcessMode to Delete and delete data by RowNum
        /// </summary>
        public virtual async Task<bool> DeleteAsync(long RowNum)
        {
            if (!Delete())
                return false;
            if ( !(await GetDataAsync(RowNum).ConfigureAwait(false)) )
                return false;
            return await DeleteDataAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// Set ProcessMode to Delete and delete data by Unique Id
        /// </summary>
        public virtual async Task<bool> DeleteAsync(string id)
        {
            if (!Delete())
                return false;
            if (!(await GetDataByIdAsync(id).ConfigureAwait(false)))
                return false;
            return await DeleteDataAsync().ConfigureAwait(false);
        }

        #endregion Service Method - Delete process

    }
}
