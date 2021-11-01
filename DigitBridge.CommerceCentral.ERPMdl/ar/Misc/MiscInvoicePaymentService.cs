


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
    public partial class MiscInvoicePaymentService : MiscInvoiceTransactionService, IMiscInvoicePaymentService
    {
        public MiscInvoicePaymentService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public override MiscInvoiceTransactionService Init()
        {
            return base.Init();
        }


        public async Task<bool> AddMiscPayment(string misInvoiceUuid, string invoiceUuid, decimal amount)
        {
            Add();
            if (misInvoiceUuid.IsZero())
                return false;
            if (!await LoadMiscInvoiceAsync(misInvoiceUuid))
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
                Description = "Add misc payment trans for applying presales amount",

                TotalAmount = amount > header.Balance ? header.Balance : amount,
                PaidBy = (int)PaidByEnum.PreSales,
                CheckNum = invoiceUuid,
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

            return await SaveDataAsync();
        }
    }
}



