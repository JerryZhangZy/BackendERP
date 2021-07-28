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

    }
}
