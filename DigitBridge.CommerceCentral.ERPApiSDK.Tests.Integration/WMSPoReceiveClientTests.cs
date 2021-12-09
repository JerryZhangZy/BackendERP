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
    public partial class WMSPoReceiveClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public WMSPoReceiveClientTests(TestFixture<StartupTest> fixture)
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

        [Fact()]
        public async Task ReceiveAsync_Simple_Test()
        {
            var client = new WMSPoReceiveClient(_baseUrl, _code);
            var list = GetWMSPoReceiveItems();
            var success = await client.ReceiveAsync(MasterAccountNum, ProfileNum, list);
            Assert.True(client.ResopneData != null, $"SDK invoice failed. Error is {client.Messages.ObjectToString()}");
            //Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task ReceiveAsync_Full_Test()
        {
            var client = new WMSPoReceiveClient(_baseUrl, _code);
            var list = GetWMSPoReceiveItems();
            var success = await client.ReceiveAsync(MasterAccountNum, ProfileNum, list);
            Assert.True(client.ResopneData != null, $"SDK invoke failed. Error is {client.Messages.ObjectToString()}");
            Assert.True(success, $"SDK invoke succeed.But call integration api failed. Logic error is{client.Messages.ObjectToString()}");
        }

        protected List<WMSPoReceiveItem> GetWMSPoReceiveItems()
        {
            var result = new List<WMSPoReceiveItem>();
            result.Add(new WMSPoReceiveItem()
            {
                PoItemUuid = "71d0086f-8753-4c87-ae45-c1d0cc5c8404",
                WarehouseCode = "WarehouseCode2",
                SKU = "Fish",
                Qty = 10,
            });

            result.Add(new WMSPoReceiveItem()
            {
                PoItemUuid = "f61a1ba3-73b5-461e-bf62-05a629565c16",
                WarehouseCode = "WarehouseCode3",
                SKU = "Tuna",
                Qty = 10,
            });

            result.Add(new WMSPoReceiveItem()
            {
                PoItemUuid = "2c231ca1-98bb-4756-b26e-f5ac361b9830",
                WarehouseCode = "WarehouseCode3",
                SKU = "Salad",
                Qty = 15,
            });

            result.Add(new WMSPoReceiveItem()
            {
                PoItemUuid = "94043d6f-e6bc-4428-8585-0ef133745c2c",
                WarehouseCode = "WarehouseCode3",
                SKU = "Tuna",
                Qty = 10,
            });
            return result;
        }
    }
}
