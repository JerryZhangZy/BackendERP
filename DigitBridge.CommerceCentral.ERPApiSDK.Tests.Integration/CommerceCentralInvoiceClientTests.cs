using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
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
        private string _baseUrl { get; set; }
        private string _code { get; set; }
        protected const int MasterAccountNum = 10002;
        protected const int ProfileNum = 10003;

        public CommerceCentralInvoiceClientTests(TestFixture<StartupTest> fixture)
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
            Assert.True(client.Data != null);

            if (client.Data.InvoiceUnprocessListCount <= 0) return;

            Assert.True(client.Data.InvoiceUnprocessList != null, $"Count:{client.Data.InvoiceUnprocessListCount}, InvoiceUnprocessList:no data.");

        }
    }
}
