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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderSummaryInquiry : SqlQueryBuilder<SalesOrderSummaryQuery>
    {
        public SalesOrderSummaryInquiry(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public SalesOrderSummaryInquiry(IDataBaseFactory dbFactory, SalesOrderSummaryQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT  
COUNT(1) as [Count],
SUM(COALESCE({SalesOrderHeaderHelper.TableAllies}.TotalAmount,0)) as Amount
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {SalesOrderHeaderHelper.TableName} {SalesOrderHeaderHelper.TableAllies}  
 LEFT JOIN @SalesOrderStatus ordst ON ({SalesOrderHeaderHelper.TableAllies}.OrderStatus = ordst.num)
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            paramList.Add("@SalesOrderStatus".ToEnumParameter<SalesOrderStatus>());
            return paramList.ToArray();

        }

        #endregion override methods

        public async virtual Task SalesOrderSummaryAsync(SalesOrderPayload payload)
        {
            if (payload == null)
                payload = new SalesOrderPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.SalesOrderSummary = sb;
            }
            catch (Exception ex)
            {
                payload.SalesOrderListCount = 0;
                payload.SalesOrderList = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }


    }
}
