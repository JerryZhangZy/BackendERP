    
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
    public partial class PoTransactionService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override PoTransactionService Init()
        {
            base.Init();
            SetDtoMapper(new PoTransactionDataDtoMapperDefault());
            SetCalculator(new PoTransactionServiceCalculatorDefault(this,this.dbFactory));
            AddValidator(new PoTransactionServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(PoTransactionDataDto dto)
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
        public virtual async Task<bool> AddAsync(PoTransactionDataDto dto)
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

        public virtual bool Add(PoTransactionPayload payload)
        {
            if (payload is null || !payload.HasPoTransaction)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.PoTransaction))
                return false;

            // load data from dto
            FromDto(payload.PoTransaction);
            
            if (!LoadPurchaseOrderData(payload.PoTransaction.PoTransaction.PoNum, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(PoTransactionPayload payload)
        {
            if (payload is null || !payload.HasPoTransaction)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.PoTransaction)))
                return false;

            // load data from dto
            FromDto(payload.PoTransaction);
            
            if (!LoadPurchaseOrderData(payload.PoTransaction.PoTransaction.PoNum, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(PoTransactionDataDto dto)
        {
            if (dto is null || !dto.HasPoTransaction)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.PoTransaction.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(PoTransactionDataDto dto)
        {
            if (dto is null || !dto.HasPoTransaction)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.PoTransaction.RowNum.ToLong());

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
        public virtual bool Update(PoTransactionPayload payload)
        {
            if (payload is null || !payload.HasPoTransaction || payload.PoTransaction.PoTransaction.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.PoTransaction))
                return false;

            // load data 
            GetData(payload.PoTransaction.PoTransaction.RowNum.ToLong());

            // load data from dto
            FromDto(payload.PoTransaction);

            if (!LoadPurchaseOrderData(payload.PoTransaction.PoTransaction.PoNum, payload.ProfileNum, payload.MasterAccountNum))
                return false;
            
            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(PoTransactionPayload payload)
        {
            if (payload is null || !payload.HasPoTransaction)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.PoTransaction)))
                return false;

            // load data 
            await GetDataAsync(payload.PoTransaction.PoTransaction.RowNum.ToLong());

            // load data from dto
            FromDto(payload.PoTransaction);
            
            if (!LoadPurchaseOrderData(payload.PoTransaction.PoTransaction.PoNum, payload.ProfileNum, payload.MasterAccountNum))
                return false;

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
        public virtual async Task<bool> GetDataAsync(PoTransactionPayload payload, string orderNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetData(PoTransactionPayload payload, string orderNumber)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(PoTransactionPayload payload, string orderNumber)
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
        public virtual bool DeleteByNumber(PoTransactionPayload payload, string orderNumber)
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
        /// Get PoHeader by invoiceNumber//TODO move to invoice service
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        protected async Task<PoHeaderDto> GetPoHeaderAsync(int masterAccountNum, int profileNum, string invoiceNumber)
        {
            var poHeader = await new PoHeader(_dbFactory).GetByPoNumAsync(invoiceNumber, masterAccountNum, profileNum);
            var dto = new PoHeaderDto();
            if (poHeader != null)
                new PurchaseOrderDataDtoMapperDefault().WritePoHeader(poHeader, dto);
                // new PoDataDtoMapperDefault().WriteInvoiceHeader(invoiceHeader, dto);
            return dto;
        }
        
        protected virtual async Task<List<PoTransactionDataDto>> GetPoTransactionDataDtoListAsync(int masterAccountNum, int profileNum, string poNum,int? transNum = null)
        {
            if (string.IsNullOrEmpty(poNum)) return null;
            var dataList = await GetDataListAsync(masterAccountNum, profileNum, poNum,transNum);
            if (dataList == null || dataList.Count == 0) return null;
            var dtoList = new List<PoTransactionDataDto>();
            foreach (var dataItem in dataList)
            {
                var dtoItem = new PoTransactionDataDto();
                dtoList.Add(this.DtoMapper.WriteDto(dataItem, dtoItem));
            }
            return dtoList;
        }
        
        protected virtual async Task<List<PoTransactionData>> GetDataListAsync(int masterAccountNum, int profileNum, string poNum, int? transNum = null)
        {
            List();
            if (string.IsNullOrEmpty(poNum)) return null;
            LoadPurchaseOrderData(poNum, profileNum, masterAccountNum);
            return await _data.GetDataListAsync(poNum, masterAccountNum, profileNum, transNum);
        }
        
        protected async Task<bool> GetByNumberAsync(PoTransactionPayload payload, string poNum, int transNum)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, poNum, transNum);
        }
        protected async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string poNum, int transNum)
        {
            List();
            var success = await base.GetByNumberAsync(masterAccountNum, profileNum, transNum.ToString());
            LoadPurchaseOrderData(poNum, profileNum, masterAccountNum);
            return success;
        }

        //protected async Task<bool> GetByTransNumAsync(PoTransactionPayload payload,  int transNum)
        //{

        //    List();

        //   var poTransactions= dbFactory.Db.Query<PoTransaction>($@"");
        //    foreach (var item in poTransactions)
        //    {
        //        NewData();
        //        // var success = await Data.GetByTransNumAsync(payload.MasterAccountNum, payload.ProfileNum, transNum);
        //        LoadPurchaseOrderData(item.PoNum, payload.ProfileNum, payload.MasterAccountNum);
        //    }
        //    return success;
           
        //}

        /// <summary>
        /// Load LoadPurchaseOrder data.
        /// </summary>
        /// <param name="poNum"></param>
        protected bool LoadPurchaseOrderData(string poNum, int profileNum, int masterAccountNum)
        {
            // load LoadPurchaseOrderData
            var poData = new PurchaseOrderData(dbFactory);
            var success = poData.GetByNumber(masterAccountNum, profileNum, poNum);
            if (!success)
            { 
                AddError($"PurchaseOrderData no found");
                return false;
            }

            if (Data == null)
                NewData();
            Data.PurchaseOrderData = poData;
            Data.PoTransaction.PoUuid = poData.PoHeader.PoUuid;
            // if (Data.PoTransactionItems == null) return success;
            //
            // foreach (var item in Data.PoTransactionItems)
            // {
            //     item.PoUuid = poData.PoHeader.PoUuid;
            //     if (!item.RowNum.IsZero()) continue;
            //     item.PoItemUuid = poData.PoItems.FirstOrDefault(i => i.SKU == item.SKU && i.WarehouseUuid == item.WarehouseUuid)?.PoItemUuid;
            // }

            // var emptyItems = Data.PoTransactionItems.Where(r => r.PoItemUuid.IsZero()).ToList();
            //
            // foreach (var emptyItem in emptyItems)
            // {
            //     Data.PoTransactionItems.Remove(emptyItem);
            // }
            // if(!Data.PoTransactionItems.Any()){
            //     AddError($"No Math PoTransactionItems");
            //     return false;
            // }

            return success;
        }

       

        /// <summary>
        /// Delete invoice by invoice number
        /// </summary>
        /// <param name="poNum"></param>
        /// <returns></returns>
        protected virtual async Task<bool> DeleteByNumberAsync(PoTransactionPayload payload, string poNum, int transNum)
        {
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, poNum + "_" + transNum);
            success = success && DeleteData();
            return success;
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="poNum"></param>
        /// <returns></returns>
        protected virtual bool DeleteByNumber(PoTransactionPayload payload, string poNum, int transNum)
        {
            if (string.IsNullOrEmpty(poNum))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = GetByNumber(payload.MasterAccountNum, payload.ProfileNum, poNum + "_" + transNum);
            success = success && DeleteData();
            return success;
        }

        public async Task<int> GetRowNumByPoUuid(string poUuid)
        {
            return dbFactory.GetValue<PoHeader, int>("SELECT TOP 1 RowNum FROM PoTransaction WHERE PoUuid=@0",
                poUuid.ToSqlParameter("@0"));
        }
        
        
        #region copy invoice info to invoicereturn/invoicepayment for NewReturn/ NewPayment

        protected void CopyPoHeaderToReceive()
        {
            var poHeader = Data.PurchaseOrderData.PoHeader;
            Data.PoTransaction = new PoTransaction()
            {
                PoNum = poHeader.PoNum,
                PoUuid = poHeader.PoUuid,
                VendorName = poHeader.VendorName,
                VendorCode = poHeader.VendorCode,
                VendorUuid = poHeader.VendorUuid
            };
        }
        protected void CopyPoItemsToReceiveItems()
        {
            var poTransactionItems = new List<PoTransactionItems>();
            foreach (var item in Data.PurchaseOrderData.PoItems)
            {
                // Data.InvoiceReturnItems.
                var poTransactionItem = new PoTransactionItems()
                {
                    PoUuid = item.PoUuid,
                    PoItemUuid = item.PoItemUuid,
                    Description = item.Description,
                    ProductUuid = item.ProductUuid,
                    InventoryUuid = item.InventoryUuid,
                    WarehouseUuid = item.WarehouseUuid,
                    SKU = item.SKU,
                    Notes = item.Notes,
                    Price = item.Price,
                    ItemType = item.PoItemType,
                    TaxRate = item.TaxRate 
                };
                
                poTransactionItems.Add(poTransactionItem);
            }
            Data.PoTransactionItems = poTransactionItems;
        }

        #endregion

    }
}



