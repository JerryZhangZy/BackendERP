using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
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

        private string _baseUrl = "http://localhost:7073";
        //private string _baseUrl = "https://digitbridge-erp-event-api-dev.azurewebsites.neterpevents";
        private string _code = "drZEGmRUVmGcitmCqyp3VZe5b4H8fSoy8rDUsEMkfG9U7UURXMtnrw==";

        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public ErpEventResendClientTests(TestFixture<StartupTest> fixture)
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
