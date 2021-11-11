


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
        public async Task<bool> AddMiscPayment(string miscInvoiceUuid, string invoiceUuid, string invoiceNumber, decimal amount)
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
                AuthCode = invoiceUuid,
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
            await MiscInvoiceService.PayAsync(miscInvoiceUuid, amount);
            return true;
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
    }
}



