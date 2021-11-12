

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

            if (Data == null)
                NewData();
            Data.InvoiceData = invoiceData;
            Data.InvoiceTransaction.InvoiceUuid = invoiceData.InvoiceHeader.InvoiceUuid;

            //if (Data.InvoiceReturnItems == null) return success;

            //foreach (var item in Data.InvoiceReturnItems)
            //{
            //    item.InvoiceUuid = invoiceData.InvoiceHeader.InvoiceUuid;
            //    if (!item.RowNum.IsZero()) continue;
            //    item.InvoiceItemsUuid = invoiceData.InvoiceItems.Where(i => i.SKU == item.SKU && i.WarehouseCode == item.WarehouseCode).FirstOrDefault()?.InvoiceItemsUuid;
            //}

            return success;
        }
        /// <summary>
        /// Load Invoice data.
        /// </summary>
        /// <param name="invoiceUuid"></param>
        protected async Task<bool> LoadInvoiceAsync(string invoiceUuid)
        {
            // load invoice data
            var invoiceData = new InvoiceData(dbFactory);
            var success = await invoiceData.GetByIdAsync(invoiceUuid);
            if (!success)
            {
                AddError($"Data not found for invoiceUuid:{invoiceUuid}");
                return success;
            }

            if (Data == null)
                NewData();
            Data.InvoiceData = invoiceData;
            Data.InvoiceTransaction.InvoiceUuid = invoiceData.InvoiceHeader.InvoiceUuid; 

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
        protected virtual bool Add(InvoiceTransactionDataDto dto)
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
        protected virtual async Task<bool> AddAsync(InvoiceTransactionDataDto dto)
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

        protected virtual bool Add(InvoiceTransactionPayload payload)
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

        protected virtual async Task<bool> AddAsync(InvoiceTransactionPayload payload)
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
        protected virtual bool Update(InvoiceTransactionDataDto dto)
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

            // Keep a copy of Original Paid Amount
            Data.InvoiceTransaction.OriginalPaidAmount = Data.InvoiceTransaction.TotalAmount;

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
        protected virtual async Task<bool> UpdateAsync(InvoiceTransactionDataDto dto)
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

            // Keep a copy of Original Paid Amount
            Data.InvoiceTransaction.OriginalPaidAmount = Data.InvoiceTransaction.TotalAmount;

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

        protected int lastPaidByBeforeUpdate;
        protected string lastAuthCodeBeforeUpdate;
        protected string lastInvoiceUuidBeforeUpdate;
        protected string lastInvoiceNumberBeforeUpdate;
        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        protected virtual bool Update(InvoiceTransactionPayload payload)
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

            // Keep a copy of Original Paid Amount
            Data.InvoiceTransaction.OriginalPaidAmount = Data.InvoiceTransaction.TotalAmount;
            lastInvoiceUuidBeforeUpdate = Data.InvoiceTransaction.InvoiceUuid;
            lastInvoiceNumberBeforeUpdate = Data.InvoiceTransaction.InvoiceNumber;
            lastPaidByBeforeUpdate = Data.InvoiceTransaction.PaidBy;
            lastAuthCodeBeforeUpdate = Data.InvoiceTransaction.AuthCode;

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
        protected virtual async Task<bool> UpdateAsync(InvoiceTransactionPayload payload)
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

            // Keep a copy of Original Paid Amount
            Data.InvoiceTransaction.OriginalPaidAmount = Data.InvoiceTransaction.TotalAmount;
            lastInvoiceUuidBeforeUpdate = Data.InvoiceTransaction.InvoiceUuid;
            lastInvoiceNumberBeforeUpdate = Data.InvoiceTransaction.InvoiceNumber;
            lastPaidByBeforeUpdate = Data.InvoiceTransaction.PaidBy;
            lastAuthCodeBeforeUpdate = Data.InvoiceTransaction.AuthCode;

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
        protected virtual async Task<List<InvoiceTransactionDataDto>> GetDtoListAsync(int masterAccountNum, int profileNum, string invoiceNumber, TransTypeEnum transType, int? transNum = null)
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
        protected async Task<List<InvoiceHeaderDto>> GetInvoiceHeadersByCustomerAsync(int masterAccountNum, int profileNum, string customerCode)
        {
            var invoiceHeaders = await new InvoiceHeader(_dbFactory).GetByCustomerCodeAsync(customerCode, masterAccountNum, profileNum);
            List<InvoiceHeaderDto> result = new List<InvoiceHeaderDto>();
            var mapper = new InvoiceDataDtoMapperDefault();
            invoiceHeaders.ForEach(invoice => {
                var dto = new InvoiceHeaderDto();
                if (invoiceHeaders.Any())
                    mapper.WriteInvoiceHeader(invoice, dto);
            });

            return result;
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

        #region copy invoice info to invoicereturn/invoicepayment for NewReturn/ NewPayment

        protected void CopyInvoiceHeaderToTrans()
        {
            var invoiceHeader = Data.InvoiceData.InvoiceHeader;
            Data.InvoiceTransaction = new InvoiceTransaction()
            {
                Currency = invoiceHeader.Currency,
                InvoiceNumber = invoiceHeader.InvoiceNumber,
                InvoiceUuid = invoiceHeader.InvoiceUuid,
                TaxRate = invoiceHeader.TaxRate
            };
        }
        protected void CopyInvoiceItemsToReturnItems()
        {
            var returnItems = new List<InvoiceReturnItems>();
            foreach (var item in Data.InvoiceData.InvoiceItems)
            {
                // Data.InvoiceReturnItems.
                var returnItem = new InvoiceReturnItems()
                {
                    Currency = item.Currency,
                    ChargeAndAllowanceAmount = item.ChargeAndAllowanceAmount,
                    DamageWarehouseCode = item.WarehouseCode,
                    DamageWarehouseUuid = item.WarehouseUuid,
                    Description = item.Description,
                    InventoryUuid = item.InventoryUuid,
                    InvoiceDiscountAmount = item.DiscountAmount,
                    InvoiceDiscountPrice = item.DiscountPrice,
                    InvoiceItemsUuid = item.InvoiceItemsUuid,
                    InvoiceUuid = item.InvoiceUuid,
                    InvoiceWarehouseCode = item.WarehouseCode,
                    InvoiceWarehouseUuid = item.WarehouseUuid,
                    SKU = item.SKU,
                    LotNum = item.LotNum,
                    Notes = item.Notes,
                    PackType = item.PackType,
                    PackQty = item.PackQty,
                    Price = item.Price,
                    ProductUuid = item.ProductUuid,
                    PutBackWarehouseCode = item.WarehouseCode,
                    PutBackWarehouseUuid = item.WarehouseUuid,
                    ReturnItemType = item.InvoiceItemType,
                    TaxRate = item.TaxRate,
                    IsAr = item.IsAr,
                };
                returnItems.Add(returnItem);
            }
            Data.InvoiceReturnItems = returnItems;
        }

        #endregion

        public async override Task<bool> GetDataByIdAsync(string id)
        {
            if (!await base.GetDataByIdAsync(id))
                return false;

            var trans = Data.InvoiceTransaction;
            return LoadInvoice(trans.InvoiceNumber, trans.ProfileNum, trans.MasterAccountNum);
        }
    }
}