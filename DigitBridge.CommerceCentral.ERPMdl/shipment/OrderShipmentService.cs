
    

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
    public partial class OrderShipmentService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override OrderShipmentService Init()
        {
            base.Init();
            SetDtoMapper(new OrderShipmentDataDtoMapperDefault());
            SetCalculator(new OrderShipmentServiceCalculatorDefault());
            AddValidator(new OrderShipmentServiceValidatorDefault());
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(OrderShipmentDataDto dto)
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
        public virtual async Task<bool> AddAsync(OrderShipmentDataDto dto)
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
        public virtual bool Update(OrderShipmentDataDto dto)
        {
            if (dto is null || !dto.HasOrderShipmentHeader || dto.OrderShipmentHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(dto.OrderShipmentHeader.RowNum.ToLong());
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
            if (dto is null || !dto.HasOrderShipmentHeader || dto.OrderShipmentHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(dto.OrderShipmentHeader.RowNum.ToLong()).ConfigureAwait(false);
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }


        /// <summary>
        /// Get order shipment with detail by order shipment number
        /// </summary>
        /// <param name="orderShipmentNum"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetByOrderShipmentNumAsync(string orderShipmentNum)
        {
            if (string.IsNullOrEmpty(orderShipmentNum))
                return false;
            List();
            var rowNum = await _data.GetRowNumAsync(orderShipmentNum);
            if (!rowNum.HasValue)
                return false;
            var success = await GetDataAsync(rowNum.Value);
            //if (success) ToDto();
            return success;
        }

        /// <summary>
        /// Delete order shipment by order shipment number
        /// </summary>
        /// <param name="orderShipmentNum"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByOrderShipmentNumAsync(string orderShipmentNum)
        {
            if (string.IsNullOrEmpty(orderShipmentNum))
                return false;
            Delete();
            var rowNum = await _data.GetRowNumAsync(orderShipmentNum);
            if (!rowNum.HasValue)
                return false;
            var success = await GetDataAsync(rowNum.Value);
            success = success && await DeleteDataAsync();
            return success;
        }
    }
}



