

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
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class SalesOrderService
    {

        #region Service Property

        private CustomerService _customerService;
        public CustomerService customerService
        {
            get
            {
                if (_customerService is null)
                    _customerService = new CustomerService(dbFactory);
                return _customerService;
            }
        }

        private InventoryService _inventoryService;
        public InventoryService inventoryService
        {
            get
            {
                if (_inventoryService is null)
                    _inventoryService = new InventoryService(dbFactory);
                return _inventoryService;
            }
        }

        #endregion

        #region override methods

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override SalesOrderService Init()
        {
            base.Init();
            SetDtoMapper(new SalesOrderDataDtoMapperDefault());
            SetCalculator(new SalesOrderServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new SalesOrderServiceValidatorDefault(this, this.dbFactory));

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
                if (this.Data?.SalesOrderHeader != null)
                {
                    await inventoryService.UpdateOpenSoQtyFromSalesOrderItemAsync(this.Data.SalesOrderHeader.SalesOrderUuid, true);
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
                if (this.Data?.SalesOrderHeader != null)
                {
                    inventoryService.UpdateOpenSoQtyFromSalesOrderItem(this.Data.SalesOrderHeader.SalesOrderUuid, true);
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
                if (this.Data?.SalesOrderHeader != null)
                {
                    await inventoryService.UpdateOpenSoQtyFromSalesOrderItemAsync(this.Data.SalesOrderHeader.SalesOrderUuid);
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
                if (this.Data?.SalesOrderHeader != null)
                {
                    inventoryService.UpdateOpenSoQtyFromSalesOrderItem(this.Data.SalesOrderHeader.SalesOrderUuid);
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
                Type = (int)ActivityLogType.SalesOrder,
                Action = (int)this.ProcessMode,
                LogSource = "SalesOrderService",

                MasterAccountNum = this.Data.SalesOrderHeader.MasterAccountNum,
                ProfileNum = this.Data.SalesOrderHeader.ProfileNum,
                DatabaseNum = this.Data.SalesOrderHeader.DatabaseNum,
                ProcessUuid = this.Data.SalesOrderHeader.SalesOrderUuid,
                ProcessNumber = this.Data.SalesOrderHeader.OrderNumber,
                ChannelNum = this.Data.SalesOrderHeaderInfo.ChannelNum,
                ChannelAccountNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty
            };

        #endregion override methods

        protected bool UpdateInventoryOpenSoQty(string salesOrderUuid, bool isReturnback)
        {
            try
            {
                var inventoryService = new InventoryService(this.dbFactory);
                inventoryService.UpdateOpenSoQtyFromSalesOrderItem(salesOrderUuid, isReturnback);

                return true;
            }
            catch (Exception)
            {
                AddWarning("There is a error when update relative data.");
                return false;
            }
        }

        protected async Task<bool> UpdateInventoryOpenSoQtyAsync(string salesOrderUuid, bool isReturnback)
        {
            try
            {
                var inventoryService = new InventoryService(this.dbFactory);
                await inventoryService.UpdateOpenSoQtyFromSalesOrderItemAsync(salesOrderUuid, isReturnback);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Add to ActivityLog record for current data and processMode
        /// Should Call this method after successful save, update, delete
        /// </summary>
        protected void AddActivityLogForCurrentData()
        {
            this.AddActivityLog(new ActivityLog(dbFactory)
            {
                Type = (int)ActivityLogType.SalesOrder,
                Action = (int)this.ProcessMode,
                LogSource = "SalesOrderService",

                MasterAccountNum = this.Data.SalesOrderHeader.MasterAccountNum,
                ProfileNum = this.Data.SalesOrderHeader.ProfileNum,
                DatabaseNum = this.Data.SalesOrderHeader.DatabaseNum,
                ProcessUuid = this.Data.SalesOrderHeader.SalesOrderUuid,
                ProcessNumber = this.Data.SalesOrderHeader.OrderNumber,
                ChannelNum = this.Data.SalesOrderHeaderInfo.ChannelNum,
                ChannelAccountNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty
            });
        }

        /// <summary>
        /// Add to ActivityLog record for current data and processMode
        /// Should Call this method after successful save, update, delete
        /// </summary>
        protected async Task AddActivityLogForCurrentDataAsync()
        {
            await this.AddActivityLogAsync(new ActivityLog(dbFactory)
            {
                Type = (int)ActivityLogType.SalesOrder,
                Action = (int)this.ProcessMode,
                LogSource = "SalesOrderService",

                MasterAccountNum = this.Data.SalesOrderHeader.MasterAccountNum,
                ProfileNum = this.Data.SalesOrderHeader.ProfileNum,
                DatabaseNum = this.Data.SalesOrderHeader.DatabaseNum,
                ProcessUuid = this.Data.SalesOrderHeader.SalesOrderUuid,
                ProcessNumber = this.Data.SalesOrderHeader.OrderNumber,
                ChannelNum = this.Data.SalesOrderHeaderInfo.ChannelNum,
                ChannelAccountNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty

            });
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(SalesOrderDataDto dto)
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

            //UpdateInventoryOpenSoQty(dto.SalesOrderHeader.SalesOrderUuid, true);
            //var result = SaveData();
            //if (result)
            //    AddActivityLogForCurrentData();
            //UpdateInventoryOpenSoQty(dto.SalesOrderHeader.SalesOrderUuid, false);

            //return result;
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(SalesOrderDataDto dto)
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

            //await UpdateInventoryOpenSoQtyAsync(dto.SalesOrderHeader.SalesOrderUuid, true);
            //var result = await SaveDataAsync();
            //if (result)
            //    await AddActivityLogForCurrentDataAsync();
            //await UpdateInventoryOpenSoQtyAsync(dto.SalesOrderHeader.SalesOrderUuid, false);

            //return result;
        }

        public virtual bool Add(SalesOrderPayload payload)
        {
            if (payload is null || !payload.HasSalesOrder)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.SalesOrder))
                return false;

            // load data from dto
            FromDto(payload.SalesOrder);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();

            //UpdateInventoryOpenSoQty(payload.SalesOrder.SalesOrderHeader.SalesOrderUuid, true);
            //var result = SaveData();
            //if (result)
            //    AddActivityLogForCurrentData();
            //UpdateInventoryOpenSoQty(payload.SalesOrder.SalesOrderHeader.SalesOrderUuid, false);
            //return result;
        }

        public virtual async Task<bool> AddAsync(SalesOrderPayload payload)
        {
            if (payload is null || !payload.HasSalesOrder)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.SalesOrder)))
                return false;

            // load data from dto
            FromDto(payload.SalesOrder);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(SalesOrderDataDto dto)
        {
            if (dto is null || !dto.HasSalesOrderHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.SalesOrderHeader.RowNum.ToLong());

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();

            //UpdateInventoryOpenSoQty(dto.SalesOrderHeader.SalesOrderUuid, true);
            //var result = SaveData();
            //if (result)
            //    AddActivityLogForCurrentData();
            //UpdateInventoryOpenSoQty(dto.SalesOrderHeader.SalesOrderUuid, false);

            //return result;
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(SalesOrderDataDto dto)
        {
            if (dto is null || !dto.HasSalesOrderHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.SalesOrderHeader.RowNum.ToLong());

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();

            //await UpdateInventoryOpenSoQtyAsync(dto.SalesOrderHeader.SalesOrderUuid, true);
            //var result = await SaveDataAsync();
            //if (result)
            //    await AddActivityLogForCurrentDataAsync();
            //await UpdateInventoryOpenSoQtyAsync(dto.SalesOrderHeader.SalesOrderUuid, false);

            //return result;
        }

        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(SalesOrderPayload payload)
        {
            if (payload is null || !payload.HasSalesOrder || payload.SalesOrder.SalesOrderHeader.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.SalesOrder))
                return false;

            // load data 
            GetData(payload.SalesOrder.SalesOrderHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.SalesOrder);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();

            //UpdateInventoryOpenSoQty(payload.SalesOrder.SalesOrderHeader.SalesOrderUuid, true);
            //var result = SaveData();
            //if (result)
            //    AddActivityLogForCurrentData();
            //UpdateInventoryOpenSoQty(payload.SalesOrder.SalesOrderHeader.SalesOrderUuid, false);

            //return result;
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(SalesOrderPayload payload)
        {
            if (payload is null || !payload.HasSalesOrder)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.SalesOrder)))
                return false;

            // load data 
            await GetDataAsync(payload.SalesOrder.SalesOrderHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.SalesOrder);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();

            //await UpdateInventoryOpenSoQtyAsync(payload.SalesOrder.SalesOrderHeader.SalesOrderUuid, true);
            //var result = SaveData();
            //if (result)
            //    await AddActivityLogForCurrentDataAsync();
            //await UpdateInventoryOpenSoQtyAsync(payload.SalesOrder.SalesOrderHeader.SalesOrderUuid, false);

            //return result;
        }

        ///// <summary>
        ///// Get sale order with detail by orderNumber
        ///// </summary>
        ///// <param name="orderNumber"></param>
        ///// <returns></returns>
        //public virtual async Task<bool> GetByOrderNumberAsync(SalesOrderPayload payload, string orderNumber)
        //{
        //    if (string.IsNullOrEmpty(orderNumber))
        //        return false;
        //    List();
        //    if (!(await ValidateAccountAsync(payload, orderNumber)))
        //        return false;
        //    var rowNum = await _data.GetRowNumAsync(orderNumber, payload.MasterAccountNum, payload.ProfileNum);
        //    if (!rowNum.HasValue)
        //        return false;
        //    var success = await GetDataAsync(rowNum.Value);
        //    //if (success) ToDto();
        //    return success;
        //}
        /// <summary>
        /// Get multi sale order with detail by orderNumbers
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task GetListByOrderNumbersAsync(SalesOrderPayload payload)
        {
            if (payload is null || !payload.HasOrderNumbers)
            {
                AddError("OrderNumbers is required.");
                payload.Messages = this.Messages;
                payload.Success = false;
            }
            //var rowNums = await new SalesOrderList(dbFactory).GetRowNumListAsync(payload.OrderNumbers, payload.MasterAccountNum, payload.ProfileNum);

            var result = new List<SalesOrderDataDto>();
            foreach (var orderNumber in payload.OrderNumbers)
            {
                if (!(await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber)))
                    continue;
                result.Add(this.ToDto());
                this.DetachData(this.Data);
            }
            payload.SalesOrders = result;

            if (!result.Any())
                payload.ReturnError("No data be found");
        }

        ///// <summary>
        ///// Get sale order list by Uuid list
        ///// </summary>
        //public virtual async Task<SalesOrderPayload> GetListBySalesOrderUuidsNumberAsync(SalesOrderPayload salesOrderPayload)
        //{
        //    if (salesOrderPayload is null || !salesOrderPayload.HasSalesOrderUuids)
        //    {
        //        AddError("SalesOrderUuids is required.");
        //        salesOrderPayload.Messages = this.Messages;
        //        return salesOrderPayload;
        //    }

        //    var salesOrderUuids = salesOrderPayload.SalesOrderUuids;

        //    List();
        //    var result = new List<SalesOrderDataDto>();
        //    foreach (var id in salesOrderUuids)
        //    {
        //        if (!(await this.GetDataByIdAsync(id)))
        //            continue;
        //        result.Add(this.ToDto());
        //        this.DetachData(this.Data);
        //    }
        //    salesOrderPayload.SalesOrders = result;
        //    return salesOrderPayload;
        //}

        /// <summary>
        ///  get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetDataAsync(SalesOrderPayload payload, string orderNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetData(SalesOrderPayload payload, string orderNumber)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// Delete salesorder by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(SalesOrderPayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
            if (!success)
                return success;
            return await DeleteDataAsync();

            //if (success)
            //    await AddActivityLogForCurrentDataAsync();

            //return success;
        }

        /// <summary>
        /// Delete salesorder by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(SalesOrderPayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
            if (!success)
                return success;
            return DeleteData();

            //if (success)
            //    AddActivityLogForCurrentData();

            //return success;
        }

        /// <summary>
        /// Add pre payment.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual async Task<bool> AddPrepaymentAsync(SalesOrderPayload payload, string orderNumber, decimal amount)
        {
            if (string.IsNullOrEmpty(orderNumber))
            {
                AddError("orderNumber is null.");
                return false;
            }
            if (amount.IsZero())
            {
                AddError($"amount:{amount} is invalid.");
                return false;
            }

            //load salesorder data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
            if (!success)
                return false;

            // create misc invoice
            Data.SalesOrderHeader.DepositAmount = amount.ToAmount();

            var miscInvoiceService = new MiscInvoiceService(dbFactory);
            if (!(await miscInvoiceService.AddFromSalesOrderAsync(Data.SalesOrderHeader)))
            {
                this.Messages = this.Messages.Concat(miscInvoiceService.Messages).ToList();
                return false;
            }

            //set misc invoice uuid back to salesorder.
            Data.SalesOrderHeader.MiscInvoiceUuid = miscInvoiceService.Data.MiscInvoiceHeader.MiscInvoiceUuid;


            _ProcessMode = ProcessingMode.Edit;
            return await SaveDataAsync();
        }

        public async Task<bool> ExistOrderNumber(int masterAccountNum, int profileNum, string orderNumber)
        {
            return await SalesOrderHelper.ExistNumberAsync(orderNumber, masterAccountNum, profileNum);
        }

        //public virtual async Task<bool> SaveCurrentDataAsync()
        //{
        //    if (this.Data == null || this.Data.SalesOrderHeader == null) 
        //        return false;

        //    await UpdateInventoryOpenSoQtyAsync(this.Data.SalesOrderHeader.SalesOrderUuid, true);
        //    var result = await this.SaveDataAsync();
        //    if (!result) return false;

        //    await AddActivityLogForCurrentDataAsync();
        //    await UpdateInventoryOpenSoQtyAsync(this.Data.SalesOrderHeader.SalesOrderUuid, false);

        //    return result;
        //}


        public async Task<bool> UpdateStatusAsync(long rowNum, SalesOrderStatus status)
        {
            if (rowNum == 0) return false;

            var sql = $@"
UPDATE SalesOrderHeader 
SET OrderStatus=@0
WHERE RowNum=@1 
";
            return await dbFactory.Db.ExecuteAsync(
                sql,
                ((int)status).ToSqlParameter("@0"),
                rowNum.ToSqlParameter("@1")
            ) > 0;
        }

        /// <summary>
        /// Get SalesOrderData by OrderDCAssignmentNum
        /// </summary>
        /// <param name="orderDCAssignmentNum"></param>
        /// <returns>SalesOrderData</returns>
        public async Task<string> GetSalesOrderUuidByDCAssignmentNumAsync(long orderDCAssignmentNum)
        {
            if (orderDCAssignmentNum == 0) return string.Empty;
            //Get SalesOrderData by uuid
            using (var trs = new ScopedTransaction(dbFactory))
                return await SalesOrderHelper.GetSalesOrderUuidAsync(orderDCAssignmentNum);
        }

        /// <summary>
        /// Get SalesOrderData by OrderDCAssignmentNum
        /// </summary>
        /// <param name="orderDCAssignmentNum"></param>
        /// <returns>SalesOrderData</returns>
        public async Task<string> GetSalesOrderNumberByUuidAsync(string salesOrderUuid)
        {
            if (string.IsNullOrEmpty(salesOrderUuid)) return string.Empty;
            //Get SalesOrderData by uuid
            using (var trs = new ScopedTransaction(dbFactory))
                return await SalesOrderHelper.GetSalesOrderNumberByUuidAsync(salesOrderUuid);
        }

        /// <summary>
        /// Update s/o item shipqty from shipment item.
        /// </summary>
        /// <param name="shipmentUuid"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShippedQtyFromShippedItemAsync(string shipmentUuid, bool isReturnBack = false)
        {
            if (shipmentUuid.IsZero())
            {
                return false;
            }

            var op = isReturnBack ? "-" : "+";

            var sql = $@"
UPDATE soItem 
SET soItem.ShipQty=soItem.ShipQty {op} shippedItem.ShippedQty
FROM SalesOrderItems soItem 
INNER JOIN OrderShipmentShippedItem shippedItem on  shippedItem.SalesOrderItemsUuid=soItem.SalesOrderItemsUuid 
WHERE shippedItem.OrderShipmentUuid=@0  
";
            return await dbFactory.Db.ExecuteAsync(
                sql,
                shipmentUuid.ToSqlParameter("@0")
            ) > 0;
        }
    }
}



