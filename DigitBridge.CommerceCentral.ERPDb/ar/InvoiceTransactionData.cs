


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using System.Linq;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class InvoiceTransactionData
    {

        public async Task<List<InvoiceTransactionData>> GetDataListAsync(string invoiceNumber, int masterAccountNum, int profileNum, TransTypeEnum transType, int? transNum = null)
        {
            var trans = await new InvoiceTransaction().GetByInvoiceNumberAsync(invoiceNumber, masterAccountNum, profileNum, transType, transNum);
            return await WrapData(trans, transType == TransTypeEnum.Return);

        }
        private async Task<List<InvoiceTransactionData>> WrapData(List<InvoiceTransaction> invoiceTransactions, bool loadOthers)
        {
            if (invoiceTransactions is null || invoiceTransactions.Count == 0) return null;

            List<InvoiceReturnItems> returnItems = null;
            if (loadOthers)
            {
                var transUuids = invoiceTransactions?.Select(i => i.TransUuid).ToList();
                returnItems = await new InvoiceReturnItems().GetReturnItems(transUuids);
            }

            List<InvoiceTransactionData> datas = new List<InvoiceTransactionData>();

            foreach (var tran in invoiceTransactions)
            {
                var data = new InvoiceTransactionData();
                data.InvoiceTransaction = tran;
                data.InvoiceReturnItems = returnItems != null ? returnItems.Where(i => i.TransUuid == tran.TransUuid).ToList() : null;
                datas.Add(data);
            }
            return datas;
        }
    }
}



