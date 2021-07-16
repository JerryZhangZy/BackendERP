


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
            AddValidator(new SalesOrderServiceValidatorDefault());
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
            var success = await GetDataAsync(rowNum);
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
            var success = GetData(rowNum);
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
            var success = GetData(rowNum);
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
            var success = await GetDataAsync(rowNum);
            success = success && await DeleteDataAsync();
            return success;
        }
    }
}



