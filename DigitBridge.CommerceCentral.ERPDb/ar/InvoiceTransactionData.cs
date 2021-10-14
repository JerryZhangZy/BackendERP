


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class InvoiceTransactionData
    {
        /// <summary>
        /// Load All return items of this invoice.
        /// </summary>
        public List<InvoiceReturnItems> ReturnItemsOfInvoice;

        /// <summary>
        /// Load original invoicedata.
        /// </summary>
        public InvoiceData InvoiceData;


        //public override void CheckIntegrityOthers()
        //{
        //    if (this.InvoiceTransaction == null) return;
        //    //if(this.InvoiceTransaction.TransTime==null)
        //    if (_InvoiceData != null)
        //    {
        //        this.InvoiceTransaction.InvoiceUuid = _InvoiceData.InvoiceHeader.InvoiceUuid;
        //    }
        //    if (_InvoiceData != null && this.InvoiceReturnItems != null && this.InvoiceReturnItems.Count > 0)
        //    {
        //        foreach (var returnItem in this.InvoiceReturnItems)
        //        {
        //            returnItem.InvoiceUuid = _InvoiceData.InvoiceHeader.InvoiceUuid;

        //            var invoiceItem = _InvoiceData.InvoiceItems.Where(i => i.InvoiceItemsUuid == returnItem.InvoiceItemsUuid).FirstOrDefault();
        //            if (invoiceItem == null) continue;
        //            returnItem.InvoiceWarehouseCode = invoiceItem.WarehouseCode;
        //            returnItem.InvoiceWarehouseUuid = invoiceItem.WarehouseUuid;
        //            returnItem.SKU = invoiceItem.SKU;
        //            returnItem.ProductUuid = invoiceItem.ProductUuid;
        //            returnItem.InventoryUuid = invoiceItem.InventoryUuid;
        //            returnItem.InvoiceDiscountPrice = invoiceItem.DiscountPrice;
        //            returnItem.LotNum = invoiceItem.LotNum;
        //            returnItem.Currency = invoiceItem.Currency;
        //        }
        //    }
        //}

        public async Task<List<InvoiceTransactionData>> GetDataListAsync(string invoiceNumber, int masterAccountNum, int profileNum, TransTypeEnum transType, int? transNum = null)
        {
            var trans = await new InvoiceTransaction(dbFactory).GetByInvoiceNumberAsync(invoiceNumber, masterAccountNum, profileNum, transType, transNum);
            return await WrapData(trans, transType == TransTypeEnum.Return);

        }
        private async Task<List<InvoiceTransactionData>> WrapData(List<InvoiceTransaction> invoiceTransactions, bool loadOthers)
        {
            if (invoiceTransactions is null || invoiceTransactions.Count == 0) return null;

            List<InvoiceReturnItems> returnItems = null;
            if (loadOthers)
            {
                var transUuids = invoiceTransactions?.Select(i => i.TransUuid).ToList();
                returnItems = await new InvoiceReturnItems(dbFactory).GetReturnItems(transUuids);
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

        public override bool GetByNumber(int masterAccountNum, int profileNum, string number)
        {
            var invoiceNumberAndTranNum = number.Split('_');
            var invoiceNumber = invoiceNumberAndTranNum[0];
            var transType = invoiceNumberAndTranNum[1];

            var sql = @"
SELECT TOP 1 * FROM InvoiceTransaction
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND InvoiceNumber = @2
AND TransType = @3

";
            var paras = new List<SqlParameter>()
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",invoiceNumber),
                new SqlParameter("@3",transType)
            };

            if (invoiceNumberAndTranNum.Length > 2)
            {
                sql += " AND TransNum=@4";
                paras.Add(new SqlParameter("@4", invoiceNumberAndTranNum[2].ToInt()));
            }

            var obj = dbFactory.GetBy<InvoiceTransaction>(sql, paras.ToArray());
            if (obj is null) return false;
            InvoiceTransaction = obj;
            GetOthers();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
        public override async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string number)
        {
            var invoiceNumberAndTranNum = number.Split('_');
            var invoiceNumber = invoiceNumberAndTranNum[0];
            var transType = invoiceNumberAndTranNum[1];

            var sql = @"
SELECT TOP 1 * FROM InvoiceTransaction
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND InvoiceNumber = @2
AND TransType = @3

";
            var paras = new List<SqlParameter>()
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",invoiceNumber),
                new SqlParameter("@3",transType)
            };

            if (invoiceNumberAndTranNum.Length > 2)
            {
                sql += " AND TransNum=@4";
                paras.Add(new SqlParameter("@4", invoiceNumberAndTranNum[2].ToInt()));
            }

            var obj = await dbFactory.GetByAsync<InvoiceTransaction>(sql, paras.ToArray()); 
            if (obj is null) return false;
            InvoiceTransaction = obj;
            await GetOthersAsync();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
    }
}



