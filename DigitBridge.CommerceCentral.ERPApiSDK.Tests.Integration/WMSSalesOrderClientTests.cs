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

        //private string _baseUrl = "http://localhost:7074/api/"; 
        private string _baseUrl = "https://digitbridge-erp-integration-api-dev.azurewebsites.net/api/";
        private string _code = "aa4QcFoSH4ADcXEROimDtbPa4h0mY/dsNFuK1GfHPAhqx5xMJRAaHw==";
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

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
            var success = await client.GetSalesOrdersOpenListAsync(MasterAccountNum, ProfileNum);
            Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task GetSalesOrdersOpenListAsync_Full_Test()
        {
            var client = new WMSSalesOrderClient(_baseUrl, _code);

            var payload = new WMSSalesOrderRequestPayload()
            {
                LoadAll = true,
                Filter = new SalesOrderOpenListFilter()
                {
                    UpdateDateUtc = DateTime.Today.AddDays(-1),
                },
            };

            var success = await client.GetSalesOrdersOpenListAsync(MasterAccountNum, ProfileNum, payload);

            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null);

            if (client.ResopneData.SalesOrderOpenListCount <= 0) return;

            Assert.True(client.ResopneData.SalesOrderOpenList != null, $"Count:{client.ResopneData.SalesOrderOpenListCount}, SalesOrderOpenList:no data.");

        }
    }
}
