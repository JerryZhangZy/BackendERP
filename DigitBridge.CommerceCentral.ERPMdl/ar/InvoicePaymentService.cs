


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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InvoicePaymentService : InvoiceTransactionService
    {
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


        #region add payment
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

                //add payment success. then pay invoice.
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
                PaidAmount = Data.InvoiceTransaction.TotalAmount,
                TransRowNum = trans.RowNum
            };
        }

        protected async Task<bool> PayInvoiceAsync(ApplyInvoice applyInvoice, int masterAccountNum, int profileNum)
        {
            var changedPaidAmount = applyInvoice.PaidAmount - Data.InvoiceTransaction.OriginalPaidAmount;
            using (var trs = new ScopedTransaction(dbFactory))
                return await InvoiceHelper.PayInvoiceAsync(applyInvoice.InvoiceNumber, changedPaidAmount, masterAccountNum, profileNum);
        }
        #endregion


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
                if (!await base.UpdateAsync(paymentPayload))
                {
                    payload.Success = false;
                    applyInvoice.Success = false;
                    AddError($"Update payment failed for InvoiceNumber:{applyInvoice.InvoiceNumber} ");
                    continue;
                }

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
            return await base.DeleteByNumberAsync(payload, invoiceNumber, TransTypeEnum.Payment, transNum);
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

            //unpaid amount.
            Data.InvoiceTransaction.TotalAmount = Data.InvoiceData.InvoiceHeader.Balance.IsZero() ? 0 : Data.InvoiceData.InvoiceHeader.Balance;

            payload.InvoiceTransaction = this.ToDto().InvoiceTransaction;

            return await LoadInvoiceList(payload, Data.InvoiceData.InvoiceHeader.CustomerCode);
        }

        public async Task<bool> NewPaymentByCustomerCode(InvoiceNewPaymentPayload payload, string customerCode)
        {
            if (!await LoadInvoiceList(payload, customerCode))
            {
                return false;
            }

            var invoiceList = JsonConvert.DeserializeObject<List<InvoiceHeader>>(payload.InvoiceList.ToString());

            NewData();
            // All unpaid amount.
            Data.InvoiceTransaction.TotalAmount = invoiceList.Sum(i => i.Balance.IsZero() ? 0 : i.Balance);

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
        /// <summary>
        /// convert erp invoice payment to a queue message then put it to qbo queue
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns></returns>
        public async Task<bool> AddQboPaymentEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.InvoiceTransaction.TransUuid,
            };
            return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksPayment");
        }

        public async Task<bool> DeleteQboRefundEventAsync(int masterAccountNum, int profileNum)
        {
            var eventDto = new AddEventDto()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = Data.InvoiceTransaction.TransUuid,
            };
            return await ErpEventClientHelper.AddEventERPAsync(eventDto, "/addQuicksBooksPaymentDelete");
        }
        #endregion
    }
}



