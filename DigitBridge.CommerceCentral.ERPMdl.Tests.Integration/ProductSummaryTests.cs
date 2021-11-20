using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class ProductSummaryTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory dataBaseFactory { get; set; }

        public ProductSummaryTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            try
            {
                var Seq = 0;
                dataBaseFactory = DataBaseFactory.CreateDefault(Configuration["dsn"].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Dispose()
        {
        }

        #region async methods
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ProductSummaryAsync_Test()
        {
            var payload = new InventoryPayload();
            payload.MasterAccountNum = 10001;
            payload.ProfileNum = 10001;
            var qry = new ProductSummaryQuery();
            var srv = new ProductSummaryInquiry(dataBaseFactory, qry);

            StringBuilder sb = new StringBuilder();
            using (var b = new Benchmark("ProductSummaryAsync"))
            {
                await srv.GetProductSummaryAsync(payload);
            }

            System.Diagnostics.Debug.WriteLine($"result is {payload.ProductSummary}");

            System.Diagnostics.Debug.WriteLine($"error is {payload.Messages}");

            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetCompanySummaryAsync_Test()
        {
            var payload = new CompanySummaryPayload();
            payload.MasterAccountNum = 10001;
            payload.ProfileNum = 10001;
            payload.Filters = new SummaryInquiryFilter
            {
                DateFrom = new DateTime(DateTime.UtcNow.Date.Year, 1, 1),
                DateTo = DateTime.UtcNow.Date
            };

            var qry = new ProductSummaryQuery();
            var srv = new ProductSummaryInquiry(dataBaseFactory, qry);

            StringBuilder sb = new StringBuilder();
            using (var b = new Benchmark("GetCompanySummaryAsync"))
            {
                await srv.GetCompanySummaryAsync(payload);
            }

            System.Diagnostics.Debug.WriteLine($"result is {payload.Summary}");

            System.Diagnostics.Debug.WriteLine($"error is {payload.Messages}");

            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion async methods

    }
}
