﻿using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
{Helper.RowNum()}, 
{Helper.TransUuid()}, 
{Helper.TransNum()}, 
{Helper.InvoiceUuid()}, 
{Helper.InvoiceNumber()}, 
{Helper.TransStatus()},  
COALESCE(rts.text, '') TransStatusText,  
{Helper.TransDate()}, 
{Helper.TransTime()}, 
{Helper.SubTotalAmount()},
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
 LEFT JOIN @RetrunTransStatus rts ON ({Helper.TableAllies}.TransStatus = rts.num) 
";
            return this.SQL_From;
        }
        //protected override string GetSQL_where()
        //{
        //    var whereSql = base.GetSQL_where();
        //    //if (string.IsNullOrWhiteSpace(whereSql))
        //    //    whereSql = $" WHERE ";
        //    var existSql =
        //        $" exists (" +
        //            $" select 1 from {InvoiceReturnItemsHelper.TableName} {InvoiceReturnItemsHelper.TableAllies}  " +
        //            $" where  {InvoiceReturnItemsHelper.TableAllies}.TransUuid= {Helper.TableAllies}.TransUuid " +
        //                $"AND  {InvoiceReturnItemsHelper.TableAllies}.SKU= @SKU "  
        //        $") ";
        //    return whereSql;
        //}
        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            paramList.Add("@RetrunTransStatus".ToEnumParameter<TransStatus>());
            paramList.Add("@PaidBy".ToEnumParameter<PaidByEnum>());
            return paramList.ToArray();
        }

        #endregion override methods

        public virtual void GetInvoiceReturnList(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.InvoiceTransactionListCount = Count();
                payload.Success = ExcuteJson(sb);
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

        public virtual async Task GetInvoiceReturnListAsync(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

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
            StringBuilder sb = new StringBuilder();
            using (var trs = new ScopedTransaction(dbFactory))
            {
                await SqlQuery.QueryJsonAsync(sb, GetExportSql(), System.Data.CommandType.Text, GetSqlParameters().ToArray());
            }
            return sb;
        }

        public virtual StringBuilder GetExportData(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            using (var trs = new ScopedTransaction(dbFactory))
            {
                SqlQuery.QueryJson(sb, GetExportSql(), System.Data.CommandType.Text, GetSqlParameters().ToArray());
            }
            return sb;
        }
    }
}
