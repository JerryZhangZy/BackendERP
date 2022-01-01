using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;
using DigitBridge.Base.Common;
using System;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class ApPaymentService : ApTransactionService, IApPaymentService
    {
        public ApPaymentService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        #region override methods

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override ApTransactionService Init()
        {
            SetDtoMapper(new ApTransactionDataDtoMapperDefault());
            SetCalculator(new ApPaymentServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new ApPaymentServiceValidatorDefault(this, this.dbFactory));
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
                if (this.Data?.ApInvoiceTransaction != null)
                {
                    await ApInvoiceService.UpdateInvoiceBalanceAsync(this.Data.ApInvoiceTransaction.TransUuid, true);
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
                if (this.Data?.ApInvoiceTransaction != null)
                {
                    ApInvoiceService.UpdateInvoiceBalance(this.Data.ApInvoiceTransaction.TransUuid, true);
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
                if (this.Data?.ApInvoiceTransaction != null)
                {
                    await ApInvoiceService.UpdateInvoiceBalanceAsync(this.Data.ApInvoiceTransaction.TransUuid);
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
                if (this.Data?.ApInvoiceTransaction != null)
                {
                    ApInvoiceService.UpdateInvoiceBalance(this.Data.ApInvoiceTransaction.TransUuid);
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
                Type = (int)ActivityLogType.ApInvoicePayment,
                Action = (int)this.ProcessMode,
                LogSource = "ApPaymentService",

                MasterAccountNum = this.Data.ApInvoiceTransaction.MasterAccountNum,
                ProfileNum = this.Data.ApInvoiceTransaction.ProfileNum,
                DatabaseNum = this.Data.ApInvoiceTransaction.DatabaseNum,
                ProcessUuid = this.Data.ApInvoiceTransaction.TransUuid,
                ProcessNumber = $"{this.Data.ApInvoiceTransaction.ApInvoiceNum}-{this.Data.ApInvoiceTransaction.TransNum}",
                ChannelNum = 0,
                ChannelAccountNum = 0,

                LogMessage = string.Empty
            };

        #endregion override methods

        #region services property
        private ApInvoiceService _apInvoiceService;
        public ApInvoiceService ApInvoiceService
        {
            get
            {
                if (_apInvoiceService == null) _apInvoiceService = new ApInvoiceService(dbFactory);
                return _apInvoiceService;
            }
        }
        #endregion

        #region add multi payments
        public virtual async Task<bool> AddAsync(ApPaymentPayload payload)
        {
            if (!payload.HasApTransaction)
            {
                AddError("ApTransaction is required.");
                return false;
            }

            if (!payload.ApTransaction.HasApInvoiceTransaction)
            {
                AddError("ApTransaction.ApInvoiceTransaction is required.");
                return false;
            }

            if (!payload.HasApApplyInvoices)
            {
                //TODO check this adderror or ApplyPaymentToCurrentInvoice
                // all payment apply to one invoice linked to trans apInvoiceNumber. 
                payload.ApApplyInvoices = new List<ApApplyInvoice>() { ApplyPaymentToCurrentInvoice(payload) };
            }
            else if (payload.ApApplyInvoices.Count(i => string.IsNullOrEmpty(i.ApInvoiceNum)) > 0)
            {
                AddError("ApApplyInvoice.InvoiceNumber is required.");
                return false;
            }

            foreach (var applyInvoice in payload.ApApplyInvoices)
            {
                var paymentPayload = GetPayload(payload, applyInvoice);
                if (!await base.AddAsync(paymentPayload))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Add payment failed for apInvoiceNumber:{applyInvoice.ApInvoiceNum} ");
                    continue;
                }

                applyInvoice.TransUuid = Data.ApInvoiceTransaction.TransUuid;

                ////add payment success. then pay invoice.
                //var success = await PayInvoiceAsync(applyInvoice, payload.MasterAccountNum, payload.ProfileNum);
                //if (!success)
                //{
                //    payload.Success = false;
                //    applyInvoice.Success = false;
                //    AddError($"{applyInvoice.ApInvoiceNum} is not applied.");
                //}
            }

            return payload.Success;
        }

        public virtual bool Add(ApPaymentPayload payload)
        {
            if (!payload.HasApTransaction || payload.ApTransaction.ApInvoiceTransaction is null)
            {
                AddError("ApTransaction is required.");
                return false;
            }

            if (!payload.HasApApplyInvoices)
            {
                //TODO check this adderror or ApplyPaymentToCurrentInvoice
                // all payment apply to one invoice linked to trans apInvoiceNumber. 
                payload.ApApplyInvoices = new List<ApApplyInvoice>() { ApplyPaymentToCurrentInvoice(payload) };
            }
            else if (payload.ApApplyInvoices.Count(i => string.IsNullOrEmpty(i.ApInvoiceNum)) > 0)
            {
                AddError("ApApplyInvoice.InvoiceNumber is required.");
                return false;
            }

            foreach (var applyInvoice in payload.ApApplyInvoices)
            {
                var paymentPayload = GetPayload(payload, applyInvoice);
                if (!base.Add(paymentPayload))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Add payment failed for apInvoiceNumber:{applyInvoice.ApInvoiceNum} ");
                    continue;
                }

                applyInvoice.TransUuid = Data.ApInvoiceTransaction.TransUuid;

                ////add payment success. then pay invoice.
                //var success = await PayInvoiceAsync(applyInvoice, payload.MasterAccountNum, payload.ProfileNum);
                //if (!success)
                //{
                //    payload.Success = false;
                //    applyInvoice.Success = false;
                //    AddError($"{applyInvoice.ApInvoiceNum} is not applied.");
                //}
            }

            return payload.Success;
        }

        protected ApPaymentPayload GetPayload(ApPaymentPayload payload, ApApplyInvoice apApplyInvoice)
        {
            var originalTrans = payload.ApTransaction.ApInvoiceTransaction;
            var trans = new ApInvoiceTransactionDto()
            {
                AuthCode = originalTrans.AuthCode,
                BankAccountCode = originalTrans.BankAccountCode,
                BankAccountUuid = originalTrans.BankAccountUuid,
                CheckNum = originalTrans.CheckNum,
                CreditAccount = originalTrans.CreditAccount,
                Currency = originalTrans.Currency,
                DebitAccount = originalTrans.DebitAccount,
                ExchangeRate = originalTrans.ExchangeRate,
                PaidBy = originalTrans.PaidBy,

                Amount = apApplyInvoice.PaidAmount,

                ApInvoiceNum = apApplyInvoice.ApInvoiceNum,
                ApInvoiceUuid = apApplyInvoice.ApInvoiceUuid,
                Description = originalTrans.Description,
                Notes = originalTrans.Notes,
                EnterBy = originalTrans.EnterBy,
                TransDate = originalTrans.TransDate,
                TransTime = originalTrans.TransTime,
                //TaxRate = originalTrans.TaxRate,
                //TransSourceCode = originalTrans.TransSourceCode,
                TransStatus = originalTrans.TransStatus,
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                RowNum = apApplyInvoice.TransRowNum
            };
            using (var tx = new ScopedTransaction(dbFactory))
            {
                trans.TransNum = ApTransactionHelper.GetTranSeqNum(trans.ApInvoiceNum, payload.ProfileNum);
            }

            var dataDto = new ApTransactionDataDto()
            {
                ApInvoiceTransaction = trans
            };
            return new ApPaymentPayload()
            {
                ProfileNum = payload.ProfileNum,
                MasterAccountNum = payload.MasterAccountNum,
                ApTransaction = dataDto,
                DatabaseNum = payload.DatabaseNum,
            };
        }

        /// <summary>
        ///when no applyivoice in payload,then apply all payamount to current invoice
        /// </summary>
        /// <returns></returns>
        protected ApApplyInvoice ApplyPaymentToCurrentInvoice(ApPaymentPayload payload)
        {
            // all payment apply in current invoice. 
            var trans = payload.ApTransaction.ApInvoiceTransaction;
            return new ApApplyInvoice()
            {
                ApInvoiceNum = trans.ApInvoiceNum,
                ApInvoiceUuid = trans.ApInvoiceUuid,
                PaidAmount = trans.Amount.ToAmount(),
                TransRowNum = trans.RowNum
            };
        }

        //protected async Task<bool> PayInvoiceAsync(ApApplyInvoice apApplyInvoice, int masterAccountNum, int profileNum)
        //{
        //    var changedPaidAmount = apApplyInvoice.PaidAmount - Data.ApInvoiceTransaction.OriginalPaidAmount;
        //    using (var trs = new ScopedTransaction(dbFactory))
        //        return await InvoiceHelper.PayInvoiceAsync(apApplyInvoice.ApInvoiceNum, changedPaidAmount, masterAccountNum, profileNum);
        //}
        //protected async Task<bool> PayInvoiceAsync(string apInvoiceNumber, int masterAccountNum, int profileNum, decimal paidAmount)
        //{
        //    using (var trs = new ScopedTransaction(dbFactory))
        //        return await InvoiceHelper.PayInvoiceAsync(apInvoiceNumber, paidAmount, masterAccountNum, profileNum);
        //}

        //protected bool PayInvoice(ApApplyInvoice applyInvoice, int masterAccountNum, int profileNum)
        //{
        //    var changedPaidAmount = applyInvoice.PaidAmount - Data.ApInvoiceTransaction.OriginalPaidAmount;
        //    using (var trs = new ScopedTransaction(dbFactory))
        //        return InvoiceHelper.PayInvoice(applyInvoice.ApInvoiceNum, changedPaidAmount, masterAccountNum, profileNum);
        //}
        #endregion

        #region update multi payments

        /// <summary>
        /// update payment
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(ApPaymentPayload payload)
        {
            if (!payload.HasApTransaction || payload.ApTransaction.ApInvoiceTransaction is null)
            {
                AddError("ApTransaction is required.");
                return false;
            }

            if (!payload.HasApApplyInvoices)
            {
                //TODO check this adderror or ApplyPaymentToCurrentInvoice
                // all payment apply to one invoice linked to trans apInvoiceNumber. 
                payload.ApApplyInvoices = new List<ApApplyInvoice>() { ApplyPaymentToCurrentInvoice(payload) };
            }

            if (payload.ApApplyInvoices.Count(i => !i.TransRowNum.HasValue) > 0)
            {
                AddError("TransRowNum is required.");
                return false;
            }

            if (payload.ApApplyInvoices.Count(i => i.TransRowNum.IsZero()) > 0)
            {
                AddError("TransRowNum should be a positive number.");
                return false;
            }

            foreach (var applyInvoice in payload.ApApplyInvoices)
            {
                var paymentPayload = GetPayload(payload, applyInvoice);
                if (!await base.UpdateAsync(paymentPayload))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Update payment failed for apInvoiceNumber:{applyInvoice.ApInvoiceNum} ");
                    continue;
                }

                applyInvoice.TransUuid = Data.ApInvoiceTransaction.TransUuid;

                ////update payment success. then pay invoice.
                //var success = await PayInvoiceAsync(applyInvoice, payload.MasterAccountNum, payload.ProfileNum);
                //if (!success)
                //{
                //    payload.Success = false;
                //    applyInvoice.Success = false;
                //    AddError($"{applyInvoice.apInvoiceNumber} is not applied.");
                //}
            }

            return payload.Success;
        }

        /// <summary>
        /// update payment
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public virtual bool Update(ApPaymentPayload payload)
        {
            if (!payload.HasApTransaction || payload.ApTransaction.ApInvoiceTransaction is null)
            {
                AddError("ApTransaction is required.");
                return false;
            }

            if (!payload.HasApApplyInvoices)
            {
                //TODO check this adderror or ApplyPaymentToCurrentInvoice
                // all payment apply to one invoice linked to trans apInvoiceNumber. 
                payload.ApApplyInvoices = new List<ApApplyInvoice>() { ApplyPaymentToCurrentInvoice(payload) };
            }

            if (payload.ApApplyInvoices.Count(i => !i.TransRowNum.HasValue) > 0)
            {
                AddError("TransRowNum is required.");
                return false;
            }

            if (payload.ApApplyInvoices.Count(i => i.TransRowNum.IsZero()) > 0)
            {
                AddError("TransRowNum should be a positive number.");
                return false;
            }

            foreach (var applyInvoice in payload.ApApplyInvoices)
            {
                var paymentPayload = GetPayload(payload, applyInvoice);
                if (!base.Update(paymentPayload))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Update payment failed for apInvoiceNumber:{applyInvoice.ApInvoiceNum} ");
                    continue;
                }

                applyInvoice.TransUuid = Data.ApInvoiceTransaction.TransUuid;

                ////update payment success. then pay invoice.
                //var success = await PayInvoiceAsync(applyInvoice, payload.MasterAccountNum, payload.ProfileNum);
                //if (!success)
                //{
                //    payload.Success = false;
                //    applyInvoice.Success = false;
                //    AddError($"{applyInvoice.apInvoiceNumber} is not applied.");
                //}
            }

            return payload.Success;
        }

        #endregion

        #region get by number
        public async Task<bool> GetByNumberAsync(ApPaymentPayload payload, string apInvoiceNumber, int transNum)
        {
            return await base.GetByNumberAsync(payload, apInvoiceNumber, TransTypeEnum.Payment, transNum);
        }

        public async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string apInvoiceNumber, int transNum)
        {
            return await base.GetByNumberAsync(masterAccountNum, profileNum, apInvoiceNumber, TransTypeEnum.Payment, transNum);
        }

        /// <summary>
        /// Get invoice payment with detail and invoiceheader by apInvoiceNumber
        /// </summary>
        /// <param name="apInvoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task GetPaymentWithApHeaderAsync(ApPaymentPayload payload, string apInvoiceNumber, int? transNum = null)
        {
            payload.Success = LoadApInvoice(payload.MasterAccountNum, payload.ProfileNum, apInvoiceNumber);
            if (!payload.Success)
            {
                payload.Messages = this.Messages;
                return;
            }

            payload.ApTransactions = await GetDtoListAsync(payload.MasterAccountNum, payload.ProfileNum, apInvoiceNumber, TransTypeEnum.Payment, transNum);
            var apInvoiceMapper = new ApInvoiceDataDtoMapperDefault();
            payload.ApInvoiceDataDto = apInvoiceMapper.WriteDto(Data.ApInvoiceData, null);
        }
        #endregion

        #region delete by number
        /// <summary>
        /// Delete invoice by invoice number
        /// </summary>
        /// <param name="apInvoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(ApPaymentPayload payload, string apInvoiceNumber, int transNum)
        {
            return await base.DeleteByNumberAsync(payload, apInvoiceNumber, TransTypeEnum.Payment, transNum);
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="apInvoiceNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(ApPaymentPayload payload, string apInvoiceNumber, int transNum)
        {
            return base.DeleteByNumber(payload, apInvoiceNumber, TransTypeEnum.Payment, transNum);
        }
        #endregion



        #region New payment

        public async Task<bool> NewPaymentByApInvoiceNumAsync(ApNewPaymentPayload payload, string apInvoiceNum)
        {
            if (!await LoadApInvoice(payload, apInvoiceNum))
                return false;

            NewData();

            payload.ApplyInvoices = payload.Payments
                .Select(i => new ApplyInvoice
                {
                    InvoiceUuid = i.ApInvoiceUuid,
                    InvoiceNumber = i.ApInvoiceNumber,
                    Success = false,
                    PaidAmount = 0
                }).ToList();

            payload.ApTransaction = this.ToDto().ApInvoiceTransaction;

            return true;
        }

        public async Task<bool> NewPaymentByVendorNum(ApNewPaymentPayload payload, string vendorCode)
        {
            if (!await LoadApInvoiceList(payload, vendorCode))
            {
                return false;
            }

            NewData();

            payload.ApplyInvoices = payload.Payments
                .Select(i => new ApplyInvoice
                {
                    InvoiceUuid = i.ApInvoiceUuid,
                    InvoiceNumber = i.ApInvoiceNumber,
                    Success = false,
                    PaidAmount = 0
                }).ToList();

            payload.ApTransaction = this.ToDto().ApInvoiceTransaction;

            return true;
        }

        public async Task<bool> UpdateApInvoicePaymentAsync(ApNewPaymentPayload payload)
        {
            if (!(await ValidateAccountAsync(payload)))
                return false;

            payload.ApTransaction.MasterAccountNum = payload.MasterAccountNum;
            payload.ApTransaction.ProfileNum = payload.ProfileNum;
            var succes = true;
            if (string.IsNullOrEmpty(payload.ApTransaction.PaymentUuid) || payload.ApTransaction.PaymentNumber.ToLong() <= 0)
            {
                payload.ApTransaction.PaymentUuid = Guid.NewGuid().ToString();
                payload.ApTransaction.PaymentNumber = await GetNextPaymentNumberAsync(payload.MasterAccountNum, payload.ProfileNum);
                succes = await AddNewApplyPaymentAsync(payload);
            }
            else
            {
                succes = await UpdateApplyPaymentAsync(payload);
            }

            // if any payment not save success, delete all previous saved payment.
            if (!succes)
                foreach (var payment in payload.ApplyInvoices.Where(x => x.Success))
                    await DeleteAsync(payment.TransUuid);

            await GetByPaymentNumberAsync(payload, payload.ApTransaction.PaymentNumber.ToLong());
            return succes;
        }

        public async Task<long> GetNextPaymentNumberAsync(int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COALESCE(MAX(PaymentNumber), 0) 
FROM ApInvoiceTransaction
WHERE MasterAccountNum=@0 AND ProfileNum=@1
";
            return await dbFactory.Db.ExecuteScalarAsync<long>(
                sql,
                masterAccountNum.ToSqlParameter("@0"),
                profileNum.ToSqlParameter("@1")
            ) + 1;
        }

        public async Task<int> GetNextTransNumAsync(string invoiceUuid)
        {
            var sql = $@"
SELECT COALESCE(MAX(TransNum), 0)
FROM ApInvoiceTransaction
WHERE ApInvoiceUuid=@0
";
            return await dbFactory.Db.ExecuteScalarAsync<int>(
                sql,
                invoiceUuid.ToSqlParameter("@0")
            ) + 1;
        }

        protected async virtual Task<bool> AddNewApplyPaymentAsync(ApNewPaymentPayload payload)
        {
            payload.ApTransaction.MasterAccountNum = payload.MasterAccountNum;
            payload.ApTransaction.ProfileNum = payload.ProfileNum;
            var succes = true;

            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                if (applyInvoice == null || string.IsNullOrEmpty(applyInvoice.InvoiceUuid) || applyInvoice.PaidAmount <= 0)
                    continue;
                applyInvoice.Success = false;

                var invoice = await ApInvoiceService.GetApInvoiceHeaderAsync(applyInvoice.InvoiceUuid);
                if (invoice == null)
                {
                    AddError($"Invoice {applyInvoice.InvoiceUuid} does not exist");
                    succes = false;
                    return succes;
                }
                var trans = GenerateTransaction(applyInvoice, invoice, payload.ApTransaction);

                // Always add new payment
                trans.ApInvoiceTransaction.TransUuid = Guid.NewGuid().ToString();
                trans.ApInvoiceTransaction.TransNum = await GetNextTransNumAsync(trans.ApInvoiceTransaction.ApInvoiceUuid);
                trans.ApInvoiceTransaction.RowNum = 0;

                succes = await AddAsync(trans);
                if (!succes)
                    return succes;

                applyInvoice.TransRowNum = Data.ApInvoiceTransaction.RowNum;
                applyInvoice.TransUuid = Data.ApInvoiceTransaction.TransUuid;
                applyInvoice.TransNum = Data.ApInvoiceTransaction.TransNum;
                applyInvoice.PaidAmount = Data.ApInvoiceTransaction.Amount;
                applyInvoice.Success = true;
            }

            return succes;
        }

        protected async virtual Task<bool> UpdateApplyPaymentAsync(ApNewPaymentPayload payload)
        {
            payload.ApTransaction.MasterAccountNum = payload.MasterAccountNum;
            payload.ApTransaction.ProfileNum = payload.ProfileNum;
            // load exist payment list
            var existPayment = await GetByPaymentNumberAsync(payload.ApTransaction.PaymentNumber.ToLong(), payload.MasterAccountNum, payload.ProfileNum);

            var succes = true;
            var removed = new List<ApInvoiceTransaction>();
            // if exist old payment, but not exist in current apply payment, need delete old payment
            foreach (var payment in existPayment)
            {
                if (payment == null || string.IsNullOrEmpty(payment.TransUuid)) continue;
                await GetDataByIdAsync(payment.TransUuid);
                var obj = payload.ApplyInvoices.FirstOrDefault(x => x.TransUuid.EqualsIgnoreSpace(payment.TransUuid));
                if (obj == null || obj.PaidAmount <= 0)
                {
                    await DeleteAsync(payment.TransUuid);
                    removed.Add(payment);
                }
            }
            foreach (var rem in removed)
                existPayment.Remove(rem);

            // add or update payment for each apply invoice
            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                if (applyInvoice == null || string.IsNullOrEmpty(applyInvoice.InvoiceUuid) || applyInvoice.PaidAmount <= 0)
                    continue;
                applyInvoice.Success = false;

                // load invoice header
                var invoice = await ApInvoiceService.GetApInvoiceHeaderAsync(applyInvoice.InvoiceUuid);
                if (invoice == null)
                {
                    AddError($"Invoice {applyInvoice.InvoiceUuid} does not exist");
                    succes = false;
                    return succes;
                }
                var trans = GenerateTransaction(applyInvoice, invoice, payload.ApTransaction);

                var obj = existPayment.FirstOrDefault(x => x.TransUuid.EqualsIgnoreSpace(trans.ApInvoiceTransaction.TransUuid));
                if (obj != null)
                {
                    trans.ApInvoiceTransaction.RowNum = obj.RowNum;
                    succes = await UpdateAsync(trans);
                }
                else
                {
                    trans.ApInvoiceTransaction.TransUuid = Guid.NewGuid().ToString();
                    trans.ApInvoiceTransaction.TransNum = await GetNextTransNumAsync(trans.ApInvoiceTransaction.ApInvoiceUuid);
                    trans.ApInvoiceTransaction.RowNum = 0;
                    succes = await AddAsync(trans);
                }

                if (!succes)
                    return succes;

                applyInvoice.TransRowNum = Data.ApInvoiceTransaction.RowNum;
                applyInvoice.TransUuid = Data.ApInvoiceTransaction.TransUuid;
                applyInvoice.TransNum = Data.ApInvoiceTransaction.TransNum;
                applyInvoice.PaidAmount = Data.ApInvoiceTransaction.Amount;
                applyInvoice.Success = true;
            }

            return succes;
        }
        
        public async Task<bool> GetByPaymentNumberAsync(ApNewPaymentPayload payload, long paymentNumber)
        {
            var payments = await GetByPaymentNumberAsync(paymentNumber, payload.MasterAccountNum, payload.ProfileNum);
            if (payments == null || payments.Count == 0)
                return payload.ReturnError($"Payment number {paymentNumber} not found.");

            var pay1 = payments[0];
            payload.ApTransaction = new ApInvoiceTransactionDto()
            {
                DatabaseNum = pay1.DatabaseNum,
                MasterAccountNum = pay1.MasterAccountNum,
                PaymentUuid = pay1.PaymentUuid,
                PaymentNumber = pay1.PaymentNumber,
                TransDate = pay1.TransDate,
                PaidBy = pay1.PaidBy,
                BankAccountUuid = pay1.BankAccountUuid,
                BankAccountCode = pay1.BankAccountCode,
                CheckNum = pay1.CheckNum,
                AuthCode = pay1.AuthCode,
                Currency = pay1.Currency,
                Amount = 0
            };

            var invoice1 = await ApInvoiceService.GetApInvoiceHeaderAsync(pay1.ApInvoiceUuid);

            if (!await LoadApInvoiceList(payload, invoice1.VendorCode))
            {
                payload.Payments = new List<ApInvoiceListForPayment>();
            }

            var invoiceUuids = payments.Select(p =>
            {
                return p != null && !string.IsNullOrEmpty(p.ApInvoiceUuid) && payload.Payments.FindByInvoiceUuid(p.ApInvoiceUuid) == null
                    ? p.ApInvoiceUuid
                    : null;
            }).Where(x => !string.IsNullOrEmpty(x)).ToList();

            if (invoiceUuids != null && invoiceUuids.Count > 0)
            {
                var newInvoiceList = await LoadInvoiceListByUuidsAsync(payload, invoiceUuids);
                if (newInvoiceList != null && newInvoiceList.Count > 0)
                {
                    foreach (var item in newInvoiceList)
                        payload.Payments.Add(item);
                }
            }

            if (payload.Payments == null || payload.Payments.Count == 0)
                return payload.ReturnError($"Payment number {paymentNumber} not found.");

            foreach (var pay in payments)
            {
                if (pay == null) continue;
                var ins = payload.Payments.FindByInvoiceUuid(pay.ApInvoiceUuid);
                if (ins == null) continue;
                ins.TransRowNum = pay.RowNum;
                ins.TransUuid = pay.TransUuid;
                ins.TransNum = pay.TransNum;
                ins.Description = pay.Description;
                ins.Notes = pay.Notes;
                ins.PayAmount = pay.Amount;
                ins.PaidAmount = ins.PaidAmount;
                ins.Balance = ins.PaidAmount;

                payload.ApTransaction.Amount += pay.Amount;
            }
            payload.Payments = payload.Payments
                .OrderByDescending(x => x.PaidAmount)
                .OrderBy(x => x.ApInvoiceDate)
                .ToList();

            return true;
        }

        public async Task<IList<ApInvoiceListForPayment>> LoadInvoiceListByUuidsAsync(ApNewPaymentPayload payload, IList<string> uuids)
        {
            var invoicePayload = new ApInvoicePayload()
            {
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                LoadAll = true
            };

            var invoiceQuery = new ApInvoiceQuery();
            invoiceQuery.ApInvoiceUuid.SetMultipleFilterValueList(uuids);
            invoiceQuery.ApInvoiceUuid.FilterMode = FilterBy.eq;
            invoiceQuery.DisableDate();

            var srv = new ApInvoiceList(this.dbFactory, invoiceQuery);
            await srv.GetApInvoiceListAsync(invoicePayload);

            return invoicePayload.ApInvoiceList.ToObject<IList<ApInvoiceListForPayment>>();
        }

        public async Task<IList<ApInvoiceTransaction>> GetByPaymentNumberAsync(long paymentNumber, int masterAccountNum, int profileNum)
        {
            var sql = $@"WHERE MasterAccountNum=@0 AND ProfileNum=@1 AND TransType = 1 AND PaymentNumber=@2";
            return (await dbFactory.FindAsync<ApInvoiceTransaction>(
                sql,
                masterAccountNum,
                profileNum,
                paymentNumber
            )).ToList();
        }

        protected ApTransactionDataDto GenerateTransaction(ApplyInvoice applyInvoice, ApInvoiceHeader invoiceHeader, ApInvoiceTransactionDto templateTransaction)
        {
            var result = new ApTransactionDataDto()
            {
                ApInvoiceTransaction = new ApInvoiceTransactionDto
                {
                    DatabaseNum = invoiceHeader.DatabaseNum,
                    MasterAccountNum = invoiceHeader.MasterAccountNum,
                    ProfileNum = invoiceHeader.ProfileNum,
                    ApInvoiceUuid = invoiceHeader.ApInvoiceUuid,
                    ApInvoiceNum = invoiceHeader.ApInvoiceNum,

                    TransUuid = applyInvoice.TransUuid,
                    TransNum = applyInvoice.TransNum,
                    RowNum = applyInvoice.TransRowNum,

                    PaymentUuid = templateTransaction.PaymentUuid,
                    PaymentNumber = templateTransaction.PaymentNumber,

                    TransType = (int)TransTypeEnum.Payment,
                    TransStatus = 0,
                    TransDate = DateTime.UtcNow.Date,
                    TransTime = DateTime.UtcNow,
                    Description = applyInvoice.Description,
                    Notes = applyInvoice.Notes,
                    
                    //VendorCode = invoiceHeader.VendorCode,
                    PaidBy = templateTransaction.PaidBy,
                    BankAccountUuid = templateTransaction.BankAccountUuid,
                    BankAccountCode = templateTransaction.BankAccountCode,
                    CheckNum = templateTransaction.CheckNum,
                    AuthCode = templateTransaction.AuthCode,

                    Currency = invoiceHeader.Currency,
                    Amount = applyInvoice.PaidAmount,
                }
            };

            return result;
        }
        private async Task<bool> LoadApInvoiceList(ApNewPaymentPayload payload, string vendorCode)
        {
            if (string.IsNullOrEmpty(vendorCode))
                return false;

            var apInvoicePayload = new ApInvoicePayload()
            {
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                LoadAll = true
            };

            var apInvoiceQuery = new ApInvoiceQuery();
            apInvoiceQuery.InitFilterByVendor(vendorCode);

            var srv = new ApInvoiceList(this.dbFactory, apInvoiceQuery);
            await srv.GetApInvoiceListAsync(apInvoicePayload);

            if (!apInvoicePayload.Success)
                this.Messages = this.Messages.Concat(srv.Messages).ToList();

            payload.Payments = apInvoicePayload.ApInvoiceList.ToObject<IList<ApInvoiceListForPayment>>();
            payload.ApInvoiceListCount = apInvoicePayload.ApInvoiceListCount;

            return apInvoicePayload.Success;
        }

        private async Task<bool> LoadApInvoice(ApNewPaymentPayload payload, string invoiceNum)
        {
            if (string.IsNullOrEmpty(invoiceNum))
                return false;

            var apInvoicePayload = new ApInvoicePayload
            { 
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                LoadAll = true
            };

            var apInvoiceQuery = new ApInvoiceQuery();
            apInvoiceQuery.InitFilterByInvoiceNum(invoiceNum);

            var srv = new ApInvoiceList(this.dbFactory, apInvoiceQuery);
            await srv.GetApInvoiceListAsync(apInvoicePayload);

            if (!apInvoicePayload.Success)
                this.Messages = this.Messages.Concat(srv.Messages).ToList();

            payload.Payments = apInvoicePayload.ApInvoiceList.ToObject<IList<ApInvoiceListForPayment>>();
            payload.ApInvoiceListCount = apInvoicePayload.ApInvoiceListCount;

            return apInvoicePayload.Success;
        }

        #endregion

    }
}



