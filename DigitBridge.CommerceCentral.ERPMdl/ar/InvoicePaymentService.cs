


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
                ChannelNum = this.Data.InvoiceData.InvoiceHeaderInfo.ChannelNum,
                ChannelAccountNum = this.Data.InvoiceData.InvoiceHeaderInfo.ChannelAccountNum,

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
                InvoiceDate = invoiceHeader.InvoiceDate.ToDateTime(),
                DueDate = invoiceHeader.DueDate.ToDateTime(),
                QuickbookDocNum = invoiceHeader.QboDocNumber,
                InvoiceTotalAmount = invoiceHeader.TotalAmount.ToDecimal(),
                InvoicePaidAmount = invoiceHeader.PaidAmount.ToDecimal(),
                InvoiceBalance = invoiceHeader.Balance.ToDecimal(),
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

                await AddMiscPaymentAsync();
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

                await AddMiscPaymentAsync();
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

                AddMiscPayment();
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
                if (await base.UpdateAsync(paymentDataDto))
                {
                    await UpdateMiscPaymentAsync();
                }
                else
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

                UpdateMiscPayment();

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

        public async virtual Task<bool> UpdateInvoicePayments(InvoiceNewPaymentPayload payload)
        {
            if (!payload.HasApplyInvoices)
                return false;

            var invoices = await GetInvoiceHeadersByCustomerAsync(payload.MasterAccountNum, payload.ProfileNum, payload.InvoiceTransaction.CustomerCode);
            if (invoices?.Count == 0)
                return false;

            payload.InvoiceTransaction.MasterAccountNum = payload.MasterAccountNum;
            payload.InvoiceTransaction.ProfileNum = payload.ProfileNum;
            foreach (var applyInvoice in payload.ApplyInvoices)
            {
                var invoice = invoices.SingleOrDefault(i => i.InvoiceUuid == applyInvoice.InvoiceUuid || i.InvoiceNumber == applyInvoice.InvoiceNumber);
                if (invoice == null)
                {
                    AddError($"Invoice {applyInvoice.InvoiceUuid} does not exist");
                    continue;
                }
                var trans = applyInvoice.GenerateTransaction(invoice, payload.InvoiceTransaction);
                bool result = false;
                if (applyInvoice.TransRowNum.HasValue)
                    result = await base.UpdateAsync(trans);
                else
                    result = await base.AddAsync(trans);

                if (result)
                {
                    this.AddActivityLogForCurrentData();
                    
                    if (!await UpdateMiscPaymentAsync())
                    {
                        applyInvoice.Success = false;
                        AddError($"{applyInvoice.InvoiceNumber} is not applied.");
                    }

                    applyInvoice.TransRowNum = Data.InvoiceTransaction.RowNum;
                    applyInvoice.TransUuid = Data.InvoiceTransaction.TransUuid;
                    applyInvoice.InvoiceDate = invoice.InvoiceDate ?? default;
                    applyInvoice.DueDate = invoice.DueDate ?? default;
                    applyInvoice.QuickbookDocNum = invoice.QboDocNumber;
                    applyInvoice.InvoiceTotalAmount = invoice.TotalAmount ?? default;
                    applyInvoice.PaidAmount = invoice.PaidAmount ?? default;
                    applyInvoice.InvoiceBalance = invoice.Balance ?? default;
                    applyInvoice.Success = true;
                }
                else
                {
                    applyInvoice.Success = false;
                    AddError($"Save payment for invoice {applyInvoice.InvoiceNumber} failed");
                }
            }

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
            if (!await base.AddAsync())
            {
                return false;
            }

            return await AddMiscPaymentAsync();
        }

        #endregion

        #region Save misc payment

        protected async Task<bool> AddMiscPaymentAsync()
        {
            var paymentData = Data.InvoiceTransaction;

            if (paymentData.PaidBy != (int)PaidByAr.PrePayment)
                return true;

            //Add mis payment and withdraw from misinvoice
            if (!await MiscPaymentService.AddMiscPayment(paymentData.AuthCode, paymentData.TransUuid, paymentData.InvoiceNumber, paymentData.TotalAmount))
            {
                this.Messages.Add(MiscPaymentService.Messages);
                return false;
            }
            return true;
        }

        //TODO need MiscPaymentService provide sync method
        protected bool AddMiscPayment()
        {
            throw new Exception("Please Add sync AddMiscPayment to MiscPaymentService");

            //var paymentData = Data.InvoiceTransaction;

            //if (paymentData.PaidBy != (int)PaidByAr.CreditMemo)
            //    return true;

            ////Add mis payment and withdraw from misinvoice
            //if (!await MiscPaymentService.AddMiscPayment(paymentData.AuthCode, paymentData.TransUuid, paymentData.InvoiceNumber, paymentData.TotalAmount))
            //{
            //    this.Messages.Add(MiscPaymentService.Messages);
            //    return false;
            //}
            //return true;
        }


        protected async Task<bool> UpdateMiscPaymentAsync()
        {
            var success = true;
            //original paydby is creditmemo then delete mispayment.
            if (lastPaidByBeforeUpdate == (int)PaidByAr.PrePayment)
            {
                success = await MiscPaymentService.DeleteMiscPayment(lastAuthCodeBeforeUpdate, lastTransUuidBeforeUpdate, Data.InvoiceTransaction.OriginalPaidAmount);
            }

            //new paydby is creditmemo then add miscpayment
            if (Data.InvoiceTransaction.PaidBy == (int)PaidByAr.PrePayment)
            {
                success = success && await AddMiscPaymentAsync();
            }

            return success;
        }

        //TODO need MiscPaymentService provide sync method
        protected bool UpdateMiscPayment()
        {
            throw new Exception("Please Add sync UpdateMiscPayment to MiscPaymentService");
            //var success = true;
            ////original paydby is creditmemo then delete mispayment.
            //if (lastPaidByBeforeUpdate == (int)PaidByAr.CreditMemo)
            //{
            //    success = await MiscPaymentService.DeleteMiscPayment(lastAuthCodeBeforeUpdate, lastTransUuidBeforeUpdate, Data.InvoiceTransaction.OriginalPaidAmount);
            //}

            ////new paydby is creditmemo then add miscpayment
            //if (Data.InvoiceTransaction.PaidBy == (int)PaidByAr.CreditMemo)
            //{
            //    success = success && await AddMiscPaymentAsync();
            //}

            //return success;
        }
        #endregion 

    }
}



