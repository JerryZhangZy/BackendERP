
    
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
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class CustomerService
    {

        protected CustomerAddressService _customerAddressService;
        [XmlIgnore, JsonIgnore]
        public CustomerAddressService customerAddressService
        {
            get
            {
                if (_customerAddressService is null)
                    _customerAddressService = new CustomerAddressService(dbFactory);
                return _customerAddressService;
            }
        }


        #region override methods

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override CustomerService Init()
        {
            base.Init();
            SetDtoMapper(new CustomerDataDtoMapperDefault());
            SetCalculator(new CustomerServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new CustomerServiceValidatorDefault(this, this.dbFactory));
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
                if (this.Data?.Customer != null)
                {
                    if (_ProcessMode == ProcessingMode.Add)
                    {
                        await initNumbersService.UpdateMaxNumberAsync(this.Data.Customer.MasterAccountNum, this.Data.Customer.ProfileNum, ActivityLogType.Customer, this.Data.Customer.CustomerCode);
                    }
                }
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
                if (this.Data?.Customer != null)
                {
                    if (_ProcessMode == ProcessingMode.Add)
                    {
                         initNumbersService.UpdateMaxNumber(this.Data.Customer.MasterAccountNum, this.Data.Customer.ProfileNum, ActivityLogType.Invoice, this.Data.Customer.CustomerCode);
                    }
                }
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
                Type = (int)ActivityLogType.Customer,
                Action = (int)this.ProcessMode,
                LogSource = "CustomerService",

                MasterAccountNum = this.Data.Customer.MasterAccountNum,
                ProfileNum = this.Data.Customer.ProfileNum,
                DatabaseNum = this.Data.Customer.DatabaseNum,
                ProcessUuid = this.Data.Customer.CustomerUuid,
                ProcessNumber = this.Data.Customer.CustomerCode,
                ChannelNum = this.Data.Customer.ChannelNum,
                ChannelAccountNum = this.Data.Customer.ChannelAccountNum,

                LogMessage = string.Empty
            };

        #endregion override methods


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

            if (!(await ValidateAsync(dto)))
                return false;

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
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

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.Customer)))
                return false;

            // load data from dto
            FromDto(payload.Customer);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }



        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(CustomerDataDto dto)
        {
            if (dto is null || !dto.HasCustomer)
                return false;

            //set edit mode before validate
            Edit();

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

            //set edit mode before validate
            Edit();

            if (!(await ValidateAsync(dto)))
                return false;

            // set Add mode and clear data
            await EditAsync(dto.Customer.RowNum.ToLong());

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
        public virtual bool Update(CustomerPayload payload)
        {
            if (payload is null || !payload.HasCustomer || payload.Customer.Customer.RowNum.ToLong() <= 0)
                return false;

            //set edit mode before validate
            Edit();

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

            //set edit mode before validate
            Edit();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.Customer)))
                return false;
            // load data 
           //await GetDataAsync(payload.Customer.Customer.RowNum.ToLong());
            // set Add mode and clear data
            await EditAsync(payload.Customer.Customer.RowNum.ToLong());

            // load data from dto
            FromDto(payload.Customer);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        public CustomerPayload GetCustomersByCodeArray(CustomerPayload payload)
        {
            if (!payload.HasCustomerCodes)
                return payload;
            var list = new List<CustomerDataDto>();
            var msglist = new List<MessageClass>();
            var rowNumList = new List<long>();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                rowNumList = CustomerServiceHelper.GetRowNumsByCustomerCodes(payload.CustomerCodes, payload.MasterAccountNum, payload.ProfileNum);
            }
            foreach (var rowNum in rowNumList)
            {
                if (GetData(rowNum))
                    list.Add(ToDto());
            }
            payload.Customers = list;
            payload.Messages = msglist;
            return payload;
        }

        public async Task<CustomerPayload> GetCustomersByCodeArrayAsync(CustomerPayload payload)
        {
            if (!payload.HasCustomerCodes)
                return payload;
            var list = new List<CustomerDataDto>();
            var msglist = new List<MessageClass>();
            var rowNumList = new List<long>();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                rowNumList =await CustomerServiceHelper.GetRowNumsByCustomerCodesAsync(payload.CustomerCodes, payload.MasterAccountNum, payload.ProfileNum);
            }
            foreach (var rowNum in rowNumList)
            {
                if (await GetDataAsync(rowNum))
                    list.Add(ToDto());
            }
            payload.Customers = list;
            payload.Messages = msglist;
            return payload;
        }

        public async Task<bool> GetCustomerByCustomerCodeAsync(CustomerPayload payload,string customerCode)
        {
            if (string.IsNullOrEmpty(customerCode))
                return false;
            List();
            if (!(await ValidateAccountAsync(payload, customerCode)))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum =await CustomerServiceHelper.GetRowNumByCustomerCodeAsync(customerCode,payload.MasterAccountNum,payload.ProfileNum);
            }
            if (rowNum == 0)
            {
                AddError($"Data not found for {customerCode}.");
                return false;
            }

            return await GetDataAsync(rowNum);
        }

        public async Task<bool> DeleteByCodeAsync(CustomerPayload payload, string customerCode)
        {
            if (string.IsNullOrEmpty(customerCode))
                return false;
            Delete();
            if (!(await ValidateAccountAsync(payload, customerCode)))
                return false;
            long rowNum = 0;
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNum = await CustomerServiceHelper.GetRowNumByCustomerCodeAsync(customerCode, payload.MasterAccountNum, payload.ProfileNum);
            }
            var success = await GetDataAsync(rowNum);
            if (success)
            {
                return await DeleteDataAsync();
            }
            return false;
        }

        /// <summary>
        /// Get row num by channel and channel account
        /// </summary>
        public virtual async Task<long> GetRowNumByChannelAsync(int channelNum, int channelAccountNum, int masterAccountNum, int profileNum)
        {
            return (await dbFactory.GetValueAsync<Customer, long?>(
                "SELECT TOP 1 RowNum FROM Customer WHERE MasterAccountNum=@0 AND ProfileNum=@1 AND ChannelNum=@2 AND ChannelAccountNum=@3",
                    masterAccountNum,
                    profileNum,
                    channelNum,
                    channelAccountNum
                )).ToLong();
        }

        /// <summary>
        /// Get CustomerData by channel and channel account
        /// </summary>
        public async Task<bool> GetCustomerByChannelAsync(int channelNum, int channelAccountNum, int masterAccountNum, int profileNum)
        {
            List();
            var rowNum = await GetRowNumByChannelAsync(channelNum, channelAccountNum, masterAccountNum, profileNum);
            if (rowNum == 0)
                return false;
            return await GetDataAsync(rowNum);
        }

        /// <summary>
        /// Get row num by CustomerFindClass 
        /// 1. try find by CustomerUuid
        /// 2. try find by CustomerCode
        /// 3. try find by Phone1 + CustomerName
        /// 4. try find by Email + CustomerName
        /// </summary>
        public virtual async Task<long> GetRowNumByCustomerFindAsync(CustomerFindClass find)
        {
            if (find == null)
                return 0;

            var sql = $@"
                SELECT  
                COALESCE(
                    (SELECT TOP 1 RowNum FROM Customer WHERE CustomerUuid=@4 AND CustomerUuid != ''),
                    (SELECT TOP 1 RowNum FROM Customer WHERE MasterAccountNum=@0 AND ProfileNum=@1 AND CustomerCode=@5 AND CustomerCode!=''),
                    (SELECT TOP 1 RowNum FROM Customer WHERE MasterAccountNum=@0 AND ProfileNum=@1 AND ChannelNum=@2 AND ChannelAccountNum=@3),
                    (SELECT TOP 1 RowNum FROM Customer WHERE MasterAccountNum=@0 AND ProfileNum=@1 AND Phone1=@7 AND CustomerName=@6),
                    (SELECT TOP 1 RowNum FROM Customer WHERE MasterAccountNum=@0 AND ProfileNum=@1 AND Email=@8 AND CustomerName=@6),
                    0
                )
            ";
            return (await dbFactory.GetValueAsync<Customer, long?>(
                    sql,
                    find.MasterAccountNum,      //0
                    find.ProfileNum,            //1
                    find.ChannelNum,            //2
                    find.ChannelAccountNum,     //3
                    find.CustomerUuid,          //4
                    find.CustomerCode,          //5
                    find.CustomerName,          //6
                    find.Phone1,                //7
                    find.Email                  //8
                )).ToLong();
        }

        /// <summary>
        /// Get CustomerData by CustomerFindClass
        /// </summary>
        public async Task<bool> GetCustomerByCustomerFindAsync(CustomerFindClass find)
        {
            if (find == null)
                return false;

            List();
            var rowNum = await GetRowNumByCustomerFindAsync(find);
            if (rowNum == 0)
                return false;
            return await GetDataAsync(rowNum);
        }

        /// <summary>
        /// Get CustomerData by CustomerFindClass
        /// </summary>
        public async Task<bool> AddCustomerAsync(CustomerData data)
        {
            if (data == null)
                return false;

            // set Add mode and clear data
            Add();
            this.AttachData(data);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        public async Task<string> GetNextNumberAsync(int masterAccountNum, int profileNum)
        {
            return await initNumbersService.GetNextNumberAsync(masterAccountNum, profileNum, Base.Common.ActivityLogType.Customer);

        }
        public string GetNextNumber(int masterAccountNum, int profileNum)
        {
            return  initNumbersService.GetNextNumber(masterAccountNum, profileNum, Base.Common.ActivityLogType.Customer);

        }
 
        public async Task<bool> ExistCustomerCodeAsync(string customerCode, int masterAccountNum, int profileNum)
        {
            return await dbFactory.ExistsAsync<Customer>(
                $"WHERE MasterAccountNum = @0 AND ProfileNum = @1 AND CustomerCode = @2",
                masterAccountNum,
                profileNum,
                customerCode);
 
        }
        public bool ExistCustomerCode(string customerCode, int masterAccountNum, int profileNum)
        {
            return dbFactory.Exists<Customer>(
                $"WHERE MasterAccountNum = @0 AND ProfileNum = @1 AND CustomerCode = @2",
                masterAccountNum,
                profileNum,
                customerCode);
        }


        public bool ExistCustomer(string customerUuid, int masterAccountNum, int profileNum)
        {
            return dbFactory.Exists<Customer>(
                $"WHERE MasterAccountNum = @0 AND ProfileNum = @1 AND customerUuid = @2",
                masterAccountNum,
                profileNum,
                customerUuid);
        }
        #region customer Address
        public async Task<bool> AddCustomerAddressAsync(CustomerAddressPayload payload)
        {
            return await customerAddressService.AddAsync(payload);
        }
        public async Task<bool> UpdateCustomerAddressAsync(CustomerAddressPayload payload)
        {
            return await customerAddressService.UpdateAsync(payload);
        }

        public async Task<bool> DeleteCustomerAddressAsync(int masterAccountNum,int profileNum, string customerCode, string addressCode)
        {
            string customerUuid = string.Empty;
            using (var tx = new ScopedTransaction(dbFactory))
            {
               long rowNum = await CustomerServiceHelper.GetRowNumByCustomerCodeAsync(customerCode, masterAccountNum, profileNum);
                if (GetData(rowNum))
                {
                    customerUuid = this.Data.Customer.CustomerUuid;
                }
            }
            return await customerAddressService.DeleteCustomerAddressAsync(customerUuid, addressCode);

        }
      
        public CustomerAddressDataDto ToCustomerAddressDto()
        {

            return customerAddressService.ToDto();
        }
        #endregion customer Address

    }
}



