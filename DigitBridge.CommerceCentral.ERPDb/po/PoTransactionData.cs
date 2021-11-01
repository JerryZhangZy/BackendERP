using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class PoTransactionData
    {
        /// <summary>
        /// Load original invoicedata.
        /// </summary>
        public PurchaseOrderData PurchaseOrderData;
        public async Task<List<PoTransactionData>> GetDataListAsync(string poNum, int masterAccountNum, int profileNum,
            int? transNum = null)
        {
            var trans = await new PoTransaction(dbFactory).GetByPoNumberAsync(poNum, masterAccountNum, profileNum,
                transNum);
            return await WrapData(trans);
        }

        private async Task<List<PoTransactionData>> WrapData(List<PoTransaction> poTransactions)
        {
            if (poTransactions is null || poTransactions.Count == 0) return null;

            var transUuids = poTransactions?.Select(i => i.TransUuid).ToList();
            var transactionItems = await new PoTransactionItems(dbFactory).GetPoTransactionItemsItems(transUuids);

            List<PoTransactionData> datas = new List<PoTransactionData>();

            foreach (var tran in poTransactions)
            {
                var data = new PoTransactionData();
                data.PoTransaction = tran;
                data.PoTransactionItems = transactionItems?.Where(i => i.TransUuid == tran.TransUuid).ToList();
                datas.Add(data);
            }

            return datas;
        }
    }
}