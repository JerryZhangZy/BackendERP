

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
    public partial class SalesOrderService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override SalesOrderService Init()
        {
            base.Init();
            SetDtoMapper(new SalesOrderDataDtoMapperDefault());
            SetCalculator(new SalesOrderServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new SalesOrderServiceValidatorDefault(this, this.dbFactory));

            _data.OnBeforeSave(order => {
                var inventoryService = new InventoryService(this.dbFactory);
                inventoryService.UpdateOpenSoQtyFromSalesOrderItem(order.SalesOrderHeader.SalesOrderUuid, true);
                return true;
            });
            _data.OnAfterSave(order => {
                var inventoryService = new InventoryService(this.dbFactory);
                inventoryService.UpdateOpenSoQtyFromSalesOrderItem(order.SalesOrderHeader.SalesOrderUuid, false);
                return true;
            });

            return this;
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

            return await SaveDataAsync();
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
            success = success && DeleteData();
            return success;
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
            success = success && DeleteData();
            return success;
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
    }
}



