

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class OrderShipmentSummaryTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory dataBaseFactory { get; set; }

        public OrderShipmentSummaryTests(TestFixture<StartupTest> fixture)
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
        public async Task OrderShipmentSummaryAsync_Test()
        {
            var payload = new OrderShipmentPayload();
            payload.MasterAccountNum = 10001;
            payload.ProfileNum = 10001;
            //payload.Filter = new JObject()
            //{
            //    { "OrderDateFrom",  DateTime.Today.AddDays(-30) },
            //    { "OrderStatus",  "11,18,86" }
            //};

            var qry = new ShipmentSummaryQuery();
            var srv = new ShipmentSummaryInquiry(dataBaseFactory, qry);

            StringBuilder sb = new StringBuilder();
            using (var b = new Benchmark("OrderShipmentSummaryAsync"))
            {
                await srv.ShipmentSummaryAsync(payload);
            }

            System.Diagnostics.Debug.WriteLine($"result is {payload.ShipmentSummary}");

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
            //payload.Filter = new JObject()
            //{
            //    { "OrderDateFrom",  DateTime.Today.AddDays(-30) },
            //    { "OrderStatus",  "11,18,86" }
            //};

            var qry = new ShipmentSummaryQuery();
            var srv = new ShipmentSummaryInquiry(dataBaseFactory, qry);

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

