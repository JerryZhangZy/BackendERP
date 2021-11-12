
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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class MiscInvoiceService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override MiscInvoiceService Init()
        {
            base.Init();
            SetDtoMapper(new MiscInvoiceDataDtoMapperDefault());
            SetCalculator(new MiscInvoiceServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new MiscInvoiceServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(MiscInvoiceDataDto dto)
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

            var rtn = SaveData();
            if (rtn) AddActivityLogForCurrentData();
            return rtn;
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(MiscInvoiceDataDto dto)
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

            var rtn = await SaveDataAsync();
            if (rtn) AddActivityLogForCurrentData();
            return rtn;
        }

        public virtual bool Add(MiscInvoicePayload payload)
        {
            if (payload is null || !payload.HasMiscInvoice)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.MiscInvoice))
                return false;

            // load data from dto
            FromDto(payload.MiscInvoice);

            // validate data for Add processing
            if (!Validate())
                return false;

            var rtn = SaveData();
            if (rtn) AddActivityLogForCurrentData();
            return rtn;
        }

        public virtual async Task<bool> AddAsync(MiscInvoicePayload payload)
        {
            if (payload is null || !payload.HasMiscInvoice)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.MiscInvoice)))
                return false;

            // load data from dto
            FromDto(payload.MiscInvoice);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            var rtn = await SaveDataAsync();
            if (rtn) AddActivityLogForCurrentData();
            return rtn;
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(MiscInvoiceDataDto dto)
        {
            if (dto is null || !dto.HasMiscInvoiceHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.MiscInvoiceHeader.RowNum.ToLong());

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!Validate())
                return false;

            var rtn = SaveData();
            if (rtn) AddActivityLogForCurrentData();
            return rtn;
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(MiscInvoiceDataDto dto)
        {
            if (dto is null || !dto.HasMiscInvoiceHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.MiscInvoiceHeader.RowNum.ToLong());

            // load data from dto
            FromDto(dto);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            var rtn = await SaveDataAsync();
            if (rtn) AddActivityLogForCurrentData();
            return rtn;
        }

        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(MiscInvoicePayload payload)
        {
            if (payload is null || !payload.HasMiscInvoice || payload.MiscInvoice.MiscInvoiceHeader.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.MiscInvoice))
                return false;

            // load data 
            GetData(payload.MiscInvoice.MiscInvoiceHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.MiscInvoice);

            // validate data for Add processing
            if (!Validate())
                return false;

            var rtn = SaveData();
            if (rtn) AddActivityLogForCurrentData();
            return rtn;
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(MiscInvoicePayload payload)
        {
            if (payload is null || !payload.HasMiscInvoice)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.MiscInvoice)))
                return false;

            // load data 
            await GetDataAsync(payload.MiscInvoice.MiscInvoiceHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.MiscInvoice);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            var rtn = await SaveDataAsync ();
            if (rtn) AddActivityLogForCurrentData();
            return rtn;
        }

        /// <summary>
        ///  get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetDataAsync(MiscInvoicePayload payload, string orderNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetData(MiscInvoicePayload payload, string orderNumber)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(MiscInvoicePayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
            success = success && DeleteData();
            if (success)
                AddActivityLogForCurrentData();
            return success;
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(MiscInvoicePayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
            success = success && DeleteData();
            if (success)
                AddActivityLogForCurrentData();
            return success;
        }


        /// <summary>
        /// Add MiscInvoice from salesOrder
        /// </summary>
        public virtual async Task<bool> AddFromSalesOrderAsync(SalesOrderHeader salesOrder)
        {
            Add();

            Data.MiscInvoiceHeader = new MiscInvoiceHeader()
            {
                MasterAccountNum = salesOrder.MasterAccountNum,
                ProfileNum = salesOrder.ProfileNum,
                DatabaseNum = salesOrder.DatabaseNum,

                Balance = salesOrder.DepositAmount,
                TotalAmount = salesOrder.DepositAmount,
                Currency = salesOrder.Currency,
                CreditAmount = salesOrder.DepositAmount,
                PaidBy = (int)PaidByAr.CreditMemo,

                CustomerCode = salesOrder.CustomerCode,
                CustomerName = salesOrder.CustomerName,
                CustomerUuid = salesOrder.CustomerUuid,

                EnterBy = salesOrder.EnterBy,
                InvoiceSourceCode = $"SalesOrderUuid:{salesOrder.SalesOrderUuid}",

                MiscInvoiceNumber = NumberGenerate.Generate(),
                MiscInvoiceUuid = Guid.NewGuid().ToString(),
                MiscInvoiceDate = DateTime.Now,
                MiscInvoiceTime = DateTime.Now.TimeOfDay,

                //MiscInvoiceStatus
                //MiscInvoiceType
                //CheckNum 
            };
            var rtn = await SaveDataAsync();
            if (rtn) AddActivityLogForCurrentData();
            return rtn;
        }

        ///// <summary>
        ///// Withdraw money from misc invoice(this method is for internal,no validate for uuid)
        ///// </summary>
        //public virtual async Task<bool> WithdrawAsync(string miscInvoiceUuid, decimal amount)
        //{
        //    Edit();

        //    if (!await GetDataByIdAsync(miscInvoiceUuid))
        //    {
        //        AddError($"Data not found for miscInvoiceUuid:{miscInvoiceUuid}");
        //        return false;
        //    }
        //    Data.MiscInvoiceHeader.Balance = Data.MiscInvoiceHeader.Balance - amount;

        //    if (!await SaveDataAsync())
        //    {
        //        AddError("WithdrawAsync->SaveDataAsync error.");
        //        return false;
        //    }

        //    return true;
        //}
        public virtual async Task GetListByMiscInvoiceNumbersAsync(MiscInvoicePayload payload)
        {
            if (payload is null || !payload.HasMiscInvoiceNumbers)
            {
                AddError("InvoiceNumbers is required.");
                payload.Messages = this.Messages;
                payload.Success = false;
            }
            //var rowNums = await new InvoiceList(dbFactory).GetRowNumListAsync(payload.InvoiceNumbers, payload.MasterAccountNum, payload.ProfileNum);

            var result = new List<MiscInvoiceDataDto>();
            foreach (var miscInvoiceNumber in payload.MiscInvoiceNumbers)
            {
                if (!(await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, miscInvoiceNumber)))
                    continue;
                result.Add(this.ToDto());
                this.DetachData(this.Data);
            }
            payload.MiscInvoices = result;
        }

        public virtual async Task<bool> CheckNumberExistAsync(int masterAccountNum, int profileNum, string miscInvoiceNumber)
        {
            NewData();
            if (string.IsNullOrEmpty(miscInvoiceNumber))
            {
                AddError($"Number is required.");
                return false;
            }
            var success = _data.GetByNumber(masterAccountNum, profileNum, miscInvoiceNumber);
            return success;
        }

        public async Task PayAsync(string uuid, decimal payAmount)
        {
            var sql = $"UPDATE MiscInvoiceHeader SET PaidAmount=PaidAmount+({payAmount}), Balance=Balance-({payAmount}) WHERE MiscInvoiceUuid='{uuid}'";
            await dbFactory.Db.ExecuteAsync(sql);
            this.AddActivityLog(new ActivityLog(dbFactory)
            {
                Type = ActivityLogType.MiscInvoice.ToInt(),
                Action = 1,
                LogSource = "MiscInvoiceService",

                //MasterAccountNum = this.Data.MiscInvoiceHeader.MasterAccountNum,
                //ProfileNum = this.Data.MiscInvoiceHeader.ProfileNum,
                //DatabaseNum = this.Data.MiscInvoiceHeader.DatabaseNum,
                ProcessUuid = uuid,
                //ProcessNumber = this.Data.MiscInvoiceHeader.MiscInvoiceNumber,
                //ChannelNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,
                //ChannelAccountNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,

                LogMessage = $"Update MiscInvoiceHeader PaidAmount and Balance with payAmount {payAmount}"
            });
        }
        public void Pay(string uuid, decimal payAmount)
        {
            var sql = $"UPDATE MiscInvoiceHeader SET PaidAmount=PaidAmount+({payAmount}), Balance=Balance-({payAmount}) WHERE MiscInvoiceUuid='{uuid}'";
            dbFactory.Db.Execute(sql);
            this.AddActivityLog(new ActivityLog(dbFactory)
            {
                Type = ActivityLogType.MiscInvoice.ToInt(),
                Action = 1,
                LogSource = "MiscInvoiceService",

                //MasterAccountNum = this.Data.MiscInvoiceHeader.MasterAccountNum,
                //ProfileNum = this.Data.MiscInvoiceHeader.ProfileNum,
                //DatabaseNum = this.Data.MiscInvoiceHeader.DatabaseNum,
                ProcessUuid = uuid,
                //ProcessNumber = this.Data.MiscInvoiceHeader.MiscInvoiceNumber,
                //ChannelNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,
                //ChannelAccountNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,

                LogMessage = $"Update MiscInvoiceHeader PaidAmount and Balance with payAmount {payAmount}"
            });
        }
        protected void AddActivityLogForCurrentData()
        {
            this.AddActivityLog(new ActivityLog(dbFactory)
            {
                Type = ActivityLogType.MiscInvoice.ToInt(),
                Action = this.ProcessMode.ToInt(),
                LogSource = "MiscInvoiceService",

                MasterAccountNum = this.Data.MiscInvoiceHeader.MasterAccountNum,
                ProfileNum = this.Data.MiscInvoiceHeader.ProfileNum,
                DatabaseNum = this.Data.MiscInvoiceHeader.DatabaseNum,
                ProcessUuid = this.Data.MiscInvoiceHeader.MiscInvoiceUuid,
                ProcessNumber = this.Data.MiscInvoiceHeader.MiscInvoiceNumber,
                //ChannelNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,
                //ChannelAccountNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty
            });
        }
    }
}



