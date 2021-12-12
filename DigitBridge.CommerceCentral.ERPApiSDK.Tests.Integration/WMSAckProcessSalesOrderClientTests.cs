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
    public partial class WMSAckProcessSalesOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public WMSAckProcessSalesOrderClientTests(TestFixture<StartupTest> fixture)
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

        /// <summary>
        /// Only test the api invocke successfully.
        /// </summary>
        /// <returns></returns>
        [Fact()]
        public async Task AckProcessSalesOrdersAsync_Simple_Test()
        {
            var client = new WMSAckProcessSalesOrderClient(_baseUrl, _code);
            var processResults = new List<ProcessResult>();
            var processResult = new ProcessResult()
            {
                ProcessStatus = Base.Common.EventProcessProcessStatusEnum.Success,
                ProcessData = JObject.Parse("{\"message\": \"This is a test message from AckProcessSalesOrdersAsync_Simple_Test\"}"),
                ProcessUuid = "15082ed6-b62f-4faf-9491-dc182d7bd4a9"
            };
            processResults.Add(processResult);
            var success = await client.AckProcessSalesOrdersAsync(MasterAccountNum, ProfileNum, processResults);
            Assert.True(success);
        }

        [Fact()]
        public async Task AckProcessSalesOrdersAsync_Full_Test()
        {
            var salesOrderUuids = await GetOpenSalesOrderUuids();

            var client = new WMSAckProcessSalesOrderClient(_baseUrl, _code);

            var processResults = new List<ProcessResult>();
            foreach (var uuid in salesOrderUuids)
            {
                var processResult = new ProcessResult()
                {
                    ProcessStatus = Base.Common.EventProcessProcessStatusEnum.Success,
                    ProcessData = JObject.Parse("{\"message\": \"This is a test message from AckProcessSalesOrdersAsync_Full_Test\"}"),
                    ProcessUuid=uuid,
                };
                processResults.Add(processResult);
            }

            var success = await client.AckProcessSalesOrdersAsync(MasterAccountNum, ProfileNum, processResults);
            Assert.True(success, client.Messages.ObjectToString()); 
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

            var salesOrderUuids = client?.Data?.SalesOrderOpenList?.Select(i => i.SalesOrderUuid).ToList();

            Assert.True(salesOrderUuids != null, "no open salesorder.");

            return salesOrderUuids;
        }
    }
}
