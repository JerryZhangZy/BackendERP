

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
    public partial class InvoiceTransactionService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InvoiceTransactionService Init()
        {
            base.Init();
            SetDtoMapper(new InvoiceTransactionDataDtoMapperDefault());
            SetCalculator(new InvoiceTransactionServiceCalculatorDefault());
            AddValidator(new InvoiceTransactionServiceValidatorDefault(this, this.dbFactory));
            return this;
        }

        /// <summary>
        /// Load Invoice data.
        /// </summary>
        /// <param name="invoiceUuid"></param>
        private void LoadInvoice(string invoiceNumber, int profileNum, int masterAccountNum)
        {
            // load invoice data
            var invoiceData = new InvoiceData(dbFactory);
            var invoiceRowNum = invoiceData.GetRowNum(invoiceNumber, profileNum, masterAccountNum);
            if (invoiceRowNum.HasValue)
            {
                invoiceData.Get(invoiceRowNum.Value);
                Data._InvoiceData = invoiceData;
            }
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InvoiceTransactionDataDto dto)
        {
            if (dto is null)
                return false;
            // set Add mode and clear data
            Add();

            if (!Validate(dto))
                return false;

            // load data from dto
            FromDto(dto);

            //load invoice data.
            LoadInvoice(dto.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(InvoiceTransactionDataDto dto)
        {
            if (dto is null)
                return false;
            // set Add mode and clear data
            Add();

            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(dto);

            //load invoice data.
            LoadInvoice(dto.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        public virtual bool Add(InvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasInvoiceTransaction)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.InvoiceTransaction))
                return false;

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            //load invoice data.
            LoadInvoice(payload.InvoiceTransaction.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(InvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasInvoiceTransaction)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.InvoiceTransaction).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            //load invoice data.
            LoadInvoice(payload.InvoiceTransaction.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InvoiceTransactionDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceTransaction)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.InvoiceTransaction.RowNum.ToLong());

            //load invoice data.
            LoadInvoice(dto.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value);

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
        public virtual async Task<bool> UpdateAsync(InvoiceTransactionDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceTransaction)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // load data 
            await GetDataAsync(dto.InvoiceTransaction.RowNum.ToLong()).ConfigureAwait(false);

            //load invoice data.
            LoadInvoice(dto.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value);

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
        public virtual bool Update(InvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasInvoiceTransaction || payload.InvoiceTransaction.InvoiceTransaction.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.InvoiceTransaction))
                return false;

            // load data 
            GetData(payload.InvoiceTransaction.InvoiceTransaction.RowNum.ToLong());

            //load invoice data.
            LoadInvoice(payload.InvoiceTransaction.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum);

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasInvoiceTransaction)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.InvoiceTransaction).ConfigureAwait(false)))
                return false;

            // load data 
            await GetDataAsync(payload.InvoiceTransaction.InvoiceTransaction.RowNum.ToLong()).ConfigureAwait(false);

            //load invoice data.
            LoadInvoice(payload.InvoiceTransaction.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum);

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

        #region get by invoice number 
        protected virtual async Task<List<InvoiceTransactionData>> GetDataListAsync(int masterAccountNum, int profileNum, string invoiceNumber, TransTypeEnum transType, int? transNum = null)
        {
            List();
            if (string.IsNullOrEmpty(invoiceNumber)) return null;
            return await _data.GetDataListAsync(invoiceNumber, masterAccountNum, profileNum, transType, transNum).ConfigureAwait(false);
        }
        protected virtual async Task<List<InvoiceTransactionDataDto>> GetDtoListAsync(int masterAccountNum, int profileNum, string invoiceNumber, TransTypeEnum transType, int? transNum = null)
        {
            if (string.IsNullOrEmpty(invoiceNumber)) return null;
            var dataList = await GetDataListAsync(masterAccountNum, profileNum, invoiceNumber, transType, transNum).ConfigureAwait(false);
            if (dataList == null || dataList.Count == 0) return null;
            var dtoList = new List<InvoiceTransactionDataDto>();
            foreach (var dataItem in dataList)
            {
                var dtoItem = new InvoiceTransactionDataDto();
                dtoList.Add(this.DtoMapper.WriteDto(dataItem, dtoItem));
            }
            return dtoList;
        }

        /// <summary>
        /// Get InvoiceHeader by invoiceNumber//TODO move to invoice service
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        protected async Task<InvoiceHeaderDto> GetInvoiceHeaderAsync(int masterAccountNum, int profileNum, string invoiceNumber)
        {
            var invoiceHeader = await new InvoiceHeader(_dbFactory).GetByInvoiceNumberAsync(invoiceNumber, masterAccountNum, profileNum);
            var dto = new InvoiceHeaderDto();
            if (invoiceHeader != null)
                new InvoiceDataDtoMapperDefault().WriteInvoiceHeader(invoiceHeader, dto);
            return dto;
        }
        #endregion

        /// <summary>
        /// Delete  by rownum
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByRowNumAsync(InvoiceTransactionPayload payload, long rowNum)
        {
            payload.InvoiceTransaction = new InvoiceTransactionDataDto();
            payload.InvoiceTransaction.InvoiceTransaction = new InvoiceTransactionDto();
            payload.InvoiceTransaction.InvoiceTransaction.RowNum = rowNum;

            //set delete mode
            Delete();

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            //load data
            var success = await GetDataAsync(rowNum.ToLong());
            //delete salesorder and its sub items
            success = success && DeleteData();
            return success;
        }
    }
}



