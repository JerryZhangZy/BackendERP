    
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

namespace DigitBridge.QuickBooks.Integration
{
    public partial class QuickBooksSettingInfoService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override QuickBooksSettingInfoService Init()
        {
            base.Init();
            SetDtoMapper(new QuickBooksSettingInfoDataDtoMapperDefault());
            SetCalculator(new QuickBooksSettingInfoServiceCalculatorDefault(this,this.dbFactory));
            AddValidator(new QuickBooksSettingInfoServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(QuickBooksSettingInfoDataDto dto)
        {
            if (dto is null) 
                return false;
            // set Add mode and clear data
            Add();

            if (!Validate(dto))
                return false;

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(QuickBooksSettingInfoDataDto dto)
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
        public virtual async Task<bool> GetByPayloadAsync(IPayload payload)
        {
            var rowNum = 0L;
            using (var trx = new ScopedTransaction(dbFactory))
            {
                rowNum = await QuickBooksSettingInfoHelper.GetSettingRowNumAsync(payload.MasterAccountNum, payload.ProfileNum);
            }
            return await GetDataAsync(rowNum);
        }

        public virtual bool Add(QuickBooksSettingInfoPayload payload)
        {
            if (payload is null || !payload.HasQuickBooksSettingInfo)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.QuickBooksSettingInfo))
                return false;

            // load data from dto
            FromDto(payload.QuickBooksSettingInfo);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(QuickBooksSettingInfoPayload payload)
        {
            if (payload is null || !payload.HasQuickBooksSettingInfo)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.QuickBooksSettingInfo)))
                return false;

            // load data from dto
            FromDto(payload.QuickBooksSettingInfo);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(QuickBooksSettingInfoDataDto dto)
        {
            if (dto is null || !dto.HasQuickBooksIntegrationSetting)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.QuickBooksIntegrationSetting.RowNum.ToLong());

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(QuickBooksSettingInfoDataDto dto)
        {
            if (dto is null || !dto.HasQuickBooksIntegrationSetting)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.QuickBooksIntegrationSetting.RowNum.ToLong());

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(QuickBooksSettingInfoPayload payload)
        {
            if (payload is null || !payload.HasQuickBooksSettingInfo || payload.QuickBooksSettingInfo.QuickBooksIntegrationSetting.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.QuickBooksSettingInfo))
                return false;

            // load data 
            GetData(payload.QuickBooksSettingInfo.QuickBooksIntegrationSetting.RowNum.ToLong());

            // load data from dto
            FromDto(payload.QuickBooksSettingInfo);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(QuickBooksSettingInfoPayload payload)
        {
            if (payload is null || !payload.HasQuickBooksSettingInfo)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.QuickBooksSettingInfo)))
                return false;

            // load data 
            await GetDataAsync(payload.QuickBooksSettingInfo.QuickBooksIntegrationSetting.RowNum.ToLong());

            // load data from dto
            FromDto(payload.QuickBooksSettingInfo);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        ///  get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetDataAsync(QuickBooksSettingInfoPayload payload, string orderNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetData(QuickBooksSettingInfoPayload payload, string orderNumber)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(QuickBooksSettingInfoPayload payload, string orderNumber)
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

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(QuickBooksSettingInfoPayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
            success = success && DeleteData();
            return success;
        }
    }
}


