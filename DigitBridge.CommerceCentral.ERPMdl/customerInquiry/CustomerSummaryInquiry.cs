using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class CustomerSummaryInquiry : SqlQueryBuilder<SalesOrderSummaryQuery>
    {
        protected IDataBaseFactory dbFactory;

        public CustomerSummaryInquiry(IDataBaseFactory dataBaseFactory)
        {
            dbFactory = dataBaseFactory;
        }


        protected override string GetSQL_select()
        {
            var whereCustomer = this.QueryObject.GetSQLWithoutPrefix("ins");
            var whereInvoice = this.QueryObject.GetSQLWithPrefix("ins");

            if (string.IsNullOrWhiteSpace(whereCustomer))
                whereCustomer = $" WHERE {whereCustomer}";
            var whereCustomerAnd = (string.IsNullOrWhiteSpace(whereCustomer)) ? "" : $" AND {whereCustomer}";

            if (string.IsNullOrWhiteSpace(whereInvoice))
                whereInvoice = $" AND {whereInvoice}";

            this.SQL_Select = $@"
SELECT c.[count], c.active_count, non.[Count] AS nonsales_count
    FROM (
        SELECT
        COUNT(1) as [count], 
        SUM(
            CASE WHEN COALESCE(cus.CustomerStatus, 0) = 1 THEN 1
			ELSE 0 END
		) as new_count,
		SUM(
            CASE WHEN COALESCE(cus.CustomerStatus, 0) = 9 THEN 1
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
        --ins.InvoiceDate >= '1/1/2021' AND ins.InvoiceDate <= '10/17/2021' AND ins.CustomerUuid = cus.CustomerUuid
    )
    {whereCustomerAnd}
) non
";
            return this.SQL_Select;
        }



        public async Task GetCustomerSummaryAsync(CompanySummaryPayload payload)
        {
            if (payload.Summary == null)
                payload.Summary = new SummaryInquiryInfo
                {
                    Filter = payload.Filters,
                    Summary = new SummaryInquiryInfoDetail()
                };
            using (var trx = new ScopedTransaction(dbFactory))
            {
                var sql = @$"SELECT
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
WHERE MasterAccountNum={payload.MasterAccountNum} AND ProfileNum={payload.ProfileNum}";

                using (var dataReader = await SqlQuery.ExecuteCommandAsync(sql))
                {
                    if (await dataReader.ReadAsync())
                    {
                        payload.Summary.Summary.Customer = new SummaryInquiryInfoItem { Count = dataReader.GetInt32(0) };
                        payload.Summary.Summary.NewCumstomer = new SummaryInquiryInfoItem { Count = dataReader.GetInt32(1) };
                        payload.Summary.Summary.ActiveCumstomer = new SummaryInquiryInfoItem { Count = dataReader.GetInt32(2) };
                    }
                }
            }
            using (var trx = new ScopedTransaction(dbFactory))
            {
                var sql = @$"SELECT COUNT(1)
FROM customer cus
WHERE MasterAccountNum={payload.MasterAccountNum} AND ProfileNum={payload.ProfileNum} AND 
NOT EXISTS (
	select * FROM InvoiceHeader ins WHERE ins.InvoiceDate >= '{payload.Filters.DateFrom.ToString("yyyy-MM-dd")}' AND ins.InvoiceDate <= '{payload.Filters.DateTo.ToString("yyyy-MM-dd")}'
	AND ins.CustomerUuid = cus.CustomerUuid
)";
                

                using (var dataReader = await SqlQuery.ExecuteCommandAsync(sql))
                {
                    if (await dataReader.ReadAsync())
                    {
                        payload.Summary.Summary.NonSalesCustomer = new SummaryInquiryInfoItem { Count = dataReader.GetInt32(0) };
                    }
                }
            }
        }

    }
}
