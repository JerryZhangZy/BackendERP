
    
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
    public partial class InventoryService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InventoryService Init()
        {
            base.Init();
            SetDtoMapper(new InventoryDataDtoMapperDefault());
            SetCalculator(new InventoryServiceCalculatorDefault());
            AddValidator(new InventoryServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InventoryDataDto dto)
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
        public virtual async Task<bool> AddAsync(InventoryDataDto dto)
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

        public virtual bool Add(InventoryPayload payload)
        {
            if (payload is null || !payload.HasInventory)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Inventory))
                return false;

            // load data from dto
            FromDto(payload.Inventory);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(InventoryPayload payload)
        {
            if (payload is null || !payload.HasInventory)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.Inventory).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(payload.Inventory);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InventoryDataDto dto)
        {
            if (dto is null || !dto.HasProductBasic)
                return false;

            if (!Validate(dto))
                return false;

            // set Add mode and clear data
            Edit(dto.ProductBasic.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(InventoryDataDto dto)
        {
            if (dto is null || !dto.HasProductBasic)
                return false;

            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // set Add mode and clear data
            await EditAsync(dto.ProductBasic.RowNum.ToLong()).ConfigureAwait(false);

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
        public virtual bool Update(InventoryPayload payload)
        {
            if (payload is null || !payload.HasInventory || payload.Inventory.ProductBasic.RowNum.ToLong() <= 0)
                return false;


            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Inventory))
                return false;

            // set Add mode and clear data
            Edit(payload.Inventory.ProductBasic.RowNum.ToLong());

            // load data from dto
            FromDto(payload.Inventory);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InventoryPayload payload)
        {
            if (payload is null || !payload.HasInventory)
                return false;

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.Inventory).ConfigureAwait(false)))
                return false;

            // set Add mode and clear data
            await EditAsync(payload.Inventory.ProductBasic.RowNum.ToLong()).ConfigureAwait(false);

            // load data from dto
            FromDto(payload.Inventory);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }
        public async Task<bool> AddAsync(ProductExPayload payload)
        {
            if (payload is null || !payload.HasInventoryData)
                return false;

            // set Add mode and clear data
            Add();
            if (!(await ValidateAsync(payload.InventoryData).ConfigureAwait(false)))
                return false;
            // load data from dto
            FromDto(payload.InventoryData);
            //TODO merge
            //if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
            //    return false;

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        public async Task<bool> DeleteBySkuAsync(ProductExPayload payload,string sku)
        {
            if (string.IsNullOrEmpty(sku))
                return false;
            Delete();
            if (!(await ValidateAccountAsync(payload, sku).ConfigureAwait(false)))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await InventoryHelper.GetRowNumBySkuAsync(sku, payload.MasterAccountNum, payload.ProfileNum);
            }
            var success = await GetDataAsync(rowNum);
            return success && (await DeleteDataAsync());
        }

        public async Task<bool> UpdateAsync(ProductExPayload payload)
        {

            if (payload is null || !payload.HasInventoryData || payload.InventoryData.ProductBasic.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(payload.InventoryData.ProductBasic.RowNum.ToLong()).ConfigureAwait(false);

            if (!(await ValidateAsync(payload.InventoryData).ConfigureAwait(false)))
                return false;

            // validate data for Add processing
            //TODO merge
            //if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
            //    return false;

            // load data from dto
            FromDto(payload.InventoryData);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

        public async Task<ProductExPayload> GetInventoryBySkuArrayAsync(ProductExPayload payload)
        {
            if (!payload.HasSkus)
                return payload;
            var list = new List<InventoryDataDto>();
            var msglist = new List<MessageClass>();
            foreach (var code in payload.Skus)
            {
                if (await GetInventoryBySkuAsync(payload, code))
                    list.Add(ToDto());
                else
                    msglist.AddError($"ProductSku:{code} no found");
            }
            payload.InventoryDatas = list;
            payload.Messages = msglist;
            return payload;
        }

        public async Task<bool> GetInventoryBySkuAsync(ProductExPayload payload, string sku)
        {
            if (string.IsNullOrEmpty(sku))
                return false;
            List();
            if (!(await ValidateAccountAsync(payload, sku).ConfigureAwait(false)))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await InventoryHelper.GetRowNumBySkuAsync(sku, payload.MasterAccountNum, payload.ProfileNum);
            }
            return await GetDataAsync(rowNum);
        }
    }
}



