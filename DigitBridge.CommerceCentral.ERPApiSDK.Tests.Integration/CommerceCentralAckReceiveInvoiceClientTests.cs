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
    public partial class CommerceCentralAckReceiveInvoiceClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        //private string _baseUrl = "http://localhost:7074";
        private string _baseUrl = "https://digitbridge-erp-integration-api-dev.azurewebsites.net";
        private string _code = "aa4QcFoSH4ADcXEROimDtbPa4h0mY/dsNFuK1GfHPAhqx5xMJRAaHw==";
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public CommerceCentralAckReceiveInvoiceClientTests(TestFixture<StartupTest> fixture)
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

        /// <summary>
        /// Only test the api invocke successfully.
        /// </summary>
        /// <returns></returns>
        [Fact()]
        public async Task AckReceiveInvoicesAsync_Simple_Test()
        {
            var client = new CommerceCentralAckReceiveInvoiceClient(_baseUrl, _code);
            var InvoiceUuids = new List<string>() { "6adf4237-8c32-40b0-9edf-c462b91acf5f" };
            var success = await client.AckReceiveInvoicesAsync(MasterAccountNum, ProfileNum, InvoiceUuids);
            Assert.True(client.ResopneData != null);
        }

        [Fact()]
        public async Task AckReceiveInvoicesAsync_Full_Test()
        {
            var InvoiceUuids = await GetOpenInvoiceUuids();

            var client = new CommerceCentralAckReceiveInvoiceClient(_baseUrl, _code);
            var success = await client.AckReceiveInvoicesAsync(MasterAccountNum, ProfileNum, InvoiceUuids);
            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null);
        }

        public async Task<IList<string>> GetOpenInvoiceUuids()
        {
            var client = new CommerceCentralInvoiceClient (_baseUrl, _code);

            var payload = new CommerceCentralInvoiceRequestPayload()
            {
                LoadAll = false,
                Top = 2,
            };

            var success = await client.GetUnprocessedInvoicesAsync(MasterAccountNum, ProfileNum, payload);

            Assert.True(success, client.Messages.ObjectToString());

            var InvoiceUuids = client?.ResopneData?.InvoiceUnprocessList?.Select(i => i.InvoiceHeader.InvoiceUuid).ToList();

            Assert.True(InvoiceUuids != null, "no unprocess Invoice.");

            return InvoiceUuids;
        }
    }
}
