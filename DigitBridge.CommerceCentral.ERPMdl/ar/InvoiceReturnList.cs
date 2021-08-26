using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Helper = DigitBridge.CommerceCentral.ERPDb.InvoiceTransactionHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceReturnList : SqlQueryBuilder<InvoiceReturnQuery>
    {
        public InvoiceReturnList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InvoiceReturnList(IDataBaseFactory dbFactory, InvoiceReturnQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.TableAllies}.*
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();

            return paramList.ToArray();

        }

        #endregion override methods

        public virtual InvoiceReturnPayload GetInvoiceReturnList(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.InvoiceTransactionListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.InvoiceTransactionList = sb;
            }
            catch (Exception ex)
            {
                payload.InvoiceTransactionListCount = 0;
                payload.InvoiceTransactionList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<InvoiceReturnPayload> GetInvoiceReturnListAsync(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.InvoiceTransactionListCount = await CountAsync().ConfigureAwait(false);
                result = await ExcuteJsonAsync(sb).ConfigureAwait(false);
                if (result)
                    payload.InvoiceTransactionList = sb;
            }
            catch (Exception ex)
            {
                payload.InvoiceTransactionListCount = 0;
                payload.InvoiceTransactionList = null;
                return payload;
                throw;
            }
            return payload;
        }

        //TODO where sql
        private string GetExportSql()
        {
            var sql = @"
select 
trans.TransUuid as 'InvoiceTransaction.TransUuid',
trans.TransNum as 'InvoiceTransaction.TransNum',
trans.TransStatus as 'InvoiceTransaction.TransStatus',
trans.TransDate as 'InvoiceTransaction.TransDate',
trans.TransTime as 'InvoiceTransaction.TransTime',
trans.Description as 'InvoiceTransaction.Description',
trans.Notes as 'InvoiceTransaction.Notes',
trans.PaidBy as 'InvoiceTransaction.PaidBy',
trans.BankAccountCode as 'InvoiceTransaction.BankAccountCode',
trans.CheckNum as 'InvoiceTransaction.CheckNum',
trans.AuthCode as 'InvoiceTransaction.AuthCode',
trans.Currency as 'InvoiceTransaction.Currency',
trans.ExchangeRate as 'InvoiceTransaction.ExchangeRate',
trans.SubTotalAmount as 'InvoiceTransaction.Sub TotalAmount',
trans.SalesAmount as 'InvoiceTransaction.SalesAmount',
trans.TotalAmount as 'InvoiceTransaction.TotalAmount',
trans.TaxableAmount as 'InvoiceTransaction.TaxableAmount',
trans.NonTaxableAmount as 'InvoiceTransaction.NonTaxableAmount',
trans.TaxRate as 'InvoiceTransaction.TaxRate',
trans.TaxAmount as 'InvoiceTransaction.TaxAmount',
trans.DiscountRate as 'InvoiceTransaction.DiscountRate',
trans.DiscountAmount as 'InvoiceTransaction.DiscountAmount',
trans.ShippingAmount as 'InvoiceTransaction.ShippingAmount',
trans.ShippingTaxAmount as 'InvoiceTransaction.ShippingTaxAmount',
trans.MiscAmount as 'InvoiceTransaction.MiscAmount',
trans.MiscTaxAmount as 'InvoiceTransaction.MiscTaxAmount',
trans.ChargeAndAllowanceAmount as 'InvoiceTransaction.ChargeAndAllowanceAmount',
trans.CreditAccount as 'InvoiceTransaction.CreditAccount',
trans.DebitAccount as 'InvoiceTransaction.DebitAccount',
trans.TransSourceCode as 'InvoiceTransaction.TransSourceCode',

--invoice.InvoiceNumber as 'Invoice.Invoice Number',
--invoice.InvoiceDate as 'Invoice.Invoice Date',
--invoice.BillDate as 'Invoice.Bill Date',
--invoice.OrderNumber as 'Invoice.Order Number',
--invoice.InvoiceType as 'Invoice.Invoice Type', 

(   
SELECT 
returnItem.SKU as 'InvoiceReturnItems.SKU',
returnItem.ReturnItemType as 'InvoiceReturnItems.ReturnItemType',
returnItem.ReturnDate as 'InvoiceReturnItems.ReturnDate',
returnItem.ReturnTime as 'InvoiceReturnItems.ReturnTime',
returnItem.ReceiveDate as 'InvoiceReturnItems.ReceiveDate',
returnItem.StockDate as 'InvoiceReturnItems.StockDate',
returnItem.Reason as 'InvoiceReturnItems.Reason',
returnItem.ReturnQty as 'InvoiceReturnItems.ReturnQuantity',
returnItem.ReceiveQty as 'InvoiceReturnItems.ReceiveQuantity'
FROM InvoiceReturnItems returnItem
WHERE trans.TransUuid=returnItem.TransUuid for json path
) AS InvoiceReturnItems

from InvoiceTransaction(nolock) trans 
--left join InvoiceReturnItems returnItem on trans.TransUuid=returnItem.TransUuid
left join InvoiceHeader invoice on invoice.InvoiceUuid=trans.InvoiceUuid
where trans.TransType=2
for json path;
";
            return sql;
        }
        public virtual async Task<StringBuilder> GetExportDataAsync(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

            this.LoadRequestParameter(payload);
            using var trs = new ScopedTransaction(dbFactory);
            StringBuilder sb = new StringBuilder();
            await SqlQuery.QueryJsonAsync(sb, GetExportSql(), System.Data.CommandType.Text, GetSqlParameters().ToArray());
            return sb;
        }

        public virtual StringBuilder GetExportData(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

            this.LoadRequestParameter(payload);
            using var trs = new ScopedTransaction(dbFactory);
            StringBuilder sb = new StringBuilder();
            SqlQuery.QueryJson(sb, GetExportSql(), System.Data.CommandType.Text, GetSqlParameters().ToArray());
            return sb;
        }
    }
}
