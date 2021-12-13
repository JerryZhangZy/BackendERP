    
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
using DigitBridge.Base.Utility.Enums;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class WarehouseTransferService
    {

        #region override methods

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override WarehouseTransferService Init()
        {
            base.Init();
            SetDtoMapper(new WarehouseTransferDataDtoMapperDefault());
            SetCalculator(new WarehouseTransferServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new WarehouseTransferServiceValidatorDefault(this, this.dbFactory));
            return this;
        }

        /// <summary>
        /// Before update data (Add/Update/Delete). call this function to update relative data.
        /// For example: before save shipment, rollback instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// </summary>
        public override async Task BeforeSaveAsync()
        {
            try
            {
                await base.BeforeSaveAsync();
                if (this.Data?.WarehouseTransferHeader != null)
                {
                    await InventoryLogService.UpdateByWarehouseTransferAsync(_data,false);
                    //await inventoryService.UpdateOpenSoQtyFromSalesOrderItemAsync(this.Data.SalesOrderHeader.SalesOrderUuid, true);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error before save.");
            }
        }

        /// <summary>
        /// Before update data (Add/Update/Delete). call this function to update relative data.
        /// For example: before save shipment, rollback instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// </summary>
        public override void BeforeSave()
        {
            try
            {
                base.BeforeSave();
                if (this.Data?.WarehouseTransferHeader != null)
                {
                    //inventoryService.UpdateOpenSoQtyFromSalesOrderItem(this.Data.SalesOrderHeader.SalesOrderUuid, true);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error before save.");
            }
        }

        /// <summary>
        /// After save data (Add/Update/Delete), doesn't matter success or not, call this function to update relative data.
        /// For example: after save shipment, update instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// So that, if update not success, database records will not change, this update still use then same data. 
        /// </summary>
        public override async Task AfterSaveAsync()
        {
            try
            {
                await base.AfterSaveAsync();
                if (this.Data?.WarehouseTransferHeader != null)
                {
                    await InventoryLogService.UpdateByWarehouseTransferAsync(_data);
                    //await inventoryService.UpdateOpenSoQtyFromSalesOrderItemAsync(this.Data.SalesOrderHeader.SalesOrderUuid);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save.");
            }
        }

        /// <summary>
        /// After save data (Add/Update/Delete), doesn't matter success or not, call this function to update relative data.
        /// For example: after save shipment, update instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// So that, if update not success, database records will not change, this update still use then same data. 
        /// </summary>
        public override void AfterSave()
        {
            try
            {
                base.AfterSave();
                if (this.Data?.WarehouseTransferHeader != null)
                {
                    //inventoryService.UpdateOpenSoQtyFromSalesOrderItem(this.Data.SalesOrderHeader.SalesOrderUuid);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save.");
            }
        }

        /// <summary>
        /// Only save success (Add/Update/Delete), call this function to update relative data.
        /// For example: add activity log records.
        /// </summary>
        public override async Task SaveSuccessAsync()
        {
            try
            {
                await base.SaveSuccessAsync();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save success.");
            }
        }

        /// <summary>
        /// Only save success (Add/Update/Delete), call this function to update relative data.
        /// For example: add activity log records.
        /// </summary>
        public override void SaveSuccess()
        {
            try
            {
                base.SaveSuccess();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save success.");
            }
        }

        /// <summary>
        /// Sub class should override this method to return new ActivityLog object for service
        /// </summary>
        protected override ActivityLog GetActivityLog() =>
            new ActivityLog(dbFactory)
            {
                Type = (int)ActivityLogType.WarehouseTransfer,
                Action = (int)this.ProcessMode,
                LogSource = "WarehouseTransferService",

                MasterAccountNum = this.Data.WarehouseTransferHeader.MasterAccountNum,
                ProfileNum = this.Data.WarehouseTransferHeader.ProfileNum,
                DatabaseNum = this.Data.WarehouseTransferHeader.DatabaseNum,
                ProcessUuid = this.Data.WarehouseTransferHeader.WarehouseTransferUuid,
                ProcessNumber = this.Data.WarehouseTransferHeader.BatchNumber,
                ChannelNum = 0,
                ChannelAccountNum = 0,

                LogMessage = string.Empty
            };

        #endregion override methods


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(WarehouseTransferDataDto dto)
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
        public virtual async Task<bool> AddAsync(WarehouseTransferDataDto dto)
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

        public virtual bool Add(WarehouseTransferPayload payload)
        {
            if (payload is null || !payload.HasWarehouseTransfer)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.WarehouseTransfer))
                return false;

            // load data from dto
            FromDto(payload.WarehouseTransfer);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(WarehouseTransferPayload payload)
        {
            if (payload is null || !payload.HasWarehouseTransfer)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.WarehouseTransfer)))
                return false;

            // load data from dto
            FromDto(payload.WarehouseTransfer);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(WarehouseTransferDataDto dto)
        {
            if (dto is null || !dto.HasWarehouseTransferHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.WarehouseTransferHeader.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(WarehouseTransferDataDto dto)
        {
            if (dto is null || !dto.HasWarehouseTransferHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.WarehouseTransferHeader.RowNum.ToLong());

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
        public virtual bool Update(WarehouseTransferPayload payload)
        {
            if (payload is null || !payload.HasWarehouseTransfer || payload.WarehouseTransfer.WarehouseTransferHeader.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.WarehouseTransfer))
                return false;

            // load data 
            GetData(payload.WarehouseTransfer.WarehouseTransferHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.WarehouseTransfer);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(WarehouseTransferPayload payload)
        {
            if (payload is null || !payload.HasWarehouseTransfer)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.WarehouseTransfer)))
                return false;

            // load data 
            await GetDataAsync(payload.WarehouseTransfer.WarehouseTransferHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.WarehouseTransfer);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

 
        public virtual async Task<bool> CloseAsync(WarehouseTransferPayload payload)
        {
            payload.WarehouseTransfer.WarehouseTransferHeader.WarehouseTransferStatus = (int)TransferStatus.Closed;
            return await this.UpdateAsync(payload);
        }
        /// <summary>
        ///  get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetDataAsync(WarehouseTransferPayload payload, string orderNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetData(WarehouseTransferPayload payload, string orderNumber)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(WarehouseTransferPayload payload, string orderNumber)
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
        public virtual bool DeleteByNumber(WarehouseTransferPayload payload, string orderNumber)
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
        public bool GetWarehouseTransferByBatchNumber(WarehouseTransferPayload payload, string batchNumber)
        {
            if (string.IsNullOrEmpty(batchNumber))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = WarehouseTransferHelper.GetRowNumByNumber(batchNumber, payload.MasterAccountNum, payload.ProfileNum);
            }
            return GetData(rowNum);
        }
        public async Task<bool> GetWarehouseTransferByBatchNumberAsync(WarehouseTransferPayload payload, string batchNumber)
        {
            if (string.IsNullOrEmpty(batchNumber))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await WarehouseTransferHelper.GetRowNumByNumberAsync(batchNumber, payload.MasterAccountNum, payload.ProfileNum);
            }
            return await GetDataAsync(rowNum);
        }

        public WarehouseTransferPayload GetWarehouseTransfersByCodeArray(WarehouseTransferPayload payload)
        {
            if (!payload.HasBatchNumbers)
                return payload;
            var list = new List<WarehouseTransferDataDto>();
            var msglist = new List<MessageClass>();
            var rowNumList = new List<long>();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                rowNumList = WarehouseTransferHelper.GetRowNumsByNums(payload.BatchNumbers, payload.MasterAccountNum, payload.ProfileNum);
            }
            foreach (var rowNum in rowNumList)
            {
                if (GetData(rowNum))
                    list.Add(ToDto());
            }
            payload.WarehouseTransfers = list;
            payload.Messages = msglist;
            return payload;
        }

        public async Task<WarehouseTransferPayload> GetWarehouseTransfersByCodeArrayAsync(WarehouseTransferPayload payload)
        {
            if (!payload.HasBatchNumbers)
                return payload;
            var list = new List<WarehouseTransferDataDto>();
            var msglist = new List<MessageClass>();
            var rowNumList = new List<long>();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                rowNumList = await WarehouseTransferHelper.GetRowNumsByNumsAsync(payload.BatchNumbers, payload.MasterAccountNum, payload.ProfileNum);
            }
            foreach (var rowNum in rowNumList)
            {
                if (await GetDataAsync(rowNum))
                    list.Add(ToDto());
            }
            payload.WarehouseTransfers = list;
            payload.Messages = msglist;
            return payload;
        }

        public bool DeleteByBatchNumber(WarehouseTransferPayload payload, string batchNumber)
        {
            if (string.IsNullOrEmpty(batchNumber))
                return false;
            Delete();
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = WarehouseTransferHelper.GetRowNumByNumber(batchNumber, payload.MasterAccountNum, payload.ProfileNum);
            }
            var success = GetData(rowNum);
            if (success)
                return DeleteData();
            AddError("Data not found");
            return false;
        }

        public async Task<bool> DeleteByBatchNumberAsync(WarehouseTransferPayload payload, string batchNumber)
        {
            if (string.IsNullOrEmpty(batchNumber))
                return false;
            Delete();
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await WarehouseTransferHelper.GetRowNumByNumberAsync(batchNumber, payload.MasterAccountNum, payload.ProfileNum);
            }
            var success = await GetDataAsync(rowNum);
            if (success)
                return await DeleteDataAsync();
            AddError("Data not found");
            return false;
        }

        private InventoryLogService _inventoryLogService;

        protected InventoryLogService InventoryLogService
        {
            get
            {
                if (_inventoryLogService == null)
                    _inventoryLogService = new InventoryLogService(dbFactory);
                return _inventoryLogService;
            }
        }

        public override bool SaveData()
        {
            if(base.SaveData())
            {
                InventoryLogService.UpdateByWarehouseTransfer(_data);
                return true;
            }
            return false;
        }

        public override async Task<bool> SaveDataAsync()
        {
            if (await base.SaveDataAsync())
            {
                //await InventoryLogService.UpdateByWarehouseTransferAsync(_data);
                return true;
            }
            return false;
        }

        public override bool DeleteData()
        {
            if(base.DeleteData())
            {
                _data.WarehouseTransferItems.Clear();
                //InventoryLogService.UpdateByWarehouseTransfer(_data);
                return true;
            }
            return false;
        }

        public override async Task<bool> DeleteDataAsync()
        {
            if(await base.DeleteDataAsync())
            {
                _data.WarehouseTransferItems.Clear();
                await InventoryLogService.UpdateByWarehouseTransferAsync(_data,false);
                return true;
            }
            return false;
        }
    }
}



