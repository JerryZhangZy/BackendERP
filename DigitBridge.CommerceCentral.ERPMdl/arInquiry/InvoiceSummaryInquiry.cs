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
    public class InvoiceSummaryInquiry : SqlQueryBuilder<InvoiceSummaryQuery>
    {
        public InvoiceSummaryInquiry(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InvoiceSummaryInquiry(IDataBaseFactory dbFactory, InvoiceSummaryQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT  
COUNT(1) as [Count],
SUM(COALESCE({InvoiceHeaderHelper.TableAllies}.TotalAmount,0)) as Amount
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {InvoiceHeaderHelper.TableName} {InvoiceHeaderHelper.TableAllies}  
";
            return this.SQL_From;
        }

        #endregion override methods

        public async virtual Task InvoiceSummaryAsync(InvoicePayload payload)
        {
            if (payload == null)
                payload = new InvoicePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.InvoiceSummary = sb;
            }
            catch (Exception ex)
            {
                payload.InvoiceSummary = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }


    }
}
