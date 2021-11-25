using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Xunit;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.ERPDb;
using Bogus;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class InvoiceManagerTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Manager Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public InvoiceManagerTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();

            // CreateInvoice_Test();
        }
        protected void InitForTest()
        {
            var Seq = 0;
            DataBaseFactory = new DataBaseFactory(Configuration["dsn"]);
        }
        public void Dispose()
        {
        }


        #region sync methods
        [Fact]
        //[Fact(Skip = SkipReason)]
        public void CreateInvoice_Test()
        {
            var uuids = new List<string> {
            //"ac1e7f54-e5a2-4a45-80bd-a04a38b105b3",
            //"11cb3ad7-80b1-48ba-a7a7-02a1ddb238b7",
            //"ffb46bc8-a1f0-4245-9f7f-11f641411e11",
            //"0221637b-7c9d-45e4-aec1-c7f1235912b2",
            //"d9ec860f-8a9e-4f61-a9a4-9cedbd887961",
            //"f13f3fd4-715f-4772-8c39-e68494ec87c3",
            //"ed239ada-6526-4fed-99f2-e9a4fe7ad0f3",
            //"736bf01f-31a2-49b5-a14c-f6235e88e096",
            //"f9973f32-5d61-4e7c-8862-3f98f73da427"
            "cd1eefbb-d0f8-4110-9ae7-66f126802f63"
            };
            InvoiceManager invoiceManager = new InvoiceManager(DataBaseFactory);
            bool result = true;
            var invoiceUuid = string.Empty;
            foreach (var uuid in uuids)
            {
                try
                {
                    using (var b = new Benchmark("CreateSalesOrderByChannelOrderIdAsync_Test"))
                    {
                        invoiceUuid = invoiceManager.CreateInvoiceByOrderShipmentIdAsync(uuid).Result;
                        result = !string.IsNullOrEmpty(invoiceUuid);
                    }
                    if (result)
                        Assert.True(result);
                    else
                        Assert.False(result);
                }
                catch (Exception ex)
                {
                    Assert.False(false, ex.Message);

                }
            }

        }

        #endregion sync methods

        #region async methods

        [Fact()]
        public async Task CreateInvoiceFromShipmentAsync_Test()
        {
            var shipmentData = await SaveShipmentAndSalesOrder();
            var managerService = new InvoiceManager(DataBaseFactory);
            var invoiceUuid = await managerService.CreateInvoiceFromShipmentAsync(shipmentData);
            Assert.True(!string.IsNullOrEmpty(invoiceUuid), managerService.Messages.ObjectToString());
            Assert.False(string.IsNullOrEmpty(invoiceUuid));
        }
        [Fact()]
        public async Task CreateInvoiceByOrderShipmentIdAsync_Test()
        {
            var shipmentData = await SaveShipmentAndSalesOrder();
            var managerService = new InvoiceManager(DataBaseFactory);
            var invoiceUuid = await managerService.CreateInvoiceByOrderShipmentIdAsync(shipmentData.UniqueId);
            Assert.True(!string.IsNullOrEmpty(invoiceUuid), managerService.Messages.ObjectToString());
            Assert.False(string.IsNullOrEmpty(invoiceUuid));
        }

        [Fact()]
        public async Task UpdateFaultInvoicesAsync_Simple_Test()
        {
            var payload = new InvoicePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
            }; 

            var faultInvoiceList = GetFaultInvoiceList("UpdateFaultInvoicesAsync_Full_Test");
            var managerService = new InvoiceManager(DataBaseFactory);
            var result = await managerService.UpdateFaultInvoicesAsync(payload, faultInvoiceList);
            Assert.True(result != null, managerService.Messages.ObjectToString());

        }
        [Fact()]
        public async Task UpdateFaultInvoicesAsync_Full_Test()
        {
            var payload = new InvoicePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
            };

            var faultInvoiceList = GetFaultInvoiceList("UpdateFaultInvoicesAsync_Full_Test");
            var managerService = new InvoiceManager(DataBaseFactory);
            var result = await managerService.UpdateFaultInvoicesAsync(payload, faultInvoiceList);
            Assert.True(result != null, managerService.Messages.ObjectToString());

            var failedResult = result.Where(i => !i.Success);
            Assert.True(failedResult.Count() == 0, failedResult.SelectMany(i => i.Messages).ObjectToString());
        }



        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetInitNumber_Test()
        {
            var srv = new InvoiceManager(DataBaseFactory);
            string iniNumber = await srv.GetNextNumberAsync(10001, 10001, "eadf5c15-3702-ff74-7d68-5be78956ad45");
            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }
        #endregion async methods

        #region get faker data
        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;

        protected async Task<OrderShipmentData> SaveShipmentAndSalesOrder()
        {
            var skus = InventoryDataTests.GetInventories(DataBaseFactory);

            var shipmentService = new OrderShipmentService(DataBaseFactory);
            var shipmentDto = OrderShipmentDataDtoTests.GetFakerDataDto(DataBaseFactory, skus);
            var success = await shipmentService.AddAsync(shipmentDto);
            Assert.True(success && shipmentService.Data.OrderShipmentHeader.OrderShipmentNum > 0, shipmentService.Messages.ObjectToString());

            await SaveSalesOrder(shipmentService.Data, skus);

            return shipmentService.Data;
        }
        protected async Task SaveSalesOrder(OrderShipmentData shipmentData, Inventory[] skus)
        {
            var salesorderService = new SalesOrderService(DataBaseFactory);
            var salesorderDataDto = SalesOrderDataDtoTests.GetFakerSalesorderDataDto(shipmentData.OrderShipmentHeader.OrderDCAssignmentNum.ToLong(), skus);
            var success = await salesorderService.AddAsync(salesorderDataDto);
            Assert.True(success, salesorderService.Messages.ObjectToString());

            //add misinvoice info.
            var soHeader = salesorderService.Data.SalesOrderHeader;
            var salesOrderPayload = new SalesOrderPayload()
            {
                MasterAccountNum = soHeader.MasterAccountNum,
                ProfileNum = soHeader.ProfileNum,
            };
            success = await salesorderService.AddPrepaymentAsync(salesOrderPayload, soHeader.OrderNumber, soHeader.DepositAmount);
            Assert.True(success, salesorderService.Messages.ObjectToString());

        }
        #endregion

        #region prepare fault invoice data 
        protected List<FaultInvoiceRequestPayload> GetFaultInvoiceList(string error, int count = 2)
        {
            var list = new List<FaultInvoiceRequestPayload>();
            var eventUuids = GetEventUuid(count);
            foreach (var eventUuidJson in eventUuids)
            {
                var eventUuid = GetEventUuid(eventUuidJson);
                var faultInvoiceRequest = new FaultInvoiceRequestPayload()
                {
                    EventUuid = eventUuid,
                    Message = new StringBuilder().Append(eventUuid + error)
                };
                list.Add(faultInvoiceRequest);
            }

            return list;
        }

        private string GetEventUuid(string eventUuidJson)
        {

            return JArray.Parse(eventUuidJson)[0]["EventUuid"].ToString();
        }

        private List<string> GetEventUuid(int n = 1)
        {
            var sql = $@"
SELECT top {n} EventUuid 
FROM EventProcessERP ins  
WHERE [MasterAccountNum]={MasterAccountNum}
AND [ProfileNum]={ProfileNum}
AND ERPEventProcessType={(int)EventProcessTypeEnum.InvoiceToCommerceCentral}
AND ActionStatus={(int)EventProcessActionStatusEnum.Pending}
order by ins.rownum desc
for json path
";
            var eventUuidList = new List<string>();
            using (var trs = new ScopedTransaction(this.DataBaseFactory))
            {
                eventUuidList = SqlQuery.Execute(sql,
                        (string eventUuid) => eventUuid
                    );
                Assert.True(eventUuidList.Count != 0, "No mathched event process data in database");
                return eventUuidList;
            }
        }

        #endregion
    }
}
