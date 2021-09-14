    
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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InventoryUpdateService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InventoryUpdateService Init()
        {
            base.Init();
            SetDtoMapper(new InventoryUpdateDataDtoMapperDefault());
            SetCalculator(new InventoryUpdateServiceCalculatorDefault(this,this.dbFactory));
            AddValidator(new InventoryUpdateServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InventoryUpdateDataDto dto)
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
        public virtual async Task<bool> AddAsync(InventoryUpdateDataDto dto)
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

        public virtual bool Add(InventoryUpdatePayload payload)
        {
            if (payload is null || !payload.HasInventoryUpdate)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.InventoryUpdate))
                return false;

            // load data from dto
            FromDto(payload.InventoryUpdate);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(InventoryUpdatePayload payload)
        {
            if (payload is null || !payload.HasInventoryUpdate)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.InventoryUpdate)))
                return false;

            // load data from dto
            FromDto(payload.InventoryUpdate);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InventoryUpdateDataDto dto)
        {
            if (dto is null || !dto.HasInventoryUpdateHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.InventoryUpdateHeader.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(InventoryUpdateDataDto dto)
        {
            if (dto is null || !dto.HasInventoryUpdateHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.InventoryUpdateHeader.RowNum.ToLong());

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
        public virtual bool Update(InventoryUpdatePayload payload)
        {
            if (payload is null || !payload.HasInventoryUpdate || payload.InventoryUpdate.InventoryUpdateHeader.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.InventoryUpdate))
                return false;

            // load data 
            GetData(payload.InventoryUpdate.InventoryUpdateHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.InventoryUpdate);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InventoryUpdatePayload payload)
        {
            if (payload is null || !payload.HasInventoryUpdate)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.InventoryUpdate)))
                return false;

            // load data 
            await GetDataAsync(payload.InventoryUpdate.InventoryUpdateHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.InventoryUpdate);

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
        public virtual async Task<bool> GetDataAsync(InventoryUpdatePayload payload, string orderNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetData(InventoryUpdatePayload payload, string orderNumber)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(InventoryUpdatePayload payload, string orderNumber)
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
        public virtual bool DeleteByNumber(InventoryUpdatePayload payload, string orderNumber)
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
        public bool GetInventoryUpdateByBatchNumber(InventoryUpdatePayload payload, string batchNumber)
        {
            if (string.IsNullOrEmpty(batchNumber))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = InventoryUpdateHelper.GetRowNumByNumber(batchNumber, payload.MasterAccountNum, payload.ProfileNum);
            }
            return GetData(rowNum);
        }
        public async Task<bool> GetInventoryUpdateByBatchNumberAsync(InventoryUpdatePayload payload, string batchNumber)
        {
            if (string.IsNullOrEmpty(batchNumber))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await InventoryUpdateHelper.GetRowNumByNumberAsync(batchNumber, payload.MasterAccountNum, payload.ProfileNum);
            }
            return await GetDataAsync(rowNum);
        }

        public InventoryUpdatePayload GetInventoryUpdatesByCodeArray(InventoryUpdatePayload payload)
        {
            if (!payload.HasBatchNumbers)
                return payload;
            var list = new List<InventoryUpdateDataDto>();
            var msglist = new List<MessageClass>();
            var rowNumList = new List<long>();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                rowNumList = InventoryUpdateHelper.GetRowNumsByNums(payload.BatchNumbers, payload.MasterAccountNum, payload.ProfileNum);
            }
            foreach (var rowNum in rowNumList)
            {
                if ( GetData(rowNum))
                    list.Add(ToDto());
            }
            payload.InventoryUpdates = list;
            payload.Messages = msglist;
            return payload;
        }

        public bool DeleteByBatchNumber(InventoryUpdatePayload payload, string batchNumber)
        {
            if (string.IsNullOrEmpty(batchNumber))
                return false;
            Delete();
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = InventoryUpdateHelper.GetRowNumByNumber(batchNumber, payload.MasterAccountNum, payload.ProfileNum);
            }
            var success = GetData(rowNum);
            if (success)
                return DeleteData();
            AddError("Data not found");
            return false;
        }

        public async Task<bool> DeleteByBatchNumberAsync(InventoryUpdatePayload payload, string batchNumber)
        {
            if (string.IsNullOrEmpty(batchNumber))
                return false;
            Delete();
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await InventoryUpdateHelper.GetRowNumByNumberAsync(batchNumber, payload.MasterAccountNum, payload.ProfileNum);
            }
            var success = await GetDataAsync(rowNum);
            if (success)
                return await DeleteDataAsync();
            AddError("Data not found");
            return false;
        }

        public async Task<InventoryUpdatePayload> GetInventoryUpdatesByCodeArrayAsync(InventoryUpdatePayload payload)
        {
            if (!payload.HasBatchNumbers)
                return payload;
            var list = new List<InventoryUpdateDataDto>();
            var msglist = new List<MessageClass>();
            var rowNumList = new List<long>();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                rowNumList = await InventoryUpdateHelper.GetRowNumsByNumsAsync(payload.BatchNumbers, payload.MasterAccountNum, payload.ProfileNum);
            }
            foreach (var rowNum in rowNumList)
            {
                if (await GetDataAsync(rowNum))
                    list.Add(ToDto());
            }
            payload.InventoryUpdates = list;
            payload.Messages = msglist;
            return payload;
        }

    }
}


