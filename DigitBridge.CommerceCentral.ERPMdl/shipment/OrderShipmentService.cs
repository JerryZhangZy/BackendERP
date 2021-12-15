

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
    public partial class OrderShipmentService
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

        private InventoryLogService _inventoryLogService;
        public InventoryLogService inventoryLogService
        {
            get
            {
                if (_inventoryLogService is null)
                    _inventoryLogService = new InventoryLogService(dbFactory);
                return _inventoryLogService;
            }
        }

        private SalesOrderService _salesOrderService;
        public SalesOrderService salesOrderService
        {
            get
            {
                if (_salesOrderService is null)
                    _salesOrderService = new SalesOrderService(dbFactory);
                return _salesOrderService;
            }
        }

        #endregion


        #region override methods

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override OrderShipmentService Init()
        {
            base.Init();
            SetDtoMapper(new OrderShipmentDataDtoMapperDefault());
            SetCalculator(new OrderShipmentServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new OrderShipmentServiceValidatorDefault(this, this.dbFactory));
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
                if (this.Data?.OrderShipmentHeader != null)
                { 

                    // Update shipped qty in S/O and openSoQty in Inventory
                    var shipmentHeader = this.Data.OrderShipmentHeader;
                    await salesOrderService.UpdateShippedQtyAndOpenQtyFromShippedItemAsync(shipmentHeader.OrderShipmentUuid, true);
                    await inventoryService.UpdateOpenSoQtyFromSalesOrderItemAsync(shipmentHeader.SalesOrderUuid, true);
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
                if (this.Data?.OrderShipmentHeader != null)
                {
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
                if (this.Data?.OrderShipmentHeader != null)
                {
                    // Update shipped qty in S/O and openSoQty in Inventory
                    var shipmentHeader = this.Data.OrderShipmentHeader;
                    await salesOrderService.UpdateShippedQtyAndOpenQtyFromShippedItemAsync(shipmentHeader.OrderShipmentUuid, false);
                    await inventoryService.UpdateOpenSoQtyFromSalesOrderItemAsync(shipmentHeader.SalesOrderUuid, false);
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
                if (this.Data?.OrderShipmentHeader != null)
                {
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
                if (this.Data?.OrderShipmentHeader != null)
                {
                    if (this.ProcessMode == ProcessingMode.Delete)
                    {
                        // update inventoryLog and update instock
                        await inventoryLogService.ClearInventoryLogByLogUuidAsync(this.Data.OrderShipmentHeader.OrderShipmentUuid);
                        //TODO roll back order status to which one?
                    }
                    else
                    {
                        // set auto idetity number to children tables 
                        await UpdateOrderShipmentNumReferenceAsync(this.Data.OrderShipmentHeader.OrderShipmentUuid);
                        // update inventoryLog and update instock
                        await inventoryLogService.UpdateByShipmentAsync(this.Data);
                    }
                    // update salesorder status.
                    await salesOrderService.UpdateOrderStautsFromShipmentAsync(this.Data.OrderShipmentHeader.SalesOrderUuid);
                }
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
                Type = (int)ActivityLogType.Shipment,
                Action = (int)this.ProcessMode,
                LogSource = "OrderShipmentService",

                MasterAccountNum = this.Data.OrderShipmentHeader.MasterAccountNum,
                ProfileNum = this.Data.OrderShipmentHeader.ProfileNum,
                DatabaseNum = this.Data.OrderShipmentHeader.DatabaseNum,
                ProcessUuid = this.Data.OrderShipmentHeader.OrderShipmentUuid,
                ProcessNumber = this.Data.OrderShipmentHeader.OrderShipmentNum.ToString(),
                ChannelNum = this.Data.OrderShipmentHeader.ChannelNum,
                ChannelAccountNum = this.Data.OrderShipmentHeader.ChannelAccountNum,

                LogMessage = string.Empty
            };

        #endregion override methods


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(OrderShipmentDataDto dto)
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
        public virtual async Task<bool> AddAsync(OrderShipmentDataDto dto)
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

        public virtual bool Add(OrderShipmentPayload payload)
        {
            if (payload is null || !payload.HasOrderShipment)
                return false;

            payload.OrderShipment.SetAccount(payload.MasterAccountNum, payload.ProfileNum, payload.DatabaseNum);

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.OrderShipment))
                return false;

            // load data from dto
            FromDto(payload.OrderShipment);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(OrderShipmentPayload payload)
        {
            if (payload is null || !payload.HasOrderShipment)
                return false;

            // set Add mode and clear data
            Add();

            //Read account info from payload to dto.
            payload.OrderShipment.SetAccount(payload.MasterAccountNum, payload.ProfileNum, payload.DatabaseNum);

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.OrderShipment)))
                return false;

            // load data from dto
            FromDto(payload.OrderShipment);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(OrderShipmentDataDto dto)
        {
            if (dto is null || !dto.HasOrderShipmentHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.OrderShipmentHeader.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(OrderShipmentDataDto dto)
        {
            if (dto is null || !dto.HasOrderShipmentHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.OrderShipmentHeader.RowNum.ToLong());

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        public async Task GetListByOrderShipmentNumbersAsync(OrderShipmentPayload payload, IList<string> orderShipmentNumbers)
        {
            if (payload is null || !payload.HasOrderShipmentNumbers)
            {
                AddError("OrderShipmentNumbers is required.");
                payload.Messages = this.Messages;
                payload.Success = false;
            }
            //var rowNums = await new InvoiceList(dbFactory).GetRowNumListAsync(payload.InvoiceNumbers, payload.MasterAccountNum, payload.ProfileNum);

            var result = new List<OrderShipmentDataDto>();
            foreach (var orderShipmentNumber in payload.OrderShipmentNumbers)
            {
                if (!(await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderShipmentNumber.ToString())))
                    continue;
                result.Add(this.ToDto());
                this.DetachData(this.Data);
            }
            payload.OrderShipments = result;

            if (!result.Any())
                payload.ReturnError("No data be found");
        }

        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(OrderShipmentPayload payload)
        {
            if (payload is null || !payload.HasOrderShipment || payload.OrderShipment.OrderShipmentHeader.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.OrderShipment))
                return false;

            // load data 
            GetData(payload.OrderShipment.OrderShipmentHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.OrderShipment);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(OrderShipmentPayload payload)
        {
            if (payload is null || !payload.HasOrderShipment)
                return false;

            //Read account info from payload to dto.
            payload.OrderShipment.SetAccount(payload.MasterAccountNum, payload.ProfileNum, payload.DatabaseNum);

            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.OrderShipment)))
                return false;

            // load data 
            await GetDataAsync(payload.OrderShipment.OrderShipmentHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.OrderShipment);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        InventoryLogService logService;
        /// <summary>
        /// Delete order shipment by order shipment number
        /// </summary>
        /// <param name="orderShipmentNum"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(OrderShipmentPayload payload, long orderShipmentNum)
        {
            if (orderShipmentNum.IsZero())
            {
                AddError("orderShipmentNum is invalid");
                return false;
            }
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderShipmentNum.ToString());
            if (!success)
                return false;
            return await DeleteDataAsync();
        }

        public virtual async Task<bool> GetDataAsync(OrderShipmentPayload payload, string number)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, number);
        }

        public virtual bool GetData(OrderShipmentPayload payload, string number)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, number);
        }

        public async Task<bool> UpdateProcessStatusAsync(string ordershipmentUuid, OrderShipmentProcessStatusEnum status, string invoiceUuid, string invoiceNumber)
        {
            var sql = $@"
UPDATE OrderShipmentHeader 
SET ProcessStatus=@0, 
ProcessDateUtc=@1,
InvoiceUuid=@2,
InvoiceNumber=@3
WHERE OrderShipmentUuid=@4 
";
            return await dbFactory.Db.ExecuteAsync(
                sql,
                ((int)status).ToSqlParameter("@0"),
                DateTime.UtcNow.ToSqlParameter("@1"),
                invoiceUuid.ToSqlParameter("@2"),
                invoiceNumber.ToSqlParameter("@3"),
                ordershipmentUuid.ToSqlParameter("@4")
            ) > 0;
        }

        public async Task<bool> UpdateOrderShipmentNumReferenceAsync(string orderShipmentUuid)
        {
            var sql = $@"
UPDATE spk 
SET spk.OrderShipmentNum=shd.OrderShipmentNum
,spk.RowNum=spk.OrderShipmentPackageNum
FROM OrderShipmentPackage spk 
INNER JOIN OrderShipmentHeader shd ON (spk.OrderShipmentUuid = shd.OrderShipmentUuid)
WHERE spk.OrderShipmentUuid=@0 
";
            await dbFactory.Db.ExecuteAsync(sql,
                orderShipmentUuid.ToSqlParameter("@0")
            );

            var sql1 = $@"
UPDATE spi
SET spi.OrderShipmentNum=spk.OrderShipmentNum
,spi.OrderShipmentPackageNum=spk.OrderShipmentPackageNum
,spi.RowNum=spi.OrderShipmentShippedItemNum
FROM OrderShipmentShippedItem spi 
INNER JOIN OrderShipmentPackage spk ON (spk.OrderShipmentPackageUuid = spi.OrderShipmentPackageUuid)
WHERE spi.OrderShipmentUuid=@0 
";
            await dbFactory.Db.ExecuteAsync(sql1,
                orderShipmentUuid.ToSqlParameter("@0")
            );


            var sql2 = $@"
UPDATE spc 
SET spc.OrderShipmentNum=shd.OrderShipmentNum
,spc.RowNum=spc.OrderShipmentCanceledItemNum
FROM OrderShipmentCanceledItem spc
INNER JOIN OrderShipmentHeader shd ON (spc.OrderShipmentUuid = shd.OrderShipmentUuid)
WHERE spc.OrderShipmentUuid=@0 
";
            await dbFactory.Db.ExecuteAsync(sql2,
                orderShipmentUuid.ToSqlParameter("@0")
                );

            return true;
        }

        /// <summary>
        /// Get ShipmentUuid by OrderDCAssignmentNum or sSalesOrderUuid
        /// </summary>
        public async Task<string> GetOrderShipmentUuidBySalesOrderUuidOrDCAssignmentNumAsync(string salesOrderUuid, long orderDCAssignmentNum)
        {
            if (orderDCAssignmentNum.IsZero() && salesOrderUuid.IsZero()) return string.Empty;

            using (var trs = new ScopedTransaction(dbFactory))
                return await OrderShipmentHelper.GetOrderShipmentUuidBySalesOrderUuidOrDCAssignmentNumAsync(salesOrderUuid, orderDCAssignmentNum);
        }

        public async Task<(string, string)> GetShipmentUuidAndInvoiceUuidAsync(int masterAccountNum, int profileNum, string shipmentID)
        {
            using (var tx = new ScopedTransaction(dbFactory))
            {
                return await OrderShipmentHelper.GetShipmentUuidAndInvoiceUuidAsync(shipmentID, masterAccountNum, profileNum);
            }
        }

    }
}



