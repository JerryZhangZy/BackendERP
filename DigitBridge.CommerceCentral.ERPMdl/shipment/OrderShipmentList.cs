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
using Helper = DigitBridge.CommerceCentral.ERPDb.OrderShipmentHeaderHelper;
using InfoHelper = DigitBridge.CommerceCentral.ERPDb.OrderShipmentPackageHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class OrderShipmentList : SqlQueryBuilder<OrderShipmentQuery>
    {
        public OrderShipmentList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public OrderShipmentList(IDataBaseFactory dbFactory, OrderShipmentQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.TableAllies}.*
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
LEFT JOIN {InfoHelper.TableName} {InfoHelper.TableAllies} ON ({Helper.TableAllies}.OrderShipmentUuid = {InfoHelper.TableAllies}.OrderShipmentUuid)
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();

            return paramList.ToArray();
        
        }

        #endregion override methods

        public virtual OrderShipmentPayload GetOrderShipmentList(OrderShipmentPayload payload)
        {
            if (payload == null)
                payload = new OrderShipmentPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.OrderShipmentListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.OrderShipmentList = sb;
            }
            catch (Exception ex)
            {
                payload.OrderShipmentListCount = 0;
                payload.OrderShipmentList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<OrderShipmentPayload> GetOrderShipmentListAsync(OrderShipmentPayload payload)
        {
            if (payload == null)
                payload = new OrderShipmentPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.OrderShipmentListCount = await CountAsync().ConfigureAwait(false);
                result = await ExcuteJsonAsync(sb).ConfigureAwait(false);
                if (result)
                    payload.OrderShipmentList = sb;
            }
            catch (Exception ex)
            {
                payload.OrderShipmentListCount = 0;
                payload.OrderShipmentList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(OrderShipmentPayload payload)
        {
            if (payload == null)
                payload = new OrderShipmentPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();

            var sql = $@"
SELECT distinct {Helper.TableAllies}.OrderShipmentNum 
{GetSQL_from()} 
{GetSQL_where()}
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

        public virtual IList<long> GetRowNumList(OrderShipmentPayload payload)
        {
            if (payload == null)
                payload = new OrderShipmentPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            var sql = $@"
SELECT distinct {Helper.TableAllies}.OrderShipmentNum 
{GetSQL_from()} 
{GetSQL_where()}
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

    }
}
