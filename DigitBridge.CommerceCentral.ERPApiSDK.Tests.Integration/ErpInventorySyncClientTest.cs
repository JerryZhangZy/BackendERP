using DigitBridge.CommerceCentral.ERPApiSDK.ApiClient;
using DigitBridge.CommerceCentral.ERPApiSDK.Model;

using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{

    public partial class ErpInventorySyncClientTest : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug ErpInventorySyncClientTest Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl = "http://localhost:7071";
        private string _code = "drZEGmRUVmGcitmCqyp3VZe5b4H8fSoy8rDUsEMkfG9U7UURXMtnrw==";

        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public ErpInventorySyncClientTest(TestFixture<StartupTest> fixture)
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
            var client = new ErpInventorySyncClient(_baseUrl, _code);

            var result = await client.InventorySyncAsync(MasterAccountNum, ProfileNum, new InventorySyncUpdatePayload()
            {
                InventorySyncItems = new List<InventorySyncItem>()
                {
                    new InventorySyncItem() { SKU = "Bike", WarehouseCode = "nobis", Qty = 20 }
                }
            });
            Assert.True(result, "succ");
            Assert.True(client.Messages.Count == 0, "succ");

        }


        public void Dispose()
        {
        }
    }
}
