
    
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
    public partial class CustomerService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override CustomerService Init()
        {
            base.Init();
            SetDtoMapper(new CustomerDataDtoMapperDefault());
            SetCalculator(new CustomerServiceCalculatorDefault());
            AddValidator(new CustomerServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(CustomerDataDto dto)
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
        public virtual async Task<bool> AddAsync(CustomerDataDto dto)
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

        public virtual bool Add(CustomerPayload payload)
        {
            if (payload is null || !payload.HasCustomer)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Customer))
                return false;

            // load data from dto
            FromDto(payload.Customer);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(CustomerPayload payload)
        {
            if (payload is null || !payload.HasCustomer)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.Customer).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(payload.Customer);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(CustomerDataDto dto)
        {
            if (dto is null || !dto.HasCustomer)
                return false;

            if (!Validate(dto))
                return false;

            // set Add mode and clear data
            Edit(dto.Customer.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(CustomerDataDto dto)
        {
            if (dto is null || !dto.HasCustomer)
                return false;

            if (!(await ValidateAsync(dto).ConfigureAwait(false)))
                return false;

            // set Add mode and clear data
            await EditAsync(dto.Customer.RowNum.ToLong()).ConfigureAwait(false);

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
        public virtual bool Update(CustomerPayload payload)
        {
            if (payload is null || !payload.HasCustomer || payload.Customer.Customer.RowNum.ToLong() <= 0)
                return false;


            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Customer))
                return false;

            // set Add mode and clear data
            Edit(payload.Customer.Customer.RowNum.ToLong());

            // load data from dto
            FromDto(payload.Customer);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(CustomerPayload payload)
        {
            if (payload is null || !payload.HasCustomer)
                return false;

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            if (!(await ValidateAsync(payload.Customer).ConfigureAwait(false)))
                return false;

            // set Add mode and clear data
            await EditAsync(payload.Customer.Customer.RowNum.ToLong()).ConfigureAwait(false);

            // load data from dto
            FromDto(payload.Customer);

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }
        public string GetCustomerUuidByCode(int profileNum, string customerCode)
        {
            return dbFactory.Db.FirstOrDefault<string>($"select CustomerUuid from Customer where CustomerCode='{customerCode}' and ProfileNum={profileNum}");
        }
        public async Task<bool> DeleteByCodeAsync(CustomerPayload payload)
        {
            var uuid = GetCustomerUuidByCode(payload.ProfileNum, payload.CustomerCodes.First());

            if (string.IsNullOrEmpty(uuid))
            {
                AddError($"Customer not found.");
                return false;
            }

            Delete();
            var success = await GetDataByIdAsync(uuid);

            // validate data for Add processing
            //TODO merge
            //if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
            //    return false;

            success = success && await DeleteDataAsync();
            return success;
        }
        public List<string> GetCustomerUuidsByCodeArray(int profileNum, IList<string> cutomerCodes)
        {
            var customersWhere = string.Join(",", cutomerCodes.Select(x => $"'{x}'").ToArray());
            return dbFactory.Db.Query<string>($"select CustomerUuid from Customer where CustomerCode in ({customersWhere}) and ProfileNum={profileNum}").ToList();
        }
        public CustomerPayload GetCustomersByCodeArray(CustomerPayload payload)
        {
            var uuids = GetCustomerUuidsByCodeArray(payload.ProfileNum, payload.CustomerCodes);
            var list = new List<CustomerDataDto>();
            uuids.ForEach(x =>
            {
                if (GetDataById(x))
                    list.Add(ToDto());
            });
            payload.Customers = list;
            return payload;
        }
    }
}



