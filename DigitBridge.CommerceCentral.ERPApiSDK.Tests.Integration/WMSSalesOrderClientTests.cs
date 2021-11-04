using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class WMSSalesOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl = "http://localhost:7074/api/";
        private string _code = "put online app key";

        public WMSSalesOrderClientTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;
            InitForTest();
        }
        protected void InitForTest()
        {
        }
        public void Dispose()
        {
        }


        [Fact()]
        public async Task GetSalesOrdersOpenListAsync_Simple_Test()
        {
            var client = new WMSSalesOrderClient(_baseUrl, _code);
            var requestData = new WMSSalesOrderRequestPayload
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                IsQueryTotalCount = true,
                LoadAll = true
            };
            var success = await client.GetSalesOrdersOpenListAsync(requestData);
            Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task GetSalesOrdersOpenListAsync_Full_Test()
        {
            var client = new WMSSalesOrderClient(_baseUrl, _code);
            var requestData = new WMSSalesOrderRequestPayload
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                IsQueryTotalCount = true,
                LoadAll = true
            };
            var success = await client.GetSalesOrdersOpenListAsync(requestData);

            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null);

            if (client.ResopneData.SalesOrderOpenListCount <= 0) return;

            Assert.True(!client.ResopneData.SalesOrderOpenList.ToString().IsZero(), $"Count:{client.ResopneData.SalesOrderOpenListCount}, SalesOrderOpenList:no data.");
        }
    }
}
