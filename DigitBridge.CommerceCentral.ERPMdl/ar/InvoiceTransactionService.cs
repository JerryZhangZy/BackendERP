

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
using DigitBridge.Base.Common;

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
            SetCalculator(new InvoiceTransactionServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new InvoiceTransactionServiceValidatorDefault(this, this.dbFactory));
            return this;
        }

        /// <summary>
        /// Load Invoice data.
        /// </summary>
        /// <param name="invoiceUuid"></param>
        protected bool LoadInvoice(string invoiceNumber, int profileNum, int masterAccountNum)
        {
            // load invoice data
            var invoiceData = new InvoiceData(dbFactory);
            var success = invoiceData.GetByNumber(masterAccountNum, profileNum, invoiceNumber);
            if (!success) return success;

            Data.InvoiceData = invoiceData;
            Data.InvoiceTransaction.InvoiceUuid = invoiceData.InvoiceHeader.InvoiceUuid;
            if (Data.InvoiceReturnItems == null) return success;

            foreach (var item in Data.InvoiceReturnItems)
            {
                item.InvoiceUuid = invoiceData.InvoiceHeader.InvoiceUuid;
                if (!item.RowNum.IsZero()) continue;
                item.InvoiceItemsUuid = invoiceData.InvoiceItems.Where(i => i.SKU == item.SKU && i.WarehouseCode == item.WarehouseCode).FirstOrDefault()?.InvoiceItemsUuid;
            }

            return success;
        }


        /// <summary>
        /// Load returned qty for each trans return item 
        /// </summary>
        /// <returns></returns>
        protected virtual void LoadReturnedQty()
        {
            if (this.Data.InvoiceReturnItems == null || this.Data.InvoiceReturnItems.Count == 0)
                return;

            var returnItems = InvoiceTransactionHelper.GetReturnItemsByInvoiceUuid(dbFactory, this.Data.InvoiceData.UniqueId);
            if (returnItems == null || returnItems.Count == 0)
                return;

            foreach (var item in this.Data.InvoiceReturnItems)
            {
                item.ReturnedQty = returnItems.Where(i => i.sku == item.SKU && i.rowNum != item.RowNum).Sum(j => j.returnQty).ToQty();//+ item.ReturnQty;
            }
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InvoiceTransactionDataDto dto)
        {
            if (dto is null)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }
            // set Add mode and clear data
            Add();

            if (!Validate(dto))
                return false;

            // load data from dto
            FromDto(dto);

            //load invoice data.
            if (!LoadInvoice(dto.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value))
                return false;

            //Load returned qty for each trans return item 
            LoadReturnedQty();

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
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }
            // set Add mode and clear data
            Add();

            if (!(await ValidateAsync(dto)))
                return false;

            // load data from dto
            FromDto(dto);

            //load invoice data.
            if (!LoadInvoice(dto.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value))
                return false;

            //Load returned qty for each trans return item 
            LoadReturnedQty();

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        public virtual bool Add(InvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasInvoiceTransaction)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.InvoiceTransaction))
                return false;

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            //load invoice data.
            if (!LoadInvoice(payload.InvoiceTransaction.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            //Load returned qty for each trans return item 
            LoadReturnedQty();

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(InvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasInvoiceTransaction)
            {
                AddError($"InvoiceTransaction is required.");
                return false;
            }


            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.InvoiceTransaction)))
                return false;

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            //load invoice data.
            if (!LoadInvoice(payload.InvoiceTransaction.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            //Load returned qty for each trans return item 
            LoadReturnedQty();

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InvoiceTransactionDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceTransaction)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.InvoiceTransaction.RowNum.ToLong());

            // load data from dto
            FromDto(dto);

            //load invoice data.
            if (!LoadInvoice(Data.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value))
                return false;

            //Load returned qty for each trans return item 
            LoadReturnedQty();

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
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.InvoiceTransaction.RowNum.ToLong());

            // load data from dto
            FromDto(dto);

            //load invoice data.
            if (!LoadInvoice(Data.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value))
                return false;

            //Load returned qty for each trans return item 
            LoadReturnedQty();

            // validate data for Add processing
            if (!(await ValidateAsync()))
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
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.InvoiceTransaction))
                return false;

            // load data 
            GetData(payload.InvoiceTransaction.InvoiceTransaction.RowNum.ToLong());

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            //load invoice data.
            if (!LoadInvoice(Data.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            //Load returned qty for each trans return item 
            LoadReturnedQty();

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
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }

            //set edit mode before validate
            Edit();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.InvoiceTransaction)))
                return false;

            // load data 
            if (!(await GetDataAsync(payload.InvoiceTransaction.InvoiceTransaction.RowNum.ToLong())))
                return false;

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            //load invoice data.
            if (!(LoadInvoice(Data.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum)))
                return false;

            //Load returned qty for each trans return item 
            LoadReturnedQty();

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        #region get by invoice number 
        protected virtual async Task<List<InvoiceTransactionData>> GetDataListAsync(int masterAccountNum, int profileNum, string invoiceNumber, TransTypeEnum transType, int? transNum = null)
        {
            List();
            if (string.IsNullOrEmpty(invoiceNumber)) return null;
            //LoadInvoice(invoiceNumber, profileNum, masterAccountNum);
            return await _data.GetDataListAsync(invoiceNumber, masterAccountNum, profileNum, transType, transNum);
        }
        public virtual async Task<List<InvoiceTransactionDataDto>> GetDtoListAsync(int masterAccountNum, int profileNum, string invoiceNumber, TransTypeEnum transType, int? transNum = null)
        {
            if (string.IsNullOrEmpty(invoiceNumber)) return null;
            var dataList = await GetDataListAsync(masterAccountNum, profileNum, invoiceNumber, transType, transNum);
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
        protected async Task<bool> GetByNumberAsync(InvoiceTransactionPayload payload, string invoiceNumber, TransTypeEnum transType, int transNum)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, transType, transNum);
        }
        protected async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string invoiceNumber, TransTypeEnum transType, int transNum)
        {
            List();
            var success = await base.GetByNumberAsync(masterAccountNum, profileNum, invoiceNumber + "_" + (int)transType + "_" + transNum);
            LoadInvoice(invoiceNumber, profileNum, masterAccountNum);
            return success;
        }
        #endregion


        /// <summary>
        /// Delete invoice by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        protected virtual async Task<bool> DeleteByNumberAsync(InvoiceTransactionPayload payload, string invoiceNumber, TransTypeEnum transType, int transNum)
        {
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber + "_" + (int)transType + "_" + transNum);
            success = success && DeleteData();
            return success;
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        protected virtual bool DeleteByNumber(InvoiceTransactionPayload payload, string invoiceNumber, TransTypeEnum transType, int transNum)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = GetByNumber(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber + "_" + (int)transType + "_" + transNum);
            success = success && DeleteData();
            return success;
        }

        #region To qbo queue 

        /// <summary>
        /// convert erp trans to a queue message then put it to qbo queue
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authcode"></param>
        /// <param name="payload"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public virtual async Task ToQboQueueAsync(IPayload payload, ErpEventType eventType)
        {
            var message = new Event_ERPDto()
            {
                DatabaseNum = payload.DatabaseNum,
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                ERPEventType = (int)eventType,
                ProcessSource = Data.InvoiceTransaction.InvoiceNumber + "_" + Data.InvoiceTransaction.TransNum,
                ProcessUuid = Data.InvoiceTransaction.TransUuid,
            };
            await ErpEventClientHelper.ToQueueAsync(message);
        }

        /// <summary>
        /// convert erp trans to a queue message then put it to qbo queue
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authcode"></param>
        /// <param name="payload"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public virtual void ToQboQueue(IPayload payload, ErpEventType eventType)
        {
            var message = new Event_ERPDto()
            {
                DatabaseNum = payload.DatabaseNum,
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                ERPEventType = (int)eventType,
                ProcessSource = Data.InvoiceTransaction.InvoiceNumber + "_" + Data.InvoiceTransaction.TransNum,
                ProcessUuid = Data.InvoiceTransaction.TransUuid,
            };
            ErpEventClientHelper.ToQueue(message);
        }
        #endregion
    }
}



