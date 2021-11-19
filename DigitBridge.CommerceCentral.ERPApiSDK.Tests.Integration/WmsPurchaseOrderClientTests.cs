using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class WMSPurchaseOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl = "http://localhost:7074/api/";
        //private string _baseUrl = "https://digitbridge-erp-integration-api-dev.azurewebsites.net/api/";
        private string _code = "aa4QcFoSH4ADcXEROimDtbPa4h0mY/dsNFuK1GfHPAhqx5xMJRAaHw==";
        protected int MasterAccountNum = 10001;
        protected int ProfileNum = 10001;

        public WMSPurchaseOrderClientTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;
            InitForTest();
        }
        protected void InitForTest()
        {
        }
        public void Dispose()
        {
        }


        [Fact()]
        public async Task GetPurchaseOrdersOpenListAsync_Simple_Test()
        {
            var client = new WMSPurchaseOrderClient(_baseUrl, _code);
            var success = await client.GetPurchaseOrdersOpenListAsync(MasterAccountNum, ProfileNum);
            Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task GetPurchaseOrdersOpenListAsync_Full_Test()
        {
            var client = new WMSPurchaseOrderClient(_baseUrl, _code);
            var payload = new WMSPurchaseOrderRequestPayload()
            {
                //LoadAll = true,
                Top = 10,
                //Filter = new PurchaseOrderListFilter()
                //{
                //    //UpdateDateUtc = DateTime.UtcNow.Date.AddDays(-1),
                //    WarehouseCode = "VBT001-1-3-4"
                //},
            };

            var success = await client.GetPurchaseOrdersOpenListAsync(MasterAccountNum, ProfileNum, payload);

            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null);

            if (client.ResopneData.PurchaseOrderListCount <= 0) return;

            Assert.True(client.ResopneData.PurchaseOrderList != null, $"Count:{client.ResopneData.PurchaseOrderListCount}, PurchaseOrderList:no data.");

            //success = client.ResopneData.PurchaseOrderList.Count(i => i.WarehouseCode == payload.Filter.WarehouseCode) == client.ResopneData.PurchaseOrderListCount;

            //Assert.True(success, "Filter by WarehouseCode reuslt is not correct.");

        }
    }
}
