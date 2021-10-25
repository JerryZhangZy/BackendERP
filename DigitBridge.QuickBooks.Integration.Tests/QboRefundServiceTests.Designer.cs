
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Utility;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;

namespace DigitBridge.QuickBooks.Integration.Tests
{
    public partial class QboRefundServiceTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;
        protected async Task<InvoiceTransactionData> GetFakerRefundDataAsync(InvoiceData invoiceData)
        {
            var data = InvoiceTransactionDataTests.GetFakerData();
            data.InvoiceTransaction.MasterAccountNum = MasterAccountNum;
            data.InvoiceTransaction.ProfileNum = ProfileNum;
            data.InvoiceTransaction.InvoiceNumber = invoiceData.InvoiceHeader.InvoiceNumber;

            for (int i = 0; i < data.InvoiceReturnItems.Count; i++)
            {
                var retrunItem = data.InvoiceReturnItems[i];
                var invoiceItem = invoiceData.InvoiceItems[i];
                retrunItem.ReturnQty = 1;
                retrunItem.SKU = invoiceItem.SKU;
                retrunItem.WarehouseCode = invoiceItem.WarehouseCode;
                retrunItem.InvoiceItemsUuid = invoiceItem.InvoiceItemsUuid;
            }

            return data;
        }
        protected async Task<InvoiceData> GetFakerInvoiceDataAsync()
        {
            var data = InvoiceDataTests.GetFakerData();
            data.InvoiceHeader.MasterAccountNum = MasterAccountNum;
            data.InvoiceHeader.ProfileNum = ProfileNum;

            var svc = new InventoryService(DataBaseFactory);
            var payload = new InventoryPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                Skus = new List<string>()
                {
                    "Bacon","Bike","Car","Chair","Cheese","Fish","Hat","ProductExt-NEW-0908-200018546","Salad","Shoes","Towels",
                },

            };
            payload = await svc.GetInventoryBySkuArrayAsync(payload);
            var list = payload.Inventorys;
            for (int i = 0; i < data.InvoiceItems.Count; i++)
            {
                var item = data.InvoiceItems[i];

                item.DiscountAmount = 0;
                item.ShipQty = 10;

                if (list != null && list.Count > i)
                {
                    var inventory = list[i].Inventory[0];
                    item.WarehouseCode = inventory.WarehouseCode;
                    item.SKU = inventory.SKU;
                    item.InventoryUuid = inventory.InventoryUuid; 
                }
            } 
            data.InvoiceHeader.InvoiceNumber = NumberGenerate.Generate();
            return data;
        }
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public QboRefundServiceTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            var Seq = 0;
            DataBaseFactory = new DataBaseFactory(Configuration["dsn"]);
        }
        public void Dispose()
        {
        }

        private async Task<InvoiceData> GetErpInvoiceDataAsync()
        {
            var srv = new InvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = await GetFakerInvoiceDataAsync();
            var dto = mapper.WriteDto(data, null);
            srv.Add(dto);

            var invoiceUuid = srv.Data.UniqueId;

            srv.List();
            srv.GetDataById(invoiceUuid);
            return srv.Data;
        }

        protected async Task<(string, int)> GetErpInvoiceNumberAndTranNumAsync()
        {
            var invoiceData = await GetErpInvoiceDataAsync();

            var srv = new InvoiceReturnService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = await GetFakerRefundDataAsync(invoiceData);
            var dto = mapper.WriteDto(data, null);
            srv.Add(dto);
            return (srv.Data.InvoiceTransaction.InvoiceNumber, srv.Data.InvoiceTransaction.TransNum);
        }

        protected async Task<(string, string)> GetErpInvoiceUuidAndTransUuidAsync()
        {
            var invoiceData = await GetErpInvoiceDataAsync();

            var srv = new InvoiceReturnService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = await GetFakerRefundDataAsync(invoiceData);
            var dto = mapper.WriteDto(data, null);
            var success = srv.Add(dto);

            var trans = srv.Data.InvoiceTransaction;

            return (trans.InvoiceUuid, trans.TransUuid);
        }
    }
}


