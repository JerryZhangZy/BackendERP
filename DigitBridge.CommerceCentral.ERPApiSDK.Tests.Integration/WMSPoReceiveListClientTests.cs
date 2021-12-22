using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class WMSPoReceiveListClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }
        protected int MasterAccountNum = 10002;
        protected int ProfileNum = 10003;

        public WMSPoReceiveListClientTests(TestFixture<StartupTest> fixture)
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
        public async Task GetWMSOrderPoReceiveListAsync_Simple_Test()
        {
            var list = dbFactory.Find<ERPDb.EventProcessERP>(
                $"select top 3 * from EventProcessERP where ERPEventProcessType= {(int)EventProcessTypeEnum.PoReceiveFromWMS}");
            var PoReceiveIDs = list.Select(i => i.ProcessUuid).Distinct().ToList();
            Assert.True(PoReceiveIDs != null && PoReceiveIDs.Count > 0, "No PoReceiveIDs found in EventProcessERP");

            var client = new WMSPoReceiveListClient(_baseUrl, _code); 
            var success = await client.GetWMSOrderPoReceiveListAsync(MasterAccountNum, ProfileNum, PoReceiveIDs);
            Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task GetWMSOrderPoReceiveListAsync_Full_Test()
        {
            var list = dbFactory.Find<ERPDb.EventProcessERP>(
                $"select top 2 * from EventProcessERP where ERPEventProcessType= {(int)EventProcessTypeEnum.PoReceiveFromWMS} order by rownum desc");
            var wmsBatchNums = list.Select(i => i.ProcessUuid).Distinct().ToList();
            Assert.True(wmsBatchNums != null && wmsBatchNums.Count > 0, "No wmsBatchNums found in EventProcessERP");

            var client = new WMSPoReceiveListClient(_baseUrl, _code);

            var success = await client.GetWMSOrderPoReceiveListAsync(MasterAccountNum, ProfileNum, wmsBatchNums);
            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.Data != null && client.Data != null);

            if (client.Data.Count <= 0) return;

            success = client.Data.Count(i => wmsBatchNums.Contains(i.WMSBatchNum)) > 0;

            Assert.True(success, "GetWMSOrderPoReceiveListAsync no result.");

        }
    }
}
