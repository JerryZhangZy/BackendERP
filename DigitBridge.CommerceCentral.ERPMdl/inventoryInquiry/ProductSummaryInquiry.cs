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

        public async Task GetCompanySummaryAsync(CompanySummaryPayload payload)
        {
            if (payload.Summary == null)
                payload.Summary = new SummaryInquiryInfoDetail();

            LoadSummaryParameter(payload);
            try
            {
                this.QueryObject.LoadJson = false;
                var result = await ExcuteAsync(GetSQL_select());
                if (result != null && result.HasData)
                {
                    payload.Summary.ProductCount = result.GetData("count").ToInt();
                    payload.Summary.NonSalesProductCount = result.GetData("non_count").ToInt();
                    payload.Summary.SoldProductCount = result.GetData("sold_count").ToInt();
                }
            }
            catch (Exception ex)
            {
                AddError(ex.ObjectToString());
            }
        }

        public async virtual Task GetProductSummaryAsync(InventoryPayload payload)
        {
            if (payload == null)
                payload = new InventoryPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = await ExcuteJsonAsync(sb, $"{GetSQL_select()} FOR JSON PATH");
                if (payload.Success)
                    payload.ProductSummary = sb;
            }
            catch (Exception ex)
            {
                payload.ProductSummary = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
                payload.Success = false;
            }
        }
    }
}
