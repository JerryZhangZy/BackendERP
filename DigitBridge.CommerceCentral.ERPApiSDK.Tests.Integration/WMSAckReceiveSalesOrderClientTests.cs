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
    public partial class WMSAckReceiveSalesOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        private string _baseUrl { get; set; }
        private string _code { get; set; }
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public WMSAckReceiveSalesOrderClientTests(TestFixture<StartupTest> fixture)
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

        /// <summary>
        /// Only test the api invocke successfully.
        /// </summary>
        /// <returns></returns>
        [Fact()]
        public async Task AckReceiveSalesOrdersAsync_Simple_Test()
        {
            var client = new WMSAckReceiveSalesOrderClient(_baseUrl, _code);
            var salesorderUuids = new List<string>() { "15082ed6-b62f-4faf-9491-dc182d7bd4a9", "61F6B440-17B2-4A33-BD4F-27CD7C5C3F5D" };
            var success = await client.AckReceiveSalesOrdersAsync(MasterAccountNum, ProfileNum, salesorderUuids);
            Assert.True(client.ResopneData != null);
        }

        [Fact()]
        public async Task AckReceiveSalesOrdersAsync_Full_Test()
        {
            var salesOrderUuids = await GetOpenSalesOrderUuids();

            var client = new WMSAckReceiveSalesOrderClient(_baseUrl, _code);
            var success = await client.AckReceiveSalesOrdersAsync(MasterAccountNum, ProfileNum, salesOrderUuids);
            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null);
        }

        public async Task<IList<string>> GetOpenSalesOrderUuids()
        {
            var client = new WMSSalesOrderClient(_baseUrl, _code);

            var payload = new WMSSalesOrderRequestPayload()
            {
                LoadAll = false,
                Top = 2,
            };

            var success = await client.GetSalesOrdersOpenListAsync(MasterAccountNum, ProfileNum, payload);

            Assert.True(success, client.Messages.ObjectToString());

            var salesOrderUuids = client?.ResopneData?.SalesOrderOpenList?.Select(i => i.SalesOrderUuid).ToList();

            Assert.True(salesOrderUuids != null, "no open salesorder.");

            return salesOrderUuids;
        }
    }
}
