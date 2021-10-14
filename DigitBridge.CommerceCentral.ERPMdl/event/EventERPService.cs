    
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
using DigitBridge.CommerceCentral.AzureStorage;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class EventERPService
    {
        protected string StorageAccount;

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override EventERPService Init()
        {
            base.Init();
            SetDtoMapper(new EventERPDataDtoMapperDefault());
            SetCalculator(new EventERPServiceCalculatorDefault(this,this.dbFactory));
            AddValidator(new EventERPServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(EventERPDataDto dto)
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

            if (SaveData())
            {
                var erpdata = Data.Event_ERP;
                var message = new ERPQueueMessage
                {
                    ERPEventType = (ErpEventType)erpdata.ERPEventType,
                    DatabaseNum = erpdata.DatabaseNum,
                    MasterAccountNum = erpdata.MasterAccountNum,
                    ProfileNum = erpdata.ProfileNum,
                    ProcessUuid = erpdata.ProcessUuid,
                    ProcessData = erpdata.ProcessData,
                    ProcessSource = erpdata.ProcessSource,
                    EventUuid = erpdata.EventUuid,
                };
                var queueName = message.ERPEventType.GetErpEventQueueName();
                QueueUniversal<ERPQueueMessage>.SendMessage(queueName, StorageAccount, message);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(EventERPDataDto dto)
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

            if( await SaveDataAsync())
            {
                var erpdata = Data.Event_ERP;
                var message = new ERPQueueMessage
                {
                    ERPEventType = (ErpEventType)erpdata.ERPEventType,
                    DatabaseNum = erpdata.DatabaseNum,
                    MasterAccountNum = erpdata.MasterAccountNum,
                    ProfileNum = erpdata.ProfileNum,
                    ProcessUuid = erpdata.ProcessUuid,
                    ProcessData = erpdata.ProcessData,
                    ProcessSource = erpdata.ProcessSource,
                    EventUuid = erpdata.EventUuid,
                };
                var queueName = message.ERPEventType.GetErpEventQueueName();
                await QueueUniversal<ERPQueueMessage>.SendMessageAsync(queueName, StorageAccount, message);
                return true;
            }
            return false;
        }

        public virtual bool Add(EventERPPayload payload)
        {
            if (payload is null || !payload.HasEventERP)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.EventERP))
                return false;

            // load data from dto
            FromDto(payload.EventERP);

            // validate data for Add processing
            if (!Validate())
                return false;

            if (SaveData())
            {
                var erpdata = Data.Event_ERP;
                var message = new ERPQueueMessage
                {
                    ERPEventType = (ErpEventType)erpdata.ERPEventType,
                    DatabaseNum = erpdata.DatabaseNum,
                    MasterAccountNum = erpdata.MasterAccountNum,
                    ProfileNum = erpdata.ProfileNum,
                    ProcessUuid = erpdata.ProcessUuid,
                    ProcessData = erpdata.ProcessData,
                    ProcessSource = erpdata.ProcessSource,
                    EventUuid = erpdata.EventUuid,
                };
                var queueName = message.ERPEventType.GetErpEventQueueName();
                QueueUniversal<ERPQueueMessage>.SendMessage(queueName, StorageAccount, message);
                return true;
            }
            return false;
        }

        public virtual async Task<bool> AddAsync(EventERPPayload payload)
        {
            if (payload is null || !payload.HasEventERP)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.EventERP)))
                return false;

            // load data from dto
            FromDto(payload.EventERP);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            if (await SaveDataAsync())
            {
                var erpdata = Data.Event_ERP;
                var message = new ERPQueueMessage
                {
                    ERPEventType = (ErpEventType)erpdata.ERPEventType,
                    DatabaseNum = erpdata.DatabaseNum,
                    MasterAccountNum = erpdata.MasterAccountNum,
                    ProfileNum = erpdata.ProfileNum,
                    ProcessUuid = erpdata.ProcessUuid,
                    ProcessData = erpdata.ProcessData,
                    ProcessSource = erpdata.ProcessSource,
                    EventUuid = erpdata.EventUuid,
                };
                var queueName = message.ERPEventType.GetErpEventQueueName();
                await QueueUniversal<ERPQueueMessage>.SendMessageAsync(queueName, StorageAccount, message);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(EventERPDataDto dto)
        {
            if (dto is null || !dto.HasEvent_ERP)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.Event_ERP.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(EventERPDataDto dto)
        {
            if (dto is null || !dto.HasEvent_ERP)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.Event_ERP.RowNum.ToLong());

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
        public virtual bool Update(EventERPPayload payload)
        {
            if (payload is null || !payload.HasEventERP || payload.EventERP.Event_ERP.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.EventERP))
                return false;

            // load data 
            GetData(payload.EventERP.Event_ERP.RowNum.ToLong());

            // load data from dto
            FromDto(payload.EventERP);

            // validate data for Add processing
            if (!Validate())
                return false;

            if (Data.Event_ERP.ActionStatus == 0)
                return _data.Delete();
            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(EventERPPayload payload)
        {
            if (payload is null || !payload.HasEventERP)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.EventERP)))
                return false;

            // load data 
            await GetDataAsync(payload.EventERP.Event_ERP.RowNum.ToLong());

            // load data from dto
            FromDto(payload.EventERP);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;
            if (Data.Event_ERP.ActionStatus == 0)
                return await _data.DeleteAsync();
            return await SaveDataAsync();
        }

        /// <summary>
        ///  get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetDataAsync(EventERPPayload payload, string orderNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetData(EventERPPayload payload, string orderNumber)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(EventERPPayload payload, string orderNumber)
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
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(EventERPPayload payload, string orderNumber)
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

    }
}



