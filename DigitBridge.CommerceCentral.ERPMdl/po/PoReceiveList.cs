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
using Helper = DigitBridge.CommerceCentral.ERPDb.PoTransactionHelper;

namespace DigitBridge.CommerceCentral.ERPMdl.po
{
 
    public class PoReceiveList : SqlQueryBuilder<PoReceiveQuery>
    {
        public PoReceiveList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public PoReceiveList(IDataBaseFactory dbFactory, PoReceiveQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            //COALESCE(pts.text, '') TransStatusText, 
            //COALESCE(ptt.text, '') TransTypeText, 
            this.SQL_Select = $@"
SELECT 
{Helper.RowNum()}, 
{Helper.TransUuid()}, 
{Helper.TransNum()}, 
{Helper.PoUuid()}, 
{Helper.PoNum()}, 
{Helper.TransType()},

{Helper.TransStatus()},

{Helper.TransDate()}, 
{Helper.TransTime()}, 
{Helper.Description()}, 
{Helper.Notes()}, 
{Helper.VendorUuid()}, 
{Helper.VendorInvoiceNum()}, 
{Helper.VendorInvoiceDate()}, 
{Helper.DueDate()}, 
{Helper.Currency()}, 
{Helper.SubTotalAmount()}, 
{Helper.TotalAmount()}, 
{Helper.TaxRate()}, 
{Helper.TaxAmount()}, 
{Helper.DiscountRate()}, 
{Helper.DiscountAmount()}, 
{Helper.ShippingAmount()}, 
{Helper.ShippingTaxAmount()}, 
{Helper.MiscAmount()}, 
{Helper.MiscTaxAmount()}, 
{Helper.ChargeAndAllowanceAmount()}, 
{Helper.EnterDateUtc()}, 
{Helper.UpdateDateUtc()}, 
{Helper.EnterBy()}, 
{Helper.UpdateBy()}, 
{Helper.DigitBridgeGuid()} 

 
";
            return this.SQL_Select;
        }
        protected override string GetSQL_select_summary()
        {
            this.SQL_SelectSummary = $@"
SELECT 
COUNT(1) AS totalCount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 1 THEN 1 ELSE 0 END) as stockReceiveCount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 2 THEN 1 ELSE 0 END) as apReceiveCount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 3 THEN 1 ELSE 0 END) as closedCount,

SUM(COALESCE({Helper.TableAllies}.TotalAmount, 0)) AS totalAmount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 1 THEN {Helper.TableAllies}.TotalAmount ELSE 0 END) as stockReceiveAmount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 2 THEN {Helper.TableAllies}.TotalAmount ELSE 0 END) as apReceiveAmount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 3 THEN {Helper.TableAllies}.TotalAmount ELSE 0 END) as closedAmount
";
            return this.SQL_SelectSummary;
        }

        protected override string GetSQL_from()
        {
            var masterAccountNum = $"{Helper.TableAllies}.MasterAccountNum";
            var profileNum = $"{Helper.TableAllies}.ProfileNum";

            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 


";
            //            this.SQL_From = $@"
            // FROM {Helper.TableName} {Helper.TableAllies} 
            // LEFT JOIN @PoTransStatus pts ON ({Helper.TableAllies}.TransStatus = pts.num)
            // LEFT JOIN @PoTransType ptt ON ({Helper.TableAllies}.TransType = ptt.num) 

            //";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            //paramList.Add("@PoTransStatus".ToEnumParameter<TransStatus>());
            //paramList.Add("@PoTransType".ToEnumParameter<TransTypeEnum>());
            return paramList.ToArray();
        }

        #endregion override methods


        public virtual async Task GetPoReceiveListSummaryAsync(PoReceivePayload payload)
        {
            if (payload == null)
                payload = new PoReceivePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = await ExcuteSummaryJsonAsync(sb);
                if (payload.Success)
                    payload.PoReceiveListSummary = sb;
            }
            catch (Exception ex)
            {
                payload.PoReceiveListCount = 0;
                payload.PoReceiveListSummary = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual void GetPoReceiveList(PoTransactionPayload payload)
        {
            
            if (payload == null)
                payload = new PoTransactionPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                
                payload.PoTransactionListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.PoTransactionList = sb;
            }
            catch (Exception ex)
            {
                payload.PoTransactionListCount = 0;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task GetPoReceiveListAsync(PoTransactionPayload payload)
        {
            if (payload == null)
                payload = new PoTransactionPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.PoTransactionListCount = await CountAsync();
                payload.Success = await ExcuteJsonAsync(sb);
                if (payload.Success)
                    payload.PoTransactionList = sb;
            }
            catch (Exception ex)
            {
                payload.PoTransactionListCount = 0;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(PoTransactionPayload payload)
        {
            if (payload == null)
                payload = new PoTransactionPayload();

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
                using (var trs = new ScopedTransaction(dbFactory))
                {
                    rowNumList = await SqlQuery.ExecuteAsync(
                    sql,
                    (long rowNum) => rowNum,
                    GetSqlParameters().ToArray()
                );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowNumList;
        }

        public virtual IList<long> GetRowNumList(PoTransactionPayload payload)
        {
            if (payload == null)
                payload = new PoTransactionPayload();

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
                using (var trs = new ScopedTransaction(dbFactory))
                {
                    rowNumList = SqlQuery.Execute(
                    sql,
                    (long rowNum) => rowNum,
                    GetSqlParameters().ToArray()
                );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowNumList;
        }


        //TODO where sql&more column displayed.
        private string GetExportSql()
        {
            var sql = $@"  
select {Helper.TableAllies}.*
from InvoiceTransaction {Helper.TableAllies}
where {Helper.TableAllies}.TransType=1
for json path;
";
            return sql;
        }

        public virtual async Task<StringBuilder> GetExportDataAsync(PoTransactionPayload payload)
        {
            if (payload == null)
                payload = new PoTransactionPayload();

            this.LoadRequestParameter(payload);
            var sql = GetExportSql();
            StringBuilder sb = new StringBuilder();
            using (var trs = new ScopedTransaction(dbFactory))
            {
                await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text, GetSqlParameters().ToArray());
            }
            return sb;
        }

        public virtual StringBuilder GetExportData(PoTransactionPayload payload)
        {
            if (payload == null)
                payload = new PoTransactionPayload();

            this.LoadRequestParameter(payload);
            var sql = GetExportSql();

            StringBuilder sb = new StringBuilder();
            using (var trs = new ScopedTransaction(dbFactory))
            {
                SqlQuery.QueryJson(sb, sql, System.Data.CommandType.Text, GetSqlParameters().ToArray());
            }
            return sb;
        }
    }
}
