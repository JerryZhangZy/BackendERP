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
    public partial class WMSShipmentClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        private string _baseUrl { get; set; }
        private string _code { get; set; }
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public WMSShipmentClientTests(TestFixture<StartupTest> fixture)
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
        public async Task AddShipmentAsync_Simple_Test()
        {
            var client = new WMSShipmentClient(_baseUrl, _code);
            var shipment = GetWmsShipment();
            var success = await client.AddShipmentAsync(MasterAccountNum, ProfileNum, shipment);
            Assert.True(client.ResopneData != null, $"SDK invoice failed. Error is {client.Messages.ObjectToString()}");
            //Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task AddShipmentAsync_Full_Test()
        {
            var client = new WMSShipmentClient(_baseUrl, _code);
            var shipment = GetWmsShipment();
            var success = await client.AddShipmentAsync(MasterAccountNum, ProfileNum, shipment);
            Assert.True(client.ResopneData != null, $"SDK invoice failed. Error is {client.Messages.ObjectToString()}");
            Assert.True(success, $"SDK invoice succeed.But call integration api failed. You can try CreateShipmentAsync_Test to test logic,Logic error is{client.Messages.ObjectToString()}");
        }


        [Fact()]
        public async Task AddShipmentListAsync_Simple_Test()
        {
            var client = new WMSShipmentClient(_baseUrl, _code);
            var shipmentList = GetWmsShipmentList();
            var success = await client.AddShipmentListAsync(MasterAccountNum, ProfileNum, shipmentList);
            Assert.True(client.ResopneData != null, $"SDK invoke failed. Error is {client.Messages.ObjectToString()}");
            //Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task AddShipmentListAsync_Full_Test()
        {
            var client = new WMSShipmentClient(_baseUrl, _code);
            var shipmentList = GetWmsShipmentList();
            var success = await client.AddShipmentListAsync(MasterAccountNum, ProfileNum, shipmentList);
            Assert.True(client.ResopneData != null, $"SDK invoke failed. Error is {client.Messages.ObjectToString()}");
            Assert.True(success, $"SDK invoke succeed.But call integration api failed. You can try CreateShipmentAsync_Test to test logic,Logic error is{client.Messages.ObjectToString()}");
        }

        protected List<InputOrderShipmentType> GetWmsShipmentList(int count = 10)
        {
            var list = new List<InputOrderShipmentType>();
            int i = 0;
            while (i < count)
            {
                i++;
                list.Add(GetWmsShipment());
            }

            return list;
        }

        protected InputOrderShipmentType GetWmsShipment()
        {
            return new InputOrderShipmentType()
            {
                ShipmentHeader = new InputOrderShipmentHeaderType()
                {
                    ChannelOrderID = new Random().Next(1, 100).ToString(),
                    ShipmentID = Guid.NewGuid().ToString(),
                    MainTrackingNumber = Guid.NewGuid().ToString(),

                },
                PackageItems = new List<InputOrderShipmentPackageItemsType>()
                {
                    new InputOrderShipmentPackageItemsType()
                    {
                         ShipmentPackage=new InputOrderShipmentPackageType ()
                         {
                              PackageID=Guid.NewGuid().ToString(),
                              PackageQty=new Random().Next(1,100),
                              PackageTrackingNumber=Guid.NewGuid().ToString(),
                         },
                        ShippedItems=new List<InputOrderShipmentShippedItemType> ()
                        {
                            new InputOrderShipmentShippedItemType()
                            {
                                CentralOrderLineNum=new Random().Next(1,100),
                                ShippedQty=new Random().Next(1,100),
                                SKU=Guid.NewGuid().ToString(),

                            },
                        },
                    }
                }
            };
        }
    }
}
