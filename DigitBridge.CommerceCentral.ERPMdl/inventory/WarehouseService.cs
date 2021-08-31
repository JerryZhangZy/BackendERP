
    
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
    public partial class WarehouseService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override WarehouseService Init()
        {
            base.Init();
            SetDtoMapper(new WarehouseDataDtoMapperDefault());
            SetCalculator(new WarehouseServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new WarehouseServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(WarehouseDataDto dto)
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
        public virtual async Task<bool> AddAsync(WarehouseDataDto dto)
        {
            if (dto is null)
                return false;
            // set Add mode and clear data
            Add();

            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        public virtual bool Add(WarehousePayload payload)
        {
            if (payload is null || !payload.HasWarehouse)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Warehouse))
                return false;

            // load data from dto
            FromDto(payload.Warehouse);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(WarehousePayload payload)
        {
            if (payload is null || !payload.HasWarehouse)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.Warehouse).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(payload.Warehouse);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(WarehouseDataDto dto)
        {
            if (dto is null || !dto.HasDistributionCenter)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.DistributionCenter.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(WarehouseDataDto dto)
        {
            if (dto is null || !dto.HasDistributionCenter)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // load data 
            await GetDataAsync(dto.DistributionCenter.RowNum.ToLong()).ConfigureAwait(false);

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(WarehousePayload payload)
        {
            if (payload is null || !payload.HasWarehouse || payload.Warehouse.DistributionCenter.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Warehouse))
                return false;

            // load data 
            GetData(payload.Warehouse.DistributionCenter.RowNum.ToLong());

            // load data from dto
            FromDto(payload.Warehouse);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(WarehousePayload payload)
        {
            if (payload is null || !payload.HasWarehouse)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.Warehouse).ConfigureAwait(false)))
                return false;

            // load data 
            await GetDataAsync(payload.Warehouse.DistributionCenter.RowNum.ToLong()).ConfigureAwait(false);

            // load data from dto
            FromDto(payload.Warehouse);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

        public async Task<bool> DeleteByWarehouseCodeAsync(WarehousePayload payload, string warehouseCode)
        {
            if (string.IsNullOrEmpty(warehouseCode))
                return false;
            Delete();
            if (!(await ValidateAccountAsync(payload, warehouseCode).ConfigureAwait(false)))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await WarehouseServiceHelper.GetRowNumByWarehouseCodeAsync(warehouseCode, payload.MasterAccountNum, payload.ProfileNum);
            }
            var success = await GetDataAsync(rowNum);
            return success && (await DeleteDataAsync());
        }

        public WarehousePayload GetWarehouseByWarehouseCodeArray(WarehousePayload payload)
        {
            if (!payload.HasWarehouseCodes)
                return payload;
            var list = new List<WarehouseDataDto>();
            var msglist = new List<MessageClass>();
            var rowNumList = new List<long>();
            using(var trx=new ScopedTransaction(dbFactory))
            {
                rowNumList = WarehouseServiceHelper.GetRowNumsByWarehouseCodes(payload.WarehouseCodes, payload.MasterAccountNum, payload.ProfileNum);
            }
            foreach (var rownum in rowNumList)
            {
                if (GetData(rownum))
                    list.Add(ToDto());
            }
            payload.Warehouses = list;
            payload.Messages = msglist;
            return payload;
        }

        public async Task<WarehousePayload> GetWarehouseByWarehouseCodeArrayAsync(WarehousePayload payload)
        {
            if (!payload.HasWarehouseCodes)
                return payload;
            var list = new List<WarehouseDataDto>();
            var msglist = new List<MessageClass>();
            var rowNumList = new List<long>();
            using(var trx=new ScopedTransaction(dbFactory))
            {
                rowNumList = await WarehouseServiceHelper.GetRowNumsByWarehouseCodesAsync(payload.WarehouseCodes, payload.MasterAccountNum, payload.ProfileNum);
            }
            foreach (var rownum in rowNumList)
            {
                if (await GetDataAsync(rownum))
                    list.Add(ToDto());
            }
            payload.Warehouses = list;
            payload.Messages = msglist;
            return payload;
        }

        public bool GetWarehouseByWarehouseCode(WarehousePayload payload, string warehouseCode)
        {
            if (string.IsNullOrEmpty(warehouseCode))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = WarehouseServiceHelper.GetRowNumByWarehouseCode(warehouseCode, payload.MasterAccountNum, payload.ProfileNum);
            }
            return GetData(rowNum);
        }

        public async Task<bool> GetWarehouseByWarehouseCodeAsync(WarehousePayload payload, string warehouseCode)
        {
            if (string.IsNullOrEmpty(warehouseCode))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await WarehouseServiceHelper.GetRowNumByWarehouseCodeAsync(warehouseCode, payload.MasterAccountNum, payload.ProfileNum);
            }
            return await GetDataAsync(rowNum);
        }

    }
}



