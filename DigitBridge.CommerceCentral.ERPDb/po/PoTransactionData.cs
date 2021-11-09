using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class PoTransactionData
    {
        /// <summary>
        /// Load original invoicedata.
        /// </summary>
        public PurchaseOrderData PurchaseOrderData;

        public bool FirstAPReceiveStatus;

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
        
        public override bool GetByNumber(int masterAccountNum, int profileNum, string number)
        {
            var poNumAndTranNum = number.Split('_');
            var poNum = poNumAndTranNum[0];

            var sql = @"
SELECT TOP 1 * FROM PoTransaction
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND PoNum = @2

";
            var paras = new List<SqlParameter>()
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",poNum)
            };

            if (poNumAndTranNum.Length > 1)
            {
                sql += " AND TransNum=@3";
                paras.Add(new SqlParameter("@3", poNumAndTranNum[1].ToInt()));
            }

            var obj = dbFactory.GetBy<PoTransaction>(sql, paras.ToArray());
            if (obj is null) return false;
            PoTransaction = obj;
            GetOthers();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
        public override async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string number)
        {
            var poNumAndTranNum = number.Split('_');
            var poNum = poNumAndTranNum[0];

            var sql = @"
SELECT TOP 1 * FROM PoTransaction
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND PoNum = @2

";
            var paras = new List<SqlParameter>()
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",poNum),
            };

            if (poNumAndTranNum.Length > 1)
            {
                sql += " AND TransNum=@3";
                paras.Add(new SqlParameter("@3", poNumAndTranNum[1].ToInt()));
            }

            var obj = await dbFactory.GetByAsync<PoTransaction>(sql, paras.ToArray()); 
            if (obj is null) return false;
            PoTransaction = obj;
            await GetOthersAsync();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
    }
}