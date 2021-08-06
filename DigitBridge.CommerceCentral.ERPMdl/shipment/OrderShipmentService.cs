


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
            AddValidator(new OrderShipmentServiceValidatorDefault(this));
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

        public virtual async Task<bool> AddAsync(OrderShipmentPayload payload)
        {
            if (payload is null || !payload.HasOrderShipment)
                return false;

            // set Add mode and clear data
            Add();
            // load data from dto
            FromDto(payload.OrderShipment);

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
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(OrderShipmentPayload payload)
        {
            if (payload is null || !payload.HasOrderShipment || payload.OrderShipment.OrderShipmentHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(payload.OrderShipment.OrderShipmentHeader.RowNum.ToLong()).ConfigureAwait(false);

            // validate data for Add processing
            if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(payload.OrderShipment);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        } 

        /// <summary>
        /// Delete order shipment by order shipment number
        /// </summary>
        /// <param name="orderShipmentUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByOrderShipmentUuidAsync(string orderShipmentUuid, OrderShipmentPayload payload)
        {
            if (string.IsNullOrEmpty(orderShipmentUuid))
                return false;
            Delete(); 
            var success = await GetDataByIdAsync(orderShipmentUuid);
            // validate before deleting
            if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
                return false;
            success = success && await DeleteDataAsync();
            return success;
        }
        public virtual async Task<bool> GetDataAsync(long orderShipmentNum, OrderShipmentPayload payload)
        {
            if (orderShipmentNum < 0)
                return false;
            List();
            //var orderShipmentNum_db = await _data.GetOrderShipmentNumAsync(orderShipmentNum, payload.ProfileNum, payload.MasterAccountNum);
            //if (!orderShipmentNum_db.HasValue)
            //    return false;
            var success = await GetDataAsync(orderShipmentNum);

            // validate data for Add processing
            if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
                return false;
            //if (success) ToDto();
            return success;
        }
    }
}



