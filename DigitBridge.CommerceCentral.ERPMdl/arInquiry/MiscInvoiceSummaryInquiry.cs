﻿using DigitBridge.Base.Common;
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
    public class MiscInvoiceSummaryInquiry : SqlQueryBuilder<MiscInvoiceSummaryQuery>
    {
        public MiscInvoiceSummaryInquiry(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public MiscInvoiceSummaryInquiry(IDataBaseFactory dbFactory, MiscInvoiceSummaryQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT  
COUNT(1) as [Count],
SUM(COALESCE({MiscInvoiceHeaderHelper.TableAllies}.TotalAmount,0)) as Amount
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {MiscInvoiceHeaderHelper.TableName} {MiscInvoiceHeaderHelper.TableAllies}  
";
            return this.SQL_From;
        }

        #endregion override methods

        public async virtual Task MiscInvoiceSummaryAsync(MiscInvoicePayload payload)
        {
            if (payload == null)
                payload = new MiscInvoicePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.MiscInvoiceSummary = sb;
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.MiscInvoiceSummary = null;
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
            QueryObject.QueryFilterList.First(x => x.Name == "MiscInvoiceDateFrom").SetValue(payload.Filters.DateFrom);
            QueryObject.QueryFilterList.First(x => x.Name == "MiscInvoiceDateTo").SetValue(payload.Filters.DateTo);
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
                    payload.Summary.MiscInvoiceCount = result.GetData("Count").ToString().ToInt();
                    payload.Summary.MiscInvoiceAmount = result.GetData("Amount").ToString().ToAmount();
                }
            }
            catch (Exception ex)
            {
                AddError(ex.ObjectToString());
            }
        }

    }
}