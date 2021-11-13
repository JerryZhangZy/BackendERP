


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

        public async Task CreatePaymentForCustomerAsync(InvoicePaymentPayload payload, string customerCode)
        {
            var invoices = await GetInvoiceHeadersByCustomerAsync(payload.MasterAccountNum, payload.ProfileNum, customerCode);
            payload.InvoiceHeaders = invoices;
            payload.CustomerCode = customerCode;
            payload.ApplyInvoices = invoices.Select(i => new ApplyInvoice { 
                InvoiceNumber = i.InvoiceNumber,
                InvoiceUuid = i.InvoiceUuid
            }).ToList();

            payload.Success = true;
            payload.Messages = this.Messages;
        }

        public async Task AddPaymentsForInvoicesAsync(InvoicePaymentPayload payload)
        {
            if (!payload.HasApplyInvoices)
            {
                AddError("ApplyInvoices  is required.");
                payload.Success = false;
                return;
            }

            var invoices = await GetInvoiceHeadersByCustomerAsync(payload.MasterAccountNum, payload.ProfileNum, payload.CustomerCode);
            decimal sumOfAssignment = payload.ApplyInvoices.Sum(a => a.PaidAmount);
            if (payload.ApplyInvoices.Any(i => string.IsNullOrEmpty(i.InvoiceNumber)))
            {
                AddError("ApplyInvoices.InvoiceNumber is required.");
                payload.Success = false;
                return;
            }
            if (sumOfAssignment != payload.TotalAmount)
            {
                AddError("The total payment amount not equals sum of paid for each invoices");
                payload.Success = false;
                return;
            }

            StringBuilder errorMessage = new StringBuilder();
            int successCount = 0;
            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                var transaction = GenerateTransactionByInvoice(payload, applyInvoice.InvoiceUuid);
                if (!await base.AddAsync(transaction))
                {
                    applyInvoice.Success = false;
                    errorMessage.AppendLine($"Add payment failed for InvoiceNumber:{applyInvoice.InvoiceNumber} ");
                    continue;
                }
                successCount++;

                applyInvoice.TransUuid = transaction.InvoiceTransaction.TransUuid;

                if (await PayInvoiceAsync(applyInvoice, payload.MasterAccountNum, payload.ProfileNum))
                {
                    var invoiceTransaction = Data.InvoiceTransaction;
                    if (invoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
                    {
                        payload.Success = await MiscPaymentService.AddMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.InvoiceUuid, invoiceTransaction.InvoiceNumber, invoiceTransaction.TotalAmount);
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
            if (successCount != payload.ApplyInvoices.Count)
                AddError(errorMessage.ToString());
        }

        private InvoiceTransactionDataDto GenerateTransactionByInvoice(InvoicePaymentPayload payload, string invoiceUuid)
        {
            var invoice = payload.InvoiceHeaders.FirstOrDefault(i => i.InvoiceUuid == invoiceUuid);
            var result = new InvoiceTransactionDataDto 
            { 
                InvoiceTransaction = new InvoiceTransactionDto
                { 
                    InvoiceUuid = invoice.InvoiceUuid,
                    InvoiceNumber = invoice.InvoiceNumber,
                    CustomerCode = payload.CustomerCode,
                    TransType = 1,
                    ProfileNum = payload.ProfileNum,
                    MasterAccountNum = payload.MasterAccountNum,
                    TransTime = DateTime.UtcNow,
                }
            };
            throw new NotImplementedException();
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
            LoadInvoice(invoiceNumber, profileNum, masterAccountNum);
            return (payments, Data.InvoiceData);
        }

        #region add multi payments
        public virtual async Task<bool> AddAsync(InvoicePaymentPayload payload)
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
                if (!await base.AddAsync(paymentPayload))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Add payment failed for InvoiceNumber:{applyInvoice.InvoiceNumber} ");
                    continue;
                }

                applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;

                //add payment success. then pay invoice.
                var success = await PayInvoiceAsync(applyInvoice, payload.MasterAccountNum, payload.ProfileNum);
                if (success)
                {
                    var invoiceTransaction = Data.InvoiceTransaction;
                    if (invoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
                    {
                        payload.Success = await MiscPaymentService.AddMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.InvoiceUuid, invoiceTransaction.InvoiceNumber, invoiceTransaction.TotalAmount);
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
                    var invoiceTransaction = Data.InvoiceTransaction;
                    if (lastPaidByBeforeUpdate == (int)PaidByAr.CreditMemo)
                        payload.Success = payload.Success && await MiscPaymentService.AddMiscPayment(lastAuthCodeBeforeUpdate, lastInvoiceUuidBeforeUpdate, lastInvoiceNumberBeforeUpdate, -invoiceTransaction.OriginalPaidAmount);
                    if (payload.Success)
                        if (Data.InvoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
                        {
                            payload.Success = payload.Success && await MiscPaymentService.AddMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.InvoiceUuid, invoiceTransaction.InvoiceNumber, invoiceTransaction.TotalAmount);
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
                    var invoiceTransaction = Data.InvoiceTransaction;
                    if (lastPaidByBeforeUpdate == (int)PaidByAr.CreditMemo)
                        payload.Success = payload.Success && MiscPaymentService.AddMiscPayment(lastAuthCodeBeforeUpdate, lastInvoiceUuidBeforeUpdate, lastInvoiceNumberBeforeUpdate, -invoiceTransaction.OriginalPaidAmount).Result;
                    if (payload.Success)
                        if (Data.InvoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
                        {
                            payload.Success = payload.Success && MiscPaymentService.AddMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.InvoiceUuid, invoiceTransaction.InvoiceNumber, invoiceTransaction.TotalAmount).Result;
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
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber + "_" + (int)TransTypeEnum.Payment + "_" + transNum);
            if (success && Data.InvoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
            {
                var invoiceTransaction = Data.InvoiceTransaction;
                success = await MiscPaymentService.AddMiscPayment(invoiceTransaction.AuthCode, invoiceTransaction.InvoiceUuid, invoiceTransaction.InvoiceNumber, -invoiceTransaction.TotalAmount);
                if (!success) AddError("Update Misc.Invoice payment failed");
            }
            success = success && DeleteData();
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
            if (!LoadInvoice(invoiceNumber, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            CopyInvoiceHeaderToTrans();

            Data.InvoiceTransaction.CustomerCode = Data.InvoiceData.InvoiceHeader.CustomerCode;
            ////unpaid amount
            //Data.InvoiceTransaction.TotalAmount = Data.InvoiceData.InvoiceHeader.Balance.IsZero() ? 0 : Data.InvoiceData.InvoiceHeader.Balance;

            payload.InvoiceTransaction = this.ToDto().InvoiceTransaction;

            return await LoadInvoiceList(payload, Data.InvoiceData.InvoiceHeader.CustomerCode);
        }

        public async Task<bool> NewPaymentByCustomerCode(InvoiceNewPaymentPayload payload, string customerCode)
        {
            if (!await LoadInvoiceList(payload, customerCode))
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

        private async Task<bool> LoadInvoiceList(InvoiceNewPaymentPayload payload, string customerCode)
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

                TransDate = DateTime.Now,
                TransTime = DateTime.Now.TimeOfDay,
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



