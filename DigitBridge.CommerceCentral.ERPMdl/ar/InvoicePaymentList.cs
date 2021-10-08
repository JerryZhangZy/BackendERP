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
{Helper.RowNum()}, 
{Helper.TransUuid()}, 
{Helper.TransNum()}, 
{Helper.InvoiceUuid()}, 
{Helper.InvoiceNumber()}, 
{Helper.TransStatus()},
{Helper.PaidBy()},  
COALESCE(pts.text, '') TransStatusText, 
COALESCE(ppb.text, '') PaidByText, 
{Helper.TransDate()}, 
{Helper.TransTime()}, 
{Helper.TotalAmount()}, 
{InvoiceHeaderHelper.DueDate()}, 
{InvoiceHeaderHelper.CustomerUuid()}, 
{InvoiceHeaderHelper.CustomerCode()}, 
{InvoiceHeaderHelper.CustomerName()},  
{InvoiceHeaderInfoHelper.CentralOrderNum()},
{InvoiceHeaderInfoHelper.ChannelNum()},
{InvoiceHeaderInfoHelper.ChannelOrderID()},
{InvoiceHeaderInfoHelper.BillToEmail()},
{InvoiceHeaderInfoHelper.ShipToName()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
 LEFT JOIN {InvoiceHeaderHelper.TableName} {InvoiceHeaderHelper.TableAllies} ON ({InvoiceHeaderHelper.TableAllies}.InvoiceUuid = {Helper.TableAllies}.InvoiceUuid)
 LEFT JOIN {InvoiceHeaderInfoHelper.TableName} {InvoiceHeaderInfoHelper.TableAllies} ON ({InvoiceHeaderInfoHelper.TableAllies}.InvoiceUuid = {Helper.TableAllies}.InvoiceUuid)
 LEFT JOIN @PaymentTransStatus pts ON ({Helper.TableAllies}.TransStatus = pts.num)
 LEFT JOIN @PaidBy ppb ON ({Helper.TableAllies}.PaidBy = ppb.num) 
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            paramList.Add("@PaymentTransStatus".ToEnumParameter<TransStatus>());
            paramList.Add("@PaidBy".ToEnumParameter<PaidByEnum>());
            return paramList.ToArray();
        }

        #endregion override methods

        public virtual void GetInvoicePaymentList(InvoicePaymentPayload payload)
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
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task GetInvoicePaymentListAsync(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.InvoiceTransactionListCount = await CountAsync();
                payload.Success = await ExcuteJsonAsync(sb);
                if (payload.Success)
                    payload.InvoiceTransactionList = sb;
            }
            catch (Exception ex)
            {
                payload.InvoiceTransactionListCount = 0;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

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

        public virtual IList<long> GetRowNumList(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

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

        public virtual async Task<StringBuilder> GetExportDataAsync(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

            this.LoadRequestParameter(payload);
            var sql = GetExportSql();
            StringBuilder sb = new StringBuilder();
            using (var trs = new ScopedTransaction(dbFactory))
            {
                await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text, GetSqlParameters().ToArray());
            }
            return sb;
        }

        public virtual StringBuilder GetExportData(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

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
