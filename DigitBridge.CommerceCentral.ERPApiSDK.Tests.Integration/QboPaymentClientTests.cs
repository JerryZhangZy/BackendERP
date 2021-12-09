using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
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
        private string _baseUrl { get; set; }
        private string _code { get; set; }

        public QboPaymentClientTests(TestFixture<StartupTest> fixture)
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
        public async Task SendAddData_Test()
        {
            var client = new QboPaymentClient(_baseUrl, _code);
            var data=new AddErpEventDto{
                MasterAccountNum=10001,
                ProcessUuid=Guid.NewGuid().ToString(),
                ProfileNum=10001
            };
            var success = await client.SendAddQboPaymentAsync(data); 
            Assert.True(success, client.Messages.ObjectToString());

            data =new AddErpEventDto{
                MasterAccountNum=10001,
                ProcessUuid=Guid.NewGuid().ToString(),
                ProfileNum=10001
            };
            success = await client.SendDeleteQboPaymentAsync(data);
            Assert.True(success, client.Messages.ObjectToString());
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
