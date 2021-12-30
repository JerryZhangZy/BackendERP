using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public class InventoryLogSummaryTest : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory dataBaseFactory { get; set; }

        public InventoryLogSummaryTest(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            try
            {
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

        [Fact]
        public async Task GetCompanySummaryAsync_Test()
        {
            var payload = new CompanySummaryPayload();
            payload.MasterAccountNum = 10001;
            payload.ProfileNum = 10001;
            payload.Filters = new SummaryInquiryFilter
            {
                DateFrom = DateTime.UtcNow.Date.AddDays(-31),
                DateTo = DateTime.UtcNow.Date
            };

            var qry = new InventoryLogSummaryQuery();
            var srv = new InventoryLogInquiry(dataBaseFactory, qry);

            StringBuilder sb = new StringBuilder();
            using (var b = new Benchmark("GetCompanySummaryAsync"))
            {
                await srv.GetCompanySummaryAsync(payload);
            }

            System.Diagnostics.Debug.WriteLine($"result is {payload.Summary}");

            System.Diagnostics.Debug.WriteLine($"error is {payload.Messages}");

            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }
    }
}
