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
using ReturnHelper = DigitBridge.CommerceCentral.ERPDb.InvoiceReturnItemsHelper;

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
{Helper.TableAllies}.*,
(select {ReturnHelper.TableAllies}.* from {ReturnHelper.TableName} {ReturnHelper.TableAllies} where {Helper.TableAllies}.TransUuid={ReturnHelper.TableAllies}.TransUuid for json auto,include_null_values ) as InvoiceReturnItems
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
LEFT JOIN {ReturnHelper.TableName} {ReturnHelper.TableAllies} ON {Helper.TableAllies}.TransUuid={ReturnHelper.TableAllies}.TransUuid
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

        public virtual async Task<IList<long>> GetRowNumListAsync(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();

            var sql = $@"
SELECT distinct {Helper.TableAllies}.RowNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.RowNum  
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
";
            try
            {
                using var trs = new ScopedTransaction(dbFactory);
                rowNumList = await SqlQuery.ExecuteAsync(
                    sql,
                    (long rowNum) => rowNum,
                    GetSqlParameters().ToArray()
                );
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowNumList;
        }

        public virtual IList<long> GetRowNumList(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            var sql = $@"
SELECT distinct {Helper.TableAllies}.RowNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.RowNum  
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
";
            try
            {
                using var trs = new ScopedTransaction(dbFactory);
                rowNumList = SqlQuery.Execute(
                    sql,
                    (long rowNum) => rowNum,
                    GetSqlParameters().ToArray()
                );
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowNumList;
        }


        //        //TODO where sql
        private string GetExportSql()
        {
            var sql = $@"
select  
{Helper.TableAllies}.*, 

(   
SELECT 
{InvoiceReturnItemsHelper.TableAllies}.*
FROM InvoiceReturnItems {InvoiceReturnItemsHelper.TableAllies}
WHERE {Helper.TableAllies}.TransUuid={InvoiceReturnItemsHelper.TableAllies}.TransUuid for json path
) AS InvoiceReturnItems

from InvoiceTransaction(nolock) {Helper.TableAllies} 
--left join InvoiceReturnItems returnItem on {Helper.TableAllies}.TransUuid=returnItem.TransUuid
--left join InvoiceHeader invoice on invoice.InvoiceUuid={Helper.TableAllies}.InvoiceUuid
where {Helper.TableAllies}.TransType=2
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
