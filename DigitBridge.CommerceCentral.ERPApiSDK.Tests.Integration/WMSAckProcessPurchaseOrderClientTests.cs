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
    public partial class WMSAckProcessPurchaseOrderClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public WMSAckProcessPurchaseOrderClientTests(TestFixture<StartupTest> fixture)
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
        public async Task AckProcessPurchaseOrdersAsync_Simple_Test()
        {
            var client = new WMSAckProcessPurchaseOrderClient(_baseUrl, _code);
            var processResults = new List<ProcessResult>();
            var processResult = new ProcessResult()
            {
                ProcessStatus = Base.Common.EventProcessProcessStatusEnum.Success,
                ProcessData = JObject.Parse("{\"message\": \"This is a test message from AckProcessPurchaseOrdersAsync_Simple_Test\"}"),
                ProcessUuid = "15082ed6-b62f-4faf-9491-dc182d7bd4a9"
            };
            processResults.Add(processResult);
            var success = await client.AckProcessPurchaseOrdersAsync(MasterAccountNum, ProfileNum, processResults);
            Assert.True(client.ResopneData != null);
        }

        [Fact()]
        public async Task AckProcessPurchaseOrdersAsync_Full_Test()
        {
            var PurchaseOrderUuids = await GetOpenPurchaseOrderUuids();

            var client = new WMSAckProcessPurchaseOrderClient(_baseUrl, _code);

            var processResults = new List<ProcessResult>();
            foreach (var uuid in PurchaseOrderUuids)
            {
                var processResult = new ProcessResult()
                {
                    ProcessStatus = Base.Common.EventProcessProcessStatusEnum.Success,
                    ProcessData = JObject.Parse("{\"message\": \"This is a test message from AckProcessPurchaseOrdersAsync_Full_Test\"}"),
                    ProcessUuid=uuid,
                };
                processResults.Add(processResult);
            }

            var success = await client.AckProcessPurchaseOrdersAsync(MasterAccountNum, ProfileNum, processResults);
            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null);
        }

        public async Task<IList<string>> GetOpenPurchaseOrderUuids()
        {
            var client = new WMSPurchaseOrderClient(_baseUrl, _code);

            var payload = new WMSPurchaseOrderRequestPayload()
            {
                LoadAll = false,
                Top = 2,
            };

            var success = await client.GetPurchaseOrdersOpenListAsync(MasterAccountNum, ProfileNum, payload);

            Assert.True(success, client.Messages.ObjectToString());

            var PurchaseOrderUuids = client?.ResopneData?.PurchaseOrderList?.Select(i => i.PoUuid).ToList();

            Assert.True(PurchaseOrderUuids != null, "no open PurchaseOrder.");

            return PurchaseOrderUuids;
        }
    }
}
