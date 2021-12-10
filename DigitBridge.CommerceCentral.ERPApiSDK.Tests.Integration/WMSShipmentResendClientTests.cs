using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class WMSShipmentResendClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }

        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public WMSShipmentResendClientTests(TestFixture<StartupTest> fixture)
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
        public async Task ResendEventAsync_Simple_Test()
        {
            var list = dbFactory.Find<ERPDb.EventProcessERP>(
               $"select top 3 * from EventProcessERP where ERPEventProcessType= {(int)EventProcessTypeEnum.ShipmentFromWMS} order by rownum desc");
            var shipmentIDs = list.Select(i => i.ProcessUuid).Distinct().ToList();
            Assert.True(shipmentIDs != null && shipmentIDs.Count > 0, "No shipmentIDs found in EventProcessERP");

            var client = new WMSShipmentResendClient(_baseUrl, _code);
            //var shipmentIDs = new List<string>() {"113-10000001139","113-10000001140",};
            var success = await client.ResendWMSShipmentToErpAsync(MasterAccountNum, ProfileNum, shipmentIDs);
            Assert.True(success, client.Messages.ObjectToString());

        }
        [Fact()]
        public async Task ResendEventAsync_Full_Test()
        {
            var list = dbFactory.Find<ERPDb.EventProcessERP>(
               $"select top 3 * from EventProcessERP where ERPEventProcessType= {(int)EventProcessTypeEnum.ShipmentFromWMS} order by rownum desc");
            var shipmentIDs = list.Select(i => i.ProcessUuid).Distinct().ToList();
            Assert.True(shipmentIDs != null && shipmentIDs.Count > 0, "No shipmentIDs found in EventProcessERP");

            var client = new WMSShipmentResendClient(_baseUrl, _code);
            //var shipmentIDs = new List<string>() { "113-10000001139", "113-10000001140", };
            var success = await client.ResendWMSShipmentToErpAsync(MasterAccountNum, ProfileNum, shipmentIDs);

            Assert.True(success, client.Messages.ObjectToString());

            var sentShipmentIDs = client.Data;
            Assert.True(sentShipmentIDs != null && sentShipmentIDs.Count > 0, "All shipmentID send failed.");

            foreach (var sentShipmentID in sentShipmentIDs)
            {
                Assert.True(sentShipmentIDs.Contains(sentShipmentID), $"EventUuid send failed, EventUuid:{sentShipmentID}");
            }

        }
        public void Dispose()
        {
        }
    }
}
