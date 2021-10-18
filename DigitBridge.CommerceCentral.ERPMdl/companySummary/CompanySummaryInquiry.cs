using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class CompanySummaryInquiry
    {
        protected CustomerSummaryInquiry CustomerSummaryService;
        protected ProductSummaryInquiry ProductSummaryService;

        protected IDataBaseFactory dbFactory;

        public CompanySummaryInquiry(IDataBaseFactory dataBaseFactory)
        {
            dbFactory = dataBaseFactory;
            CustomerSummaryService = new CustomerSummaryInquiry(dataBaseFactory);
            ProductSummaryService = new ProductSummaryInquiry(dataBaseFactory);
        }

        public async Task GetCompaySummaryAsync(CompanySummaryPayload payload)
        {
            await CustomerSummaryService.GetCustomerSummaryAsync(payload);
            await ProductSummaryService.GetProductSummaryAsync(payload);
        }
    }
}
