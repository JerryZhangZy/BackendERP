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
    public partial class CommerceCentralAckProcessInvoiceClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; } 
        private string _code { get; set; }
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public CommerceCentralAckProcessInvoiceClientTests(TestFixture<StartupTest> fixture)
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
        public async Task AckProcessInvoicesAsync_Simple_Test()
        {
            var client = new CommerceCentralAckProcessInvoiceClient(_baseUrl, _code);
            var processResults = new List<ProcessResult>();
            var processResult = new ProcessResult()
            {
                ProcessStatus = Base.Common.EventProcessProcessStatusEnum.Success,
                ProcessData = JObject.Parse("{\"ProcessData\": \"This is a test message from AckProcessInvoicesAsync_Simple_Test\"}"),
                EventMessage = JObject.Parse("{\"EventMessage\": \"This is a test message from AckProcessInvoicesAsync_Simple_Test\"}"),
                ProcessUuid = "bc361a5c-9961-4daa-bf02-239d44178306"
            };
            processResults.Add(processResult);
            var success = await client.AckProcessInvoicesAsync(MasterAccountNum, ProfileNum, processResults);
            Assert.True(success);
        }

        [Fact()]
        public async Task AckProcessInvoicesAsync_Full_Test()
        {
            var InvoiceUuids = await GetOpenInvoiceUuids();

            var client = new CommerceCentralAckProcessInvoiceClient(_baseUrl, _code);

            var processResults = new List<ProcessResult>();
            foreach (var uuid in InvoiceUuids)
            {
                var processResult = new ProcessResult()
                {
                    ProcessStatus = Base.Common.EventProcessProcessStatusEnum.Success,
                    ProcessData = JObject.Parse("{\"message\": \"This is a test message from AckProcessInvoicesAsync_Full_Test\"}"),
                    ProcessUuid = uuid,
                };
                processResults.Add(processResult);
            }

            var success = await client.AckProcessInvoicesAsync(MasterAccountNum, ProfileNum, processResults);
            Assert.True(success, client.Messages.ObjectToString());
        }

        public async Task<IList<string>> GetOpenInvoiceUuids()
        {
            var client = new CommerceCentralInvoiceClient(_baseUrl, _code);

            var payload = new CommerceCentralInvoiceRequestPayload()
            {
                LoadAll = false,
                Top = 2,
            };

            var success = await client.GetUnprocessedInvoicesAsync(MasterAccountNum, ProfileNum, payload);

            Assert.True(success, client.Messages.ObjectToString());

            var InvoiceUuids = client?.Data?.InvoiceUnprocessList?.Select(i => i.InvoiceHeader.InvoiceUuid).ToList();

            Assert.True(InvoiceUuids != null, "no unprocess Invoice.");

            return InvoiceUuids;
        }
    }
}
