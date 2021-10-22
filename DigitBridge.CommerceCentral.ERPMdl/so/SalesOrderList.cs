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
using Hepler = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderHelper;
using InfoHepler = DigitBridge.CommerceCentral.ERPDb.SalesOrderHeaderInfoHelper;

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
{Hepler.SalesOrderUuid()}, 
{Hepler.OrderNumber()}, 
{Hepler.OrderType()}, 
COALESCE(ordtp.text, '') orderTypeText, 
{Hepler.OrderStatus()}, 
COALESCE(ordst.text, '') orderStatusText, 
{Hepler.OrderDate()}, 
{Hepler.ShipDate()}, 
{Hepler.CustomerCode()}, 
{Hepler.CustomerName()}, 
{Hepler.Terms()}, 
{Hepler.TermsDays()}, 
{Hepler.SubTotalAmount()},
{Hepler.TotalAmount()},
{Hepler.OrderSourceCode()},
{InfoHepler.CentralFulfillmentNum()},
{InfoHepler.ShippingCarrier()},
{InfoHepler.ShippingClass()},
{InfoHepler.DistributionCenterNum()},
{InfoHepler.CentralOrderNum()},
{InfoHepler.CentralOrderUuid()},
{InfoHepler.ChannelNum()},
{InfoHepler.ChannelAccountNum()},
{InfoHepler.ChannelOrderID()},
{InfoHepler.SecondaryChannelOrderID()},
{InfoHepler.WarehouseUuid()},
{InfoHepler.WarehouseCode()},
{InfoHepler.RefNum()},
{InfoHepler.CustomerPoNum()},
{InfoHepler.ShipToName()},
{InfoHepler.ShipToAttention()},
{InfoHepler.ShipToAddressLine1()},
{InfoHepler.ShipToAddressLine2()},
{InfoHepler.ShipToCity()},
{InfoHepler.ShipToState()},
{InfoHepler.ShipToPostalCode()},
{InfoHepler.ShipToCountry()},
{InfoHepler.ShipToEmail()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Hepler.TableName} {Hepler.TableAllies} 
 LEFT JOIN {InfoHepler.TableName} {InfoHepler.TableAllies} ON ({InfoHepler.TableAllies}.SalesOrderUuid = {InfoHepler.TableAllies}.SalesOrderUuid)
-- LEFT JOIN {CustomerHelper.TableName} {CustomerHelper.TableAllies} ON ({ERPDb.CustomerHelper.TableAllies}.CustomerUuid = {Hepler.TableAllies}.CustomerUuid)
 LEFT JOIN @SalesOrderStatus ordst ON ({Hepler.TableAllies}.OrderStatus = ordst.num)
 LEFT JOIN @SalesOrderType ordtp ON ({Hepler.TableAllies}.OrderType = ordtp.num)
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

        public virtual void GetSalesOrderList(SalesOrderPayload payload)
        {
            if (payload == null)
                payload = new SalesOrderPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.SalesOrderListCount = Count();
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.SalesOrderList = sb;
            }
            catch (Exception ex)
            {
                payload.SalesOrderListCount = 0;
                payload.SalesOrderList = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task GetSalesOrderListAsync(SalesOrderPayload payload)
        {
            if (payload == null)
                payload = new SalesOrderPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                //TODO 
                //if(payload.IsQueryTotalCount)
                payload.SalesOrderListCount = await CountAsync();
                payload.Success = await ExcuteJsonAsync(sb);
                if (payload.Success)
                    payload.SalesOrderList = sb;
            }
            catch (Exception ex)
            {
                payload.SalesOrderListCount = 0;
                payload.SalesOrderList = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
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

        //TODO where sql
        private string GetExportSql()
        {
            var sql = $@"
SELECT JSON_QUERY((SELECT * FROM SalesOrderHeader i where i.RowNum={SalesOrderHeaderHelper.TableAllies}.RowNum FOR JSON PATH,WITHOUT_ARRAY_WRAPPER)) AS SalesOrderHeader,
JSON_QUERY((SELECT * FROM SalesOrderHeaderInfo i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON PATH,WITHOUT_ARRAY_WRAPPER)) AS SalesOrderHeaderInfo,
JSON_QUERY((SELECT * FROM SalesOrderHeaderAttributes i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON PATH,WITHOUT_ARRAY_WRAPPER)) AS SalesOrderHeaderAttributes,
(SELECT * FROM SalesOrderItems i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON PATH) AS SalesOrderItems,
(SELECT * FROM SalesOrderItemsAttributes i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON PATH) AS SalesOrderItemsAttributes
FROM SalesOrderHeader {SalesOrderHeaderHelper.TableAllies}
";
            return sql;
        }

        private string GetExportCommandText(SalesOrderPayload payload)
        {
            this.LoadRequestParameter(payload);
            return $@"
{GetExportSql()}
{GetSQL_where()}
ORDER BY  {SalesOrderHeaderHelper.TableAllies}.RowNum  
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
FOR JSON PATH
";
        }

        public virtual void GetExportJsonList(SalesOrderPayload payload)
        {
            if (payload == null)
                payload = new SalesOrderPayload();

            var sql = GetExportCommandText(payload);

            try
            {
                payload.SalesOrderListCount = Count();
                StringBuilder sb = new StringBuilder();
                var result = ExcuteJson(sb, sql, GetSqlParameters().ToArray());
                if (result)
                    payload.SalesOrderDataList = sb;
            }
            catch (Exception ex)
            {
                payload.SalesOrderDataList = null;
                throw;
            }
        }

        public virtual async Task GetExportJsonListAsync(SalesOrderPayload payload)
        {
            if (payload == null)
                payload = new SalesOrderPayload();

            var sql = GetExportCommandText(payload);

            try
            {
                payload.SalesOrderListCount = await CountAsync();
                StringBuilder sb = new StringBuilder();
                var result = await ExcuteJsonAsync(sb, sql, GetSqlParameters().ToArray());
                if (result)
                    payload.SalesOrderDataList = sb;
            }
            catch (Exception ex)
            {
                payload.SalesOrderDataList = null;
                throw;
            }
        }

    }
}
