


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
    public partial class InvoicePaymentService : InvoiceTransactionService, IInvoicePaymentService
    {
        #region Service Property

        private MiscInvoicePaymentService _miscServicePayment;
        public MiscInvoicePaymentService MiscPaymentService
        {
            get
            {
                if (_miscServicePayment == null) _miscServicePayment = new MiscInvoicePaymentService(dbFactory);
                return _miscServicePayment;
            }
        }

        private InvoicePaymentService _anotherInvoicePaymentService;
        public InvoicePaymentService AnotherInvoicePaymentService
        {
            get
            {
                if (_anotherInvoicePaymentService == null) _anotherInvoicePaymentService = new InvoicePaymentService(dbFactory);
                return _anotherInvoicePaymentService;
            }
        }

        #endregion

        public InvoicePaymentService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        #region override methods

        public override InvoiceTransactionService Init()
        {
            SetDtoMapper(new InvoiceTransactionDataDtoMapperDefault());
            SetCalculator(new InvoicePaymentServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new InvoicePaymentServiceValidatorDefault(this, this.dbFactory));
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
                if (this.Data?.InvoiceTransaction != null)
                {
                    await InvoiceService.UpdateInvoiceBalanceAsync(this.Data.InvoiceTransaction.TransUuid, true);
                    await DeleteCreditPaymentAsync(this.Data);
                    await DeleteMiscPaymentAsync(this.Data);
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
                if (this.Data?.InvoiceTransaction != null)
                {
                    InvoiceService.UpdateInvoiceBalance(this.Data.InvoiceTransaction.TransUuid, true);
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
                if (this.Data?.InvoiceTransaction != null)
                {
                    await InvoiceService.UpdateInvoiceBalanceAsync(this.Data.InvoiceTransaction.TransUuid);
                    await AddCreditPaymentAsync(this.Data);
                    await AddMiscPaymentAsync(this.Data);
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
                if (this.Data?.InvoiceTransaction != null)
                {
                    InvoiceService.UpdateInvoiceBalance(this.Data.InvoiceTransaction.TransUuid);
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
                Type = (int)ActivityLogType.InvoicePayment,
                Action = (int)this.ProcessMode,
                LogSource = "InvoicePaymentService",

                MasterAccountNum = this.Data.InvoiceTransaction.MasterAccountNum,
                ProfileNum = this.Data.InvoiceTransaction.ProfileNum,
                DatabaseNum = this.Data.InvoiceTransaction.DatabaseNum,
                ProcessUuid = this.Data.InvoiceTransaction.TransUuid,
                ProcessNumber = $"{this.Data.InvoiceTransaction.InvoiceNumber}-{this.Data.InvoiceTransaction.TransUuid}",
                ChannelNum = this.Data.InvoiceData.InvoiceHeaderInfo != null ? this.Data.InvoiceData.InvoiceHeaderInfo.ChannelNum : 0,
                ChannelAccountNum = this.Data.InvoiceData.InvoiceHeaderInfo != null ? this.Data.InvoiceData.InvoiceHeaderInfo.ChannelAccountNum : 0,

                LogMessage = string.Empty
            };

        #endregion override methods


        /// <summary>
        /// Get invoice payment with detail and invoiceheader by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task GetPaymentsAsync(InvoiceNewPaymentPayload payload, string invoiceNumber, int? transNum = null)
        {
            var transList = await GetDataListAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, TransTypeEnum.Payment, transNum);
            if (transList == null)
            {
                payload.ApplyInvoices = new List<ApplyInvoice>();
                payload.Success = true;
                if (transNum.HasValue)
                    payload.Messages.AddInfo($"The invoice {invoiceNumber} doesn't contains payment which Number is {transNum.Value}");
                else
                    payload.Messages.AddInfo($"The invoice {invoiceNumber} doesn't contains any payment");
                return;
            }
            var invoiceHeader = await GetInvoiceHeaderAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber);
            
            payload.ApplyInvoices = transList.Select(t => new ApplyInvoice { 
                TransRowNum = t.InvoiceTransaction.RowNum,
                TransUuid = t.InvoiceTransaction.TransUuid,
                InvoiceUuid = t.InvoiceTransaction.InvoiceUuid,
                InvoiceNumber = t.InvoiceTransaction.InvoiceNumber,
                PaidAmount = t.InvoiceTransaction.TotalAmount,
                Success = true
            }).ToList();
            
            payload.Success = true;
            payload.Messages = Messages;
        }

        public async Task<bool> ExistCheckNumber(string number, int masterAccountNum, int profileNum)
        {
            return await InvoicePaymentHelper.ExistCheckNumberAsync(number, masterAccountNum, profileNum);
        }

        public async Task CreatePaymentForCustomerAsync(InvoicePaymentPayload payload, string customerCode)
        {
            var invoices = await GetInvoiceHeadersByCustomerAsync(payload.MasterAccountNum, payload.ProfileNum, customerCode);
            payload.InvoiceHeaders = invoices;
            payload.ApplyInvoices = invoices.Select(i => new ApplyInvoice
            {
                InvoiceNumber = i.InvoiceNumber,
                InvoiceUuid = i.InvoiceUuid
            }).ToList();
            payload.InvoiceTransactions = invoices.Select(i => GenerateTransByInvoiceHeader(i)).ToList();

            payload.Success = true;
            payload.Messages = this.Messages;
        }

        InvoiceTransactionDataDto GenerateTransByInvoiceHeader(InvoiceHeaderDto invoiceHeader)
        {
            var trans = new InvoiceTransactionDataDto
            {
                InvoiceReturnItems = null,
                InvoiceTransaction = new InvoiceTransactionDto
                {
                    DatabaseNum = invoiceHeader.DatabaseNum,
                    MasterAccountNum = invoiceHeader.MasterAccountNum,
                    ProfileNum = invoiceHeader.ProfileNum,

                    Currency = invoiceHeader.Currency,
                    ExchangeRate = 1,
                    InvoiceNumber = invoiceHeader.InvoiceNumber,
                    InvoiceUuid = invoiceHeader.InvoiceUuid,
                    TaxRate = invoiceHeader.TaxRate,
                    DiscountRate = invoiceHeader.DiscountRate,
                    DiscountAmount = invoiceHeader.DiscountAmount,

                    SubTotalAmount = invoiceHeader.SubTotalAmount,
                    SalesAmount = invoiceHeader.SalesAmount,
                    TotalAmount = 0,
                    TaxableAmount = invoiceHeader.TaxableAmount,
                    NonTaxableAmount = invoiceHeader.NonTaxableAmount,
                    TaxAmount = invoiceHeader.TaxAmount,
                    ShippingAmount = invoiceHeader.ShippingAmount,
                    ShippingTaxAmount = invoiceHeader.ShippingTaxAmount,
                    MiscAmount = invoiceHeader.MiscAmount,
                    MiscTaxAmount = invoiceHeader.MiscTaxAmount,
                    ChargeAndAllowanceAmount = invoiceHeader.ChargeAndAllowanceAmount
                }
            };

            return trans;
        }

        public async Task AddPaymentsForInvoicesAsync(InvoicePaymentPayload payload)
        {
            int successCount = 0;
            string paymentBatch = Guid.NewGuid().ToString();
            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                var transaction = payload.InvoiceTransactions.Single(t => t.InvoiceTransaction.InvoiceUuid == applyInvoice.InvoiceUuid);
                transaction.InvoiceTransaction.MasterAccountNum = payload.MasterAccountNum;
                transaction.InvoiceTransaction.ProfileNum = payload.ProfileNum;
                transaction.InvoiceTransaction.TotalAmount = applyInvoice.PaidAmount;
                transaction.InvoiceTransaction.PaymentUuid = paymentBatch;
                if (!await base.AddAsync(transaction))
                {
                    applyInvoice.Success = false;
                    continue;
                }

                successCount++;

                applyInvoice.TransUuid = transaction.InvoiceTransaction.TransUuid;
                applyInvoice.TransRowNum = transaction.InvoiceTransaction.RowNum;
            }

            payload.Success = successCount > 0;
        }

        /// <summary>
        /// get payments and invoice data.
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        public virtual async Task<(List<InvoiceTransaction>, InvoiceData)> GetPaymentsWithInvoice(int masterAccountNum, int profileNum, string invoiceNumber, int? transNum = null)
        {
            var tranDatas = await GetDataListAsync(masterAccountNum, profileNum, invoiceNumber, TransTypeEnum.Payment, transNum);
            var payments = new List<InvoiceTransaction>();
            if (tranDatas != null && tranDatas.Count > 0)
            {
                payments = tranDatas.Select(i => i.InvoiceTransaction).ToList();
            }
            await LoadInvoiceAsync(invoiceNumber, profileNum, masterAccountNum);
            return (payments, Data.InvoiceData);
        }

        #region add multi payments
        public virtual async Task<bool> AddAsync(InvoicePaymentPayload payload)
        {
            if (!payload.HasInvoiceTransaction || payload.InvoiceTransaction.InvoiceTransaction is null)
            {
                AddError("Payment information is required.");
                return false;
            }

            if (!payload.HasApplyInvoices)
            {
                //TODO check this adderror or ApplyPaymentToCurrentInvoice
                // all payment apply to one invoice linked to trans invoicenumber. 
                payload.ApplyInvoices = new List<ApplyInvoice>() { ApplyPaymentToCurrentInvoice(payload) };
            }
            else if (payload.ApplyInvoices.Count(i => string.IsNullOrEmpty(i.InvoiceNumber)) > 0)
            {
                AddError("ApplyInvoices.InvoiceNumber is required.");
                return false;
            }

            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                var paymentDataDto = GetPaymentDataDto(payload, applyInvoice);
                if (!await base.AddAsync(paymentDataDto))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    continue;
                }

                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;
            }

            return payload.Success;
        }

        public virtual bool Add(InvoicePaymentPayload payload)
        {
            if (!payload.HasInvoiceTransaction || payload.InvoiceTransaction.InvoiceTransaction is null)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }

            if (!payload.HasApplyInvoices)
            {
                //TODO check this adderror or ApplyPaymentToCurrentInvoice
                // all payment apply to one invoice linked to trans invoicenumber. 
                payload.ApplyInvoices = new List<ApplyInvoice>() { ApplyPaymentToCurrentInvoice(payload) };
            }
            else if (payload.ApplyInvoices.Count(i => string.IsNullOrEmpty(i.InvoiceNumber)) > 0)
            {
                AddError("ApplyInvoices.InvoiceNumber is required.");
                return false;
            }

            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                var paymentDataDto = GetPaymentDataDto(payload, applyInvoice);
                if (!base.Add(paymentDataDto))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    continue;
                }

                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;
            }

            return payload.Success;
        }

        protected InvoiceTransactionDataDto GetPaymentDataDto(InvoicePaymentPayload payload, ApplyInvoice applyInvoice)
        {
            var originalTrans = payload.InvoiceTransaction.InvoiceTransaction;
            var trans = new InvoiceTransactionDto()
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

                TotalAmount = applyInvoice.PaidAmount,

                InvoiceNumber = applyInvoice.InvoiceNumber,
                InvoiceUuid = applyInvoice.InvoiceUuid,
                Description = originalTrans.Description,
                Notes = originalTrans.Notes,
                EnterBy = originalTrans.EnterBy,
                TransDate = originalTrans.TransDate,
                TransTime = originalTrans.TransTime,
                TaxRate = originalTrans.TaxRate,
                TransSourceCode = originalTrans.TransSourceCode,
                TransStatus = originalTrans.TransStatus,
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                DatabaseNum = payload.DatabaseNum,
                RowNum = applyInvoice.TransRowNum
            };
            using (var tx = new ScopedTransaction(dbFactory))
            {
                trans.TransNum = InvoiceTransactionHelper.GetTranSeqNum(trans.InvoiceNumber, payload.ProfileNum);
            }

            return new InvoiceTransactionDataDto()
            {
                InvoiceTransaction = trans
            };
        }

        /// <summary>
        ///when no applyivoice in payload,then apply all payamount to current invoice
        /// </summary>
        /// <returns></returns>
        protected ApplyInvoice ApplyPaymentToCurrentInvoice(InvoicePaymentPayload payload)
        {
            // all payment apply in current invoice. 
            var trans = payload.InvoiceTransaction.InvoiceTransaction;
            return new ApplyInvoice()
            {
                InvoiceNumber = trans.InvoiceNumber,
                InvoiceUuid = trans.InvoiceUuid,
                PaidAmount = trans.TotalAmount.ToAmount(),
                TransRowNum = trans.RowNum
            };
        }
        #endregion

        #region update multi payments

        /// <summary>
        /// update payment
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(InvoicePaymentPayload payload)
        {
            if (!payload.HasInvoiceTransaction || payload.InvoiceTransaction.InvoiceTransaction is null)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }

            if (!payload.HasApplyInvoices)
            {
                //TODO check this adderror or ApplyPaymentToCurrentInvoice
                // all payment apply to one invoice linked to trans invoicenumber. 
                payload.ApplyInvoices = new List<ApplyInvoice>() { ApplyPaymentToCurrentInvoice(payload) };
            }

            if (payload.ApplyInvoices.Count(i => !i.TransRowNum.HasValue) > 0)
            {
                AddError("TransRowNum is required.");
                return false;
            }

            if (payload.ApplyInvoices.Count(i => i.TransRowNum.IsZero()) > 0)
            {
                AddError("TransRowNum should be a positive number.");
                return false;
            }

            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                var paymentDataDto = GetPaymentDataDto(payload, applyInvoice);
                if (!(await base.UpdateAsync(paymentDataDto)))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Update payment failed for InvoiceNumber:{applyInvoice.InvoiceNumber} ");
                    continue;
                }

                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;
            }

            return payload.Success;
        }

        /// <summary>
        /// update payment
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public virtual bool Update(InvoicePaymentPayload payload)
        {
            if (!payload.HasInvoiceTransaction || payload.InvoiceTransaction.InvoiceTransaction is null)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }

            if (!payload.HasApplyInvoices)
            {
                //TODO check this adderror or ApplyPaymentToCurrentInvoice
                // all payment apply to one invoice linked to trans invoicenumber. 
                payload.ApplyInvoices = new List<ApplyInvoice>() { ApplyPaymentToCurrentInvoice(payload) };
            }

            if (payload.ApplyInvoices.Count(i => !i.TransRowNum.HasValue) > 0)
            {
                AddError("TransRowNum is required.");
                return false;
            }

            if (payload.ApplyInvoices.Count(i => i.TransRowNum.IsZero()) > 0)
            {
                AddError("TransRowNum should be a positive number.");
                return false;
            }

            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                var paymentDataDto = GetPaymentDataDto(payload, applyInvoice);
                if (!base.Update(paymentDataDto))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    continue;
                }
                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;
                applyInvoice.TransRowNum = Data.InvoiceTransaction.RowNum;
            }

            return payload.Success;
        }

        #endregion

        public async Task<bool> GetByNumberAsync(InvoicePaymentPayload payload, string invoiceNumber, int transNum)
        {
            return await base.GetByNumberAsync(payload, invoiceNumber, TransTypeEnum.Payment, transNum);
        }

        public async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string invoiceNumber, int transNum)
        {
            return await base.GetByNumberAsync(masterAccountNum, profileNum, invoiceNumber, TransTypeEnum.Payment, transNum);
        }

        /// <summary>
        /// Delete invoice by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(InvoiceNewPaymentPayload payload, string invoiceNumber, int transNum)
        {
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, transNum);
            if (success && Data.InvoiceTransaction.PaidBy == (int)PaidByAr.PrePayment)
            {
                var invoiceTransaction = Data.InvoiceTransaction;
                success = await MiscPaymentService.DeleteMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.TransUuid, invoiceTransaction.TotalAmount);
                if (!success) AddError("Update Misc.Invoice payment failed");
            }
            success = success && DeleteData();
            return success;
        } 


        #region New payment

        public async Task<bool> NewPaymentByInvoiceNumberAsync(InvoiceNewPaymentPayload payload, string invoiceNumber)
        {
            NewData();
            if (!(await LoadInvoiceAsync(invoiceNumber, payload.ProfileNum, payload.MasterAccountNum)))
                return false;

            CopyInvoiceHeaderToTrans();

            Data.InvoiceTransaction.CustomerCode = Data.InvoiceData.InvoiceHeader.CustomerCode;
            ////unpaid amount
            //Data.InvoiceTransaction.TotalAmount = Data.InvoiceData.InvoiceHeader.Balance.IsZero() ? 0 : Data.InvoiceData.InvoiceHeader.Balance;

            payload.InvoiceTransaction = this.ToDto().InvoiceTransaction;
            payload.ApplyInvoices = new List<ApplyInvoice> {
                new ApplyInvoice
                {
                    InvoiceUuid = Data.InvoiceData.InvoiceHeader.InvoiceUuid,
                    InvoiceNumber = Data.InvoiceData.InvoiceHeader.InvoiceNumber
                }
            };
            return await LoadInvoiceListAsync(payload, Data.InvoiceData.InvoiceHeader.CustomerCode);
        }

        public async Task<bool> NewPaymentByCustomerCode(InvoiceNewPaymentPayload payload, string customerCode)
        {
            if (!await LoadInvoiceListAsync(payload, customerCode))
            {
                return false;
            }

            NewData();
            Data.InvoiceTransaction.CustomerCode = customerCode;
            payload.ApplyInvoices = payload.InvoiceList
                .Select(i => new ApplyInvoice { 
                    InvoiceUuid = i.invoiceUuid,
                    InvoiceNumber = i.invoiceNumber,
                    Success = false,
                    PaidAmount = 0
                }).ToList();

            payload.InvoiceTransaction = this.ToDto().InvoiceTransaction;

            return true;
        }

        public async virtual Task<bool> UpdateInvoicePaymentsAsync(InvoiceNewPaymentPayload payload)
        {
            if (!(await ValidateAccountAsync(payload)))
                return false;

            //var invoices = await GetInvoiceHeadersByCustomerAsync(payload.MasterAccountNum, payload.ProfileNum, payload.InvoiceTransaction.CustomerCode);
            //if (invoices?.Count == 0)
            //    return false;
            payload.InvoiceTransaction.MasterAccountNum = payload.MasterAccountNum;
            payload.InvoiceTransaction.ProfileNum = payload.ProfileNum;
            var succes = true;
            if (string.IsNullOrEmpty(payload.InvoiceTransaction.PaymentUuid) || payload.InvoiceTransaction.PaymentNumber.ToLong() <= 0)
            {
                payload.InvoiceTransaction.PaymentUuid = Guid.NewGuid().ToString();
                payload.InvoiceTransaction.PaymentNumber = await GetNextPaymentNumberAsync(payload.MasterAccountNum, payload.ProfileNum);
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

            await GetByPaymentNumberAsync(payload, payload.InvoiceTransaction.PaymentNumber.ToLong());
            return succes;
        }

        protected async virtual Task<bool> AddNewApplyPaymentAsync(InvoiceNewPaymentPayload payload)
        {
            payload.InvoiceTransaction.MasterAccountNum = payload.MasterAccountNum;
            payload.InvoiceTransaction.ProfileNum = payload.ProfileNum;
            var succes = true;

            // add payment for each apply invoice
            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                if (applyInvoice == null || string.IsNullOrEmpty(applyInvoice.InvoiceUuid) || applyInvoice.PaidAmount <= 0)
                    continue;
                applyInvoice.Success = false;

                // load invoice header
                //await InvoiceService.GetDataByIdAsync(applyInvoice.InvoiceUuid);
                //var invoice = InvoiceService.Data.InvoiceHeader;
                
                var invoice = await InvoiceService.GetInvoiceHeaderAsync(applyInvoice.InvoiceUuid);
                if (invoice == null)
                {
                    AddError($"Invoice {applyInvoice.InvoiceUuid} does not exist");
                    succes = false;
                    return succes;
                }
                var trans = GenerateTransaction(applyInvoice, invoice, payload.InvoiceTransaction);

                // Always add new payment
                trans.InvoiceTransaction.TransUuid = Guid.NewGuid().ToString();
                trans.InvoiceTransaction.TransNum = await GetNextTransNumAsync(trans.InvoiceTransaction.InvoiceUuid);
                trans.InvoiceTransaction.RowNum = 0;

                succes = await AddAsync(trans);
                if (!succes)
                    return succes;

                applyInvoice.TransRowNum = Data.InvoiceTransaction.RowNum;
                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;
                applyInvoice.TransNum = Data.InvoiceTransaction.TransNum;
                applyInvoice.PaidAmount = Data.InvoiceTransaction.TotalAmount;
                applyInvoice.Success = true;
            }

            return succes;
        }

        protected async virtual Task<bool> UpdateApplyPaymentAsync(InvoiceNewPaymentPayload payload)
        {
            payload.InvoiceTransaction.MasterAccountNum = payload.MasterAccountNum;
            payload.InvoiceTransaction.ProfileNum = payload.ProfileNum;
            // load exist payment list
            var existPayment = await GetByPaymentNumberAsync(payload.InvoiceTransaction.PaymentNumber.ToLong(), payload.MasterAccountNum, payload.ProfileNum);

            var succes = true;
            var removed = new List<InvoiceTransaction>();
            // if exist old payment, but not exist in current apply payment, need delete old payment
            foreach (var payment in existPayment)
            {
                if (payment == null || string.IsNullOrEmpty(payment.TransUuid)) continue;
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
                var invoice = await InvoiceService.GetInvoiceHeaderAsync(applyInvoice.InvoiceUuid);
                if (invoice == null)
                {
                    AddError($"Invoice {applyInvoice.InvoiceUuid} does not exist");
                    succes = false;
                    return succes;
                }
                var trans = GenerateTransaction(applyInvoice, invoice, payload.InvoiceTransaction);

                var obj = existPayment.FirstOrDefault(x => x.TransUuid.EqualsIgnoreSpace(trans.InvoiceTransaction.TransUuid));
                if (obj != null)
                {
                    trans.InvoiceTransaction.RowNum = obj.RowNum;
                    succes = await UpdateAsync(trans);
                }
                else
                {
                    trans.InvoiceTransaction.TransUuid = Guid.NewGuid().ToString();
                    trans.InvoiceTransaction.TransNum = await GetNextTransNumAsync(trans.InvoiceTransaction.InvoiceUuid);
                    trans.InvoiceTransaction.RowNum = 0;
                    succes = await AddAsync(trans);
                }

                if (!succes)
                    return succes;

                applyInvoice.TransRowNum = Data.InvoiceTransaction.RowNum;
                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;
                applyInvoice.TransNum = Data.InvoiceTransaction.TransNum;
                applyInvoice.PaidAmount = Data.InvoiceTransaction.TotalAmount;
                applyInvoice.Success = true;
            }

            return succes;
        }

        protected InvoiceTransactionDataDto GenerateTransaction(ApplyInvoice applyInvoice, InvoiceHeader invoiceHeader, InvoiceTransactionDto templateTransaction)
        {
            var result = new InvoiceTransactionDataDto()
            {
                InvoiceReturnItems = null,
                InvoiceDataDto = null,
                InvoiceTransaction = new InvoiceTransactionDto
                {
                    DatabaseNum = invoiceHeader.DatabaseNum,
                    MasterAccountNum = invoiceHeader.MasterAccountNum,
                    ProfileNum = invoiceHeader.ProfileNum,
                    InvoiceUuid = invoiceHeader.InvoiceUuid,
                    InvoiceNumber = invoiceHeader.InvoiceNumber,

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

                    CustomerCode = invoiceHeader.CustomerCode,
                    PaidBy = templateTransaction.PaidBy,
                    BankAccountUuid = templateTransaction.BankAccountUuid,
                    BankAccountCode = templateTransaction.BankAccountCode,
                    CheckNum = templateTransaction.CheckNum,
                    AuthCode = templateTransaction.AuthCode,

                    Currency = invoiceHeader.Currency,
                    TotalAmount = applyInvoice.PaidAmount,
                }
            };

            return result;
        }

        public async Task<bool> GetByPaymentNumberAsync(InvoiceNewPaymentPayload payload, long paymentNumber)
        {
            // load exist payment list
            var payments = await GetByPaymentNumberAsync(paymentNumber, payload.MasterAccountNum, payload.ProfileNum);
            if (payments == null || payments.Count == 0)
                return payload.ReturnError($"Payment number {paymentNumber} not found.");

            // use first invoiceTransaction for payment info
            var pay1 = payments[0];
            payload.InvoiceTransaction = new InvoiceTransactionDto()
            {
                DatabaseNum = pay1.DatabaseNum,
                MasterAccountNum = pay1.MasterAccountNum,
                PaymentUuid = pay1.PaymentUuid,
                PaymentNumber = pay1.PaymentNumber,
                CustomerCode = pay1.CustomerCode,
                TransDate = pay1.TransDate,
                PaidBy = pay1.PaidBy,
                BankAccountUuid = pay1.BankAccountUuid,
                BankAccountCode = pay1.BankAccountCode,
                CheckNum = pay1.CheckNum,
                AuthCode = pay1.AuthCode,
                Currency = pay1.Currency,
                TotalAmount = 0
            };

            // load first invoice
            var invoice1 = await InvoiceService.GetInvoiceHeaderAsync(pay1.InvoiceUuid);

            // load outstanding invoice list for customer
            if (!await LoadInvoiceListAsync(payload, invoice1.CustomerCode))
            {
                payload.InvoiceList = new List<InvoiceListForPayment>();
            }

            // Get invoiceUuid which exist payment but not in outstanding invoice list
            var invoiceUuids = payments.Select(p =>
            {
                return p != null && !string.IsNullOrEmpty(p.InvoiceUuid) && payload.InvoiceList.FindByInvoiceUuid(p.InvoiceUuid) == null
                    ? p.InvoiceUuid
                    : null;
            }).Where(x => !string.IsNullOrEmpty(x)).ToList();
            // load this payment invoice list
            if (invoiceUuids != null && invoiceUuids.Count > 0)
            {
                var newInvoiceList = await LoadInvoiceListByUuidsAsync(payload, invoiceUuids);
                if (newInvoiceList != null && newInvoiceList.Count > 0)
                {
                    foreach (var item in newInvoiceList)
                        payload.InvoiceList.Add(item);
                }
            }

            if (payload.InvoiceList == null || payload.InvoiceList.Count == 0)
                return payload.ReturnError($"Payment number {paymentNumber} not found.");

            // add exist payment amount and TransUuid tp invoice list
            foreach (var pay in payments)
            {
                if (pay == null) continue;
                // find invoiceUuid, if not exist in outstanding invoice list, need load this invoice later
                var ins = payload.InvoiceList.FindByInvoiceUuid(pay.InvoiceUuid);
                if (ins == null) continue;
                ins.TransRowNum = pay.RowNum;
                ins.TransUuid = pay.TransUuid;
                ins.TransNum = pay.TransNum;
                ins.Description = pay.Description;
                ins.Notes = pay.Notes;
                ins.pay = pay.TotalAmount;
                ins.paidAmount -= ins.pay;
                ins.balance += ins.pay;

                payload.InvoiceTransaction.TotalAmount += pay.TotalAmount;
            }
            payload.InvoiceList = payload.InvoiceList
                .OrderByDescending(x => x.pay)
                .OrderBy(x => x.invoiceDate)
                .ToList();

            return true;
        }

        public async Task<bool> LoadInvoiceListAsync(InvoiceNewPaymentPayload payload, string customerCode)
        {
            var invoicePayload = new InvoicePayload()
            {
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                LoadAll = true
            };

            var invoiceQuery = new InvoiceQuery();
            invoiceQuery.InitForNewPaymet(customerCode);

            var srv = new InvoiceList(this.dbFactory, invoiceQuery);
            await srv.GetInvoiceListAsync(invoicePayload);

            if (!invoicePayload.Success)
                this.Messages = this.Messages.Concat(srv.Messages).ToList();

            payload.InvoiceList = invoicePayload.InvoiceList.ToObject<IList<InvoiceListForPayment>>();

            payload.InvoiceListCount = invoicePayload.InvoiceListCount;

            return invoicePayload.Success;
        }

        public async Task<IList<InvoiceListForPayment>> LoadInvoiceListByUuidsAsync(InvoiceNewPaymentPayload payload, IList<string> uuids)
        {
            var invoicePayload = new InvoicePayload()
            {
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                LoadAll = true
            };

            var invoiceQuery = new InvoiceQuery();
            invoiceQuery.InvoiceUuid.SetMultipleFilterValueList(uuids);
            invoiceQuery.InvoiceUuid.FilterMode = FilterBy.eq;
            invoiceQuery.DisableDate();

            var srv = new InvoiceList(this.dbFactory, invoiceQuery);
            await srv.GetInvoiceListAsync(invoicePayload);

            return invoicePayload.InvoiceList.ToObject<IList<InvoiceListForPayment>>();
        }

        #endregion

        #region To qbo queue 

        private QboPaymentClient _qboPaymentClient;

        protected QboPaymentClient qboPaymentClient
        {
            get
            {
                if (_qboPaymentClient is null)
                    _qboPaymentClient = new QboPaymentClient();
                return _qboPaymentClient;
            }
        }

        /// <summary>
        /// convert erp invoice payment to a queue message then put it to qbo queue
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns></returns>
        public async Task AddQboPaymentEventAsync(int masterAccountNum, int profileNum, IList<ApplyInvoice> applyInvoices)
        {
            if (applyInvoices == null || applyInvoices.Count == 0) return;

            foreach (var item in applyInvoices)
            {
                if (!item.Success)
                    continue;

                var eventDto = new AddErpEventDto()
                {
                    MasterAccountNum = masterAccountNum,
                    ProfileNum = profileNum,
                    ProcessUuid = item.TransUuid,
                };
                //await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksPayment");
                await qboPaymentClient.SendAddQboPaymentAsync(eventDto);
            }
        }

        public async Task<bool> AddQboPaymentEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddErpEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.InvoiceTransaction.TransUuid,
            };
            return await qboPaymentClient.SendAddQboPaymentAsync(eventDto);
            //return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksPayment");
        }

        public async Task<bool> DeleteQboPaymentEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddErpEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.InvoiceTransaction.TransUuid,
            };
            return await qboPaymentClient.SendDeleteQboPaymentAsync(eventDto);
            //return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksPaymentDelete");
        }
        #endregion

        #region Handle prepayment for auto transfer invoice from shipment.

        protected async Task<bool> GetPaymentDataAsync(string invoiceUuid, string miscInvoiceUuid, decimal amount)
        {
            if (!await LoadInvoiceAsync(invoiceUuid))
            {
                return false;
            }

            var header = Data.InvoiceData.InvoiceHeader;

            var trans = new InvoiceTransaction()
            {
                ProfileNum = header.ProfileNum,
                MasterAccountNum = header.MasterAccountNum,
                DatabaseNum = header.DatabaseNum,

                TransUuid = Guid.NewGuid().ToString(),
                TransType = (int)TransTypeEnum.Payment,
                TransStatus = (int)TransStatus.Paid,

                Currency = header.Currency,
                InvoiceNumber = header.InvoiceNumber,
                InvoiceUuid = header.InvoiceUuid,
                TaxRate = header.TaxRate,

                TotalAmount = amount,
                PaidBy = (int)PaidByAr.PrePayment,
                CheckNum = miscInvoiceUuid,//TODO
                AuthCode = miscInvoiceUuid,
                Description = "Add payment from prepayment",
            };

            using (var tx = new ScopedTransaction(dbFactory))
            {
                trans.TransNum = InvoiceTransactionHelper.GetTranSeqNum(header.InvoiceNumber, header.ProfileNum);
            }

            Data.InvoiceTransaction = trans;
            Data.InvoiceReturnItems = null;

            return true;
        }

        /// <summary>
        /// Handle prepayment
        /// Add payment and payInvoice
        /// Add miscpayment and withdraw from misinvoice 
        /// </summary>
        /// <param name="miscInvoiceUuid"></param>
        /// <param name="invoiceUuid"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string invoiceUuid, decimal amount, string miscInvoiceUuid)
        {
            if (miscInvoiceUuid.IsZero())
            {
                AddError($"miscInvoiceUuid cann't be empty");
                return false;
            }

            if (invoiceUuid.IsZero())
            {
                AddError($"invoiceUuid cann't be empty");
                return false;
            }

            if (amount.IsZero())
            {
                AddError($"amount cann't be zero.");
                return false;
            }

            //get actual amount from misinvoice. 
            var actualApplyAmount = await MiscPaymentService.GetCanApplyAmount(miscInvoiceUuid, amount);

            // while actualApplyAmount is zero, no need to add payment & update invoice and add mispayment and update misinvoice.
            if (actualApplyAmount.IsZero())
            {
                AddError($"Misc invoice balance is zero.");
                return true;// this will not affect the transfer flow.
            }

            if (!await GetPaymentDataAsync(invoiceUuid, miscInvoiceUuid, actualApplyAmount))
            {
                return false;
            }

            // add payment and pay invoice.
            return !await base.AddAsync();
        }

        #endregion

        #region Save misc invoice payment

        /// <summary>
        /// Get RowNum of Misc payment by regular payment transUuid
        /// If PaidBy = CreditMemo and TransUuid = AuthCode of credit payment
        /// </summary>
        public async Task<long> GetRowNumForMiscPaymentAsync(string transUuid)
        {
            if (string.IsNullOrEmpty(transUuid))
                return 0;

            var sql = $@"
SELECT trs.RowNum
FROM MiscInvoiceTransaction trs
WHERE Exists (SELECT RowNum FROM InvoiceTransaction WHERE TransUuid = @0 AND PaidBy=@1)
AND trs.AuthCode = @2 
";
            return await dbFactory.Db.ExecuteScalarAsync<long>(
                sql,
                transUuid.ToSqlParameter("@0"),
                ((int)PaidByAr.PrePayment).ToSqlParameter("@1"),
                transUuid.ToSqlParameter("@2")
            );
        }

        protected async Task<bool> DeleteMiscPaymentAsync(InvoiceTransactionData data)
        {
            if (data == null || data.InvoiceTransaction == null)
                return false;

            var rowNum = await GetRowNumForMiscPaymentAsync(data.InvoiceTransaction.TransUuid);
            if (rowNum <= 0)
                return false;

            var miscService = MiscPaymentService;
            return await miscService.DeleteAsync(rowNum);
        }

        protected async Task<bool> AddMiscPaymentAsync(InvoiceTransactionData data)
        {
            if (data == null || data.InvoiceTransaction == null)
                return false;
            if (data.InvoiceTransaction.PaidBy != (int)PaidByAr.PrePayment)
                return false;

            var pay = data.InvoiceTransaction;

            var rowNum = await GetRowNumForMiscPaymentAsync(data.InvoiceTransaction.TransUuid);

            var miscService = MiscPaymentService;
            if (rowNum <= 0 || !(await miscService.EditAsync(rowNum)))
            {
                miscService.Add();
                miscService.NewData();
            }
            miscService.Data.MiscInvoiceTransaction = await GenerateMiscPayment(miscService.Data.MiscInvoiceTransaction, pay);
            return await miscService.SaveDataAsync();
        }

        protected async Task<MiscInvoiceTransaction> GenerateMiscPayment(MiscInvoiceTransaction trs, InvoiceTransaction pay)
        {
            trs.DatabaseNum = pay.DatabaseNum;
            trs.MasterAccountNum = pay.MasterAccountNum;
            trs.ProfileNum = pay.ProfileNum;

            trs.MiscInvoiceUuid = pay.AuthCode;
            trs.MiscInvoiceNumber = pay.CheckNum;
            trs.CheckNum = pay.InvoiceNumber;
            trs.AuthCode = pay.TransUuid;
            trs.TransType = (int)TransType.Payment;
            trs.TotalAmount = pay.TotalAmount;

            trs.TransUuid = Guid.NewGuid().ToString();
            trs.TransNum = await MiscPaymentService.GetNextTransNumAsync(trs.MiscInvoiceUuid);
            trs.TransStatus = 0;
            trs.TransDate = pay.TransDate;
            trs.TransTime = pay.TransTime;
            trs.Description = pay.Description;
            trs.Notes = pay.Notes;
            trs.PaidBy = pay.PaidBy;
            trs.BankAccountUuid = pay.BankAccountUuid;
            trs.BankAccountCode = pay.BankAccountCode;
            trs.Currency = pay.Currency;
            trs.CreditAccount = pay.CreditAccount;
            trs.DebitAccount = pay.DebitAccount;
            //trs.TransSourceCode = 
            return trs;
        }

        //protected async Task<bool> AddMiscPaymentAsync()
        //{
        //    var paymentData = Data.InvoiceTransaction;

        //    if (paymentData.PaidBy != (int)PaidByAr.PrePayment)
        //        return true;

        //    //Add mis payment and withdraw from misinvoice
        //    if (!await MiscPaymentService.AddMiscPayment(paymentData.AuthCode, paymentData.TransUuid, paymentData.InvoiceNumber, paymentData.TotalAmount))
        //    {
        //        this.Messages.Add(MiscPaymentService.Messages);
        //        return false;
        //    }
        //    return true;
        //}

        ////TODO need MiscPaymentService provide sync method
        //protected bool AddMiscPayment()
        //{
        //    throw new Exception("Please Add sync AddMiscPayment to MiscPaymentService");

        //    //var paymentData = Data.InvoiceTransaction;

        //    //if (paymentData.PaidBy != (int)PaidByAr.CreditMemo)
        //    //    return true;

        //    ////Add mis payment and withdraw from misinvoice
        //    //if (!await MiscPaymentService.AddMiscPayment(paymentData.AuthCode, paymentData.TransUuid, paymentData.InvoiceNumber, paymentData.TotalAmount))
        //    //{
        //    //    this.Messages.Add(MiscPaymentService.Messages);
        //    //    return false;
        //    //}
        //    //return true;
        //}


        //protected async Task<bool> UpdateMiscPaymentAsync()
        //{
        //    var success = true;
        //    //original paydby is creditmemo then delete mispayment.
        //    if (lastPaidByBeforeUpdate == (int)PaidByAr.PrePayment)
        //    {
        //        success = await MiscPaymentService.DeleteMiscPayment(lastAuthCodeBeforeUpdate, lastTransUuidBeforeUpdate, Data.InvoiceTransaction.OriginalPaidAmount);
        //    }

        //    //new paydby is creditmemo then add miscpayment
        //    if (Data.InvoiceTransaction.PaidBy == (int)PaidByAr.PrePayment)
        //    {
        //        success = success && await AddMiscPaymentAsync();
        //    }

        //    return success;
        //}

        ////TODO need MiscPaymentService provide sync method
        //protected bool UpdateMiscPayment()
        //{
        //    throw new Exception("Please Add sync UpdateMiscPayment to MiscPaymentService");
        //    //var success = true;
        //    ////original paydby is creditmemo then delete mispayment.
        //    //if (lastPaidByBeforeUpdate == (int)PaidByAr.CreditMemo)
        //    //{
        //    //    success = await MiscPaymentService.DeleteMiscPayment(lastAuthCodeBeforeUpdate, lastTransUuidBeforeUpdate, Data.InvoiceTransaction.OriginalPaidAmount);
        //    //}

        //    ////new paydby is creditmemo then add miscpayment
        //    //if (Data.InvoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
        //    //{
        //    //    success = success && await AddMiscPaymentAsync();
        //    //}

        //    //return success;
        //}
        #endregion 

        #region Save credit invoice payment

        /// <summary>
        /// Get RowNum of credit payment by regular payment transUuid
        /// If PaidBy = CreditMemo and TransUuid = AuthCode of credit payment
        /// </summary>
        public async Task<long> GetRowNumForCreditPaymentAsync(string transUuid)
        {
            if (string.IsNullOrEmpty(transUuid)) 
                return 0;

            var sql = $@"
SELECT trs.RowNum
FROM InvoiceTransaction trs
WHERE Exists (SELECT RowNum FROM InvoiceTransaction WHERE TransUuid = @0 AND PaidBy=@1)
AND trs.AuthCode = @2 
";
            return await dbFactory.Db.ExecuteScalarAsync<long>(
                sql,
                transUuid.ToSqlParameter("@0"),
                ((int)PaidByAr.CreditMemo).ToSqlParameter("@1"),
                transUuid.ToSqlParameter("@2")
            );
        }

        protected async Task<bool> DeleteCreditPaymentAsync(InvoiceTransactionData data)
        {
            if (data == null || data.InvoiceTransaction == null)
                return false;

            var rowNum = await GetRowNumForCreditPaymentAsync(data.InvoiceTransaction.TransUuid);
            if (rowNum <= 0) 
                return false;

            var creditService = AnotherInvoicePaymentService;
            return await creditService.DeleteAsync(rowNum);
        }

        protected async Task<bool> AddCreditPaymentAsync(InvoiceTransactionData data)
        {
            if (data == null || data.InvoiceTransaction == null)
                return false;
            if (data.InvoiceTransaction.PaidBy != (int)PaidByAr.CreditMemo)
                return false;

            var pay = data.InvoiceTransaction;

            var rowNum = await GetRowNumForCreditPaymentAsync(data.InvoiceTransaction.TransUuid);

            var creditService = AnotherInvoicePaymentService;
            if (rowNum <= 0 || !(await creditService.EditAsync(rowNum)))
            {
                creditService.Add();
                creditService.NewData();
            }
            creditService.Data.InvoiceTransaction = await GenerateCreditPayment(creditService.Data.InvoiceTransaction, pay);

            await creditService.LoadInvoiceAsync(creditService.Data.InvoiceTransaction.InvoiceNumber, creditService.Data.InvoiceTransaction.ProfileNum, creditService.Data.InvoiceTransaction.MasterAccountNum);
            return await creditService.SaveDataAsync();
        }

        protected async Task<InvoiceTransaction> GenerateCreditPayment(InvoiceTransaction trs, InvoiceTransaction pay)
        {
            trs.DatabaseNum = pay.DatabaseNum;
            trs.MasterAccountNum = pay.MasterAccountNum;
            trs.ProfileNum = pay.ProfileNum;

            trs.InvoiceUuid = pay.AuthCode;
            trs.InvoiceNumber = pay.CheckNum;
            trs.CheckNum = pay.InvoiceNumber;
            trs.AuthCode = pay.TransUuid;
            trs.TransType = (int)TransType.CreditPayment;
            trs.TotalAmount = -pay.TotalAmount;

            trs.TransUuid = Guid.NewGuid().ToString();
            trs.TransNum = await GetNextTransNumAsync(trs.InvoiceUuid);
            trs.PaymentUuid = string.Empty;
            trs.PaymentNumber = 0;
            trs.TransStatus = 0;
            trs.TransDate = pay.TransDate;
            trs.TransTime = pay.TransTime;
            trs.Description = pay.Description;
            trs.Notes = pay.Notes;
            trs.PaidBy = pay.PaidBy;
            trs.BankAccountUuid = pay.BankAccountUuid;
            trs.BankAccountCode = pay.BankAccountCode;
            trs.Currency = pay.Currency;
            trs.ExchangeRate = pay.ExchangeRate;
            trs.CreditAccount = pay.CreditAccount;
            trs.DebitAccount = pay.DebitAccount;
            //trs.TransSourceCode = 
            return trs;
        }

        #endregion 

        public async Task<long> GetNextPaymentNumberAsync(int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COALESCE(MAX(PaymentNumber), 0) 
FROM InvoiceTransaction
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
FROM InvoiceTransaction
WHERE InvoiceUuid=@0
";
            return await dbFactory.Db.ExecuteScalarAsync<int>(
                sql,
                invoiceUuid.ToSqlParameter("@0")
            ) + 1;
        }

        public async Task<IList<InvoiceTransaction>> GetByPaymentNumberAsync(long paymentNumber, int masterAccountNum, int profileNum)
        {
            var sql = $@"WHERE MasterAccountNum=@0 AND ProfileNum=@1 AND TransType = 1 AND PaymentNumber=@2";
            return (await dbFactory.FindAsync<InvoiceTransaction>(
                sql,
                masterAccountNum,
                profileNum,
                paymentNumber
            )).ToList();
        }

    }
}



