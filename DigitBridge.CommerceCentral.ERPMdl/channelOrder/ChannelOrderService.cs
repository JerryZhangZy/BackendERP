
    

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
    public partial class ChannelOrderService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override ChannelOrderService Init()
        {
            base.Init();
            SetDtoMapper(new ChannelOrderDataDtoMapperDefault());
            SetCalculator(new ChannelOrderServiceCalculatorDefault());
            AddValidator(new ChannelOrderServiceValidatorDefault(this));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(ChannelOrderDataDto dto)
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
        public virtual async Task<bool> AddAsync(ChannelOrderDataDto dto)
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
        public virtual bool Update(ChannelOrderDataDto dto)
        {
            if (dto is null || !dto.HasOrderHeader || dto.OrderHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(dto.OrderHeader.RowNum.ToLong());
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
        public virtual async Task<bool> UpdateAsync(ChannelOrderDataDto dto)
        {
            if (dto is null || !dto.HasOrderHeader || dto.OrderHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(dto.OrderHeader.RowNum.ToLong()).ConfigureAwait(false);
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

    }
}



