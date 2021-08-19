
    
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
    public partial class DCAssignmentService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override DCAssignmentService Init()
        {
            base.Init();
            SetDtoMapper(new DCAssignmentDataDtoMapperDefault());
            SetCalculator(new DCAssignmentServiceCalculatorDefault());
            AddValidator(new DCAssignmentServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(DCAssignmentDataDto dto)
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
        public virtual async Task<bool> AddAsync(DCAssignmentDataDto dto)
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

        public virtual bool Add(DCAssignmentPayload payload)
        {
            if (payload is null || !payload.HasDCAssignment)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.DCAssignment))
                return false;

            // load data from dto
            FromDto(payload.DCAssignment);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(DCAssignmentPayload payload)
        {
            if (payload is null || !payload.HasDCAssignment)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.DCAssignment).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(payload.DCAssignment);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(DCAssignmentDataDto dto)
        {
            if (dto is null || !dto.HasOrderDCAssignmentHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.OrderDCAssignmentHeader.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(DCAssignmentDataDto dto)
        {
            if (dto is null || !dto.HasOrderDCAssignmentHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // load data 
            await GetDataAsync(dto.OrderDCAssignmentHeader.RowNum.ToLong()).ConfigureAwait(false);

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
        public virtual bool Update(DCAssignmentPayload payload)
        {
            if (payload is null || !payload.HasDCAssignment || payload.DCAssignment.OrderDCAssignmentHeader.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.DCAssignment))
                return false;

            // load data 
            GetData(payload.DCAssignment.OrderDCAssignmentHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.DCAssignment);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(DCAssignmentPayload payload)
        {
            if (payload is null || !payload.HasDCAssignment)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.DCAssignment).ConfigureAwait(false)))
                return false;

            // load data 
            await GetDataAsync(payload.DCAssignment.OrderDCAssignmentHeader.RowNum.ToLong()).ConfigureAwait(false);

            // load data from dto
            FromDto(payload.DCAssignment);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }


        /// <summary>
        /// Get DCAssignmentData list by centralOrderUuid
        /// </summary>
        public virtual async Task<IList<DCAssignmentData>> GetByCentralOrderUuidAsync(string centralOrderUuid)
        {
            if (string.IsNullOrEmpty(centralOrderUuid))
                return null;

            List();
            var orderDCAssignmentNums = await DCAssignmentHelper.GetRowNumByCentralOrderUuidAsync(centralOrderUuid);
            if (orderDCAssignmentNums is null || orderDCAssignmentNums.Count <= 0)
                return null;

            var result = new List<DCAssignmentData>();
            foreach (var orderDCAssignmentNum in orderDCAssignmentNums)
            {
                if (!(await this.GetDataAsync(orderDCAssignmentNum)))
                    continue;
                result.Add(this.Data);
                this.DetachData(this.Data);
            }
            return result;
        }

        /// <summary>
        /// Get DCAssignmentData list by centralOrderUuid
        /// </summary>
        public virtual IList<DCAssignmentData> GetByCentralOrderUuid(string centralOrderUuid)
        {
            if (string.IsNullOrEmpty(centralOrderUuid))
                return null;

            List();
            var orderDCAssignmentNums = DCAssignmentHelper.GetRowNumByCentralOrderUuid(centralOrderUuid);
            if (orderDCAssignmentNums is null || orderDCAssignmentNums.Count <= 0)
                return null;

            var result = new List<DCAssignmentData>();
            foreach (var orderDCAssignmentNum in orderDCAssignmentNums)
            {
                if (!this.GetData(orderDCAssignmentNum))
                    continue;
                result.Add(this.Data);
                this.DetachData(this.Data);
            }
            return result;
        }


    }
}



