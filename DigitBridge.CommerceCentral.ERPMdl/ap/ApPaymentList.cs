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
using Helper = DigitBridge.CommerceCentral.ERPDb.ApInvoiceTransactionHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ApPaymentList : SqlQueryBuilder<ApPaymentQuery>
    {
        public ApPaymentList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public ApPaymentList(IDataBaseFactory dbFactory, ApPaymentQuery queryObject)
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
{Helper.ApInvoiceUuid()}, 
{Helper.ApInvoiceNum()}, 
{Helper.TransType()},
COALESCE(ptt.text, '') TransTypeText, 
{Helper.TransStatus()},
COALESCE(pts.text, '') TransStatusText, 
{Helper.TransDate()}, 
{Helper.TransTime()}, 
{Helper.Description()}, 
{Helper.Notes()}, 
{Helper.PaidBy()},
{Helper.BankAccountUuid()}, 
{Helper.BankAccountCode()}, 
{Helper.CheckNum()}, 
{Helper.AuthCode()},
{Helper.Amount()}, 
 
{ApInvoiceHeaderHelper.ApInvoiceDate()},  
{ApInvoiceHeaderHelper.DueDate()},  
{ApInvoiceHeaderHelper.TableAllies}.TotalAmount AS ApInvoiceTotalAmount,
{ApInvoiceHeaderHelper.Balance()},

{ApInvoiceHeaderInfoHelper.CentralFulfillmentNum()},

{ApInvoiceHeaderInfoHelper.ShippingCarrier()},
{ApInvoiceHeaderInfoHelper.ShippingClass()},
{ApInvoiceHeaderInfoHelper.DistributionCenterNum()},
{ApInvoiceHeaderInfoHelper.CentralOrderNum()},
{ApInvoiceHeaderInfoHelper.ChannelNum()},
{ApInvoiceHeaderInfoHelper.ChannelAccountNum()},
chanel.ChannelName,
channelAccount.ChannelAccountName,
{ApInvoiceHeaderInfoHelper.ChannelOrderID()},
{ApInvoiceHeaderInfoHelper.RefNum()},
{ApInvoiceHeaderInfoHelper.CustomerPoNum()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            var masterAccountNum = $"{Helper.TableAllies}.MasterAccountNum";
            var profileNum = $"{Helper.TableAllies}.ProfileNum";
            var channelNum = $"{ApInvoiceHeaderInfoHelper.TableAllies}.ChannelNum";
            var channelAccountNum = $"{ApInvoiceHeaderInfoHelper.TableAllies}.ChannelAccountNum";

            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
 LEFT JOIN {ApInvoiceHeaderHelper.TableName} {ApInvoiceHeaderHelper.TableAllies} ON ({ApInvoiceHeaderHelper.TableAllies}.InvoiceUuid = {Helper.TableAllies}.InvoiceUuid)
 LEFT JOIN {ApInvoiceHeaderInfoHelper.TableName} {ApInvoiceHeaderInfoHelper.TableAllies} ON ({ApInvoiceHeaderInfoHelper.TableAllies}.InvoiceUuid = {Helper.TableAllies}.InvoiceUuid)
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
            var transTypePara = paramList.Where(i => i.ParameterName.ToLower() == "@transtype").FirstOrDefault();
            if (transTypePara != null)
                transTypePara.Value = (int)TransTypeEnum.Payment;
            paramList.Add("@PaymentTransStatus".ToEnumParameter<TransStatus>());
            paramList.Add("@PaymentTransType".ToEnumParameter<TransTypeEnum>());
            return paramList.ToArray();
        }

        #endregion override methods

        public virtual void GetApPaymentList(ApPaymentPayload payload)
        {
            if (payload == null)
                payload = new ApPaymentPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.ApTransactionListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.ApTransactionList = sb;
            }
            catch (Exception ex)
            {
                payload.ApTransactionListCount = 0;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task GetApPaymentListAsync(ApPaymentPayload payload)
        {
            if (payload == null)
                payload = new ApPaymentPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.ApTransactionListCount = await CountAsync();
                payload.Success = await ExcuteJsonAsync(sb);
                if (payload.Success)
                    payload.ApTransactionList = sb;
            }
            catch (Exception ex)
            {
                payload.ApTransactionListCount = 0;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(ApPaymentPayload payload)
        {
            if (payload == null)
                payload = new ApPaymentPayload();

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

        public virtual IList<long> GetRowNumList(ApPaymentPayload payload)
        {
            if (payload == null)
                payload = new ApPaymentPayload();

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

        public virtual async Task<StringBuilder> GetExportDataAsync(ApPaymentPayload payload)
        {
            if (payload == null)
                payload = new ApPaymentPayload();

            this.LoadRequestParameter(payload);
            var sql = GetExportSql();
            StringBuilder sb = new StringBuilder();
            using (var trs = new ScopedTransaction(dbFactory))
            {
                await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text, GetSqlParameters().ToArray());
            }
            return sb;
        }

        public virtual StringBuilder GetExportData(ApPaymentPayload payload)
        {
            if (payload == null)
                payload = new ApPaymentPayload();

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
