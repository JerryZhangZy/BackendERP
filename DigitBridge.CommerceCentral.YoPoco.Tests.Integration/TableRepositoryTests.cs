using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Bogus;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.Base.Utility;

namespace DigitBridge.CommerceCentral.YoPoco.Tests.Integration
{
    /// <summary>
    /// Represents a Tester for InvoiceItems.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class InvoiceItemsTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
	{
        private const string SkipReason = "Debug Helper Function";

        private TestFixture<StartupTest> Fixture { get; }
        private IConfiguration Configuration { get; }
        private Faker<InvoiceItems> FakerData { get; set; }
        private IDataBaseFactory DataBaseFactory { get; set; }

		public InvoiceItemsTests(TestFixture<StartupTest> fixture)
		{
			Fixture = fixture;
			Configuration = fixture.Configuration;

			InitForTest();
		}

        private void InitForTest()
        {
            DataBaseFactory = new DataBaseFactory(Configuration["dsn"]);
			var Seq = 0;
			#region faker data rules
			FakerData = new Faker<InvoiceItems>()
							.RuleFor(u => u.InvoiceItemsId, f => f.Random.Guid().ToString())
							.RuleFor(u => u.InvoiceId, f => f.Random.Guid().ToString())
							.RuleFor(u => u.Seq, f => f.Random.Int(1, 100))
							.RuleFor(u => u.InvoiceItemType, f => f.PickRandom(TestHelper.InvoiceItemType))
							.RuleFor(u => u.InvoiceItemStatus, f => f.PickRandom(TestHelper.InvoiceItemStatus))
							.RuleFor(u => u.ItemDate, f => f.Date.Past(0).Date)
							.RuleFor(u => u.ItemTime, f => f.Date.Timespan())
							.RuleFor(u => u.ShipDate, f => f.Date.Past(0).Date)
							.RuleFor(u => u.EtaArrivalDate, f => f.Date.Past(0).Date)
							.RuleFor(u => u.SKU, f => f.Lorem.Sentence().TruncateTo(100))
							.RuleFor(u => u.InventoryId, f => f.Random.Guid().ToString())
							.RuleFor(u => u.WarehouseID, f => f.Random.Guid().ToString())
							.RuleFor(u => u.LotNum, f => f.Lorem.Sentence().TruncateTo(100))
							.RuleFor(u => u.Description, f => f.Lorem.Sentence().TruncateTo(200))
							.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
							.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
							.RuleFor(u => u.UOM, f => f.PickRandom(TestHelper.UOM))
							.RuleFor(u => u.PackType, f => f.PickRandom(TestHelper.PackType))
							.RuleFor(u => u.PackQty, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.OrderPack, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.ShipPack, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.CancelledPack, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.OrderQty, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.ShipQty, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.CancelledQty, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.PriceRule, f => f.PickRandom(TestHelper.PriceRule))
							.RuleFor(u => u.Price, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.DiscountRate, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.DiscountAmount, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.DiscountPrice, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.ExtAmount, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.TaxRate, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.TaxAmount, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.ShippingAmount, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.ShippingTaxAmount, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.MiscAmount, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.MiscTaxAmount, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.ChargeAndAllowanceAmount, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.ItemTotalAmount, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.Stockable, f => f.Random.Bool())
							.RuleFor(u => u.IsAr, f => f.Random.Bool())
							.RuleFor(u => u.Taxable, f => f.Random.Bool())
							.RuleFor(u => u.Costable, f => f.Random.Bool())
							.RuleFor(u => u.UnitCost, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.AvgCost, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.LotCost, f => f.Random.Decimal(1, 1000, 4))
							.RuleFor(u => u.LotInDate, f => f.Date.Past(0).Date)
							.RuleFor(u => u.LotExpDate, f => f.Date.Past(0).Date)
							.RuleFor(u => u.EnterDateUtc, f => f.Date.Past(0).Date)
							.RuleFor(u => u.UpdateDateUtc, f => f.Date.Past(0).Date)
							.RuleFor(u => u.EnterBy, f => f.Lorem.Sentence().TruncateTo(100))
							.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
							.RuleFor(u => u.DigitBridgeGuid, f => f.Random.Guid())
							;
			#endregion faker data rules
		}
		public void Dispose()
		{
		}

		//[Fact()]
		[Fact(Skip = SkipReason)]
		public void Register_Test()
		{
			var data = FakerData.Generate();
			data.Register();
			var poco = data.GetPocoData();

			Assert.True(true, "This test is a debug helper");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void CopyFrom_Test()
        {
            var data = FakerData.Generate();
            var newData = FakerData.Generate();
            data.CopyFrom(newData);
            var result = !data.Equals(newData);

			Assert.True(true, "This test is a debug helper");
        }

        #region sync methods

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void Add_Test()
		{
			var data = FakerData.Generate();
            data.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
			data.Add();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<InvoiceItems>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This test is a debug helper");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>().ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<InvoiceItems>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>().ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new InvoiceItems();
            dataOrig.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data.CopyFrom(newData);
            data.Patch(new[] { "OrderPack", "ShipPack" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<InvoiceItems>(data.RowNum);
            var result = dataGet.OrderPack != dataOrig.OrderPack &&
                            dataGet.ShipPack != dataOrig.ShipPack &&
                            dataGet.OrderPack == newData.OrderPack &&
                            dataGet.ShipPack == newData.ShipPack;

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Save_Test()
        {
	        var dataNew = FakerData.Generate();
            dataNew.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
            dataNew.Save();
            DataBaseFactory.Commit();

            var dataUpdate = DataBaseFactory.GetById<InvoiceItems>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate.CopyFrom(dataChanged, new[] {"InvoiceItemsId"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<InvoiceItems>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>().ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<InvoiceItems>(data.UniqueId);

            Assert.True(!result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>().ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.Get<InvoiceItems>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>().ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<InvoiceItems>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var invoiceId = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceId = invoiceId);
            list
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .Save<InvoiceItems>();

            var cnt = DataBaseFactory.Count<InvoiceItems>("WHERE InvoiceId = @0", invoiceId);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var invoiceId = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceId = invoiceId);
            list
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .Save<InvoiceItems>();

            var listFind = DataBaseFactory.Find<InvoiceItems>("WHERE InvoiceId = @0 ORDER BY Seq", invoiceId).ToList();
            listFind.ToList().ForEach(x => x.ItemTotalAmount = 999);
            listFind.Save<InvoiceItems>();

            list = DataBaseFactory.Find<InvoiceItems>("WHERE InvoiceId = @0 ORDER BY Seq", invoiceId).ToList();
            var result = list.Where(x => x.ItemTotalAmount == 999).Count() == listFind.Count();

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var invoiceId = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceId = invoiceId);
            list
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<InvoiceItems>("WHERE InvoiceId = @0 ORDER BY Seq", invoiceId).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<InvoiceItems>("WHERE InvoiceId = @0", invoiceId);
            var result = cnt == 0;

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>("SELECT TOP 1 * FROM InvoiceItems").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<InvoiceItems>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<InvoiceItems>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This test is a debug helper");
        }

        #endregion sync methods

        #region async methods

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddAsync_Test()
        {
            var data = FakerData.Generate();
            data.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
            await data.AddAsync().ConfigureAwait(false);
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<InvoiceItems>(data.UniqueId).ConfigureAwait(false);
            var result = data.Equals(dataGet);

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InvoiceItems>().ConfigureAwait(false)).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data.CopyFrom(newData);
            await data.PutAsync().ConfigureAwait(false);
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<InvoiceItems>(data.UniqueId).ConfigureAwait(false);
            var result = data.Equals(dataGet);

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InvoiceItems>().ConfigureAwait(false)).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new InvoiceItems();
            dataOrig.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data.CopyFrom(newData);
            await data.PatchAsync(new[] { "OrderPack", "ShipPack" }).ConfigureAwait(false);
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<InvoiceItems>(data.RowNum).ConfigureAwait(false);
            var result = dataGet.OrderPack != dataOrig.OrderPack &&
                            dataGet.ShipPack != dataOrig.ShipPack &&
                            dataGet.OrderPack == newData.OrderPack &&
                            dataGet.ShipPack == newData.ShipPack;

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveAsync_Test()
        {
            var dataNew = FakerData.Generate();
            dataNew.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
            await dataNew.SaveAsync().ConfigureAwait(false);
            DataBaseFactory.Commit();

            var dataUpdate = await DataBaseFactory.GetByIdAsync<InvoiceItems>(dataNew.UniqueId).ConfigureAwait(false);
            var dataChanged = FakerData.Generate();
            dataUpdate.CopyFrom(dataChanged, new[] { "InvoiceItemsId" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync().ConfigureAwait(false);
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<InvoiceItems>(dataUpdate.UniqueId).ConfigureAwait(false);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InvoiceItems>().ConfigureAwait(false)).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync().ConfigureAwait(false);
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<InvoiceItems>(data.UniqueId).ConfigureAwait(false);

            Assert.True(!result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InvoiceItems>().ConfigureAwait(false)).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<InvoiceItems>(listData.RowNum).ConfigureAwait(false);
            var result = data.Equals(listData);

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InvoiceItems>().ConfigureAwait(false)).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<InvoiceItems>(listData.UniqueId).ConfigureAwait(false);
            var result = data.Equals(listData);

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var invoiceId = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceId = invoiceId);
            await list
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .SaveAsync<InvoiceItems>()
                .ConfigureAwait(false);

            var cnt = await DataBaseFactory.CountAsync<InvoiceItems>("WHERE InvoiceId = @0", invoiceId).ConfigureAwait(false);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var invoiceId = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceId = invoiceId);
            await list
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .SaveAsync<InvoiceItems>()
                .ConfigureAwait(false);

            var listFind = (await DataBaseFactory.FindAsync<InvoiceItems>("WHERE InvoiceId = @0 ORDER BY Seq", invoiceId).ConfigureAwait(false)).ToList();
            listFind.ToList().ForEach(x => x.ItemTotalAmount = 999);
            await listFind.SaveAsync<InvoiceItems>().ConfigureAwait(false);

            list = DataBaseFactory.Find<InvoiceItems>("WHERE InvoiceId = @0 ORDER BY Seq", invoiceId).ToList();
            var result = list.Where(x => x.ItemTotalAmount == 999).Count() == listFind.Count();

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var invoiceId = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceId = invoiceId);
            await list
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .SaveAsync()
                .ConfigureAwait(false);

            var listFind = (await DataBaseFactory.FindAsync<InvoiceItems>("WHERE InvoiceId = @0 ORDER BY Seq", invoiceId).ConfigureAwait(false)).ToList();
            await listFind.DeleteAsync().ConfigureAwait(false);

            var cnt = await DataBaseFactory.CountAsync<InvoiceItems>("WHERE InvoiceId = @0", invoiceId).ConfigureAwait(false);
            var result = cnt == 0;

            Assert.True(result, "This test is a debug helper");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InvoiceItems>("SELECT TOP 1 * FROM InvoiceItems").ConfigureAwait(false)).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<InvoiceItems>(data.UniqueId).ConfigureAwait(false);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<InvoiceItems>(data.UniqueId).ConfigureAwait(false);

            var result = data1 == data2;

            Assert.True(result, "This test is a debug helper");
        }

        #endregion sync methods

    }
}
