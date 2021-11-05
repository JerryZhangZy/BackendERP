using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class WmsPurchaseOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl = "http://localhost:7074/api/purchaseOrders/find";
        private string _code = "drZEGmRUVmGcitmCqyp3VZe5b4H8fSoy8rDUsEMkfG9U7UURXMtnrw==";

        public WmsPurchaseOrderClientTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Fact()]
        public async Task SendAddData_Test()
        {
            var client = new WmsPurchaseOrderClient(_baseUrl, _code);
            var query = new WmsQueryModel()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                UpdateDateFrom = DateTime.Now.AddDays(-10),
                UpdateDateTo = DateTime.Now
            };
            var result =await client.QueryWmsPurchaseOrderListAsync(query);
            Assert.True(client.Data != null, "succ");
            Assert.True(client.ResultTotalCount >0, "succ");
            Assert.True(result, "succ");
        }
        [Fact()]
        public async Task SendAddDataWithConfig_Test()
        {
            var client = new WmsPurchaseOrderClient();
            var query = new WmsQueryModel()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                UpdateDateFrom = DateTime.Now.AddDays(-10),
                UpdateDateTo = DateTime.Now
            };
            var result =await client.QueryWmsPurchaseOrderListAsync(query);
            Assert.True(client.Data != null, "succ");
            Assert.True(client.ResultTotalCount >0, "succ");
            Assert.True(result, "succ");
        }

        public void Dispose()
        {
        }
    }
}
