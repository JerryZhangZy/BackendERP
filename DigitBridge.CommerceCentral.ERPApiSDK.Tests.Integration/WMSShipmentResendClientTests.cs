using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class WMSShipmentResendClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        //private string _baseUrl = "http://localhost:7073";
        private string _baseUrl = "https://digitbridge-erp-integration-api-dev.azurewebsites.net";
        private string _code = "drZEGmRUVmGcitmCqyp3VZe5b4H8fSoy8rDUsEMkfG9U7UURXMtnrw==";

        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public WMSShipmentResendClientTests(TestFixture<StartupTest> fixture)
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
        public async Task ResendEventAsync_Simple_Test()
        {
            var client = new WMSShipmentResendClient(_baseUrl, _code);
            var shipmentIDs = new List<string>() {"113-10000001139","113-10000001140",};
            var success = await client.ResendWMSShipmentToErpAsync(MasterAccountNum, ProfileNum, shipmentIDs);
            Assert.True(success, client.Messages.ObjectToString());

        }
        [Fact()]
        public async Task ResendEventAsync_Full_Test()
        {
            var client = new WMSShipmentResendClient(_baseUrl, _code);
            var shipmentIDs = new List<string>() { "113-10000001139", "113-10000001140", };
            var success = await client.ResendWMSShipmentToErpAsync(MasterAccountNum, ProfileNum, shipmentIDs);

            Assert.True(success, client.Messages.ObjectToString());

            var sentShipmentIDs = client.ResopneData?.SentWMSShipmentIDs;
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
