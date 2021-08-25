


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
        /// <summary>
        /// Load original invoicedata.
        /// </summary>
        public InvoiceData _InvoiceData;


        public override void CheckIntegrityOthers()
        {
            if (_InvoiceData != null && this.InvoiceTransaction != null)
            {
                this.InvoiceTransaction.InvoiceNumber = _InvoiceData.InvoiceHeader.InvoiceNumber;
            }
            if (_InvoiceData != null && this.InvoiceReturnItems != null && this.InvoiceReturnItems.Count > 0)
            {
                foreach (var returnItem in this.InvoiceReturnItems)
                { 
                    returnItem.InvoiceUuid = _InvoiceData.InvoiceHeader.InvoiceUuid;

                    var invoiceItem = _InvoiceData.InvoiceItems.Where(i => i.InvoiceItemsUuid == returnItem.InvoiceItemsUuid).FirstOrDefault();
                    if (invoiceItem == null) continue;
                    returnItem.InvoiceWarehouseCode = invoiceItem.WarehouseCode;
                    returnItem.InvoiceWarehouseUuid = invoiceItem.WarehouseUuid;
                    returnItem.SKU = invoiceItem.SKU;
                    returnItem.ProductUuid = invoiceItem.ProductUuid;
                    returnItem.InventoryUuid = invoiceItem.InventoryUuid; 
                    returnItem.InvoiceDiscountPrice = invoiceItem.DiscountPrice;
                    returnItem.LotNum = invoiceItem.LotNum;
                    returnItem.Currency = invoiceItem.Currency;
                }
            }
        }

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
    }
}



