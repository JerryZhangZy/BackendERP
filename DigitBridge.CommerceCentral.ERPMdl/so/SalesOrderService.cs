


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
    public partial class SalesOrderService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override SalesOrderService Init()
        {
            base.Init();
            SetDtoMapper(new SalesOrderDataDtoMapperDefault());
            SetCalculator(new SalesOrderServiceCalculatorDefault());
            AddValidator(new SalesOrderServiceValidatorDefault(this));
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
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        public virtual bool Add(SalesOrderPayload payload)
        {
            if (payload is null || !payload.HasSalesOrder)
                return false;

            // set Add mode and clear data
            Add();
            // load data from dto
            FromDto(payload.SalesOrder);

            if (!ValidatePayload(payload))
                return false;

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
            // load data from dto
            FromDto(payload.SalesOrder);

            if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
                return false;

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }


        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(SalesOrderDataDto dto)
        {
            if (dto is null || !dto.HasSalesOrderHeader || dto.SalesOrderHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(dto.SalesOrderHeader.RowNum.ToLong());
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
            if (dto is null || !dto.HasSalesOrderHeader || dto.SalesOrderHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(dto.SalesOrderHeader.RowNum.ToLong()).ConfigureAwait(false);
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(SalesOrderPayload payload)
        {
            if (payload is null || !payload.HasSalesOrder || payload.SalesOrder.SalesOrderHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(payload.SalesOrder.SalesOrderHeader.RowNum.ToLong());

            if (!ValidatePayload(payload))
                return false;

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
            if (payload is null || !payload.HasSalesOrder || payload.SalesOrder.SalesOrderHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(payload.SalesOrder.SalesOrderHeader.RowNum.ToLong()).ConfigureAwait(false);

            // validate data for Add processing
            if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(payload.SalesOrder);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }


        /// <summary>
        /// Get sale order with detail by orderNumber
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetByOrderNumberAsync(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            List();
            var rowNum = await _data.GetRowNumAsync(orderNumber);
            if (!rowNum.HasValue)
                return false;
            var success = await GetDataAsync(rowNum.Value);
            //if (success) ToDto();
            return success;
        }
        /// <summary>
        /// Get sale order with detail by orderNumber
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetByOrderNumber(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            List();
            var rowNum = _data.GetRowNum(orderNumber);
            if (!rowNum.HasValue)
                return false;
            var success = GetData(rowNum.Value);
            //if (success) ToDto();
            return success;
        }

        /// <summary>
        /// Get sale order with detail by orderNumber
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetByOrderNumberAsync(SalesOrderPayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            List();
            var rowNum = await _data.GetRowNumAsync(orderNumber);
            if (!rowNum.HasValue)
                return false;
            var success = await GetDataAsync(rowNum.Value);

            // validate data for Add processing
            if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
                return false;

            //if (success) ToDto();
            return success;
        }
        /// <summary>
        /// Get sale order with detail by orderNumber
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetByOrderNumber(SalesOrderPayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            List();
            var rowNum = _data.GetRowNum(orderNumber);
            if (!rowNum.HasValue)
                return false;
            var success = GetData(rowNum.Value);

            // validate data for Add processing
            if (!ValidatePayload(payload))
                return false;

            //if (success) ToDto();
            return success;
        }


        /// <summary>
        /// Delete salesorder by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByOrderNumber(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            Delete();
            var rowNum = _data.GetRowNum(orderNumber);
            if (!rowNum.HasValue)
                return false;
            var success = GetData(rowNum.Value);
            success = success && DeleteData();
            return success;
        }
        /// <summary>
        /// Delete salesorder by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByOrderNumberAsync(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            Delete();
            var rowNum = await _data.GetRowNumAsync(orderNumber);
            if (!rowNum.HasValue)
                return false;
            var success = await GetDataAsync(rowNum.Value);
            success = success && await DeleteDataAsync();
            return success;
        }

        /// <summary>
        /// Delete salesorder by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByOrderNumber(SalesOrderPayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            Delete();
            var rowNum = _data.GetRowNum(orderNumber);
            if (!rowNum.HasValue)
                return false;
            var success = GetData(rowNum.Value);

            // validate data for Add processing
            if (!ValidatePayload(payload))
                return false;

            success = success && DeleteData();
            return success;
        }
        /// <summary>
        /// Delete salesorder by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByOrderNumberAsync(SalesOrderPayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            Delete();
            var rowNum = await _data.GetRowNumAsync(orderNumber);
            if (!rowNum.HasValue)
                return false;
            var success = await GetDataAsync(rowNum.Value);

            // validate data for Add processing
            if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
                return false;

            success = success && await DeleteDataAsync();
            return success;
        }


        /// <summary>
        /// Get sale order list by Uuid list
        /// </summary>
        public virtual async Task<SalesOrderPayload> GetListBySalesOrderUuidsNumberAsync(SalesOrderPayload salesOrderPayload)
        {
            if (salesOrderPayload is null || !salesOrderPayload.HasSalesOrderUuids)
                return null;
            var salesOrderUuids = salesOrderPayload.SalesOrderUuids;

            List();
            var result = new List<SalesOrderDataDto>();
            foreach (var id in salesOrderUuids)
            {
                if (!(await this.GetDataByIdAsync(id)))
                    continue;
                result.Add(this.ToDto());
                this.DetachData(this.Data);
            }
            salesOrderPayload.SalesOrders = result;
            return salesOrderPayload;
        }

    }
}



