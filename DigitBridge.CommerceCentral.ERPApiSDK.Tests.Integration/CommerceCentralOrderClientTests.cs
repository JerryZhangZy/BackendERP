using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
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
        private IDataBaseFactory dbFactory { get; set; }
        protected void InitForTest()
        {
            _baseUrl = Configuration["ERP_Integration_Api_BaseUrl"];
            _code = Configuration["ERP_Integration_Api_AuthCode"];
            dbFactory = new DataBaseFactory(Configuration["dsn"]);
        }

        [Fact()]
        public async Task CentralOrderToErpAsync_Test()
        {
            var client = new CommerceCentralOrderClient(_baseUrl, _code);
            var centralOrderUuid = "638E9BB0-651E-487A-8E9E-D973E28272A2";
            var success = await client.CentralOrderToErpAsync(10001, 10001, centralOrderUuid);
            Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task CentralOrderToErpAsync_SendList_Test()
        {
            var list = dbFactory.Find<ERPDb.OrderDCAssignmentHeader>(
                $"select top 2 * from OrderDCAssignmentHeader order by RowNum desc");
            var centralOrderUuidList = list.Select(i => i.CentralOrderUuid).Distinct().ToList();
            Assert.True(centralOrderUuidList!=null && centralOrderUuidList.Count > 0, "No centralOrderUuid found.");

            
            foreach (var centralOrderUuid in centralOrderUuidList)
            {
                var client = new CommerceCentralOrderClient(_baseUrl, _code);
                var success = await client.CentralOrderToErpAsync(MasterAccountNum, ProfileNum, centralOrderUuid);
                Assert.True(success, client.Messages.ObjectToString());
            }
            
        }

        //[Fact()]
        //public async Task SendActionResult_Test()
        //{
        //    var client = new CommerceCentralOrderClient(_baseUrl, _code);
        //    var data = new UpdateErpEventDto
        //    {
        //        MasterAccountNum = 10001,
        //        EventUuid = Guid.NewGuid().ToString(),
        //        ProfileNum = 10001,
        //        EventMessage = "Tester",
        //        ActionStatus = 0
        //    };
        //    var result = await client.SendActionResultAsync(data);
        //    Assert.True(client.Messages.Count > 0, "succ");
        //    Assert.True(!result, "succ");
        //} 
        public void Dispose()
        {
        }
    }
}
