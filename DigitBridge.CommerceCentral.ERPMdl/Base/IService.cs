﻿using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface IService<TService, TEntity, TDto>
        where TService : ServiceBase<TService, TEntity, TDto>
        where TEntity : StructureRepository<TEntity>, new()
        where TDto : class, new()
    {

        TService Init();
        IDataBaseFactory dbFactory { get; }
        TService SetDataBaseFactory(IDataBaseFactory dbFactory);

        TEntity Data { get; }
        ProcessingMode ProcessMode { get; }
        void SetProcessMode(ProcessingMode mode);

        IDtoMapper<TEntity, TDto> DtoMapper { get; }
        void SetDtoMapper(IDtoMapper<TEntity, TDto> mapper);

        ICalculator<TEntity> Calculator { get; }
        void SetCalculator(ICalculator<TEntity> calculator);

        IList<IValidator<TEntity,TDto>> Validators { get; }
        void AddValidator(IValidator<TEntity,TDto> validator);

        void OnClear(TEntity data);
        bool OnAfterLoad(TEntity data);
        bool OnBeforeSave(TEntity data);
        bool OnSave(IDataBaseFactory dataBaseFactory, TEntity data);
        bool OnAfterSave(TEntity data);
        bool OnBeforeDelete(IDataBaseFactory dataBaseFactory, TEntity data);
        bool OnDelete(TEntity data);
        bool OnAfterDelete(TEntity data);
        bool OnException(Exception ex);


        TService Clear();
        TService AttachData(TEntity data);
        TService DetachData(TEntity data);
        TService NewData();
        TService ClearData();
        TEntity CloneData();

        TDto ToDto();
        TDto ToDto(TEntity data);
        TEntity FromDto(TDto dto);
        TEntity FromDto(TEntity data, TDto dto);


        void PrepareData();
        void SetDefault();
        bool Calculate();
        bool Validate();
        Task<bool> ValidateAsync();

        bool ValidateAccount(IPayload payload, string number = null);
        Task<bool> ValidateAccountAsync(IPayload payload, string number = null);

        bool Validate(TDto dto);
        Task<bool> ValidateAsync(TDto dto);

        bool GetData(long RowNum);
        bool GetDataById(string id);
        bool SaveData();
        bool DeleteData();
        Task<bool> GetDataAsync(long RowNum);
        Task<bool> GetDataByIdAsync(string id);
        Task<bool> SaveDataAsync();
        Task<bool> DeleteDataAsync();


        bool Add();
        bool Edit();
        bool Edit(long RowNum);
        bool Edit(string id);
        Task<bool> EditAsync(long RowNum);
        Task<bool> EditAsync(string id);
        bool List();
        bool List(string id);
        Task<bool> ListAsync(long RowNum);
        Task<bool> ListAsync(string id);
        bool Delete();
        bool Delete(long RowNum);
        bool Delete(string id);
        Task<bool> DeleteAsync(long RowNum);
        Task<bool> DeleteAsync(string id);

    }
}
