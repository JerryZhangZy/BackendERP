using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
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
                payload.Summary = new SummaryInquiryInfoDetail();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                using (var dataReader = await SqlQuery.ExecuteCommandAsync(GetSQL_select()))
                {
                    if (await dataReader.ReadAsync())
                    {
                        payload.Summary.CustomerCount = dataReader.GetInt32(0) ;
                        payload.Summary.NewCustomerCount = dataReader.GetInt32(0) ;
                        payload.Summary.NonSalesCustomerCount = dataReader.GetInt32(0) ;
                    }
                }
            }
        }

    }
}
