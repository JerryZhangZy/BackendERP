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
SUM( 
	CASE WHEN COALESCE({SalesOrderHeaderHelper.TableAllies}.OrderStatus, 0) = 0 OR COALESCE({SalesOrderHeaderHelper.TableAllies}.OrderStatus, 1) = 1 THEN 1
	ELSE 0 END
) as open_count,
SUM( 
	CASE WHEN COALESCE({SalesOrderHeaderHelper.TableAllies}.OrderStatus, 0) = 0 OR COALESCE({SalesOrderHeaderHelper.TableAllies}.OrderStatus, 1) = 1 THEN COALESCE({SalesOrderHeaderHelper.TableAllies}.TotalAmount, 0)
	ELSE 0 END
) as open_amount,
SUM( 
	CASE WHEN COALESCE({SalesOrderHeaderHelper.TableAllies}.OrderStatus, 0) = 255 THEN 1
	ELSE 0 END
) as cancel_count,
SUM( 
	CASE WHEN COALESCE({SalesOrderHeaderHelper.TableAllies}.OrderStatus, 0) = 255 THEN COALESCE({SalesOrderHeaderHelper.TableAllies}.TotalAmount, 0)
	ELSE 0 END
) as cancel_amount
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {SalesOrderHeaderHelper.TableName} {SalesOrderHeaderHelper.TableAllies} 
";
            return this.SQL_From;
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
                payload.SalesOrderSummary = null;
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
                    payload.Summary.SalesOrderCount = result.GetData("Count").ToInt();
                    payload.Summary.SalesOrderAmount = result.GetData("Amount").ToString().ToAmount();
                    payload.Summary.OpenSalesOrderCount = result.GetData("open_count").ToInt();
                    payload.Summary.OpenSalesOrderAmount = result.GetData("open_amount").ToString().ToAmount();
                    payload.Summary.CancelSalesOrderCount = result.GetData("cancel_count").ToInt();
                    payload.Summary.CancelSalesOrderAmount = result.GetData("cancel_amount").ToString().ToAmount();
                }
            }
            catch (Exception ex)
            {
                AddError(ex.ObjectToString());
            }
        }


    }
}
