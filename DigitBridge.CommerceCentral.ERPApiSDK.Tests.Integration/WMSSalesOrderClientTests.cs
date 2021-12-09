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
        protected void InitForTest()
        {
            _baseUrl = Configuration["ERP_Integration_Api_BaseUrl"];
            _code = Configuration["ERP_Integration_Api_AuthCode"];
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
                LoadAll = true,
                //Top = 10,
                //Filter = new SalesOrderOpenListFilter()
                //{
                //    //UpdateDateUtc = DateTime.UtcNow.Date.AddDays(-1),
                //    WarehouseCode = "VBT001-1-3-4"
                //},
            };

            var success = await client.GetSalesOrdersOpenListAsync(MasterAccountNum, ProfileNum, payload);

            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null);

            if (client.ResopneData.SalesOrderOpenListCount <= 0) return;

            Assert.True(client.ResopneData.SalesOrderOpenList != null, $"Count:{client.ResopneData.SalesOrderOpenListCount}, SalesOrderOpenList:no data.");

            //success = client.ResopneData.SalesOrderOpenList.Count(i => i.WarehouseCode == payload.Filter.WarehouseCode) == client.ResopneData.SalesOrderOpenListCount;

            Assert.True(success, "Filter by WarehouseCode reuslt is not correct.");

        }
    }
}
