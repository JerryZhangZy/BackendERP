


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
        MiscInvoicePaymentService _miscServicePayment;
        MiscInvoicePaymentService MiscPaymentService
        {
            get
            {
                if (_miscServicePayment == null) _miscServicePayment = new MiscInvoicePaymentService(dbFactory);
                return _miscServicePayment;
            }
        }
        public InvoicePaymentService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public override InvoiceTransactionService Init()
        {
            SetDtoMapper(new InvoiceTransactionDataDtoMapperDefault());
            SetCalculator(new InvoicePaymentServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new InvoicePaymentServiceValidatorDefault(this, this.dbFactory));
            return this;
        }

        /// <summary>
        /// Add to ActivityLog record for current data and processMode
        /// Should Call this method after successful save, update, delete
        /// </summary>
        protected void AddActivityLogForCurrentData()
        {
            this.AddActivityLog(new ActivityLog(dbFactory)
            {
                Type = (int)ActivityLogType.InvoicePayment,
                Action = (int)this.ProcessMode,
                LogSource = "InvoicePaymentService",

                MasterAccountNum = this.Data.InvoiceTransaction.MasterAccountNum,
                ProfileNum = this.Data.InvoiceTransaction.ProfileNum,
                DatabaseNum = this.Data.InvoiceTransaction.DatabaseNum,
                ProcessUuid = this.Data.InvoiceTransaction.TransUuid,
                ProcessNumber = this.Data.InvoiceTransaction.TransNum.ToString(),
                ChannelNum = this.Data.InvoiceData.InvoiceHeaderInfo.ChannelAccountNum,
                ChannelAccountNum = this.Data.InvoiceData.InvoiceHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty
            });
        }

        /// <summary>
        /// Add to ActivityLog record for current data and processMode
        /// Should Call this method after successful save, update, delete
        /// </summary>
        protected async Task AddActivityLogForCurrentDataAsync()
        {
            await this.AddActivityLogAsync(new ActivityLog(dbFactory)
            {
                Type = (int)ActivityLogType.InvoicePayment,
                Action = (int)this.ProcessMode,
                LogSource = "InvoicePaymentService",

                MasterAccountNum = this.Data.InvoiceTransaction.MasterAccountNum,
                ProfileNum = this.Data.InvoiceTransaction.ProfileNum,
                DatabaseNum = this.Data.InvoiceTransaction.DatabaseNum,
                ProcessUuid = this.Data.InvoiceTransaction.TransUuid,
                ProcessNumber = this.Data.InvoiceTransaction.TransNum.ToString(),
                ChannelNum = this.Data.InvoiceData.InvoiceHeaderInfo.ChannelAccountNum,
                ChannelAccountNum = this.Data.InvoiceData.InvoiceHeaderInfo.ChannelAccountNum,

                LogMessage = string.Empty
            });
        }

        /// <summary>
        /// Get invoice payment with detail and invoiceheader by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task GetPaymentWithInvoiceHeaderAsync(InvoicePaymentPayload payload, string invoiceNumber, int? transNum = null)
        {
            payload.InvoiceTransactions = await GetDtoListAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, TransTypeEnum.Payment, transNum);
            payload.InvoiceHeader = await GetInvoiceHeaderAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber);
            payload.Success = true;
            payload.Messages = this.Messages;
        }

        public async Task<bool> ExistCheckNumber(string number, int masterAccountNum, int profileNum)
        {
            return await InvoicePaymentHelper.ExistCheckNumberAsync(number, masterAccountNum, profileNum);
        }

        public async Task CreatePaymentForCustomerAsync(InvoicePaymentPayload payload, string customerCode)
        {
            var invoices = await GetInvoiceHeadersByCustomerAsync(payload.MasterAccountNum, payload.ProfileNum, customerCode);
            payload.InvoiceHeaders = invoices;
            payload.ApplyInvoices = invoices.Select(i => new ApplyInvoice { 
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
                    AddError($"Add payment failed for InvoiceNumber:{applyInvoice.InvoiceNumber} ");
                    continue;
                }
                this.AddActivityLogForCurrentData();
                successCount++;

                applyInvoice.TransUuid = transaction.InvoiceTransaction.TransUuid;
                applyInvoice.TransRowNum = transaction.InvoiceTransaction.RowNum;

                if (await PayInvoiceAsync(applyInvoice, payload.MasterAccountNum, payload.ProfileNum))
                {
                    var invoiceTransaction = Data.InvoiceTransaction;
                    if (invoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
                    {
                        payload.Success = await MiscPaymentService.AddMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.TransUuid, invoiceTransaction.InvoiceNumber, invoiceTransaction.TotalAmount);
                        if (!payload.Success)
                        {
                            AddError("Add miscInvoice fail");
                        }
                    }
                }
                else
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"{applyInvoice.InvoiceNumber} is not applied.");
                }
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
                var paymentPayload = GetPayload(payload, applyInvoice);
                if (!await base.AddAsync(paymentPayload))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Add payment failed for InvoiceNumber:{applyInvoice.InvoiceNumber} ");
                    continue;
                }
                this.AddActivityLogForCurrentData();
                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;

                //add payment success. then pay invoice.
                var success = await PayInvoiceAsync(applyInvoice, payload.MasterAccountNum, payload.ProfileNum);
                if (success)
                {
                    var invoiceTransaction = Data.InvoiceTransaction;
                    if (invoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
                    {
                        payload.Success = await MiscPaymentService.AddMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.TransUuid, invoiceTransaction.InvoiceNumber, invoiceTransaction.TotalAmount);
                        if (!payload.Success)
                        {
                            AddError("Add miscInvoice fail");
                        }
                    }
                }
                else
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"{applyInvoice.InvoiceNumber} is not applied.");
                }
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
                var paymentPayload = GetPayload(payload, applyInvoice);
                if (!base.Add(paymentPayload))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Add payment failed for InvoiceNumber:{applyInvoice.InvoiceNumber} ");
                    continue;
                }
                this.AddActivityLogForCurrentData();
                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;

                //add payment success. then pay invoice.
                var success = PayInvoice(applyInvoice, payload.MasterAccountNum, payload.ProfileNum);
                if (!success)
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"{applyInvoice.InvoiceNumber} is not applied.");
                }
            }

            return payload.Success;
        }

        protected InvoicePaymentPayload GetPayload(InvoiceTransaction originalTrans, ApplyInvoice applyInvoice)
        {
            var invoiceTransactionDto = new InvoiceTransactionDto();
            var mapper = (base.DtoMapper as InvoiceTransactionDataDtoMapperDefault);
            mapper.WriteInvoiceTransaction(originalTrans, invoiceTransactionDto);
            invoiceTransactionDto.TotalAmount = applyInvoice.PaidAmount;
            invoiceTransactionDto.InvoiceNumber = applyInvoice.InvoiceNumber;
            invoiceTransactionDto.InvoiceUuid = applyInvoice.InvoiceUuid;
            invoiceTransactionDto.RowNum = applyInvoice.TransRowNum;

            //var trans = new InvoiceTransactionDto()
            //{
            //    AuthCode = originalTrans.AuthCode,
            //    BankAccountCode = originalTrans.BankAccountCode,
            //    BankAccountUuid = originalTrans.BankAccountUuid,
            //    CheckNum = originalTrans.CheckNum,
            //    CreditAccount = originalTrans.CreditAccount,
            //    Currency = originalTrans.Currency,
            //    DebitAccount = originalTrans.DebitAccount,
            //    ExchangeRate = originalTrans.ExchangeRate,
            //    PaidBy = originalTrans.PaidBy,

            //    TotalAmount = applyInvoice.PaidAmount,

            //    InvoiceNumber = applyInvoice.InvoiceNumber,
            //    InvoiceUuid = applyInvoice.InvoiceUuid,
            //    Description = originalTrans.Description,
            //    Notes = originalTrans.Notes,
            //    EnterBy = originalTrans.EnterBy,
            //    TransDate = originalTrans.TransDate,
            //    TransTime = originalTrans.TransTime.ToDateTime(),
            //    TaxRate = originalTrans.TaxRate,
            //    TransSourceCode = originalTrans.TransSourceCode,
            //    TransStatus = originalTrans.TransStatus,
            //    MasterAccountNum = originalTrans.MasterAccountNum,
            //    ProfileNum = originalTrans.ProfileNum,
            //    RowNum = applyInvoice.TransRowNum
            //};
            using (var tx = new ScopedTransaction(dbFactory))
            {
                invoiceTransactionDto.TransNum = InvoiceTransactionHelper.GetTranSeqNum(invoiceTransactionDto.InvoiceNumber, originalTrans.ProfileNum);
            }

            var dataDto = new InvoiceTransactionDataDto()
            {
                InvoiceTransaction = invoiceTransactionDto
            };
            return new InvoicePaymentPayload()
            {
                ProfileNum = invoiceTransactionDto.ProfileNum.Value,
                MasterAccountNum = invoiceTransactionDto.MasterAccountNum.Value,
                InvoiceTransaction = dataDto,
                DatabaseNum = invoiceTransactionDto.DatabaseNum.Value,
            };
        }

        protected InvoicePaymentPayload GetPayload(InvoicePaymentPayload payload, ApplyInvoice applyInvoice)
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
                RowNum = applyInvoice.TransRowNum
            };
            using (var tx = new ScopedTransaction(dbFactory))
            {
                trans.TransNum = InvoiceTransactionHelper.GetTranSeqNum(trans.InvoiceNumber, payload.ProfileNum);
            }

            var dataDto = new InvoiceTransactionDataDto()
            {
                InvoiceTransaction = trans
            };
            return new InvoicePaymentPayload()
            {
                ProfileNum = payload.ProfileNum,
                MasterAccountNum = payload.MasterAccountNum,
                InvoiceTransaction = dataDto,
                DatabaseNum = payload.DatabaseNum,
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

        protected async Task<bool> PayInvoiceAsync(ApplyInvoice applyInvoice, int masterAccountNum, int profileNum)
        {
            var changedPaidAmount = applyInvoice.PaidAmount - Data.InvoiceTransaction.OriginalPaidAmount;
            using (var trs = new ScopedTransaction(dbFactory))
                return await InvoiceHelper.PayInvoiceAsync(applyInvoice.InvoiceNumber, changedPaidAmount, masterAccountNum, profileNum);
        }
        protected async Task<bool> PayInvoiceAsync(string invoiceNumber, int masterAccountNum, int profileNum, decimal paidAmount)
        {
            using (var trs = new ScopedTransaction(dbFactory))
                return await InvoiceHelper.PayInvoiceAsync(invoiceNumber, paidAmount, masterAccountNum, profileNum);
        }

        protected bool PayInvoice(ApplyInvoice applyInvoice, int masterAccountNum, int profileNum)
        {
            var changedPaidAmount = applyInvoice.PaidAmount - Data.InvoiceTransaction.OriginalPaidAmount;
            using (var trs = new ScopedTransaction(dbFactory))
                return InvoiceHelper.PayInvoice(applyInvoice.InvoiceNumber, changedPaidAmount, masterAccountNum, profileNum);
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
                var paymentPayload = GetPayload(payload, applyInvoice);
                if (await base.UpdateAsync(paymentPayload))
                {
                    this.AddActivityLogForCurrentData();
                    var invoiceTransaction = Data.InvoiceTransaction;
                    if (lastPaidByBeforeUpdate == (int)PaidByAr.CreditMemo)
                        payload.Success = payload.Success && await MiscPaymentService.DeleteMiscPayment(lastAuthCodeBeforeUpdate, lastTransUuidBeforeUpdate, invoiceTransaction.OriginalPaidAmount);
                    if (payload.Success)
                        if (Data.InvoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
                        {
                            payload.Success = payload.Success && await MiscPaymentService.AddMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.TransUuid, invoiceTransaction.InvoiceNumber, invoiceTransaction.TotalAmount);
                        }
                }
                else
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Update payment failed for InvoiceNumber:{applyInvoice.InvoiceNumber} ");
                    continue;
                }

                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;

                //update payment success. then pay invoice.
                var success = await PayInvoiceAsync(applyInvoice, payload.MasterAccountNum, payload.ProfileNum);
                if (!success)
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"{applyInvoice.InvoiceNumber} is not applied.");
                }
            }

            return payload.Success;
        }

        public async Task UpdatePayment(InvoicePaymentPayload data)
        {
            Edit();
            data.Success = true;
            string invoiceNumber = "";
            ApplyInvoice applyInvoice = data.ApplyInvoices.First();
            if (await base.UpdateAsync(data))
            {
                this.AddActivityLogForCurrentData();
                var invoiceTransaction = Data.InvoiceTransaction;
                invoiceNumber = data.InvoiceHeader.InvoiceNumber;
                if (lastPaidByBeforeUpdate == (int)PaidByAr.CreditMemo)
                    data.Success &= await MiscPaymentService.DeleteMiscPayment(lastAuthCodeBeforeUpdate, lastTransUuidBeforeUpdate, invoiceTransaction.OriginalPaidAmount);
                if (data.Success && Data.InvoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
                    data.Success &= await MiscPaymentService.AddMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.TransUuid, invoiceTransaction.InvoiceNumber, invoiceTransaction.TotalAmount);
            }
            else
            {
                data.Success = false;
                AddError($"Update payment failed for InvoiceNumber: {invoiceNumber} ");
            }

            //update payment success. then pay invoice.
            var success = await PayInvoiceAsync(applyInvoice, data.MasterAccountNum, data.ProfileNum);
            if (!success)
            {
                data.Success = false;
                applyInvoice.Success = false;
                AddError($"{applyInvoice.InvoiceNumber} is not applied.");
            }

            applyInvoice.Success = true;
        }

        public async virtual Task<bool> UpdateInvoicePayments(InvoicePaymentPayload payload)
        {
            payload.Success = true;
            var mapper = DtoMapper as InvoiceTransactionDataDtoMapperDefault;
            List<InvoiceTransactionData> readyToUpdate = new List<InvoiceTransactionData>();
            Dictionary<string, decimal> dictionaryOfNewBatchPaymentSumAmount = new Dictionary<string, decimal>();

            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                List();
                await GetDataAsync(applyInvoice.TransRowNum.Value);
                readyToUpdate.Add(this.Data);

                string batchId = this.Data.InvoiceTransaction.PaymentUuid;
                decimal amount = this.Data.InvoiceTransaction.TotalAmount;
                if (dictionaryOfNewBatchPaymentSumAmount.ContainsKey(batchId))
                    dictionaryOfNewBatchPaymentSumAmount[batchId] += amount;
                else
                    dictionaryOfNewBatchPaymentSumAmount.Add(batchId, amount);
            }

            var dictionaryOfOriginalBatchPaymentSumAmount = readyToUpdate.GroupBy(up => up.InvoiceTransaction.PaymentUuid)
                .Select(g => new KeyValuePair<string, decimal>(
                    g.Key, 
                    g.Sum(p => p.InvoiceTransaction.TotalAmount))
                ).ToList();

            dictionaryOfOriginalBatchPaymentSumAmount.ForEach(p => {
                if (dictionaryOfNewBatchPaymentSumAmount[p.Key] != p.Value)
                    dictionaryOfNewBatchPaymentSumAmount.Remove(p.Key);
            });

            foreach (var item in dictionaryOfNewBatchPaymentSumAmount)
            {
                readyToUpdate.Where(up => up.InvoiceTransaction.PaymentUuid == item.Key)
                    .ToList().ForEach(async up => {
                        var applyInvoice = payload.ApplyInvoices.Single(a => a.TransUuid == up.InvoiceTransaction.TransUuid);
                        var updatePayload = new InvoicePaymentPayload
                        {
                            DatabaseNum = payload.DatabaseNum,
                            MasterAccountNum = payload.MasterAccountNum,
                            ProfileNum = payload.ProfileNum,
                            InvoiceTransaction = ToDto(up),
                            ApplyInvoices = new List<ApplyInvoice> { applyInvoice }
                        };
                        await UpdatePayment(updatePayload);
                        payload.Success &= updatePayload.Success;
                    });
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
                var paymentPayload = GetPayload(payload, applyInvoice);
                if (base.Update(paymentPayload))
                {
                    this.AddActivityLogForCurrentData();
                    var invoiceTransaction = Data.InvoiceTransaction;
                    if (lastPaidByBeforeUpdate == (int)PaidByAr.CreditMemo)
                        payload.Success = payload.Success && MiscPaymentService.DeleteMiscPayment(lastAuthCodeBeforeUpdate, lastTransUuidBeforeUpdate, invoiceTransaction.OriginalPaidAmount).Result;
                    if (payload.Success)
                        if (Data.InvoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
                        {
                            payload.Success = payload.Success && MiscPaymentService.AddMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.TransUuid, invoiceTransaction.InvoiceNumber, invoiceTransaction.TotalAmount).Result;
                        }
                }
                else
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Update payment failed for InvoiceNumber:{applyInvoice.InvoiceNumber} ");
                    continue;
                }

                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;

                //update payment success. then pay invoice.
                var success = PayInvoice(applyInvoice, payload.MasterAccountNum, payload.ProfileNum);
                if (!success)
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"{applyInvoice.InvoiceNumber} is not applied.");
                }
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
        public virtual async Task<bool> DeleteByNumberAsync(InvoicePaymentPayload payload, string invoiceNumber, int transNum)
        {
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, transNum);
            if (success && Data.InvoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
            {
                var invoiceTransaction = Data.InvoiceTransaction;
                success = await MiscPaymentService.DeleteMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.TransUuid, invoiceTransaction.TotalAmount);
                if (!success) AddError("Update Misc.Invoice payment failed");
            }
            success = success && DeleteData();
            if (success)
                this.AddActivityLogForCurrentData();
            return success;
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(InvoicePaymentPayload payload, string invoiceNumber, int transNum)
        {
            return base.DeleteByNumber(payload, invoiceNumber, TransTypeEnum.Payment, transNum);
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

            return await LoadInvoiceListAsync(payload, Data.InvoiceData.InvoiceHeader.CustomerCode);
        }

        public async Task<bool> NewPaymentByCustomerCode(InvoiceNewPaymentPayload payload, string customerCode)
        {
            if (!await LoadInvoiceListAsync(payload, customerCode))
            {
                return false;
            }
            //if (payload.InvoiceList.ToString().IsZero())
            //{
            //    AddError($"No outstanding invoice for customer:{customerCode}");
            //    return false;
            //}

            //var invoiceList = JsonConvert.DeserializeObject<List<InvoiceHeader>>(payload.InvoiceList.ToString());

            NewData();
            Data.InvoiceTransaction.CustomerCode = customerCode;

            // All unpaid amount.
            //Data.InvoiceTransaction.TotalAmount = invoiceList.Sum(i => i.Balance.IsZero() ? 0 : i.Balance);

            payload.InvoiceTransaction = this.ToDto().InvoiceTransaction;

            return true;
        }

        private async Task<bool> LoadInvoiceListAsync(InvoiceNewPaymentPayload payload, string customerCode)
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

            payload.InvoiceList = invoicePayload.InvoiceList;

            payload.InvoiceListCount = invoicePayload.InvoiceListCount;

            return invoicePayload.Success;
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

        #region Add payment for prepayment
        public async Task<bool> AddPaymentAndPayInvoiceForPrepaymentAsync(string miscInvoiceUuid, string invoiceUuid, decimal amount)
        {
            Add();
            if (miscInvoiceUuid.IsZero())
            {
                AddError($"miscInvoiceUuid is null");
                return false;
            }

            if (invoiceUuid.IsZero())
            {
                AddError($"invoiceUuid is null");
                return false;
            }
            if (!await LoadInvoiceAsync(invoiceUuid))
            {
                return false;
            }
            var header = Data.InvoiceData.InvoiceHeader;
            Data.InvoiceTransaction = new InvoiceTransaction()
            {
                ProfileNum = header.ProfileNum,
                MasterAccountNum = header.MasterAccountNum,
                DatabaseNum = header.DatabaseNum,

                TransDate = DateTime.UtcNow,
                TransTime = DateTime.UtcNow.TimeOfDay,
                TransType = (int)TransTypeEnum.Payment,
                TransStatus = (int)TransStatus.Paid,

                Currency = header.Currency,
                InvoiceNumber = header.InvoiceNumber,
                InvoiceUuid = header.InvoiceUuid,
                TaxRate = header.TaxRate,

                TotalAmount = amount,
                PaidBy = (int)PaidByAr.CreditMemo,
                CheckNum = miscInvoiceUuid,
                Description = "Add payment from prepayment",
            };

            using (var tx = new ScopedTransaction(dbFactory))
            {
                Data.InvoiceTransaction.TransNum = await InvoiceTransactionHelper.GetTranSeqNumAsync(header.InvoiceNumber, header.ProfileNum);
            }

            var success = await SaveDataAsync();
            if (!success)
            {
                AddError("AddPaymentAndPayInvoiceForPrepaymentAsync->SaveDataAsync error.");
                return false;
            }

            var trans = Data.InvoiceTransaction;
            //add payment success. then pay invoice.
            success = await PayInvoiceAsync(trans.InvoiceNumber, trans.MasterAccountNum, trans.ProfileNum, trans.TotalAmount);
            if (!success)
            {
                AddError($"AddPaymentAndPayInvoiceForPrepaymentAsync->PayInvoiceAsync error.");
                return false;
            }

            return true;
        }
        #endregion
    }
}



