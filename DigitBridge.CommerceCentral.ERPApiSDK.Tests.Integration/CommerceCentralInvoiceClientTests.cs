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
    public partial class CommerceCentralInvoiceClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        //private string _baseUrl = "http://localhost:7074";
        private string _baseUrl = "https://digitbridge-erp-integration-api-dev.azurewebsites.net";
        private string _code = "aa4QcFoSH4ADcXEROimDtbPa4h0mY/dsNFuK1GfHPAhqx5xMJRAaHw==";
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public CommerceCentralInvoiceClientTests(TestFixture<StartupTest> fixture)
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
        public async Task GetUnprocessedInvoicesAsync_Simple_Test()
        {
            var client = new CommerceCentralInvoiceClient(_baseUrl, _code);
            var success = await client.GetUnprocessedInvoicesAsync(MasterAccountNum, ProfileNum);
            Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task GetUnprocessedInvoicesAsync_Full_Test()
        {
            var client = new CommerceCentralInvoiceClient(_baseUrl, _code);

            var payload = new CommerceCentralInvoiceRequestPayload()
            {
                //LoadAll = true,
            };

            //Add your filter here.
            payload.Filter = new InvoiceUnprocessPayloadFilter()
            {
                ChannelAccountNum = 10001,
            };

            var success = await client.GetUnprocessedInvoicesAsync(MasterAccountNum, ProfileNum, payload);

            Assert.True(success, client.Messages.ObjectToString());
            Assert.True(client.ResopneData != null);

            if (client.ResopneData.InvoiceUnprocessListCount <= 0) return;

            Assert.True(client.ResopneData.InvoiceUnprocessList != null, $"Count:{client.ResopneData.InvoiceUnprocessListCount}, InvoiceUnprocessList:no data.");

        }
    }
}
