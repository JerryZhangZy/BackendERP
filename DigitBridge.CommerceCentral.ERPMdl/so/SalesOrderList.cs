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
COALESCE(ordtp.text, '') OrderTypeText, 
{SalesOrderHeaderHelper.OrderStatus()}, 
COALESCE(ordst.text, '') OrderStatusText, 
{SalesOrderHeaderHelper.OrderDate()}, 
{SalesOrderHeaderHelper.OrderTime()}, 
{SalesOrderHeaderHelper.CustomerUuid()}, 
{SalesOrderHeaderHelper.CustomerCode()}, 
{SalesOrderHeaderHelper.CustomerName()}, 
{SalesOrderHeaderHelper.Terms()}, 
{SalesOrderHeaderHelper.TermsDays()}, 
{SalesOrderHeaderHelper.TotalAmount()} 
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {SalesOrderHeaderHelper.TableName} {SalesOrderHeaderHelper.TableAllies} 
 LEFT JOIN {CustomerHelper.TableName} {CustomerHelper.TableAllies} ON ({CustomerHelper.TableAllies}.CustomerUuid = {SalesOrderHeaderHelper.TableAllies}.CustomerUuid)
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
                payload.SalesOrderListCount = await CountAsync().ConfigureAwait(false);
                result = await ExcuteJsonAsync(sb).ConfigureAwait(false);
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

        public virtual async Task<IList<SalesOrderData>> GetSalesOrderDatasAsync(SalesOrderQuery queryObject)
        {
            this.QueryObject = queryObject;

            //SetSecurityParameter(1, 1);
            //this.LoadRequestParameter(payload);
            var sqlWhere = QueryObject.GetSQLWithPrefixBySqlParameter(SalesOrderHeaderHelper.TableAllies);
            var sql = $@"
SELECT {SalesOrderHeaderHelper.TableAllies}.*,
(SELECT * FROM SalesOrderHeaderInfo i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON AUTO) AS SalesOrderHeaderInfoJson,
(SELECT * FROM SalesOrderHeaderAttributes i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON AUTO) AS SalesOrderHeaderAttributesJson,
(SELECT * FROM SalesOrderItems i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON AUTO) AS SalesOrderItemsJson,
(SELECT * FROM SalesOrderItemsAttributes i WHERE i.SalesOrderUuid = {SalesOrderHeaderHelper.TableAllies}.SalesOrderUuid FOR JSON AUTO) AS SalesOrderItemsAttributesJson
FROM SalesOrderHeader {SalesOrderHeaderHelper.TableAllies}
WHERE {sqlWhere}
FOR JSON AUTO
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
                    return datas;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
