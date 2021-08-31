using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using Helper = DigitBridge.CommerceCentral.ERPDb.InvoiceHeaderHelper;
using InfoHelper = DigitBridge.CommerceCentral.ERPDb.InvoiceHeaderInfoHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceList : SqlQueryBuilder<InvoiceQuery>
    {
        public InvoiceList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InvoiceList(IDataBaseFactory dbFactory, InvoiceQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.RowNum()}, 
{Helper.InvoiceUuid()}, 
{Helper.InvoiceNumber()}, 
{Helper.SalesOrderUuid()}, 
{Helper.OrderNumber()}, 
{Helper.InvoiceType()}, 
COALESCE(itt.text, '') invoiceTypeText, 
{Helper.InvoiceStatus()}, 
COALESCE(ist.text, '') invoiceStatusText, 
{Helper.InvoiceDate()}, 
{Helper.DueDate()}, 
{Helper.InvoiceTime()}, 
{Helper.CustomerUuid()}, 
{Helper.CustomerCode()}, 
{Helper.CustomerName()}, 
{Helper.Terms()}, 
{Helper.TermsDays()}, 
{Helper.SubTotalAmount()},
{Helper.TotalAmount()},
{InfoHelper.CentralOrderNum()},
{InfoHelper.ChannelNum()},
{InfoHelper.ChannelOrderID()},
{InfoHelper.BillToEmail()},
{InfoHelper.ShipToName()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
LEFT JOIN {InfoHelper.TableName} {InfoHelper.TableAllies} ON ({Helper.TableAllies}.InvoiceUuid = {InfoHelper.TableAllies}.InvoiceUuid)
 LEFT JOIN @InvoiceStatus ist ON ({Helper.TableAllies}.InvoiceStatus = ist.num)
 LEFT JOIN @InvoiceType itt ON ({Helper.TableAllies}.InvoiceType = itt.num)
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            paramList.Add("@InvoiceStatus".ToEnumParameter<InvoiceStatus>());
            paramList.Add("@InvoiceType".ToEnumParameter<InvoiceType>());

            return paramList.ToArray();
        
        }

        #endregion override methods

        public virtual InvoicePayload GetInvoiceList(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.InvoiceListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.InvoiceList = sb;
            }
            catch (Exception ex)
            {
                payload.InvoiceListCount = 0;
                payload.InvoiceList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<InvoicePayload> GetInvoiceListAsync(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.InvoiceListCount = await CountAsync().ConfigureAwait(false);
                result = await ExcuteJsonAsync(sb).ConfigureAwait(false);
                if (result)
                    payload.InvoiceList = sb;
            }
            catch (Exception ex)
            {
                payload.InvoiceListCount = 0;
                payload.InvoiceList = null;
                return payload;
                throw;
            }
            return payload;
        }
        public virtual async Task<IList<long>> GetRowNumListAsync(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

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

        public virtual IList<long> GetRowNumList(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

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

        /// <summary>
        /// Get row num list by  numbers
        /// </summary>
        /// <param name="invoiceNumbers"></param>
        /// <returns></returns>
        public virtual async Task<List<long>> GetRowNumListAsync(IList<string> invoiceNumbers, int masterAccountNum, int profileNum)
        {
            var sql = @"
SELECT RowNum FROM InvoiceHeader ins
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND @invoiceNumbers.exist('/parameters/value[text()=sql:column(''ins.InvoiceNumber'')]')=1";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@masterAccountNum",masterAccountNum),
                new SqlParameter("@profileNum",profileNum),
                new SqlParameter("@invoiceNumbers",invoiceNumbers.ListToXml()){ DbType=DbType.Xml}
        };
            using var trs = new ScopedTransaction(dbFactory);
            return await SqlQuery.ExecuteAsync(
                sql,
                (long rowNum) => rowNum,
                paras
            );
        }

    }
}
