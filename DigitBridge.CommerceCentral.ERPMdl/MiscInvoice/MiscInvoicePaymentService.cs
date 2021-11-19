


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
        public MiscInvoicePaymentService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public override MiscInvoiceTransactionService Init()
        {
            return base.Init();
        }

        MiscInvoiceService _miscInvoiceService;
        MiscInvoiceService MiscInvoiceService
        {
            get
            {
                if (_miscInvoiceService == null) _miscInvoiceService = new MiscInvoiceService(dbFactory);
                return _miscInvoiceService;
            }
        }
        public async Task<bool> AddMiscPayment(string miscInvoiceUuid, string invoiceTransUuid, string invoiceNumber, decimal amount)
        {
            Add();
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
                PaidBy = (int)PaidByAr.CreditMemo,
                CheckNum = invoiceNumber,
                AuthCode = invoiceTransUuid,
                TransDate = DateTime.Now,
                TransTime = DateTime.Now.TimeOfDay,
                TransType = (int)TransTypeEnum.Payment,
                TransStatus = (int)TransStatus.Paid,
                TransUuid = Guid.NewGuid().ToString(),

            };

            using (var tx = new ScopedTransaction(dbFactory))
            {
                Data.MiscInvoiceTransaction.TransNum = await MiscInvoiceTransactionHelper.GetTranSeqNumAsync(header.MiscInvoiceNumber, header.ProfileNum);
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

            return miscInvoiceBalance > expectedAmount ? expectedAmount : miscInvoiceBalance;

        }
    }
}



