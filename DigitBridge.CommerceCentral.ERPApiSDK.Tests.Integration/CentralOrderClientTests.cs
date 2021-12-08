using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class CentralOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl = "http://localhost:7073";
        //private string _baseUrl = "https://digitbridge-erp-event-api-dev.azurewebsites.net";
        private string _code = "drZEGmRUVmGcitmCqyp3VZe5b4H8fSoy8rDUsEMkfG9U7UURXMtnrw==";
        protected const int MasterAccountNum = 10002;
        protected const int ProfileNum = 10003;
        public CentralOrderClientTests(TestFixture<StartupTest> fixture)
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
        public async Task SendAddData_Test()
        {
            var client = new CentralOrderClient(_baseUrl, _code);
            var centralOrderUuid = "44fb1ae8-83ea-4b34-9eec-b358f94953df";
            var result = await client.CentralOrderToErpAsync(MasterAccountNum, ProfileNum, centralOrderUuid);
            Assert.True(client.Data != null, "succ");
            Assert.True(client.Data.ProcessUuid == centralOrderUuid, "succ");
            Assert.True(result, "succ");
        } 

        [Fact()]
        public async Task SendActionResult_Test()
        {
            var client = new CentralOrderClient(_baseUrl, _code);
            var data = new UpdateErpEventDto
            {
                MasterAccountNum = 10001,
                EventUuid = Guid.NewGuid().ToString(),
                ProfileNum = 10001,
                EventMessage = "Tester",
                ActionStatus = 0
            };
            var result = await client.SendActionResultAsync(data);
            Assert.True(client.Messages.Count > 0, "succ");
            Assert.True(!result, "succ");
        }

        [Fact()]
        public async Task SendActionResultWithConfig_Test()
        {
            var client = new CentralOrderClient();
            var data = new UpdateErpEventDto
            {
                MasterAccountNum = 10001,
                EventUuid = Guid.NewGuid().ToString(),
                ProfileNum = 10001,
                EventMessage = "Tester",
                ActionStatus = 0
            };
            var result = await client.SendActionResultAsync(data);
            Assert.True(client.Messages.Count > 0, "succ");
            Assert.True(!result, "succ");
        }

        public void Dispose()
        {
        }
    }
}
