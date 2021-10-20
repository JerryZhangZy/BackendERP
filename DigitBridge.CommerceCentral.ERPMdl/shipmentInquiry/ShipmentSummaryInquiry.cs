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
    public class ShipmentSummaryInquiry : SqlQueryBuilder<ShipmentSummaryQuery>
    {
        public ShipmentSummaryInquiry(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public ShipmentSummaryInquiry(IDataBaseFactory dbFactory, ShipmentSummaryQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT  
COUNT(1) as [Count],
SUM(COALESCE({OrderShipmentHeaderHelper.TableAllies}.ShippingCost,0)) as Amount
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {OrderShipmentHeaderHelper.TableName} {OrderShipmentHeaderHelper.TableAllies} 
 LEFT JOIN {SalesOrderHeaderHelper.TableName} {SalesOrderHeaderHelper.TableAllies} ON ( {SalesOrderHeaderHelper.TableAllies}.OrderSourceCode = 'OrderDCAssignmentNum:' + Cast({OrderShipmentHeaderHelper.TableAllies}.OrderDCAssignmentNum  as varchar))
";
            return this.SQL_From;
        } 
        #endregion override methods

        public async virtual Task ShipmentSummaryAsync(OrderShipmentPayload payload)
        {
            if (payload == null)
                payload = new OrderShipmentPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.ShipmentSummary = sb;
            }
            catch (Exception ex)
            { 
                payload.ShipmentSummary = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public async Task GetCompanySummaryAsync(CompanySummaryPayload payload)
        {
            if (payload.Summary == null)
                payload.Summary = new SummaryInquiryInfoDetail();

            this.LoadRequestParameter(payload);
            try
            {
                this.QueryObject.LoadJson = false;
                var result = await ExcuteAsync();
                if (result != null && result.HasData)
                {
                    payload.Summary.ShipmentCount = result.GetData("Count").ToInt();
                    payload.Summary.ShipmentAmount = result.GetData("Amount").ToString().ToAmount();
                }
            }
            catch (Exception ex)
            {
                AddError(ex.ObjectToString());
            }
        }

    }
}
