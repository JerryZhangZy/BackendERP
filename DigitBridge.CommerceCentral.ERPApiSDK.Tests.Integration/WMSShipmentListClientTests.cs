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
    public partial class WMSShipmentListClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        //private string _baseUrl = "http://localhost:7074";
        private string _baseUrl = "https://digitbridge-erp-integration-api-dev.azurewebsites.net";
        private string _code = "aa4QcFoSH4ADcXEROimDtbPa4h0mY/dsNFuK1GfHPAhqx5xMJRAaHw==";
        protected int MasterAccountNum = 10002;
        protected int ProfileNum = 10003;

        public WMSShipmentListClientTests(TestFixture<StartupTest> fixture)
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
        public async Task GetWMSOrderShipmentListAsync_Simple_Test()
        {
            var client = new WMSShipmentListClient(_baseUrl, _code);
            var shipmentIDs = new List<string>()
            { "113-10000001169","113-10000001170"};
            var success = await client.GetWMSOrderShipmentListAsync(MasterAccountNum, ProfileNum, shipmentIDs);
            Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task GetWMSOrderShipmentListAsync_Full_Test()
        {
            var client = new WMSShipmentListClient(_baseUrl, _code);
            var shipmentIDs = new List<string>()
            { "113-10000001169","113-10000001170"};
            var success = await client.GetWMSOrderShipmentListAsync(MasterAccountNum, ProfileNum, shipmentIDs);
            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null && client.ResopneData.WMSShipmentProcessesList != null);

            if (client.ResopneData.WMSShipmentProcessesList.Count <= 0) return;

            success = client.ResopneData.WMSShipmentProcessesList.Count(i => shipmentIDs.Contains(i.ShipmentID)) > 0;

            Assert.True(success, "GetWMSOrderShipmentListAsync no result.");

        }
    }
}
