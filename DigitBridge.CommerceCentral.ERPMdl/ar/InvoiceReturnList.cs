using DigitBridge.Base.Common;
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
{Helper.TransType()},
COALESCE(ptt.text, '') transTypeText, 
{Helper.TransStatus()},
COALESCE(pts.text, '') transStatusText, 
{Helper.TransDate()}, 
{Helper.TransTime()}, 
{Helper.TotalAmount()}, 

{InvoiceHeaderHelper.QboDocNumber()}, 
{InvoiceHeaderHelper.SalesOrderUuid()}, 
{InvoiceHeaderHelper.OrderNumber()}, 
{InvoiceHeaderHelper.InvoiceDate()},  
{InvoiceHeaderHelper.DueDate()}, 
{InvoiceHeaderHelper.CustomerUuid()}, 
{InvoiceHeaderHelper.CustomerCode()}, 
{InvoiceHeaderHelper.CustomerName()},  
{InvoiceHeaderHelper.TableAllies}.TotalAmount AS invoiceTotalAmount,  
{InvoiceHeaderHelper.Balance()},

{InvoiceHeaderInfoHelper.CentralFulfillmentNum()},
{InvoiceHeaderInfoHelper.OrderShipmentNum()},
{InvoiceHeaderInfoHelper.OrderShipmentUuid()},
{InvoiceHeaderInfoHelper.ShippingCarrier()},
{InvoiceHeaderInfoHelper.ShippingClass()},
{InvoiceHeaderInfoHelper.DistributionCenterNum()},
{InvoiceHeaderInfoHelper.CentralOrderNum()},
{InvoiceHeaderInfoHelper.ChannelNum()},
{InvoiceHeaderInfoHelper.ChannelAccountNum()},
chanel.ChannelName as channelName,
channelAccount.ChannelAccountName as channelAccountName,
{InvoiceHeaderInfoHelper.ChannelOrderID()},
{InvoiceHeaderInfoHelper.RefNum()},
{InvoiceHeaderInfoHelper.CustomerPoNum()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            var masterAccountNum = $"{Helper.TableAllies}.MasterAccountNum";
            var profileNum = $"{Helper.TableAllies}.ProfileNum";
            var channelNum = $"{InvoiceHeaderInfoHelper.TableAllies}.ChannelNum";
            var channelAccountNum = $"{InvoiceHeaderInfoHelper.TableAllies}.ChannelAccountNum";

            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
 LEFT JOIN {InvoiceHeaderHelper.TableName} {InvoiceHeaderHelper.TableAllies} ON ({InvoiceHeaderHelper.TableAllies}.InvoiceUuid = {Helper.TableAllies}.InvoiceUuid)
 LEFT JOIN {InvoiceHeaderInfoHelper.TableName} {InvoiceHeaderInfoHelper.TableAllies} ON ({InvoiceHeaderInfoHelper.TableAllies}.InvoiceUuid = {Helper.TableAllies}.InvoiceUuid)
 LEFT JOIN @PaymentTransStatus pts ON ({Helper.TableAllies}.TransStatus = pts.num)
 LEFT JOIN @PaymentTransType ptt ON ({Helper.TableAllies}.TransType = ptt.num)
 {SqlStringHelper.Join_Setting_Channel(masterAccountNum, profileNum, channelNum)} 
 {SqlStringHelper.Join_Setting_ChannelAccount(masterAccountNum, profileNum, channelNum, channelAccountNum)} 
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();

            var transTypePara = paramList.Where(i => i.ParameterName.ToLower() == "transtype").FirstOrDefault();
            if (transTypePara != null)
                transTypePara.Value = (int)TransTypeEnum.Return;

            paramList.Add("@PaymentTransStatus".ToEnumParameter<TransStatus>());
            paramList.Add("@PaymentTransType".ToEnumParameter<TransTypeEnum>());
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

        protected override string GetSQL_select_summary()
        {
            this.SQL_SelectSummary = $@"
SELECT 
COUNT(1) AS totalCount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 0 THEN 1 ELSE 0 END) as draftCount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 1 THEN 1 ELSE 0 END) as overdueCount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 2 THEN 1 ELSE 0 END) as pendingCount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 3 THEN 1 ELSE 0 END) as payableCount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 4 THEN 1 ELSE 0 END) as paidCount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 5 THEN 1 ELSE 0 END) as trashCount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 6 THEN 1 ELSE 0 END) as unpaidCount,
SUM(COALESCE({Helper.TableAllies}.TotalAmount, 0)) AS totalAmount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 0 THEN {Helper.TableAllies}.TotalAmount ELSE 0 END) as draftAmount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 1 THEN {Helper.TableAllies}.TotalAmount ELSE 0 END) as overdueAmount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 2 THEN {Helper.TableAllies}.TotalAmount ELSE 0 END) as pendingAmount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 3 THEN {Helper.TableAllies}.TotalAmount ELSE 0 END) as payableAmount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 4 THEN {Helper.TableAllies}.TotalAmount ELSE 0 END) as paidAmount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 5 THEN {Helper.TableAllies}.TotalAmount ELSE 0 END) as trashAmount,
SUM(CASE WHEN COALESCE({Helper.TableAllies}.TransStatus, 0) = 6 THEN {Helper.TableAllies}.TotalAmount ELSE 0 END) as unpaidAmount
";
            return this.SQL_SelectSummary;
        }

        public virtual async Task GetInvoiceReturnListSummaryAsync(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = await ExcuteSummaryJsonAsync(sb);
                if (payload.Success)
                    payload.InvoiceTransactionListSummary = sb;
            }
            catch (Exception ex)
            {
                payload.InvoiceTransactionListCount = 0;
                payload.InvoiceTransactionListSummary = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }
    }
}
