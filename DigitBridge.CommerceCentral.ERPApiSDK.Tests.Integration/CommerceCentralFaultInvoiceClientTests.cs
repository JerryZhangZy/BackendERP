using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Tests.Integration
{
    public partial class CommerceCentralFaultInvoiceClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        private string _baseUrl = "http://localhost:7074/api/";
        //private string _baseUrl = "https://digitbridge-erp-integration-api-dev.azurewebsites.net/api/";
        private string _code = "aa4QcFoSH4ADcXEROimDtbPa4h0mY/dsNFuK1GfHPAhqx5xMJRAaHw==";
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public CommerceCentralFaultInvoiceClientTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;
            InitForTest();
        }
        protected void InitForTest()
        {
            try
            {
                var Seq = 0;
                DataBaseFactory = YoPoco.DataBaseFactory.CreateDefault(Configuration["dsn"].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Dispose()
        {
        }



        [Fact()]
        public async Task UpdateFaultInvoiceAsync_Simple_Test()
        {
            var client = new CommerceCentralFaultInvoiceClient(_baseUrl, _code);
            var request = new FaultInvoiceRequestPayload()
            {
                InvoiceUuid = GetInvoiceUuid().FirstOrDefault(),
                Message = new StringBuilder().Append("Run Test UpdateFaultInvoiceAsync_Simple_Test"),
            };
            var success = await client.UpdateFaultInvoiceAsync(MasterAccountNum, ProfileNum, request);
            Assert.True(client.ResopneData != null, $"SDK invoice failed. Error is {client.Messages.ObjectToString()}");
            //Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task UpdateFaultInvoiceAsync_Full_Test()
        {
            var client = new CommerceCentralFaultInvoiceClient(_baseUrl, _code);
            var request = new FaultInvoiceRequestPayload()
            {
                InvoiceUuid = GetInvoiceUuid().FirstOrDefault(),
                Message = new StringBuilder().Append("Run Test UpdateFaultInvoiceAsync_Full_Test"),
            };

            var success = await client.UpdateFaultInvoiceAsync(MasterAccountNum, ProfileNum, request);
            Assert.True(client.ResopneData != null, $"SDK invoice failed. Error is {client.Messages.ObjectToString()}");
            Assert.True(success, $"SDK invoice succeed.But call integration api failed. You can try CreateShipmentAsync_Test to test logic,Logic error is{client.Messages.ObjectToString()}");
        }


        [Fact()]
        public async Task UpdateFaultInvoiceListAsync_Simple_Test()
        {
            var client = new CommerceCentralFaultInvoiceClient(_baseUrl, _code);
            var shipmentList = GetFaultInvoiceList("UpdateFaultInvoiceListAsync_Simple_Test");
            var success = await client.UpdateFaultInvoiceListAsync(MasterAccountNum, ProfileNum, shipmentList);
            Assert.True(client.ResopneData != null, $"SDK invoice failed. Error is {client.Messages.ObjectToString()}");
            //Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task UpdateFaultInvoiceListAsync_Full_Test()
        {
            var client = new CommerceCentralFaultInvoiceClient(_baseUrl, _code);
            var shipmentList = GetFaultInvoiceList("UpdateFaultInvoiceListAsync_Full_Test");
            var success = await client.UpdateFaultInvoiceListAsync(MasterAccountNum, ProfileNum, shipmentList);
            Assert.True(client.ResopneData != null, $"SDK invoice failed. Error is {client.Messages.ObjectToString()}");
            Assert.True(success, $"SDK invoice succeed.But call integration api failed. You can try CreateShipmentAsync_Test to test logic,Logic error is{client.Messages.ObjectToString()}");
        }


        protected List<FaultInvoiceRequestPayload> GetFaultInvoiceList(string error, int count = 2)
        {
            var list = new List<FaultInvoiceRequestPayload>();
            var invoiceUuids = GetInvoiceUuid(count);
            foreach (var invoiceUuid in invoiceUuids)
            {
                var faultInvoiceRequest = new FaultInvoiceRequestPayload()
                {
                    InvoiceUuid = invoiceUuid,
                    Message = new StringBuilder().Append(invoiceUuid + error)
                };
                list.Add(faultInvoiceRequest);
            }

            return list;
        }


        private List<string> GetInvoiceUuid(int n = 1)
        {
            var sql = $@"
SELECT top {n} ProcessUuid 
FROM EventProcessERP ins  
WHERE [MasterAccountNum]={MasterAccountNum}
AND [ProfileNum]={ProfileNum}
AND ERPEventProcessType={(int)EventProcessTypeEnum.InvoiceToChanel}
AND ActionStatus={(int)EventProcessActionStatusEnum.Default}
order by ins.rownum desc
for json path
";
            var invoiceUuidList = new List<string>();
            using (var trs = new ScopedTransaction(this.DataBaseFactory))
            {
                invoiceUuidList = SqlQuery.Execute(sql,
                        (string processUuid) => processUuid
                    );
                Assert.True(invoiceUuidList.Count != 0, "No matched invoice uuid");
                return invoiceUuidList;
            }
        }
    }
}
