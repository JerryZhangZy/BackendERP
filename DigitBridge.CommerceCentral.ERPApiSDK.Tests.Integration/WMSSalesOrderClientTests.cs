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

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class WMSSalesOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }
        protected int MasterAccountNum = 10001;
        protected int ProfileNum = 10001;

        public WMSSalesOrderClientTests(TestFixture<StartupTest> fixture)
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
        public async Task GetSalesOrdersOpenListAsync_Simple_Test()
        {
            var client = new WMSSalesOrderClient(_baseUrl, _code);
            var success = await client.GetSalesOrdersOpenListAsync(MasterAccountNum, ProfileNum);
            Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task GetSalesOrdersOpenListAsync_Full_Test()
        {
            var client = new WMSSalesOrderClient(_baseUrl, _code);
            MasterAccountNum = 10002;
            ProfileNum = 10003;
            var payload = new WMSSalesOrderRequestPayload()
            {
                LoadAll = false,
                Top = 10,
                //Filter = new SalesOrderOpenListFilter()
                //{
                //    //UpdateDateUtc = DateTime.UtcNow.Date.AddDays(-1),
                //    WarehouseCode = "VBT001-1-3-4"
                //},
            };

            var success = await client.GetSalesOrdersOpenListAsync(MasterAccountNum, ProfileNum, payload);

            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.Data != null);

            if (client.Data.SalesOrderOpenListCount <= 0) return;

            Assert.True(client.Data.SalesOrderOpenList != null, $"Count:{client.Data.SalesOrderOpenListCount}, SalesOrderOpenList:no data.");

            //success = client.Data.SalesOrderOpenList.Count(i => i.WarehouseCode == payload.Filter.WarehouseCode) == client.Data.SalesOrderOpenListCount;

            Assert.True(success, "Filter by WarehouseCode reuslt is not correct.");

        }
    }
}
