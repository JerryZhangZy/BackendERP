


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using System.Text;
using Newtonsoft.Json;
using DigitBridge.CommerceCentral.ERPApiSDK;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class MiscInvoicePaymentService : MiscInvoiceTransactionService, IMiscInvoicePaymentService
    {
        MiscInvoiceService _miscInvoiceService;
        MiscInvoiceService MiscInvoiceService
        {
            get
            {
                if (_miscInvoiceService == null) _miscInvoiceService = new MiscInvoiceService(dbFactory);
                return _miscInvoiceService;
            }
        }

        public MiscInvoicePaymentService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        #region override methods

        public override MiscInvoiceTransactionService Init()
        {
            return base.Init();
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
                if (this.Data?.MiscInvoiceTransaction != null)
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
                if (this.Data?.MiscInvoiceTransaction != null)
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
                if (this.Data?.MiscInvoiceTransaction != null)
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
                if (this.Data?.MiscInvoiceTransaction != null)
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
                Type = (int)ActivityLogType.MiscInvoicePayment,
                Action = (int)this.ProcessMode,
                LogSource = "MiscInvoicePaymentService",

                MasterAccountNum = this.Data.MiscInvoiceTransaction.MasterAccountNum,
                ProfileNum = this.Data.MiscInvoiceTransaction.ProfileNum,
                DatabaseNum = this.Data.MiscInvoiceTransaction.DatabaseNum,
                ProcessUuid = this.Data.MiscInvoiceTransaction.TransUuid,
                ProcessNumber = $"{this.Data?.MiscInvoiceData?.MiscInvoiceHeader.MiscInvoiceNumber}-{this.Data.MiscInvoiceTransaction.TransNum}",
                ChannelNum = 0,
                ChannelAccountNum = 0,

                LogMessage = string.Empty
            };

        #endregion override methods


        protected async Task<bool> GetMiscPaymentData(string miscInvoiceUuid, string invoiceTransUuid, string invoiceNumber, decimal amount)
        {
            if (!await LoadMiscInvoiceAsync(miscInvoiceUuid))
                return false;

            var header = Data.MiscInvoiceData.MiscInvoiceHeader;

            Data.MiscInvoiceTransaction = new MiscInvoiceTransaction()
            {
                ProfileNum = header.ProfileNum,
                MasterAccountNum = header.MasterAccountNum,
                DatabaseNum = header.DatabaseNum,

                BankAccountCode = header.BankAccountCode,
                BankAccountUuid = header.BankAccountUuid,
                CreditAccount = header.CreditAmount.ToLong(),
                Currency = header.Currency,

                MiscInvoiceNumber = header.MiscInvoiceNumber,
                MiscInvoiceUuid = header.MiscInvoiceUuid,
                Description = "Add misc payment trans for applying prepayment amount",

                TotalAmount = amount > header.Balance ? header.Balance : amount,
                PaidBy = (int)PaidByAr.PrePayment,
                CheckNum = invoiceNumber,
                AuthCode = invoiceTransUuid,
                TransDate = DateTime.UtcNow,
                TransTime = DateTime.UtcNow.TimeOfDay,
                TransType = (int)TransTypeEnum.Payment,
                TransStatus = (int)TransStatus.Paid,
                TransUuid = Guid.NewGuid().ToString(),

            };

            using (var tx = new ScopedTransaction(dbFactory))
            {
                Data.MiscInvoiceTransaction.TransNum = await MiscInvoiceTransactionHelper.GetTranSeqNumAsync(header.MiscInvoiceNumber, header.ProfileNum);
            }

            return true;
        }

        public async Task<bool> AddMiscPayment(string miscInvoiceUuid, string invoiceTransUuid, string invoiceNumber, decimal amount)
        {
            _ProcessMode = ProcessingMode.Add;

            if (miscInvoiceUuid.IsZero())
            {
                AddError($"miscInvoiceUuid is null");
                return false;
            }

            if (invoiceTransUuid.IsZero())
            {
                AddError($"invoiceTransUuid is null");
                return false;
            }

            if (!await GetMiscPaymentData(miscInvoiceUuid, invoiceTransUuid, invoiceNumber, amount))
            {
                return false;
            }
            if (!await SaveDataAsync())
            {
                AddError($"AddMiscPayment->SaveDataAsync error.");
                return false;
            }
            AddActivityLogForCurrentData();
            await MiscInvoiceService.PayAsync(miscInvoiceUuid, amount);
            return true;
        }
        public async Task<bool> DeleteMiscPayment(string miscInvoiceUuid, string invoiceTransUuid, decimal amount)
        {
            if (string.IsNullOrEmpty(invoiceTransUuid))
            {
                AddError("invoiceTransUuid is null or empty");
                return false;
            }
            else
            {
                try
                {
                    await dbFactory.Db.ExecuteAsync($"DELETE MiscInvoiceTransaction WHERE AuthCode='{invoiceTransUuid}'");
                }
                catch
                {
                    AddError($"Delete MiscInvoiceTransaction with AuthCode '{invoiceTransUuid}' failed");
                }
                await MiscInvoiceService.PayAsync(miscInvoiceUuid, -amount);
                return true;
            }
        }
        protected void AddActivityLogForCurrentData()
        {
            this.AddActivityLog(new ActivityLog(dbFactory)
            {
                Type = (int)ActivityLogType.MiscInvoicePayment,
                Action = (int)this.ProcessMode,
                LogSource = "MiscInvoicePaymentService",

                MasterAccountNum = this.Data.MiscInvoiceTransaction.MasterAccountNum,
                ProfileNum = this.Data.MiscInvoiceTransaction.ProfileNum,
                DatabaseNum = this.Data.MiscInvoiceTransaction.DatabaseNum,
                ProcessUuid = this.Data.MiscInvoiceTransaction.TransUuid,
                ProcessNumber = this.Data.MiscInvoiceTransaction.TransNum.ToString(),
                //ChannelNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,
                //ChannelAccountNum = this.Data.SalesOrderHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty
            });
        }

        //Snowy:can't delete, don't know TransUuid or TransNumber
        //public override async Task<bool> DeleteAsync(string id)
        //{
        //    if (await base.DeleteAsync(id))
        //    {
        //        await MiscInvoiceService.PayAsync(Data.MiscInvoiceTransaction.MiscInvoiceUuid, -Data.MiscInvoiceTransaction.TotalAmount);
        //        return true;
        //    }
        //    return false;
        //}
        //public override bool Delete(string id)
        //{
        //    if (base.Delete(id))
        //    {
        //        MiscInvoiceService.PayAsync(Data.MiscInvoiceTransaction.MiscInvoiceUuid, -Data.MiscInvoiceTransaction.TotalAmount).Wait();
        //        return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// Get misc invoice payment with detail and miscinvoiceheader by miscInvoiceNumber
        /// </summary>
        /// <param name="miscInvoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task GetPaymentWithMiscInvoiceHeaderAsync(MiscPaymentPayload payload, string miscInvoiceNumber, int? transNum = null)
        {
            payload.Success = await LoadMiscInvoiceAsync(payload.MasterAccountNum, payload.ProfileNum, miscInvoiceNumber);
            if (!payload.Success)
            {
                payload.Messages = this.Messages;
                return;
            }

            payload.MiscTransactions = await GetDtoListAsync(payload.MasterAccountNum, payload.ProfileNum, miscInvoiceNumber, TransTypeEnum.Payment, transNum);
            var miscInvoiceMapper = new MiscInvoiceDataDtoMapperDefault();
            payload.MiscInvoiceDataDto = miscInvoiceMapper.WriteDto(Data.MiscInvoiceData, null);
        }

        /// <summary>
        /// Get actual amount that can pay to invoice.
        /// </summary>
        /// <param name="miscInvoiceUuid"></param>
        /// <param name="expectedAmount"></param>
        /// <returns></returns>
        public virtual async Task<decimal> GetCanApplyAmount(string miscInvoiceUuid, decimal expectedAmount)
        {
            if (!await LoadMiscInvoiceAsync(miscInvoiceUuid))
                return 0;

            var miscInvoiceBalance = this.Data.MiscInvoiceData.MiscInvoiceHeader.Balance;

            return expectedAmount > miscInvoiceBalance ? miscInvoiceBalance : expectedAmount;

        }
    }
}



