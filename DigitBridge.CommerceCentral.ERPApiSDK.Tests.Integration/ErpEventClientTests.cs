using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class ErpEventClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }

        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public ErpEventClientTests(TestFixture<StartupTest> fixture)
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
        public async Task SendActionResult_DataProcess_Success_Test()
        {
            var eventuuid = dbFactory.GetValue<Event_ERP, string>("select top 1 eventuuid from event_erp ");
            Assert.True(!eventuuid.IsZero(), "No event data.");

            var client = new ErpEventClient(_baseUrl, _code);
            var data = new UpdateErpEventDto
            {
                MasterAccountNum = 10001,
                EventUuid = eventuuid,
                ProfileNum = 10001,
                EventMessage = "Tester",
                ActionStatus = (int)ErpEventActionStatus.Success
            };
            var success = await client.SendActionResultAsync(data);
            Assert.True(success, client.Messages.ObjectToString());

            var checkEventuuid = dbFactory.GetValue<Event_ERP, string>($"select top 1 eventuuid from event_erp where eventuuid='{eventuuid}'");

            Assert.True(checkEventuuid.IsZero(), "process result is success, data should not be existing in db");
        }

        [Fact()]
        public async Task SendActionResult_DataProcess_Failed_Test()
        {
            var eventuuid = dbFactory.GetValue<Event_ERP, string>("select top 1 eventuuid from event_erp ");
            Assert.True(!eventuuid.IsZero(), "No event data.");

            var client = new ErpEventClient(_baseUrl, _code);
            var data = new UpdateErpEventDto
            {
                MasterAccountNum = 10001,
                EventUuid = eventuuid,
                ProfileNum = 10001,
                EventMessage = "Tester",
                ActionStatus = (int)ErpEventActionStatus.Other
            };
            var success = await client.SendActionResultAsync(data);
            Assert.True(success, client.Messages.ObjectToString());

            var checkEventuuid = dbFactory.GetValue<Event_ERP, string>($"select top 1 eventuuid from event_erp where eventuuid='{eventuuid}'");

            Assert.True(!checkEventuuid.IsZero(), "process result is failed, data should be existing in db");
        }

    }
}
