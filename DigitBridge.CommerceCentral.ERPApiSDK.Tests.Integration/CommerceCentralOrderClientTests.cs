using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class CommerceCentralOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl { get; set; } 
        private string _code { get; set; }
        protected const int MasterAccountNum = 10002;
        protected const int ProfileNum = 10003;
        public CommerceCentralOrderClientTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            _baseUrl = Configuration["ERP_Integration_Api_BaseUrl"];
            _code = Configuration["ERP_Integration_Api_AuthCode"];
        }


        [Fact()]
        public async Task CentralOrderToErpAsync_Test()
        {
            var client = new CommerceCentralOrderClient(_baseUrl, _code);
            var centralOrderUuid = "44fb1ae8-83ea-4b34-9eec-b358f94953df";
            var success = await client.CentralOrderToErpAsync(MasterAccountNum, ProfileNum, centralOrderUuid);
            Assert.True(success, client.Messages.ObjectToString());
        } 

        [Fact()]
        public async Task SendActionResult_Test()
        {
            var client = new CommerceCentralOrderClient(_baseUrl, _code);
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
