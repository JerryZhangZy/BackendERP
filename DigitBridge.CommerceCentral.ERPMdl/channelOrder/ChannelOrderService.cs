
    
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
            AddValidator(new ChannelOrderServiceValidatorDefault(this, this.dbFactory));
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
        public virtual async Task<bool> AddAsync(ChannelOrderDataDto dto)
        {
            if (dto is null)
                return false;
            // set Add mode and clear data
            Add();

            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        public virtual bool Add(ChannelOrderPayload payload)
        {
            if (payload is null || !payload.HasChannelOrder)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.ChannelOrder))
                return false;

            // load data from dto
            FromDto(payload.ChannelOrder);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(ChannelOrderPayload payload)
        {
            if (payload is null || !payload.HasChannelOrder)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.ChannelOrder).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(payload.ChannelOrder);

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
            if (dto is null || !dto.HasOrderHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.OrderHeader.RowNum.ToLong());

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
            if (dto is null || !dto.HasOrderHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // load data 
            await GetDataAsync(dto.OrderHeader.RowNum.ToLong()).ConfigureAwait(false);

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(ChannelOrderPayload payload)
        {
            if (payload is null || !payload.HasChannelOrder || payload.ChannelOrder.OrderHeader.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.ChannelOrder))
                return false;

            // load data 
            GetData(payload.ChannelOrder.OrderHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.ChannelOrder);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(ChannelOrderPayload payload)
        {
            if (payload is null || !payload.HasChannelOrder)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.ChannelOrder).ConfigureAwait(false)))
                return false;

            // load data 
            await GetDataAsync(payload.ChannelOrder.OrderHeader.RowNum.ToLong()).ConfigureAwait(false);

            // load data from dto
            FromDto(payload.ChannelOrder);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }
     }
}



