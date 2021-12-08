    
//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class CustomIOFormatService
    {

        #region override methods

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override CustomIOFormatService Init()
        {
            base.Init();
            SetDtoMapper(new CustomIOFormatDataDtoMapperDefault());
            SetCalculator(new CustomIOFormatServiceCalculatorDefault(this,this.dbFactory));
            AddValidator(new CustomIOFormatServiceValidatorDefault(this, this.dbFactory));
            return this;
        }

        /// <summary>
        /// Before update data (Add/Update/Delete). call this function to update relative data.
        /// For example: before save shipment, rollback instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// </summary>
        public override async Task BeforeSaveAsync() 
        {
            try
            {
                await base.BeforeSaveAsync();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error before save.");
            }
        }

        /// <summary>
        /// Before update data (Add/Update/Delete). call this function to update relative data.
        /// For example: before save shipment, rollback instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// </summary>
        public override void BeforeSave() 
        {
            try
            {
                base.BeforeSave();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error before save.");
            }
        }

        /// <summary>
        /// After save data (Add/Update/Delete), doesn't matter success or not, call this function to update relative data.
        /// For example: after save shipment, update instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// So that, if update not success, database records will not change, this update still use then same data. 
        /// </summary>
        public override async Task AfterSaveAsync() 
        {
            try
            {
                await base.AfterSaveAsync();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save.");
            }
        }

        /// <summary>
        /// After save data (Add/Update/Delete), doesn't matter success or not, call this function to update relative data.
        /// For example: after save shipment, update instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// So that, if update not success, database records will not change, this update still use then same data. 
        /// </summary>
        public override void AfterSave() 
        {
            try
            {
                base.AfterSave();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save.");
            }
        }

        /// <summary>
        /// Only save success (Add/Update/Delete), call this function to update relative data.
        /// For example: add activity log records.
        /// </summary>
        public override async Task SaveSuccessAsync()
        {
            try
            {
                await base.SaveSuccessAsync();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save success.");
            }
        }

        /// <summary>
        /// Only save success (Add/Update/Delete), call this function to update relative data.
        /// For example: add activity log records.
        /// </summary>
        public override void SaveSuccess()
        {
            try
            {
                base.SaveSuccess();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save success.");
            }
        }

        /// <summary>
        /// Sub class should override this method to return new ActivityLog object for service
        /// </summary>
        protected override ActivityLog GetActivityLog() => null;
            //new ActivityLog(dbFactory)
            //{
            //    Type = (int)ActivityLogType.Invoice,
            //    Action = (int)this.ProcessMode,
            //    LogSource = "CustomIOFormatService",

            //    MasterAccountNum = this.Data.CustomIOFormat.MasterAccountNum,
            //    ProfileNum = this.Data.CustomIOFormat.ProfileNum,
            //    DatabaseNum = this.Data.CustomIOFormat.DatabaseNum,
            //    ProcessUuid = this.Data.CustomIOFormat.InvoiceUuid,
            //    ProcessNumber = this.Data.CustomIOFormat.InvoiceNumber,
            //    ChannelNum = this.Data.CustomIOFormat.ChannelAccountNum,
            //    ChannelAccountNum = this.Data.CustomIOFormat.ChannelAccountNum,

            //    LogMessage = string.Empty
            //};

        #endregion override methods

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(CustomIOFormatDataDto dto)
        {
            if (dto is null)
                return false;
            // set Add mode and clear data
            Add();

            if (!(await ValidateAsync(dto)))
                return false;

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        public virtual async Task<bool> AddAsync(CustomIOFormatPayload payload)
        {
            if (payload is null || !payload.HasCustomIOFormat)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.CustomIOFormat)))
                return false;

            // load data from dto
            FromDto(payload.CustomIOFormat);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(CustomIOFormatDataDto dto)
        {
            if (dto is null || !dto.HasCustomIOFormat)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.CustomIOFormat.RowNum.ToLong());

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(CustomIOFormatPayload payload)
        {
            if (payload is null || !payload.HasCustomIOFormat)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.CustomIOFormat)))
                return false;

            // load data 
            await GetDataAsync(payload.CustomIOFormat.CustomIOFormat.RowNum.ToLong());

            // load data from dto
            FromDto(payload.CustomIOFormat);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        //public virtual async Task<bool> GetDataTemplateAsync(int masterAccountNum, int profileNum, string formatType)
        //{
        //    NewData();

        //    switch (formatType.ToLower())
        //    {
        //        case "salesorder":
        //            SalesOrderIOFormat salesOrderIOFormat = new
        //                  SalesOrderIOFormat();
        //            this._data.CustomIOFormat.FormatObject = JsonConvert.SerializeObject(salesOrderIOFormat);
        //            break;
        //    }

          
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="formatType">formatType</param>
        /// <param name="formatNumber">formatNumber</param>
        /// <returns></returns>
        public virtual async Task<bool> GetAsync(CustomIOFormatPayload payload, string formatType, int formatNumber)
        {
            var rownum = await dbFactory.Db.ExecuteScalarAsync<int>("SELECT RowNum FROM CustomIOFormat WHERE MasterAccountNum=@0 AND ProfileNum=@1  AND FormatType=@2 AND FormatNumber=@3"
                ,
                payload.MasterAccountNum.ToSqlParameter("0"),
                payload.ProfileNum.ToSqlParameter("1"),
                formatType.ToSqlParameter("2"),
                formatNumber.ToSqlParameter("3")
                );
           if (rownum == 0)
            {
                AddError($"Data not found for formatType:{formatType}formatNumber{formatNumber} .");
                return false;
            }

            return _data.Get(rownum);
        }

        /// <summary>
        ///  get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetDataAsync(CustomIOFormatPayload payload, string orderNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(CustomIOFormatPayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
            success = success && DeleteData();
            return success;
        }

        public override async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string number)
        {
            var rownum = await dbFactory.Db.ExecuteScalarAsync<int>("SELECT RowNum FROM CustomIOFormat WHERE MasterAccountNum=@0 AND ProfileNum=@1    AND FormatNumber=@2"
                 ,
                 masterAccountNum.ToSqlParameter("0"),
                 profileNum.ToSqlParameter("1"),
                 number.ToSqlParameter("2")
                 );
            if (rownum == 0)
            {
                AddError($"Data not found for formatNumber:{number} .");
                return false;
            }

            return _data.Get(rownum);
        }

    }
}



