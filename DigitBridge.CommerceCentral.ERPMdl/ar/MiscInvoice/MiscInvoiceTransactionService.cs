
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
    public partial class MiscInvoiceTransactionService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override MiscInvoiceTransactionService Init()
        {
            base.Init();
            SetDtoMapper(new MiscInvoiceTransactionDataDtoMapperDefault());
            SetCalculator(new MiscInvoiceTransactionServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new MiscInvoiceTransactionServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(MiscInvoiceTransactionDataDto dto)
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
        public virtual async Task<bool> AddAsync(MiscInvoiceTransactionDataDto dto)
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

        public virtual bool Add(MiscInvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasMiscInvoiceTransaction)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.MiscInvoiceTransaction))
                return false;

            // load data from dto
            FromDto(payload.MiscInvoiceTransaction);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(MiscInvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasMiscInvoiceTransaction)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.MiscInvoiceTransaction)))
                return false;

            // load data from dto
            FromDto(payload.MiscInvoiceTransaction);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(MiscInvoiceTransactionDataDto dto)
        {
            if (dto is null || !dto.HasMiscInvoiceTransaction)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.MiscInvoiceTransaction.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(MiscInvoiceTransactionDataDto dto)
        {
            if (dto is null || !dto.HasMiscInvoiceTransaction)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.MiscInvoiceTransaction.RowNum.ToLong());

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
        public virtual bool Update(MiscInvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasMiscInvoiceTransaction || payload.MiscInvoiceTransaction.MiscInvoiceTransaction.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.MiscInvoiceTransaction))
                return false;

            // load data 
            GetData(payload.MiscInvoiceTransaction.MiscInvoiceTransaction.RowNum.ToLong());

            // load data from dto
            FromDto(payload.MiscInvoiceTransaction);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(MiscInvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasMiscInvoiceTransaction)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.MiscInvoiceTransaction)))
                return false;

            // load data 
            await GetDataAsync(payload.MiscInvoiceTransaction.MiscInvoiceTransaction.RowNum.ToLong());

            // load data from dto
            FromDto(payload.MiscInvoiceTransaction);

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
        public virtual async Task<bool> GetDataAsync(MiscInvoiceTransactionPayload payload, string orderNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetData(MiscInvoiceTransactionPayload payload, string orderNumber)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(MiscInvoiceTransactionPayload payload, string orderNumber)
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
        public virtual bool DeleteByNumber(MiscInvoiceTransactionPayload payload, string orderNumber)
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



        /// <summary>
        /// Load MiscInvoice data.
        /// </summary>
        /// <param name="miscInvoiceUuid"></param>
        protected bool LoadMiscInvoice(string miscInvoiceUuid)
        {
            // load miscinvoice data
            var miscInvoiceData = new MiscInvoiceData(dbFactory);
            var success = miscInvoiceData.GetById(miscInvoiceUuid);
            if (!success)
            {
                AddError($"Data not found for miscInvoiceUuid:{miscInvoiceUuid}");
                return success;
            }

            if (Data == null)
                NewData();
            Data.MiscInvoiceData = miscInvoiceData;
            Data.MiscInvoiceTransaction.MiscInvoiceUuid = miscInvoiceData.MiscInvoiceHeader.MiscInvoiceUuid;

            return success;
        }

        /// <summary>
        /// Load MiscInvoice data.
        /// </summary>
        /// <param name="miscInvoiceUuid"></param>
        protected async Task<bool> LoadMiscInvoiceAsync(string miscInvoiceUuid)
        {
            // load miscinvoice data
            var miscInvoiceData = new MiscInvoiceData(dbFactory);
            var success = await miscInvoiceData.GetByIdAsync(miscInvoiceUuid);
            if (!success)
            {
                AddError($"Data not found for miscInvoiceUuid:{miscInvoiceUuid}");
                return success;
            }

            if (Data == null)
                NewData();
            Data.MiscInvoiceData = miscInvoiceData;
            Data.MiscInvoiceTransaction.MiscInvoiceUuid = miscInvoiceData.MiscInvoiceHeader.MiscInvoiceUuid;

            return success;
        }
    }
}



