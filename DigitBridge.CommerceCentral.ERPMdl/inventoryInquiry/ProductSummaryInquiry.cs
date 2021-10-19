using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ProductSummaryInquiry
    {
        protected IDataBaseFactory dbFactory;

        public ProductSummaryInquiry(IDataBaseFactory dataBaseFactory)
        {
            dbFactory = dataBaseFactory;
        }

        public async Task GetProductSummaryAsync(CompanySummaryPayload payload)
        {
            if (payload.Summary == null)
                payload.Summary = new SummaryInquiryInfoDetail();
            using (var trx = new ScopedTransaction(dbFactory))
            {
                var sql = @$"SELECT COUNT(1)
FROM ProductBasic
WHERE MasterAccountNum={payload.MasterAccountNum} AND ProfileNum={payload.ProfileNum}";
                var count = await SqlQuery.ExecuteScalarAsync<int>(sql);
                payload.Summary.ProductCount = count;
            }
            using (var trx = new ScopedTransaction(dbFactory))
            {
                var sql = @$"SELECT COUNT(1)
FROM ProductBasic pb
WHERE MasterAccountNum={payload.MasterAccountNum} AND ProfileNum={payload.ProfileNum} AND
NOT EXISTS (
	select soh.OrderDate,soi.SKU from SalesOrderHeader soh outer apply (select SKU from SalesOrderItems soi where soh.SalesOrderUuid=soi.SalesOrderUuid) as soi WHERE soh.OrderDate >= '{payload.Filters.DateFrom.ToString("yyyy-MM-dd")}' AND soh.OrderDate <= '{payload.Filters.DateTo.ToString("yyyy-MM-dd")}'
	AND pb.SKU = soi.SKU
)";
                var count = await SqlQuery.ExecuteScalarAsync<int>(sql);
                payload.Summary.NonSalesProductCount =  count;
            }
        }
    }
}
