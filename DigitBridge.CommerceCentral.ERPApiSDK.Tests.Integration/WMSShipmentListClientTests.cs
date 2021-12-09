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
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class WMSShipmentListClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }
        protected int MasterAccountNum = 10002;
        protected int ProfileNum = 10003;

        public WMSShipmentListClientTests(TestFixture<StartupTest> fixture)
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
        public void Dispose()
        {
        }


        [Fact()]
        public async Task GetWMSOrderShipmentListAsync_Simple_Test()
        {
            var list = dbFactory.Find<ERPDb.EventProcessERP>(
                $"select top 3 * from EventProcessERP where ERPEventProcessType= {(int)EventProcessTypeEnum.ShipmentFromWMS}");
            var shipmentIDs = list.Select(i => i.ProcessUuid).Distinct().ToList();
            Assert.True(shipmentIDs != null && shipmentIDs.Count > 0, "No shipmentIDs found in EventProcessERP");

            var client = new WMSShipmentListClient(_baseUrl, _code); 
            var success = await client.GetWMSOrderShipmentListAsync(MasterAccountNum, ProfileNum, shipmentIDs);
            Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task GetWMSOrderShipmentListAsync_Full_Test()
        {
            var list = dbFactory.Find<ERPDb.EventProcessERP>(
                $"select top 2 * from EventProcessERP where ERPEventProcessType= {(int)EventProcessTypeEnum.ShipmentFromWMS} order by rownum desc");
            var shipmentIDs = list.Select(i => i.ProcessUuid).Distinct().ToList();
            Assert.True(shipmentIDs != null && shipmentIDs.Count > 0, "No shipmentIDs found in EventProcessERP");

            var client = new WMSShipmentListClient(_baseUrl, _code);

            var success = await client.GetWMSOrderShipmentListAsync(MasterAccountNum, ProfileNum, shipmentIDs);
            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null && client.ResopneData.WMSShipmentProcessesList != null);

            if (client.ResopneData.WMSShipmentProcessesList.Count <= 0) return;

            success = client.ResopneData.WMSShipmentProcessesList.Count(i => shipmentIDs.Contains(i.ShipmentID)) > 0;

            Assert.True(success, "GetWMSOrderShipmentListAsync no result.");

        }
    }
}
