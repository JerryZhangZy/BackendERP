    
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
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPApiSDK;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class ApInvoiceService
    {

        #region override methods

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override ApInvoiceService Init()
        {
            base.Init();
            SetDtoMapper(new ApInvoiceDataDtoMapperDefault());
            SetCalculator(new ApInvoiceServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new ApInvoiceServiceValidatorDefault(this, this.dbFactory));
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
                if (this.Data?.ApInvoiceHeader != null)
                {
                    //await inventoryService.UpdateOpenSoQtyFromSalesOrderItemAsync(this.Data.SalesOrderHeader.SalesOrderUuid, true);
                }
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
                if (this.Data?.ApInvoiceHeader != null)
                {
                    //inventoryService.UpdateOpenSoQtyFromSalesOrderItem(this.Data.SalesOrderHeader.SalesOrderUuid, true);
                }
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
                if (this.Data?.ApInvoiceHeader != null)
                {   
                  
                    //await inventoryService.UpdateOpenSoQtyFromSalesOrderItemAsync(this.Data.SalesOrderHeader.SalesOrderUuid);
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
                if (this.Data?.ApInvoiceHeader != null)
                {
                    //inventoryService.UpdateOpenSoQtyFromSalesOrderItem(this.Data.SalesOrderHeader.SalesOrderUuid);
                }
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
                if (this.Data?.ApInvoiceHeader != null)
                {
                    if (_ProcessMode == ProcessingMode.Add)
                    {
                         
                        var result=await initNumbersService.UpdateMaxNumberAsync(this.Data.ApInvoiceHeader.MasterAccountNum, this.Data.ApInvoiceHeader.ProfileNum, ActivityLogType.ApInvoice, this.Data.ApInvoiceHeader.ApInvoiceNum);
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

                if (this.Data?.ApInvoiceHeader != null)
                {
                    if (_ProcessMode == ProcessingMode.Add)
                    {
                          initNumbersService.UpdateMaxNumber(this.Data.ApInvoiceHeader.MasterAccountNum, this.Data.ApInvoiceHeader.ProfileNum, ActivityLogType.ApInvoice, this.Data.ApInvoiceHeader.ApInvoiceNum);
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
                Type = (int)ActivityLogType.ApInvoice,
                Action = (int)this.ProcessMode,
                LogSource = "ApInvoiceService",

                MasterAccountNum = this.Data.ApInvoiceHeader.MasterAccountNum,
                ProfileNum = this.Data.ApInvoiceHeader.ProfileNum,
                DatabaseNum = this.Data.ApInvoiceHeader.DatabaseNum,
                ProcessUuid = this.Data.ApInvoiceHeader.ApInvoiceUuid,
                ProcessNumber = this.Data.ApInvoiceHeader.ApInvoiceNum,
                ChannelNum = this.Data.ApInvoiceHeaderInfo.ChannelAccountNum,
                ChannelAccountNum = this.Data.ApInvoiceHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty
            };

        #endregion override methods


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(ApInvoiceDataDto dto)
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
        public virtual async Task<bool> AddAsync(ApInvoiceDataDto dto)
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

        public virtual bool Add(ApInvoicePayload payload)
        {
            if (payload is null || !payload.HasApInvoice)
                return false;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.ApInvoice))
                return false;

            // load data from dto
            FromDto(payload.ApInvoice);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public virtual async Task<bool> AddAsync(ApInvoicePayload payload)
        {
            if (payload is null || !payload.HasApInvoice)
                return false;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.ApInvoice)))
                return false;

            // load data from dto
            FromDto(payload.ApInvoice);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(ApInvoiceDataDto dto)
        {
            if (dto is null || !dto.HasApInvoiceHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.ApInvoiceHeader.RowNum.ToLong());

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
        public virtual async Task<bool> UpdateAsync(ApInvoiceDataDto dto)
        {
            if (dto is null || !dto.HasApInvoiceHeader)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.ApInvoiceHeader.RowNum.ToLong());

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
        public virtual bool Update(ApInvoicePayload payload)
        {
            if (payload is null || !payload.HasApInvoice || payload.ApInvoice.ApInvoiceHeader.RowNum.ToLong() <= 0)
                return false;
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.ApInvoice))
                return false;

            // load data 
            GetData(payload.ApInvoice.ApInvoiceHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.ApInvoice);

            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(ApInvoicePayload payload)
        {
            if (payload is null || !payload.HasApInvoice)
                return false;
            //set edit mode before validate
            Edit();
            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.ApInvoice)))
                return false;

            // load data 
            await GetDataAsync(payload.ApInvoice.ApInvoiceHeader.RowNum.ToLong());

            // load data from dto
            FromDto(payload.ApInvoice);

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            return await SaveDataAsync();
        }

        /// <summary>
        ///  get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetDataAsync(ApInvoicePayload payload, string orderNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }

        /// <summary>
        /// get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool GetData(ApInvoicePayload payload, string orderNumber)
        {
            return GetByNumber(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
        }
        /// <summary>
        ///  get data by number
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetByNumberAsync(PayloadBase payload, string invoiceNumber)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber);
        }
        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(ApInvoicePayload payload, string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, orderNumber);
            if (success)
            {
                return DeleteData();
            }
            return false;
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(ApInvoicePayload payload, string orderNumber)
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
        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByApInvoiceNumberAsync(ApInvoicePayload payload, string apInvoiceNumber)
        {
            if (string.IsNullOrEmpty(apInvoiceNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, apInvoiceNumber);
            success = success && DeleteData();
            return success;
        }

        #region To qbo queue 
        /// <summary>
        /// convert erp invoice to a queue message then put it to qbo queue
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns></returns>
        public async Task<bool> AddQboApInvoiceEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddErpEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.ApInvoiceHeader.ApInvoiceUuid,
            };
            var qboInvoiceClient = new QboInvoiceClient();
            return await qboInvoiceClient.SendAddQboInvoiceAsync(eventDto);
            //return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksInvoice");
        }
        #endregion

        public async Task<bool> ExistApInvoiceNumber(string number, int masterAccountNum, int profileNum)
        {
            return await ApInvoiceHelper.ExistApInvoiceNumberAsync(number, masterAccountNum, profileNum);
        }

        public async Task<int?> GetRowNumByPoUuidAsync(string transUuid)
        {
           return   dbFactory.Db.ExecuteScalar<int?>("SELECT TOP 1 RowNum FROM ApInvoiceHeader WHERE TransUuid=@0", transUuid.ToSqlParameter("@0"));
       
           // return await dbFactory.GetValueAsync<ApInvoiceHeader,int>("SELECT TOP 1 RowNum FROM ApInvoiceHeader WHERE PoUuid=@0",poUuid.ToSqlParameter("@0"));
        }
        
        public int GetRowNumByPoUuid(string poUuid)
        {
            return dbFactory.GetValue<ApInvoiceHeader,int>("SELECT TOP 1 RowNum FROM ApInvoiceHeader WHERE PoUuid=@0",poUuid.ToSqlParameter("@0"));
        }
        
        public async Task<bool> ExistPoUuidAsync(string poUuid)
        {
            return await dbFactory.ExistsAsync<ApInvoiceHeader>("PoUuid=@0", poUuid.ToSqlParameter("PoUuid"));
        }

        public bool ExistPoUuid(string poUuid)
        {
            return dbFactory.Exists<ApInvoiceHeader>("PoUuid=@0", poUuid.ToSqlParameter("PoUuid"));
        }

        private (decimal subTotalAmount, decimal miscAmount, decimal shippingAmount,decimal totalAmount) GetSummaryAmountByPoUuid(
            string transUuid)
        {
            var sql = $@"select 
ISNULL(sum(SubTotalAmount),0) as SubTotalAmount,ISNULL(sum(MiscAmount),0) as MiscAmount,ISNULL(sum(ShippingAmount),0) as ShippingAmount,ISNULL(sum(TotalAmount),0) as TotalAmount
from PoTransaction
where TransUuid=@0";
            using (var tx = new ScopedTransaction(dbFactory))
            {
                return SqlQuery.Execute(sql, (decimal subTotalAmount, decimal miscAmount, decimal shippingAmount,decimal totalAmount) => (subTotalAmount, miscAmount, shippingAmount, totalAmount), transUuid.ToSqlParameter("0")).First();
            }
        }
        
        public async Task<bool> CreateOrUpdateApInvoiceByPoReceiveAsync(int masterAccountNum,int profileNum, PoTransactionData data)
        {
            if (data == null || data.PoTransaction == null)
                return false;
            var header = data.PoTransaction;

            var transUuid = data.PoTransaction.TransUuid;
            
            var rowNum = await GetRowNumByPoUuidAsync(transUuid);
            var summary = GetSummaryAmountByPoUuid(transUuid);

            Edit();
            if (rowNum!=null && await GetDataAsync(rowNum.Value))//
            {

                //Update;
                Data.ApInvoiceHeader.TotalAmount = summary.totalAmount.ToAmount();
                Data.ApInvoiceItems.First(r => r.ApInvoiceItemType == (int)ApInvoiceItemType.ReceiveItemTotalAmount)
                    .Amount = summary.subTotalAmount.ToAmount();
                Data.ApInvoiceItems.First(r => r.ApInvoiceItemType == (int)ApInvoiceItemType.HandlingCost)
                    .Amount = summary.miscAmount.ToAmount();
                Data.ApInvoiceItems.First(r => r.ApInvoiceItemType == (int)ApInvoiceItemType.ShippingCost)
                    .Amount = summary.shippingAmount.ToAmount();
            }
            else
            {
                Add();
      
                Data.ApInvoiceHeader = new ApInvoiceHeader()
                {
                    ApInvoiceUuid=System.Guid.NewGuid().ToString(),
                    ApInvoiceDate = DateTime.UtcNow.Date,
                    ApInvoiceTime = DateTime.UtcNow.TimeOfDay,
                    ApInvoiceType = 0, //PoReceive
                    TotalAmount = header.TotalAmount,
                    VendorUuid = header.VendorUuid,
                    VendorInvoiceNum = header.VendorInvoiceNum,
                    VendorInvoiceDate = header.VendorInvoiceDate,
                    MasterAccountNum= masterAccountNum,
                    ProfileNum= profileNum,
                    PoUuid= header.PoUuid,
                    PoNum=header.PoNum,
                    VendorCode=header.VendorCode,
                    VendorName=header.VendorName,
                    TransUuid= header.TransUuid,
                    TransNum=header.TransNum
                };
                Data.ApInvoiceItems = new List<ApInvoiceItems>();
                Data.AddApInvoiceItems(new ApInvoiceItems()
                {
                    ApInvoiceItemType = (int)ApInvoiceItemType.ReceiveItemTotalAmount,
                    Currency = header.Currency,
                    Amount = summary.subTotalAmount.ToAmount()
                });
                Data.AddApInvoiceItems(new ApInvoiceItems()
                {
                    ApInvoiceItemType = (int)ApInvoiceItemType.HandlingCost,
                    Currency = header.Currency,
                    Amount = summary.miscAmount.ToAmount()
                });
                Data.AddApInvoiceItems(new ApInvoiceItems()
                {
                    ApInvoiceItemType = (int)ApInvoiceItemType.ShippingCost,
                    Currency = header.Currency,
                    Amount = summary.shippingAmount.ToAmount()
                });
            }
            return await SaveDataAsync();
        }
        public async Task<string> GetNextNumberAsync(int masterAccountNum, int profileNum)
        {
            return await initNumbersService.GetNextNumberAsync(masterAccountNum, profileNum, Base.Common.ActivityLogType.ApInvoice);

        }

        public   string GetNextNumber(int masterAccountNum, int profileNum)
        {
            return  initNumbersService.GetNextNumber(masterAccountNum, profileNum, Base.Common.ActivityLogType.ApInvoice);

        }

 

        public async Task<bool> ExistInvoiceNumberAsync(string apInvoiceNum, int masterAccountNum, int profileNum)
        {
            return await dbFactory.ExistsAsync<ApInvoiceHeader>(
                $"WHERE MasterAccountNum = @0 AND ProfileNum = @1 AND ApInvoiceNum = @2",
                masterAccountNum,
                profileNum,
                apInvoiceNum);
        }
        public bool ExistInvoiceNumber(string apInvoiceNum, int masterAccountNum, int profileNum)
        {
            return dbFactory.Exists<ApInvoiceHeader>(
                $"WHERE MasterAccountNum = @0 AND ProfileNum = @1 AND ApInvoiceNum = @2",
                masterAccountNum,
                profileNum,
                apInvoiceNum);
        }

        //public bool CreateOrUpdateApInvoiceByPoReceive(PoTransactionData data)
        //{
        //    if (data == null || data.PoTransaction == null)
        //        return false;
        //    var header = data.PoTransaction;
        //    if (header.TransStatus != (int)PoTransStatus.APReceive)
        //        return false;
        //    var poUuid = data.PoTransaction.PoUuid;
        //    Edit();
        //    var rowNum = GetRowNumByPoUuid(poUuid);
        //    var summary = GetSummaryAmountByPoUuid(poUuid);
        //    if (GetData(rowNum))
        //    {
        //        //Update;
        //        Data.ApInvoiceItems.First(r => r.ApInvoiceItemType == (int)ApInvoiceItemType.ReceiveItemTotalAmount)
        //            .Amount =summary.subTotalAmount.ToAmount();
        //        Data.ApInvoiceItems.First(r => r.ApInvoiceItemType == (int)ApInvoiceItemType.HandlingCost)
        //            .Amount = summary.miscAmount.ToAmount();
        //        Data.ApInvoiceItems.First(r => r.ApInvoiceItemType == (int)ApInvoiceItemType.ShippingCost)
        //            .Amount = summary.shippingAmount.ToAmount();
        //    }
        //    else
        //    {
        //        NewData();
        //        Data.ApInvoiceHeader = new ApInvoiceHeader()
        //        {
        //            ApInvoiceDate = DateTime.UtcNow.Date,
        //            ApInvoiceTime = DateTime.UtcNow.TimeOfDay,
        //            ApInvoiceType = 0, //PoReceive
        //            TotalAmount = header.TotalAmount,
        //            VendorUuid = header.VendorUuid,
        //            VendorInvoiceNum = header.VendorInvoiceNum,
        //            VendorInvoiceDate = header.VendorInvoiceDate
        //        };
        //        Data.ApInvoiceItems = new List<ApInvoiceItems>();
        //        Data.AddApInvoiceItems(new ApInvoiceItems()
        //        {
        //            ApInvoiceItemType = (int)ApInvoiceItemType.ReceiveItemTotalAmount,
        //            Currency = header.Currency,
        //            Amount = summary.subTotalAmount.ToAmount()
        //        });
        //        Data.AddApInvoiceItems(new ApInvoiceItems()
        //        {
        //            ApInvoiceItemType = (int)ApInvoiceItemType.HandlingCost,
        //            Currency = header.Currency,
        //            Amount = summary.miscAmount.ToAmount()
        //        });
        //        Data.AddApInvoiceItems(new ApInvoiceItems()
        //        {
        //            ApInvoiceItemType = (int)ApInvoiceItemType.ShippingCost,
        //            Currency = header.Currency,
        //            Amount = summary.shippingAmount.ToAmount()
        //        });
        //    }
        //    return SaveData();
        //}

        public async Task<ApInvoiceHeader> GetApInvoiceHeaderAsync(string invoiceUuid)
        {
            if (this.Data == null)
                this.NewData();
            return await this.Data.GetApInvoiceHeaderByApInvoiceUuidAsync(invoiceUuid);
        }

        public async Task<bool> UpdateInvoiceBalanceAsync(string transUuid, bool isRollBack = false)
        {
            var op = isRollBack ? "-" : "+";
            var opp = isRollBack ? "+" : "-";

            var sql = $@"
UPDATE ins 
SET 
PaidAmount = (CASE 
    WHEN COALESCE(trs.TransType,0) = 1 THEN COALESCE(ins.PaidAmount,0){op}COALESCE(trs.Amount,0)
    ELSE ins.PaidAmount
END), 
CreditAmount = (CASE 
    WHEN COALESCE(trs.TransType,0) = 2 THEN COALESCE(ins.PaidAmount,0){op}COALESCE(trs.Amount,0)
    ELSE ins.CreditAmount
END), 
Balance = COALESCE(ins.Balance,0){opp}COALESCE(trs.Amount,0),
ApInvoiceStatus = (CASE 
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.Amount,0)) = 0 AND (COALESCE(ins.ApInvoiceStatus,0)!=@2) THEN @2
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.Amount,0)) > 0 AND (COALESCE(ins.ApInvoiceStatus,0)!=@1) THEN @1
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.Amount,0)) < 0 AND (COALESCE(ins.ApInvoiceStatus,0)!=@3) THEN @3
    ELSE COALESCE(ins.ApInvoiceStatus,0)
END)
FROM ApInvoiceHeader ins
INNER JOIN ApInvoiceTransaction trs ON (trs.ApInvoiceUuid = ins.ApInvoiceUuid AND trs.TransUuid = @0)
WHERE ins.ApInvoiceStatus != @4 AND ins.ApInvoiceStatus != @5
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
    WHEN COALESCE(trs.TransType,0) = 1 THEN COALESCE(ins.PaidAmount,0){op}COALESCE(trs.Amount,0)
    ELSE ins.PaidAmount
END), 
CreditAmount = (CASE 
    WHEN COALESCE(trs.TransType,0) = 2 THEN COALESCE(ins.PaidAmount,0){op}COALESCE(trs.Amount,0)
    ELSE ins.CreditAmount
END), 
Balance = COALESCE(ins.Balance,0){opp}COALESCE(trs.Amount,0),
ApInvoiceStatus = (CASE 
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.Amount,0)) = 0 AND (COALESCE(ins.ApInvoiceStatus,0)!=@2) THEN @2
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.Amount,0)) > 0 AND (COALESCE(ins.ApInvoiceStatus,0)!=@1) THEN @1
    WHEN (COALESCE(ins.Balance,0){opp}COALESCE(trs.Amount,0)) < 0 AND (COALESCE(ins.ApInvoiceStatus,0)!=@3) THEN @3
    ELSE COALESCE(ins.ApInvoiceStatus,0)
END)
FROM ApInvoiceHeader ins
INNER JOIN ApInvoiceTransaction trs ON (trs.ApInvoiceUuid = ins.ApInvoiceUuid AND trs.TransUuid = @0)
WHERE ins.ApInvoiceStatus != @4 AND ins.ApInvoiceStatus != @5
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
    }
}



