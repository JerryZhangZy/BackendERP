using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ProductSummaryInquiry : SqlQueryBuilder<ProductSummaryQuery>
    {
        public ProductSummaryInquiry(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        public ProductSummaryInquiry(IDataBaseFactory dbFactory, ProductSummaryQuery queryObject)
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
            var whereProduct = this.QueryObject.GetSQLWithoutPrefix("ord");
            var whereSO = this.QueryObject.GetSQLWithPrefix("ord");
            var whereProductAnd = string.Empty;

            if (!string.IsNullOrWhiteSpace(whereProduct))
            {
                whereProductAnd = $" AND {whereProduct}";
                whereProduct = $" WHERE {whereProduct}";
            }

            if (!string.IsNullOrWhiteSpace(whereSO))
                whereSO = $" WHERE {whereSO}";

            this.SQL_Select = $@"
SELECT c.count,non.non_count,c.count-non.non_count as sold_count FROM
(SELECT COUNT(1) as count FROM ProductBasic prd {whereProduct}) c
    OUTER APPLY
(SELECT COUNT(SKU) as non_count
 FROM ProductBasic prd
 WHERE SKU NOT IN(
         select ordl.SKU from SalesOrderItems ordl
             outer apply (select OrderDate from SalesOrderHeader ord where ordl.SalesOrderUuid=ord.SalesOrderUuid) ord
         {whereSO}
     ) {whereProductAnd}
) non
";
            return this.SQL_Select;
        }

        public async Task GetProductSummaryAsync(CompanySummaryPayload payload)
        {
            if (payload.Summary == null)
                payload.Summary = new SummaryInquiryInfoDetail();

            LoadSummaryParameter(payload);
            using (var trx = new ScopedTransaction(dbFactory))
            {
                using (var dataReader = await SqlQuery.ExecuteCommandAsync(GetSQL_select()))
                {
                    if (await dataReader.ReadAsync())
                    {
                        payload.Summary.ProductCount = dataReader.GetInt32(0);
                        payload.Summary.NonSalesProductCount = dataReader.GetInt32(1);
                        payload.Summary.SoldProductCount = dataReader.GetInt32(2);
                    }
                }
            }
        }

        //        public async Task GetProductSummaryAsync(CompanySummaryPayload payload)
        //        {
        //            if (payload.Summary == null)
        //                payload.Summary = new SummaryInquiryInfoDetail();
        //            using (var trx = new ScopedTransaction(dbFactory))
        //            {
        //                var sql = @$"SELECT COUNT(1)
        //FROM ProductBasic
        //WHERE MasterAccountNum={payload.MasterAccountNum} AND ProfileNum={payload.ProfileNum}";
        //                var count = await SqlQuery.ExecuteScalarAsync<int>(sql);
        //                payload.Summary.ProductCount = count;
        //            }
        //            using (var trx = new ScopedTransaction(dbFactory))
        //            {
        //                var sql = @$"SELECT COUNT(1)
        //FROM ProductBasic pb
        //WHERE MasterAccountNum={payload.MasterAccountNum} AND ProfileNum={payload.ProfileNum} AND
        //NOT EXISTS (
        //	select soh.OrderDate,soi.SKU from SalesOrderHeader soh outer apply (select SKU from SalesOrderItems soi where soh.SalesOrderUuid=soi.SalesOrderUuid) as soi WHERE soh.OrderDate >= '{payload.Filters.DateFrom.ToString("yyyy-MM-dd")}' AND soh.OrderDate <= '{payload.Filters.DateTo.ToString("yyyy-MM-dd")}'
        //	AND pb.SKU = soi.SKU
        //)";
        //                var count = await SqlQuery.ExecuteScalarAsync<int>(sql);
        //                payload.Summary.NonSalesProductCount =  count;
        //            }
        //        }
    }
}
