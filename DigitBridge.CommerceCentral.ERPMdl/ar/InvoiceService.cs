

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
using Newtonsoft.Json;
using DigitBridge.CommerceCentral.ERPApiSDK;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InvoiceService
    {
        #region Service Property

        private CustomerService _customerService;
        public CustomerService customerService
        {
            get
            {
                if (_customerService is null)
                    _customerService = new CustomerService(dbFactory);
                return _customerService;
            }
        }

        private InventoryService _inventoryService;
        public InventoryService inventoryService
        {
            get
            {
                if (_inventoryService is null)
                    _inventoryService = new InventoryService(dbFactory);
                return _inventoryService;
            }
        }

        #endregion

        #region override methods
        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InvoiceService Init()
        {
            base.Init();
            SetDtoMapper(new InvoiceDataDtoMapperDefault());
            SetCalculator(new InvoiceServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new InvoiceServiceValidatorDefault(this, this.dbFactory));
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
                if (this.Data?.InvoiceHeader != null)
                {
                    if (_ProcessMode == ProcessingMode.Add)
                    {
                        await initNumbersService.UpdateMaxNumberAsync(this.Data.InvoiceHeader.MasterAccountNum, this.Data.InvoiceHeader.ProfileNum, ActivityLogType.Invoice, this.Data.InvoiceHeader.InvoiceNumber);
                    }
                }
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
                Type = (int)ActivityLogType.Invoice,
                Action = (int)this.ProcessMode,
                LogSource = "InvoiceService",

                MasterAccountNum = this.Data.InvoiceHeader.MasterAccountNum,
                ProfileNum = this.Data.InvoiceHeader.ProfileNum,
                DatabaseNum = this.Data.InvoiceHeader.DatabaseNum,
                ProcessUuid = this.Data.InvoiceHeader.InvoiceUuid,
                ProcessNumber = this.Data.InvoiceHeader.InvoiceNumber,
                ChannelNum = this.Data.InvoiceHeaderInfo.ChannelNum,
                ChannelAccountNum = this.Data.InvoiceHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty
            };

        #endregion override methods

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InvoiceDataDto dto)
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
        public virtual async Task<bool> AddAsync(InvoiceDataDto dto)
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

        public virtual bool Add(InvoicePayload payload)
        {
            if (payload is null || !payload.HasInvoice)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Invoice))
                return false;

            // load data from dto
            FromDto(payload.Invoice);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(InvoicePayload payload)
        {
            if (payload is null || !payload.HasInvoice)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.Invoice)))
                return false;

            // load data from dto
            FromDto(payload.Invoice);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InvoiceDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.InvoiceHeader.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(InvoiceDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.InvoiceHeader.RowNum.ToLong());

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
        public virtual bool Update(InvoicePayload payload)
        {
            if (payload is null || !payload.HasInvoice || payload.Invoice.InvoiceHeader.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.Invoice))
                return false;

            // load data 
            GetData(payload.Invoice.InvoiceHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.Invoice);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InvoicePayload payload)
        {
            if (payload is null || !payload.HasInvoice)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.Invoice)))
                return false;

            // load data 
            await GetDataAsync(payload.Invoice.InvoiceHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.Invoice);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Get multi sale order with detail by InvoiceNumbers
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public virtual async Task GetListByInvoiceNumbersAsync(InvoicePayload payload)
        {
            if (payload is null || !payload.HasInvoiceNumbers)
            {
                AddError("InvoiceNumbers is required.");
                payload.Messages = this.Messages;
                payload.Success = false;
            }
            //var rowNums = await new InvoiceList(dbFactory).GetRowNumListAsync(payload.InvoiceNumbers, payload.MasterAccountNum, payload.ProfileNum);

            var result = new List<InvoiceDataDto>();
            foreach (var invoiceNumber in payload.InvoiceNumbers)
            {
                if (!(await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber)))
                    continue;
                result.Add(this.ToDto());
                this.DetachData(this.Data);
            }

            if (!result.Any())
            {
                payload.ReturnError("No data be found");
            }
            payload.Invoices = result;
        }
        /// <summary>
        ///  get InvoiceHeader data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetByNumberAsync(PayloadBase payload, string invoiceNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber);
        }
        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual bool GetByNumber(InvoicePayload payload, string invoiceNumber)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber);
        }

        /// <summary>
        /// get full InvoiceData by number
        /// </summary>
        public virtual async Task<bool> GetDataByNumberAsync(int masterAccountNum, int profileNum, string number)
        {
            if (ProcessMode == ProcessingMode.Add)
                return false;
            if (Data is null)
                NewData();

            var rowNum = await Data.GetRowNumAsync(number, profileNum, masterAccountNum);

            var success = await this.GetDataAsync(rowNum.ToLong());
            if (!success)
                AddError($"Data not found for number : {number}");
            return success;
        }

        /// <summary>
        /// get full InvoiceData by number
        /// </summary>
        public virtual bool GetDataByNumber(int masterAccountNum, int profileNum, string number)
        {
            if (ProcessMode == ProcessingMode.Add)
                return false;
            if (Data is null)
                NewData();

            var rowNum = Data.GetRowNum(number, profileNum, masterAccountNum);

            var success = this.GetData(rowNum.ToLong());
            if (!success)
                AddError($"Data not found for number : {number}");
            return success;
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByInvoiceNumberAsync(InvoicePayload payload, string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber);
            success = success && DeleteData();
            return success;
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(InvoicePayload payload, string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = GetByNumber(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber);
            success = success && DeleteData();
            return success;
        }

        public virtual async Task<bool> UpdateInvoiceDocNumberAsync(string invoiceUuid, string docNumber)
        {
            Edit();
            if (await GetDataByIdAsync(invoiceUuid))
            {
                Data.InvoiceHeader.QboDocNumber = docNumber;
                return await SaveDataAsync();
            }
            return false;
        }

        public virtual bool UpdateInvoiceDocNumber(string invoiceUuid, string docNumber)
        {
            Edit();
            if (GetDataById(invoiceUuid))
            {
                Data.InvoiceHeader.QboDocNumber = docNumber;
                return SaveData();
            }
            return false;
        }

        public async Task<bool> ExistInvoiceNumber(string invoiceNum, int masterAccountNum, int profileNum)
        {
            using (var trs = new ScopedTransaction(dbFactory))
                return await InvoiceHelper.ExistNumberAsync(invoiceNum, masterAccountNum, profileNum);
        }

        public async Task<bool> ExistInvoiceUuidAsync(string invoiceUuid, int masterAccountNum, int profileNum)
        {
            using (var trs = new ScopedTransaction(dbFactory))
                return await InvoiceHelper.ExistIdAsync(invoiceUuid, masterAccountNum, profileNum);
        }

        public async Task<string> GetInvoiceUuidByOrderShipmentUuidAsync(string orderShipmentUuid)
        {
            using (var trs = new ScopedTransaction(dbFactory))
                return await InvoiceHelper.GetInvoiceUuidByOrderShipmentUuidAsync(orderShipmentUuid);
        }

        public async Task<bool> ReceivedInvoiceTransactionReturnbackItem(InvoiceTransactionDataDto transaction)
        {
            int masterAccountNum = transaction.InvoiceDataDto.InvoiceHeader.MasterAccountNum.ToInt();
            int profileNum = transaction.InvoiceDataDto.InvoiceHeader.ProfileNum.ToInt();
            string invoiceNumber = transaction.InvoiceDataDto.InvoiceHeader.InvoiceNumber;
            await GetDataByNumberAsync(masterAccountNum, profileNum, invoiceNumber);
            decimal returnAmount =
                transaction.InvoiceTransaction.TotalAmount.ToDecimal() - transaction.InvoiceTransaction.DiscountAmount.ToDecimal();
            this.Data.InvoiceHeader.CreditAmount += returnAmount;
            this.Data.InvoiceHeader.Balance -= returnAmount;

            return await SaveDataAsync();
        }

        public async Task<InvoiceHeader> GetInvoiceHeaderAsync(string invoiceUuid)
        {
            if (this.Data == null)
                this.NewData();
            return await this.Data.GetInvoiceHeaderByInvoiceUuidAsync(invoiceUuid);
        }


        #region To qbo queue 

        private QboInvoiceClient _qboInvoiceClient;

        protected QboInvoiceClient qboInvoiceClient
        {
            get
            {
                if (_qboInvoiceClient is null)
                    _qboInvoiceClient = new QboInvoiceClient();
                return _qboInvoiceClient;
            }
        }


        /// <summary>
        /// convert erp invoice to a queue message then put it to qbo queue
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns></returns>
        public async Task<bool> AddQboInvoiceEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddErpEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.InvoiceHeader.InvoiceUuid,
            };
            return await qboInvoiceClient.SendAddQboInvoiceAsync(eventDto);
            //return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksInvoice");
        }

        public async Task<bool> VoidQboInvoiceEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddErpEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.InvoiceHeader.InvoiceUuid,
            };
            return await qboInvoiceClient.SendVoidQboInvoiceAsync(eventDto);
            //return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksInvoiceVoid");
        }

        #endregion

        #region Pay invoice

        public async Task<bool> UpdateInvoicePaidAmountAsync(InvoiceTransaction trans)
        {
            var changedPaidAmount = trans.TransType == (int)TransTypeEnum.Payment
                ? trans.TotalAmount - trans.OriginalPaidAmount
                : trans.OriginalPaidAmount - trans.TotalAmount;

            var sql = $@"
UPDATE InvoiceHeader 
SET PaidAmount=PaidAmount+@0, Balance=Balance-@0
WHERE InvoiceNumber=@1 
AND MasterAccountNum=@2 
AND ProfileNum=@3
";
            var result = await dbFactory.Db.ExecuteAsync(sql,
                    changedPaidAmount.ToSqlParameter("@0"),
                    trans.InvoiceNumber.ToSqlParameter("@1"),
                    trans.MasterAccountNum.ToSqlParameter("@2"),
                    trans.ProfileNum.ToSqlParameter("@3")
                    );
            return result > 0;
        }

        public bool UpdateInvoicePaidAmount(InvoiceTransaction trans)
        {
            var changedPaidAmount = trans.TransType == (int)TransTypeEnum.Payment
                ? trans.TotalAmount - trans.OriginalPaidAmount
                : trans.OriginalPaidAmount - trans.TotalAmount;

            var sql = $@"
UPDATE InvoiceHeader 
SET PaidAmount=PaidAmount+@0, Balance=Balance-@0
WHERE InvoiceNumber=@1 
AND MasterAccountNum=@2 
AND ProfileNum=@3
";
            var result = dbFactory.Db.Execute(sql,
                    changedPaidAmount.ToSqlParameter("@0"),
                    trans.InvoiceNumber.ToSqlParameter("@1"),
                    trans.MasterAccountNum.ToSqlParameter("@2"),
                    trans.ProfileNum.ToSqlParameter("@3")
                    );
            return result > 0;
        }

        #endregion


        public async Task<bool> UpdateStatusAsync(long rowNum, InvoiceStatusEnum status)
        {
            if (rowNum == 0) return false;

            var sql = $@"
UPDATE InvoiceHeader 
SET InvoiceStatus=@0
WHERE RowNum=@1 
";
            return await dbFactory.Db.ExecuteAsync(
                sql,
                ((int)status).ToSqlParameter("@0"),
                rowNum.ToSqlParameter("@1")
            ) > 0;
        }

        /// <summary>
        /// Update InvoiceHeader Balance and status according to InvoiceTransaction TotalAmount
        /// 
        /// </summary>
        public async Task<bool> UpdateInvoiceBalanceAsync(string transUuid, bool isRollBack = false)
        {
            var op = isRollBack ? "-" : "+";
            var opp = isRollBack ? "+" : "-";

            var sql = $@"
UPDATE ins 
SET 
PaidAmount = (CASE 
    WHEN COALESCE(trs.TransType,0) = 1 THEN COALESCE(ins.PaidAmount,0){op}COALESCE(trs.TotalAmount,0)
    ELSE ins.PaidAmount
END), 
CreditAmount = (CASE 
    WHEN COALESCE(trs.TransType,0) = 2 THEN COALESCE(ins.PaidAmount,0){op}COALESCE(trs.TotalAmount,0)
    ELSE ins.CreditAmount
END), 
Balance = COALESCE(ins.Balance,0){opp}COALESCE(trs.TotalAmount,0),
InvoiceStatus = (CASE 
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.TotalAmount,0)) = 0 AND (COALESCE(ins.InvoiceStatus,0)!=@2) THEN @2
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.TotalAmount,0)) > 0 AND (COALESCE(ins.InvoiceStatus,0)!=@1) THEN @1
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.TotalAmount,0)) < 0 AND (COALESCE(ins.InvoiceStatus,0)!=@3) THEN @3
    ELSE COALESCE(ins.InvoiceStatus,0)
END)
FROM InvoiceHeader ins
INNER JOIN InvoiceTransaction trs ON (trs.InvoiceUuid = ins.InvoiceUuid AND trs.TransUuid = @0)
WHERE ins.InvoiceStatus != @4 AND ins.InvoiceStatus != @5
";
            var result = await dbFactory.Db.ExecuteAsync(
                sql,
                transUuid.ToSqlParameter("@0"),
                ((int)InvoiceStatusEnum.Outstanding).ToSqlParameter("@1"),
                ((int)InvoiceStatusEnum.Paid).ToSqlParameter("@2"),
                ((int)InvoiceStatusEnum.OverPaid).ToSqlParameter("@3"),
                ((int)InvoiceStatusEnum.Closed).ToSqlParameter("@4"),
                ((int)InvoiceStatusEnum.Void).ToSqlParameter("@5")
            );
            return result > 0;
        }

        public bool UpdateInvoiceBalance(string transUuid, bool isRollBack = false)
        {
            var op = isRollBack ? "-" : "+";
            var opp = isRollBack ? "+" : "-";

            var sql = $@"
UPDATE ins 
SET 
PaidAmount = (CASE 
    WHEN COALESCE(trs.TransType,0) = 1 THEN COALESCE(ins.PaidAmount,0){op}COALESCE(trs.TotalAmount,0)
    ELSE ins.PaidAmount
END), 
CreditAmount = (CASE 
    WHEN COALESCE(trs.TransType,0) = 2 THEN COALESCE(ins.PaidAmount,0){op}COALESCE(trs.TotalAmount,0)
    ELSE ins.CreditAmount
END), 
Balance = COALESCE(ins.Balance,0){opp}COALESCE(trs.TotalAmount,0),
InvoiceStatus = (CASE 
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.TotalAmount,0)) = 0 AND (COALESCE(ins.InvoiceStatus,0)!=@2) THEN @2
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.TotalAmount,0)) > 0 AND (COALESCE(ins.InvoiceStatus,0)!=@1) THEN @1
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.TotalAmount,0)) < 0 AND (COALESCE(ins.InvoiceStatus,0)!=@3) THEN @3
    ELSE COALESCE(ins.InvoiceStatus,0)
END)
FROM InvoiceHeader ins
INNER JOIN InvoiceTransaction trs ON (trs.InvoiceUuid = ins.InvoiceUuid AND trs.TransUuid = @0)
WHERE ins.InvoiceStatus != @4 AND ins.InvoiceStatus != @5
";
            var result = dbFactory.Db.Execute(
                sql,
                transUuid.ToSqlParameter("@0"),
                ((int)InvoiceStatusEnum.Outstanding).ToSqlParameter("@1"),
                ((int)InvoiceStatusEnum.Paid).ToSqlParameter("@2"),
                ((int)InvoiceStatusEnum.OverPaid).ToSqlParameter("@3"),
                ((int)InvoiceStatusEnum.Closed).ToSqlParameter("@4"),
                ((int)InvoiceStatusEnum.Void).ToSqlParameter("@5")
            );
            return result > 0;
        }


        public async Task<string> GetNextNumberAsync(int masterAccountNum, int profileNum)
        {
            return await initNumbersService.GetNextNumberAsync(masterAccountNum, profileNum, ActivityLogType.Invoice);
        }
        public  string GetNextNumber(int masterAccountNum, int profileNum)
        {
            return  initNumbersService.GetNextNumber(masterAccountNum, profileNum, ActivityLogType.Invoice);
        }
    }
}


