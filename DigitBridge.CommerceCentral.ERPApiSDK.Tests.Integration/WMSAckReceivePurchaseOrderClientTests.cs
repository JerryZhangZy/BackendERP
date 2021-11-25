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
    public partial class WMSAckReceivePurchaseOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl = "http://localhost:7074";
        //private string _baseUrl = "https://digitbridge-erp-integration-api-dev.azurewebsites.net";
        private string _code = "aa4QcFoSH4ADcXEROimDtbPa4h0mY/dsNFuK1GfHPAhqx5xMJRAaHw==";
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public WMSAckReceivePurchaseOrderClientTests(TestFixture<StartupTest> fixture)
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

        /// <summary>
        /// Only test the api invocke successfully.
        /// </summary>
        /// <returns></returns>
        [Fact()]
        public async Task AckReceivePurchaseOrdersAsync_Simple_Test()
        {
            var client = new WMSAckReceivePurchaseOrderClient(_baseUrl, _code);
            var PurchaseOrderUuids = new List<string>() { "15082ed6-b62f-4faf-9491-dc182d7bd4a9", "61F6B440-17B2-4A33-BD4F-27CD7C5C3F5D" };
            var success = await client.AckReceivePurchaseOrdersAsync(MasterAccountNum, ProfileNum, PurchaseOrderUuids);
            Assert.True(client.ResopneData != null);
        }

        [Fact()]
        public async Task AckReceivePurchaseOrdersAsync_Full_Test()
        {
            var PurchaseOrderUuids = await GetOpenPurchaseOrderUuids();

            var client = new WMSAckReceivePurchaseOrderClient(_baseUrl, _code);
            var success = await client.AckReceivePurchaseOrdersAsync(MasterAccountNum, ProfileNum, PurchaseOrderUuids);
            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null);
        }

        public async Task<IList<string>> GetOpenPurchaseOrderUuids()
        {
            var client = new WMSPurchaseOrderClient(_baseUrl, _code);

            var payload = new WMSPurchaseOrderRequestPayload()
            {
                LoadAll = false,
                Top = 2,
            };

            var success = await client.GetPurchaseOrdersOpenListAsync(MasterAccountNum, ProfileNum, payload);

            Assert.True(success, client.Messages.ObjectToString());

            var PurchaseOrderUuids = client?.ResopneData?.PurchaseOrderList?.Select(i => i.PoUuid).ToList();

            Assert.True(PurchaseOrderUuids != null, "no open PurchaseOrder.");

            return PurchaseOrderUuids;
        }
    }
}
