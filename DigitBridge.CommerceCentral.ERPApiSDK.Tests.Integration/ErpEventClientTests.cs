using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class ErpEventClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl = "http://localhost:7073";
        //private string _baseUrl = "https://digitbridge-erp-event-api-dev.azurewebsites.neterpevents";
        private string _code = "drZEGmRUVmGcitmCqyp3VZe5b4H8fSoy8rDUsEMkfG9U7UURXMtnrw==";

        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public ErpEventClientTests(TestFixture<StartupTest> fixture)
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
        public async Task ResendAsync_Test()
        {
            var client = new ErpEventClient(_baseUrl, _code);
            var eventUuid = "c2dc72e4-6e74-49c3-9ab6-eb2a951d5622";
            var success = await client.ResendAsync(MasterAccountNum, ProfileNum, eventUuid);

            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.Data != null && client.Data.EventUuid == eventUuid);
        }
        public void Dispose()
        {
        }
    }
}
