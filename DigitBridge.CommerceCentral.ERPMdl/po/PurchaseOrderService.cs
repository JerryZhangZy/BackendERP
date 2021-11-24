    
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
    public partial class PurchaseOrderService
    {

        #region override methods

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override PurchaseOrderService Init()
        {
            base.Init();
            SetDtoMapper(new PurchaseOrderDataDtoMapperDefault());
            SetCalculator(new PurchaseOrderServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new PurchaseOrderServiceValidatorDefault(this, this.dbFactory));
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
                if (this.Data?.PoHeader != null)
                {
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
                if (this.Data?.PoHeader != null)
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
                if (this.Data?.PoHeader != null)
                {
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
                if (this.Data?.PoHeader != null)
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
                Type = (int)ActivityLogType.PurchaseOrder,
                Action = (int)this.ProcessMode,
                LogSource = "PurchaseOrderService",

                MasterAccountNum = this.Data.PoHeader.MasterAccountNum,
                ProfileNum = this.Data.PoHeader.ProfileNum,
                DatabaseNum = this.Data.PoHeader.DatabaseNum,
                ProcessUuid = this.Data.PoHeader.PoUuid,
                ProcessNumber = this.Data.PoHeader.PoNum,
                ChannelNum = this.Data.PoHeaderInfo.ChannelAccountNum,
                ChannelAccountNum = this.Data.PoHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty
            };

        #endregion override methods


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

            if (!(await ValidateAsync(dto)))
                return false;

            // load data from dto
            FromDto(dto); 

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
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

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.PurchaseOrder)))
                return false;

            // load data from dto
            FromDto(payload.PurchaseOrder); 

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

         

        public async Task<bool> DeleteByPoNumAsync(PurchaseOrderPayload payload, string ponum)
        {
            if (string.IsNullOrEmpty(ponum))
                return false;
            Delete();
            if (!(await ValidateAccountAsync(payload, ponum).ConfigureAwait(false)))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await PurchaseOrderHelper.GetRowNumByPoNumAsync(ponum, payload.MasterAccountNum, payload.ProfileNum);
            }
            var success = await GetDataAsync(rowNum);
            return success && (await DeleteDataAsync());
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
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.PoHeader.RowNum.ToLong());

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
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.PurchaseOrder)))
                return false;

            // load data 
            await GetDataAsync(payload.PurchaseOrder.PoHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.PurchaseOrder); 

            // validate data for Add processing
            if (!(await ValidateAsync()))
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

        
        //public virtual async Task<bool> UpdateByPoReceiveAsync(PoTransactionData data)
        //{
        //    if (data == null || data.PoTransaction == null)
        //        return false;
            
        //    await UpdatePoHeaderByPoReceiveAsync(data.PoTransaction.PoUuid);
        //    await UpdatePoItemsByPoReceiveAsync(data.PoTransaction.PoUuid);

        //    return true;
        //}
        
        public virtual bool UpdateByPoReceive(PoTransactionData data)
        {
            if (data == null || data.PoTransaction == null)
                return false;
            UpdatePoHeaderByPoReceive(data.PoTransaction.PoUuid);
            UpdatePoItemsByPoReceive(data.PoTransaction.PoUuid);
            return true;
        }
        
        private void UpdatePoHeaderByPoReceive(string poUuid)
        {
            var sql = $@"
update poh 
set poh.TotalAmount=(select ISNULL(sum(pot.TotalAmount),0) from PoTransaction pot where poh.PoUuid=pot.PoUuid)
from PoHeader poh 
where poh.PoUuid=@0;
";
            dbFactory.Db.Execute(sql, poUuid.ToSqlParameter("PoUuid"));
        }
        
        private async Task UpdatePoHeaderByPoReceiveAsync(string poUuid)
        {
            var sql = $@"
update poh 
set poh.TotalAmount=(select ISNULL(sum(pot.TotalAmount),0) from PoTransaction pot where poh.PoUuid=pot.PoUuid)
from PoHeader poh 
where poh.PoUuid=@0;
";
            await dbFactory.Db.ExecuteAsync(sql, poUuid.ToSqlParameter("PoUuid"));
        }
        
        private void UpdatePoItemsByPoReceive(string poUuid)
        {
            var sql = $@"
update poi 
set poi.ReceivedQty=(select ISNULL(sum(poti.TransQty),0) from PoTransactionItems poti where poti.PoItemUuid=poi.PoItemUuid)
from PoItems poi 
where poi.PoUuid=@0;
";
            dbFactory.Db.Execute(sql, poUuid.ToSqlParameter("PoUuid"));
        }

        public async Task UpdateReceivedQtyFromPoTransactionItemAsync(string transUuid, bool isReturnBack = false)
        {
            string op = isReturnBack ? "-" : "+";
            string command = $@"
UPDATE poi SET ReceivedQty=poi.ReceivedQty{op}COALESCE(rcv.qty,0)
FROM PoItems poi 
INNER JOIN
    (SELECT SUM(COALESCE(TransQty,0)) as qty, 
        PoItemUuid 
    FROM PoTransactionItems
    WHERE TransUuid='{transUuid}'  
    GROUP BY PoItemUuid
) rcv
ON poi.PoItemUuid=rcv.PoItemUuid
";
            await dbFactory.Db.ExecuteAsync(command.ToString());
        }

        private async Task UpdatePoItemsByPoReceiveAsync(string poUuid)
        {
            var sql = $@"
update poi 
set poi.ReceivedQty=(select ISNULL(sum(poti.TransQty),0) from PoTransactionItems poti where poti.PoItemUuid=poi.PoItemUuid)
from PoItems poi 
where poi.PoUuid=@0;
";
            await dbFactory.Db.ExecuteAsync(sql, poUuid.ToSqlParameter("PoUuid"));
        }
    }
}



