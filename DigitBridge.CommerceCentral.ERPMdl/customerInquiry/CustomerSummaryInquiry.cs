﻿using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class CustomerSummaryInquiry : SqlQueryBuilder<CustomerSummaryQuery>
    {
        public CustomerSummaryInquiry(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public CustomerSummaryInquiry(IDataBaseFactory dbFactory, CustomerSummaryQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        private void LoadSummaryParameter(CompanySummaryPayload payload)
        {
            if (payload == null)
                return;
            QueryObject.QueryFilterList.First(x => x.Name == "MasterAccountNum").SetValue(payload.MasterAccountNum);
            QueryObject.QueryFilterList.First(x => x.Name == "ProfileNum").SetValue(payload.ProfileNum);
            QueryObject.QueryFilterList.First(x => x.Name == "CustomerCode").SetValue(payload.Filters.CustomerCode);
            QueryObject.QueryFilterList.First(x => x.Name == "CustomerName").SetValue(payload.Filters.Name);
            QueryObject.QueryFilterList.First(x => x.Name == "DateFrom").SetValue(payload.Filters.DateFrom);
            QueryObject.QueryFilterList.First(x => x.Name == "DateTo").SetValue(payload.Filters.DateTo);
        }
        protected override string GetSQL_select()
        {
            var whereCustomer = this.QueryObject.GetSQLWithoutPrefix("ins");
            var whereInvoice = this.QueryObject.GetSQLWithPrefix("ins");
            var whereCustomerAnd = string.Empty;

            if (!string.IsNullOrWhiteSpace(whereCustomer))
            {
                whereCustomerAnd = $" AND {whereCustomer}";
                whereCustomer = $" WHERE {whereCustomer}";
            }

            if (!string.IsNullOrWhiteSpace(whereInvoice))
                whereInvoice = $" AND {whereInvoice}";

            this.SQL_Select = $@"
SELECT c.[count], c.new_count, c.active_count, non.[Count] AS nonsales_count
    FROM (
        SELECT
        COUNT(1) as [count], 
        SUM(
            CASE WHEN COALESCE(cus.CustomerStatus, 0) = {(int)CustomerStatus.New} THEN 1
			ELSE 0 END
		) as new_count,
		SUM(
            CASE WHEN COALESCE(cus.CustomerStatus, 0) = {(int)CustomerStatus.Active} THEN 1
			ELSE 0 END
		) as active_count
    FROM Customer cus
    {whereCustomer}
) c
OUTER APPLY(
    SELECT COUNT(1) AS[Count]
    FROM customer cus

    WHERE
    NOT EXISTS(
        select* FROM InvoiceHeader ins
        WHERE ins.CustomerUuid = cus.CustomerUuid {whereInvoice}
    )
    {whereCustomerAnd}
) non
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            return "--";
        }

        public async Task GetCompanySummaryAsync(CompanySummaryPayload payload)
        {
            if (payload.Summary == null)
                payload.Summary = new SummaryInquiryInfoDetail();

            LoadSummaryParameter(payload);
            try
            {
                this.OnlySQLSelect = true;
                var result = await ExcuteAsync();
                if (result != null && result.HasData)
                {
                    payload.Summary.CustomerCount = result.GetData("count").ToInt();
                    payload.Summary.NewCustomerCount = result.GetData("new_count").ToInt();
                    payload.Summary.NonSalesCustomerCount = result.GetData("nonsales_count").ToInt();
                }
            }
            catch (Exception ex)
            {
                AddError(ex.ObjectToString());
            }
        }

        public async virtual Task GetCustomerSummaryAsync(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            this.OnlySQLSelect = true;
            try
            {
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.CustomerSummary = sb;
            }
            catch (Exception ex)
            {
                payload.CustomerSummary = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
                payload.Success = false;
            }
        }
    }
}
