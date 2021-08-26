﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using Helper = DigitBridge.CommerceCentral.ERPDb.InvoiceTransactionHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoicePaymentList : SqlQueryBuilder<InvoicePaymentQuery>
    {
        public InvoicePaymentList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InvoicePaymentList(IDataBaseFactory dbFactory, InvoicePaymentQuery queryObject)
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

        public virtual InvoicePaymentPayload GetInvoicePaymentList(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

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

        public virtual async Task<InvoicePaymentPayload> GetInvoicePaymentListAsync(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

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

        //TODO where sql&more column displayed.
        private string GetExportSql()
        {
            var sql = @"
select
trans.InvoiceNumber as 'InvoiceTransaction.InvoiceNumber',
trans.TransUuid as 'InvoiceTransaction.TransUuid',
trans.TransNum as 'InvoiceTransaction.TransNum',
trans.TransStatus as 'InvoiceTransaction.TransStatus',
--trans.TransDate as 'InvoiceTransaction.TransDate',
(cast(trans.TransDate as datetime) + cast(trans.TransTime as datetime)) as 'InvoiceTransaction.TransTime',
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


invoice.InvoiceDate as 'Invoice.InvoiceDate',
invoice.BillDate as 'Invoice.Bill Date',
invoice.OrderNumber as 'Invoice.Order Number',
invoice.InvoiceType as 'Invoice.Invoice Type' 
 

from InvoiceTransaction(nolock) trans  
left join InvoiceHeader invoice on invoice.InvoiceUuid=trans.InvoiceUuid
where trans.TransType=1
for json path;
";
            return sql;
        }
        public virtual async Task<StringBuilder> GetExportDataAsync(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

            this.LoadRequestParameter(payload);
            using var trs = new ScopedTransaction(dbFactory);
            StringBuilder sb = new StringBuilder();
            await SqlQuery.QueryJsonAsync(sb, GetExportSql(), System.Data.CommandType.Text, GetSqlParameters().ToArray());
            return sb;
        }

        public virtual StringBuilder GetExportData(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

            this.LoadRequestParameter(payload);
            using var trs = new ScopedTransaction(dbFactory);
            StringBuilder sb = new StringBuilder();
            SqlQuery.QueryJson(sb, GetExportSql(), System.Data.CommandType.Text, GetSqlParameters().ToArray());
            return sb;
        }
    }
}
