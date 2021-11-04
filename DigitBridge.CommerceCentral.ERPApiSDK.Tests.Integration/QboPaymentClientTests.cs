using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class QboPaymentClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl = "https://digitbridge-erp-event-api-dev.azurewebsites.net/api/erpevents";
        private string _code = "drZEGmRUVmGcitmCqyp3VZe5b4H8fSoy8rDUsEMkfG9U7UURXMtnrw==";

        public QboPaymentClientTests(TestFixture<StartupTest> fixture)
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
            var client = new QboPaymentClient(_baseUrl, _code);
            var data=new AddErpEventDto{
                MasterAccountNum=10001,
                ProcessUuid=Guid.NewGuid().ToString(),
                ProfileNum=10001
            };
            var result =await client.SendAddQboPaymentAsync(data);
            Assert.True(client.Data != null, "succ");
            Assert.True(client.Data.ProcessUuid == data.ProcessUuid, "succ");
            Assert.True(result, "succ");

            data =new AddErpEventDto{
                MasterAccountNum=10001,
                ProcessUuid=Guid.NewGuid().ToString(),
                ProfileNum=10001
            };
            result =await client.SendDeleteQboPaymentAsync(data);
            Assert.True(client.Data != null, "succ");
            Assert.True(client.Data.ProcessUuid == data.ProcessUuid, "succ");
            Assert.True(result, "succ");
        }
        [Fact()]
        public async Task SendAddDataWithConfig_Test()
        {
            var client = new QboPaymentClient();
            var data=new AddErpEventDto{
                MasterAccountNum=10001,
                ProcessUuid=Guid.NewGuid().ToString(),
                ProfileNum=10001
            };
            var result =await client.SendAddQboPaymentAsync(data);
            Assert.True(client.Data != null, "succ");
            Assert.True(client.Data.ProcessUuid == data.ProcessUuid, "succ");
            Assert.True(result, "succ");

            data =new AddErpEventDto{
                MasterAccountNum=10001,
                ProcessUuid=Guid.NewGuid().ToString(),
                ProfileNum=10001
            };
            result =await client.SendDeleteQboPaymentAsync(data);
            Assert.True(client.Data != null, "succ");
            Assert.True(client.Data.ProcessUuid == data.ProcessUuid, "succ");
            Assert.True(result, "succ");
        }

        [Fact()]
        public async Task SendActionResult_Test()
        {
            var client = new QboPaymentClient(_baseUrl, _code);
            var data=new UpdateErpEventDto{
                MasterAccountNum=10001,
                EventUuid=Guid.NewGuid().ToString(),
                ProfileNum=10001,
                EventMessage="Tester",
                ActionStatus=0
            };
            var result =await client.SendActionResultAsync(data);
            Assert.True(client.Messages.Count > 0, "succ");
            Assert.True(!result, "succ");
        }

        [Fact()]
        public async Task SendActionResultWithConfig_Test()
        {
            var client = new QboPaymentClient();
            var data=new UpdateErpEventDto{
                MasterAccountNum=10001,
                EventUuid=Guid.NewGuid().ToString(),
                ProfileNum=10001,
                EventMessage="Tester",
                ActionStatus=0
            };
            var result =await client.SendActionResultAsync(data);
            Assert.True(client.Messages.Count > 0, "succ");
            Assert.True(!result, "succ");
        }

        public void Dispose()
        {
        }
    }
}
