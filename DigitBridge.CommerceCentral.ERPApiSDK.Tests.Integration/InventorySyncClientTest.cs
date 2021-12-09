using DigitBridge.CommerceCentral.ERPApiSDK.ApiClient;
using DigitBridge.CommerceCentral.ERPApiSDK.Model;

using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{

    public partial class InventorySyncClientTest : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }

        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public InventorySyncClientTest(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        private IDataBaseFactory dbFactory { get; set; }
        protected void InitForTest()
        {
            _baseUrl = Configuration["ERP_Integration_Api_BaseUrl"];
            _code = Configuration["ERP_Integration_Api_AuthCode"];
            dbFactory = new DataBaseFactory(Configuration["dsn"]);
        }


        [Fact()]
        public async Task SendAddData_Test()
        {
            var client = new WMSInventorySyncClient(_baseUrl, _code);
            var inventorySyncItems = new List<InventorySyncItemsModel>()
            {
                new InventorySyncItemsModel() { SKU = "Bike", WarehouseCode = "nobis", Qty = 20 }
            }
            ;

            var result = await client.InventoryDataAsync(MasterAccountNum, ProfileNum, inventorySyncItems);
            Assert.True(result, "succ");
            Assert.True(client.Messages.Count == 0, "succ");

        }


        public void Dispose()
        {
        }
    }
}
