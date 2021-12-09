using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var list = dbFactory.Find<ERPDb.PoItems>(
               $"select top 5000 * from PoItems order by rownum desc");
            Assert.True(list.Count() > 0, "No PoItems.");

            foreach (var item in list)
            {
                result.Add(new WMSPoReceiveItem()
                {
                    PoItemUuid = item.PoItemUuid,
                    WarehouseCode = item.WarehouseCode.IsZero() ? "test warehouse code" : item.WarehouseCode,
                    SKU = item.SKU,
                    Qty = new Random().Next(1, 1000),
                    PoUuid = item.PoUuid,
                });
            }

            //result.Add(new WMSPoReceiveItem()
            //{
            //    PoItemUuid = "71d0086f-8753-4c87-ae45-c1d0cc5c8404",
            //    WarehouseCode = "WarehouseCode2",
            //    SKU = "Fish",
            //    Qty = 10,
            //});

            //result.Add(new WMSPoReceiveItem()
            //{
            //    PoItemUuid = "f61a1ba3-73b5-461e-bf62-05a629565c16",
            //    WarehouseCode = "WarehouseCode3",
            //    SKU = "Tuna",
            //    Qty = 10,
            //});

            //result.Add(new WMSPoReceiveItem()
            //{
            //    PoItemUuid = "2c231ca1-98bb-4756-b26e-f5ac361b9830",
            //    WarehouseCode = "WarehouseCode3",
            //    SKU = "Salad",
            //    Qty = 15,
            //});

            //result.Add(new WMSPoReceiveItem()
            //{
            //    PoItemUuid = "94043d6f-e6bc-4428-8585-0ef133745c2c",
            //    WarehouseCode = "WarehouseCode3",
            //    SKU = "Tuna",
            //    Qty = 10,
            //});
            return result;
        }
    }
}
