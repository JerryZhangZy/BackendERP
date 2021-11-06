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
    public partial class WMSShipmentClientTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }

        //private string _baseUrl = "http://localhost:7074/api/"; 
        private string _baseUrl = "https://digitbridge-erp-integration-api-dev.azurewebsites.net/api/";
        private string _code = "aa4QcFoSH4ADcXEROimDtbPa4h0mY/dsNFuK1GfHPAhqx5xMJRAaHw==";
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        public WMSShipmentClientTests(TestFixture<StartupTest> fixture)
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
        public async Task AddSingleShipmentAsync_Simple_Test()
        {
            var client = new WMSShipmentClient(_baseUrl, _code);
            var shipment = GetWmsShipment();
            await client.AddSingleShipmentAsync(MasterAccountNum, ProfileNum, shipment);
            Assert.True(client.ResopneData != null, $"SDK invoice failed. Error is {client.Messages.ObjectToString()}");
            //Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task AddSingleShipmentAsync_Full_Test()
        {
            var client = new WMSShipmentClient(_baseUrl, _code);
            var shipment = GetWmsShipment();
            var jsonData = JsonConvert.SerializeObject(shipment);
            var success = await client.AddSingleShipmentAsync(MasterAccountNum, ProfileNum, shipment);
            Assert.True(client.ResopneData != null, "Invoke sdk method (AddSingleShipmentAsync) failed.");
            Assert.True(success, $"SDK invoice succeed. You can try CreateShipmentAsync_Test to test logic,Logic error is{client.Messages.ObjectToString()}");
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
