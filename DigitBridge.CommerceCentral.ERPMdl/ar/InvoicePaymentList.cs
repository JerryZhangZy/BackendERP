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
using Newtonsoft.Json;
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

        //        //TODO where sql&more column displayed.
        //        private string GetExportSql(int userConfigID)
        //        {
        //            using var trs = new ScopedTransaction(dbFactory);
        //            var datas = ExportHelper.GetExportSelect(userConfigID);

        //            var sql = $@"
        //with BaseExportTable as (
        //select
        //{Helper.TableAllies}.InvoiceNumber,
        //{Helper.TableAllies}.TransUuid,
        //{Helper.TableAllies}.TransNum,
        //{Helper.TableAllies}.TransStatus,
        //(cast({Helper.TableAllies}.TransDate as datetime) + cast({Helper.TableAllies}.TransTime as datetime)) as 'TransTime',
        //{Helper.TableAllies}.Description,
        //{Helper.TableAllies}.Notes,
        //{Helper.TableAllies}.PaidBy,
        //{Helper.TableAllies}.BankAccountCode,
        //{Helper.TableAllies}.CheckNum,
        //{Helper.TableAllies}.AuthCode,
        //{Helper.TableAllies}.Currency,
        //{Helper.TableAllies}.ExchangeRate,
        //{Helper.TableAllies}.SubTotalAmount,
        //{Helper.TableAllies}.SalesAmount,
        //{Helper.TableAllies}.TotalAmount,
        //{Helper.TableAllies}.TaxableAmount,
        //{Helper.TableAllies}.NonTaxableAmount,
        //{Helper.TableAllies}.TaxRate,
        //{Helper.TableAllies}.TaxAmount,
        //{Helper.TableAllies}.DiscountRate,
        //{Helper.TableAllies}.DiscountAmount,
        //{Helper.TableAllies}.ShippingAmount,
        //{Helper.TableAllies}.ShippingTaxAmount,
        //{Helper.TableAllies}.MiscAmount,
        //{Helper.TableAllies}.MiscTaxAmount,
        //{Helper.TableAllies}.ChargeAndAllowanceAmount,
        //{Helper.TableAllies}.CreditAccount,
        //{Helper.TableAllies}.DebitAccount,
        //{Helper.TableAllies}.TransSourceCode

        //from InvoiceTransaction {Helper.TableAllies} 
        //where {Helper.TableAllies}.TransType=1
        //)
        //select {ExportHelper.GetSelectColumnsByConfig(datas, "H", Helper.TableAllies)}
        //from BaseExportTable {Helper.TableAllies}
        //for json path;
        //";
        //            return sql;
        //        }

        //TODO where sql&more column displayed.
        private string GetExportSql(int userConfigID)
        {
            using var trs = new ScopedTransaction(dbFactory);
            var datas = ExportHelper.GetExportSelect(userConfigID);
            var tranCols = ExportHelper.GetSelectColumnsByConfig(datas, "H", Helper.TableAllies);
            var sql = $@"  
select {tranCols}
from InvoiceTransaction {Helper.TableAllies}
where {Helper.TableAllies}.TransType=1
for json path;
";
            return sql;
        }

        public virtual async Task<StringBuilder> GetExportDataAsync(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

            this.LoadRequestParameter(payload);
            var sql = GetExportSql(payload.ExportUserConfigID);
            using var trs = new ScopedTransaction(dbFactory);
            StringBuilder sb = new StringBuilder();
            await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text, GetSqlParameters().ToArray());
            return sb;
        }

        public virtual StringBuilder GetExportData(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

            this.LoadRequestParameter(payload);
            var sql = GetExportSql(payload.ExportUserConfigID);

            using var trs = new ScopedTransaction(dbFactory);
            StringBuilder sb = new StringBuilder();
            SqlQuery.QueryJson(sb, sql, System.Data.CommandType.Text, GetSqlParameters().ToArray());
            return sb;
        }
    }
}
