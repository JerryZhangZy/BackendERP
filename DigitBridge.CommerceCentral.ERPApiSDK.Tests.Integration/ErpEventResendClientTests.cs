using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class ErpEventResendClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }

        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public ErpEventResendClientTests(TestFixture<StartupTest> fixture)
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
            var client = new ErpEventResendClient(_baseUrl, _code);
            var eventUuids = new List<string>() { "c2dc72e4-6e74-49c3-9ab6-eb2a951d5622" };
            var success = await client.ResendEventAsync(MasterAccountNum, ProfileNum, eventUuids);
            Assert.True(success, client.Messages.ObjectToString());

        }
        [Fact()]
        public async Task ResendEventAsync_Full_Test()
        {
            var client = new ErpEventResendClient(_baseUrl, _code);
            var eventUuids = new List<string>() { "c2dc72e4-6e74-49c3-9ab6-eb2a951d5622" };
            var success = await client.ResendEventAsync(MasterAccountNum, ProfileNum, eventUuids);

            Assert.True(success, client.Messages.ObjectToString());

            var sentEventUuids = client.ResopneData?.SentEventUuids;
            Assert.True(sentEventUuids != null && sentEventUuids.Count > 0, "All event send failed.");

            foreach (var eventUuid in eventUuids)
            {
                Assert.True(sentEventUuids.Contains(eventUuid), $"EventUuid send failed, EventUuid:{eventUuid}");
            }

        }
        public void Dispose()
        {
        }
    }
}
