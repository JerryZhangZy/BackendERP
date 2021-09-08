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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderList : SqlQueryBuilder<SalesOrderQuery>
    {
        public SalesOrderList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public SalesOrderList(IDataBaseFactory dbFactory, SalesOrderQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{SalesOrderHeaderHelper.RowNum()}, 
{SalesOrderHeaderHelper.DatabaseNum()}, 
{SalesOrderHeaderHelper.MasterAccountNum()}, 
{SalesOrderHeaderHelper.ProfileNum()}, 
{SalesOrderHeaderHelper.SalesOrderUuid()}, 
{SalesOrderHeaderHelper.OrderNumber()}, 
{SalesOrderHeaderHelper.OrderType()}, 
COALESCE(ordtp.text, '') orderTypeText, 
{SalesOrderHeaderHelper.OrderStatus()}, 
COALESCE(ordst.text, '') orderStatusText, 
{SalesOrderHeaderHelper.OrderDate()}, 
{SalesOrderHeaderHelper.ShipDate()}, 
{SalesOrderHeaderHelper.OrderTime()}, 
{SalesOrderHeaderHelper.CustomerUuid()}, 
{SalesOrderHeaderHelper.CustomerCode()}, 
{SalesOrderHeaderHelper.CustomerName()}, 
{SalesOrderHeaderHelper.Terms()}, 
{SalesOrderHeaderHelper.TermsDays()}, 
{SalesOrderHeaderHelper.SubTotalAmount()},
{SalesOrderHeaderHelper.TotalAmount()},
{SalesOrderHeaderInfoHelper.CentralOrderNum()},
{SalesOrderHeaderInfoHelper.ChannelNum()},
{SalesOrderHeaderInfoHelper.ChannelOrderID()},
{SalesOrderHeaderInfoHelper.BillToEmail()},
{SalesOrderHeaderInfoHelper.ShipToName()}

";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {SalesOrderHeaderHelper.TableName} {SalesOrderHeaderHelper.TableAllies} 
 LEFT JOIN {SalesOrderHeaderInfoHelper.TableName} {SalesOrderHeaderInfoHelper.TableAllies} ON ({SalesOrderHeaderInfoHelper.TableAllies}.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid)
 LEFT JOIN {CustomerHelper.TableName} {CustomerHelper.TableAllies} ON ({ERPDb.CustomerHelper.TableAllies}.CustomerUuid = {SalesOrderHeaderHelper.TableAllies}.CustomerUuid)
 LEFT JOIN @SalesOrderStatus ordst ON ({SalesOrderHeaderHelper.TableAllies}.OrderStatus = ordst.num)
 LEFT JOIN @SalesOrderType ordtp ON ({SalesOrderHeaderHelper.TableAllies}.OrderType = ordtp.num)
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            paramList.Add("@SalesOrderStatus".ToEnumParameter<SalesOrderStatus>());
            paramList.Add("@SalesOrderType".ToEnumParameter<SalesOrderType>());

            return paramList.ToArray();

        }

        #endregion override methods

        public virtual SalesOrderPayload GetSalesOrderList(SalesOrderPayload payload)
        {
            if (payload == null)
                payload = new SalesOrderPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.SalesOrderListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.SalesOrderList = sb;
            }
            catch (Exception ex)
            {
                payload.SalesOrderListCount = 0;
                payload.SalesOrderList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<SalesOrderPayload> GetSalesOrderListAsync(SalesOrderPayload payload)
        {
            if (payload == null)
                payload = new SalesOrderPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                //TODO 
                //if(payload.IsQueryTotalCount)
                payload.SalesOrderListCount = await CountAsync();
                result = await ExcuteJsonAsync(sb);
                if (result)
                    payload.SalesOrderList = sb;
            }
            catch (Exception ex)
            {
                //TODO
                //Throw or return only one work.
                //Wirte ex to LogCenter then return, or throw.

                //payload.SalesOrderListCount = 0;
                //payload.SalesOrderList = null;
                //return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<IList<SalesOrderData>> GetSalesOrderDatasAsync(SalesOrderQuery queryObject)
        {
            this.QueryObject = queryObject;

            QueryObject.SetSecurityParameter(10001, 10001);
            //this.LoadRequestParameter(payload);
            var sqlWhere = QueryObject.GetSQLWithPrefixBySqlParameter(SalesOrderHeaderHelper.TableAllies);
            var sql = $@"
SELECT {SalesOrderHeaderHelper.TableAllies}.*,
(SELECT * FROM SalesOrderHeaderInfo i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON PATH) AS SalesOrderHeaderInfoJson,
(SELECT * FROM SalesOrderHeaderAttributes i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON PATH) AS SalesOrderHeaderAttributesJson,
(SELECT * FROM SalesOrderItems i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON PATH) AS SalesOrderItemsJson,
(SELECT * FROM SalesOrderItemsAttributes i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON PATH) AS SalesOrderItemsAttributesJson
FROM SalesOrderHeader {SalesOrderHeaderHelper.TableAllies}
WHERE {sqlWhere}
FOR JSON PATH
";
            var param = QueryObject.GetSqlParametersWithPrefix(SalesOrderHeaderHelper.TableAllies);

            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                result = await ExcuteJsonAsync(sb, sql, param);
                if (result)
                {
                    var datas = new List<SalesOrderData>();
                    var headers = sb.ToString().JsonToObject<IList<SalesOrderHeader>>();
                    if (headers != null)
                    {
                        foreach (var h in headers)
                        {
                            var data = new SalesOrderData(dbFactory);
                            data.Clear();
                            data.SalesOrderHeader = h;
                            if (h.HasSalesOrderHeaderInfoJson)
                            {
                                data.SalesOrderHeaderInfo = h.SalesOrderHeaderInfoJson[0];
                                h.SalesOrderHeaderInfoJson = null;
                            }
                            if (h.HasSalesOrderHeaderAttributesJson)
                            {
                                data.SalesOrderHeaderAttributes = h.SalesOrderHeaderAttributesJson[0];
                                h.SalesOrderHeaderAttributesJson = null;
                            }
                            if (h.HasSalesOrderItemsJson)
                            {
                                data.SalesOrderItems = h.SalesOrderItemsJson;
                                h.SalesOrderItemsJson = null;
                            }
                            if (h.HasSalesOrderItemsAttributesJson)
                            {
                                data.SalesOrderItemsAttributes = h.SalesOrderItemsAttributesJson;
                                h.SalesOrderItemsAttributesJson = null;
                            }
                            datas.Add(data);
                        }
                    }
                    return datas;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual IList<SalesOrderData> GetSalesOrderDatas(SalesOrderQuery queryObject)
        {
            this.QueryObject = queryObject;

            QueryObject.SetSecurityParameter(10001, 10001);
            //this.LoadRequestParameter(payload);
            var sqlWhere = QueryObject.GetSQLWithPrefixBySqlParameter(SalesOrderHeaderHelper.TableAllies);
            var sql = $@"
SELECT {SalesOrderHeaderHelper.SelectAll(SalesOrderHeaderHelper.TableAllies, SalesOrderHeaderHelper.TableName)},
{SalesOrderHeaderInfoHelper.SelectAll(SalesOrderHeaderInfoHelper.TableAllies, SalesOrderHeaderInfoHelper.TableName)},
{SalesOrderHeaderAttributesHelper.SelectAll(SalesOrderHeaderAttributesHelper.TableAllies, SalesOrderHeaderAttributesHelper.TableName)},
( SELECT {SalesOrderItemsHelper.SelectAll(SalesOrderItemsHelper.TableAllies)},{SalesOrderItemsAttributesHelper.SelectAll(SalesOrderItemsAttributesHelper.TableAllies, SalesOrderItemsAttributesHelper.TableName)} 
  FROM {SalesOrderItemsHelper.TableName} {SalesOrderItemsHelper.TableAllies} 
  LEFT JOIN {SalesOrderItemsAttributesHelper.TableName} {SalesOrderItemsAttributesHelper.TableAllies} ON ({SalesOrderItemsAttributesHelper.TableAllies}.SalesOrderItemsUuid = {SalesOrderItemsHelper.TableAllies}.SalesOrderItemsUuid)
  WHERE {SalesOrderItemsHelper.TableAllies}.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON PATH
) AS SalesOrderItems,
(SELECT * FROM SalesOrderItemsAttributes i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON PATH) AS SalesOrderItemsAttributes
FROM {SalesOrderHeaderHelper.TableName} {SalesOrderHeaderHelper.TableAllies}
LEFT JOIN {SalesOrderHeaderInfoHelper.TableName} {SalesOrderHeaderInfoHelper.TableAllies} ON ({SalesOrderHeaderInfoHelper.TableAllies}.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid)
LEFT JOIN {SalesOrderHeaderAttributesHelper.TableName} {SalesOrderHeaderAttributesHelper.TableAllies} ON ({SalesOrderHeaderAttributesHelper.TableAllies}.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid)
WHERE {sqlWhere}
FOR JSON PATH
";
            var param = QueryObject.GetSqlParametersWithPrefix(SalesOrderHeaderHelper.TableAllies);

            StringBuilder sb = new StringBuilder();
            if (ExcuteJson(sb, sql, param))
            {
                return sb.ToString().JsonToObject<IList<SalesOrderData>>();
            }
            return null;
        }
        public virtual async Task<IList<long>> GetRowNumListAsync(SalesOrderPayload payload)
        {
            if (payload == null)
                payload = new SalesOrderPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();

            var sql = $@"
SELECT distinct {SalesOrderHeaderHelper.TableAllies}.RowNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {SalesOrderHeaderHelper.TableAllies}.RowNum
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

        public virtual IList<long> GetRowNumList(SalesOrderPayload payload)
        {
            if (payload == null)
                payload = new SalesOrderPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            var sql = $@"
SELECT distinct {SalesOrderHeaderHelper.TableAllies}.RowNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {SalesOrderHeaderHelper.TableAllies}.RowNum
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

        /// <summary>
        /// Get row num list by order numbers
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<List<long>> GetRowNumListAsync(IList<string> orderNumbers, int masterAccountNum, int profileNum)
        {
            var sql = @"
SELECT RowNum FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND @orderNumbers.exist('/parameters/value[text()=sql:column(''tbl.OrderNumber'')]')=1";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@masterAccountNum",masterAccountNum),
                new SqlParameter("@profileNum",profileNum),
                new SqlParameter("@orderNumbers",orderNumbers.ListToXml()){ DbType=DbType.Xml}
        };
            using (var trs = new ScopedTransaction(dbFactory))
            {
                return await SqlQuery.ExecuteAsync(
                    sql,
                    (long rowNum) => rowNum,
                    paras
                );
            }
        }

    }
}
