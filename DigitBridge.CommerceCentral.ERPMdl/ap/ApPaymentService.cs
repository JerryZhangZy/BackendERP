


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
using DigitBridge.CommerceCentral.ERPEventSDK.ApiClient;
using DigitBridge.CommerceCentral.ERPEventSDK;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class ApPaymentService : ApTransactionService, IApPaymentService
    {
        public ApPaymentService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public override ApTransactionService Init()
        {
            SetDtoMapper(new ApTransactionDataDtoMapperDefault());
            SetCalculator(new ApPaymentServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new ApPaymentServiceValidatorDefault(this, this.dbFactory));
            return this;
        }


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
            NewData();
            if (!await LoadApInvoiceAsync(payload.MasterAccountNum, payload.ProfileNum, apInvoiceNum))
                return false;

            CopyApInvoiceHeaderToTrans();

            //unpaid amount.
            Data.ApInvoiceTransaction.Amount = Data.ApInvoiceData.ApInvoiceHeader.Balance.IsZero() ? 0 : Data.ApInvoiceData.ApInvoiceHeader.Balance.ToDecimal();

            payload.ApTransaction = this.ToDto().ApInvoiceTransaction;

            return await LoadApInvoiceList(payload, Data.ApInvoiceData.ApInvoiceHeader.VendorNum);
        }

        public async Task<bool> NewPaymentByVendorNum(ApNewPaymentPayload payload, string vendorNum)
        {
            if (!await LoadApInvoiceList(payload, vendorNum))
            {
                return false;
            }
            if (payload.InvoiceList.ToString().IsZero())
            {
                AddError($"No ApInvoice for vendorNum:{vendorNum}");
                return false;
            }

            var apInvoiceList = JsonConvert.DeserializeObject<List<ApInvoiceHeader>>(payload.InvoiceList.ToString());

            NewData();
            // All unpaid amount.
            Data.ApInvoiceTransaction.Amount = apInvoiceList.Sum(i => i.Balance.IsZero() ? 0 : i.Balance.ToDecimal());

            payload.ApTransaction = this.ToDto().ApInvoiceTransaction;

            return true;
        }

        private async Task<bool> LoadApInvoiceList(ApNewPaymentPayload payload, string vendorNum)
        {
            var apInvoicePayload = new ApInvoicePayload()
            {
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                LoadAll = true
            };

            var apInvoiceQuery = new ApInvoiceQuery();
            apInvoiceQuery.InitForNewPaymet(vendorNum);

            var srv = new ApInvoiceList(this.dbFactory, apInvoiceQuery);
            await srv.GetApInvoiceListAsync(apInvoicePayload);

            if (!apInvoicePayload.Success)
                this.Messages = this.Messages.Concat(srv.Messages).ToList();

            payload.InvoiceList = apInvoicePayload.ApInvoiceList;

            payload.InvoiceListCount = apInvoicePayload.ApInvoiceListCount;

            return apInvoicePayload.Success;
        }

        #endregion

    }
}



