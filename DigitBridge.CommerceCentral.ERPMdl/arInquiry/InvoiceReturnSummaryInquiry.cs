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
    public class InvoiceReturnSummaryInquiry : SqlQueryBuilder<InvoiceReturnSummaryQuery>
    {
        public InvoiceReturnSummaryInquiry(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InvoiceReturnSummaryInquiry(IDataBaseFactory dbFactory, InvoiceReturnSummaryQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT  
COUNT(1) as [Count],
SUM(COALESCE({ERPDb.InvoiceTransactionHelper.TableAllies}.TotalAmount,0)) as Amount
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {ERPDb.InvoiceTransactionHelper.TableName} {ERPDb.InvoiceTransactionHelper.TableAllies}
 JOIN {InvoiceHeaderHelper.TableName} {InvoiceHeaderHelper.TableAllies} ON ({InvoiceHeaderHelper.TableAllies}.InvoiceUuid = {ERPDb.InvoiceTransactionHelper.TableAllies}.InvoiceUuid)
";
            return this.SQL_From;
        }
        #endregion override methods

        public async virtual Task InvoiceReturnSummaryAsync(InvoiceReturnPayload payload)
        {
            if (payload == null)
                payload = new InvoiceReturnPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.InvoiceReturnSummary = sb;
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.InvoiceReturnSummary = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }
        private void LoadSummaryParameter(CompanySummaryPayload payload)
        {
            if (payload == null)
                return;
            QueryObject.QueryFilterList.First(x => x.Name == "MasterAccountNum").SetValue(payload.MasterAccountNum);
            QueryObject.QueryFilterList.First(x => x.Name == "ProfileNum").SetValue(payload.ProfileNum);
            QueryObject.QueryFilterList.First(x => x.Name == "CustomerCode").SetValue(payload.Filters.CustomerCode);
            QueryObject.QueryFilterList.First(x => x.Name == "TransDateFrom").SetValue(payload.Filters.DateFrom);
            QueryObject.QueryFilterList.First(x => x.Name == "TransDateTo").SetValue(payload.Filters.DateTo);
        }

        public async Task GetCompanySummaryAsync(CompanySummaryPayload payload)
        {
            if (payload.Summary == null)
                payload.Summary = new SummaryInquiryInfoDetail();

            LoadSummaryParameter(payload);
            try
            {
                this.QueryObject.LoadJson = false;
                var result = await ExcuteAsync();
                if (result != null && result.HasData)
                {
                    payload.Summary.InvoiceReturnCount = result.GetData("Count").ToInt();
                    payload.Summary.InvoiceReturnAmount = result.GetData("Amount").ToString().ToAmount();
                }
            }
            catch (Exception ex)
            {
                AddError(ex.ObjectToString());
            }
        }

    }
}
