    
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
    public partial class PurchaseOrderService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override PurchaseOrderService Init()
        {
            base.Init();
            SetDtoMapper(new PurchaseOrderDataDtoMapperDefault());
            SetCalculator(new PurchaseOrderServiceCalculatorDefault(this,this.dbFactory));
            AddValidator(new PurchaseOrderServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(PurchaseOrderDataDto dto)
        {
            if (dto is null) 
                return false;
            // set Add mode and clear data
            Add();

            if (!Validate(dto))
                return false;

            // load data from dto
            FromDto(dto);
            
            Calculate();

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(PurchaseOrderDataDto dto)
        {
            if (dto is null)
                return false;
            // set Add mode and clear data
            Add();

            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(dto);
            
            Calculate();

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        public virtual bool Add(PurchaseOrderPayload payload)
        {
            if (payload is null || !payload.HasPurchaseOrder)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.PurchaseOrder))
                return false;

            // load data from dto
            FromDto(payload.PurchaseOrder);
            
            Calculate();

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        } 
        public virtual async Task<bool> AddAsync(PurchaseOrderPayload payload)
        {
            if (payload is null || !payload.HasPurchaseOrder)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.PurchaseOrder).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(payload.PurchaseOrder);
            
            Calculate();

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

         

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(PurchaseOrderDataDto dto)
        {
            if (dto is null || !dto.HasPoHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.PoHeader.RowNum.ToLong());

            // load data from dto
            FromDto(dto);
            
            Calculate();

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(PurchaseOrderDataDto dto)
        {
            if (dto is null || !dto.HasPoHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // load data 
            await GetDataAsync(dto.PoHeader.RowNum.ToLong()).ConfigureAwait(false);

            // load data from dto
            FromDto(dto);
            
            Calculate();

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(PurchaseOrderPayload payload)
        {
            if (payload is null || !payload.HasPurchaseOrder || payload.PurchaseOrder.PoHeader.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.PurchaseOrder))
                return false;

            // load data 
            GetData(payload.PurchaseOrder.PoHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.PurchaseOrder);
            
            Calculate();

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(PurchaseOrderPayload payload)
        {
            if (payload is null || !payload.HasPurchaseOrder)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.PurchaseOrder).ConfigureAwait(false)))
                return false;

            // load data 
            await GetDataAsync(payload.PurchaseOrder.PoHeader.RowNum.ToLong()).ConfigureAwait(false);

            // load data from dto
            FromDto(payload.PurchaseOrder);
            
            Calculate();

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }


        /// <summary>
        ///  get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="poNum"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetDataAsync(PurchaseOrderPayload payload, string poNum)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, poNum);
        }

        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="poNum"></param>
        /// <returns></returns>
        public virtual bool GetData(PurchaseOrderPayload payload, string poNum)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, poNum);
        }

        /// <summary>
        /// Delete salesorder by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(PurchaseOrderPayload payload, string poNum)
        {
            if (string.IsNullOrEmpty(poNum))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, poNum);
            success = success && DeleteData();
            return success;
        }

        /// <summary>
        /// Delete purchase order by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(PurchaseOrderPayload payload, string poNum)
        {
            if (string.IsNullOrEmpty(poNum))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = GetByNumber(payload.MasterAccountNum, payload.ProfileNum, poNum);
            success = success && DeleteData();
            return success;
        }
        public virtual async Task GetListByOrderNumbersAsync(PurchaseOrderPayload payload)
        {
            if (payload is null || !payload.HasPoNums)
            {
                AddError("PoNums is required.");
                payload.Messages = this.Messages;
                payload.Success = false;
            }
            
            var result = new List<PurchaseOrderDataDto>();
            foreach (var orderNumber in payload.PoNums)
            {
                if (!(await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber)))
                    continue;
                result.Add(this.ToDto());
                this.DetachData(this.Data);
            }
            payload.PurchaseOrders = result;
        }

    }
}



