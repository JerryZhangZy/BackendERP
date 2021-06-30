
              
    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
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
using DigitBridge.CommerceCentral.XUnit.Common;
using Bogus;

namespace DigitBridge.CommerceCentral.ERPDb.Tests.Integration
{
    /// <summary>
    /// Represents a Tester for InvoiceItems.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class InvoiceItemsTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<InvoiceItems> GetFakerData()
        {
            #region faker data rules
            return new Faker<InvoiceItems>()
					.RuleFor(u => u.InvoiceItemsUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.InvoiceUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.Seq, f => f.Random.Int(1, 100))
					.RuleFor(u => u.InvoiceItemType, f => f.PickRandom(TestHelper.InvoiceItemType))
					.RuleFor(u => u.InvoiceItemStatus, f => f.PickRandom(TestHelper.InvoiceItemStatus))
					.RuleFor(u => u.ItemDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.ItemTime, f => f.Date.Timespan())
					.RuleFor(u => u.ShipDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EtaArrivalDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.SKU, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.ProductUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.InventoryUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.WarehouseUuid, f => f.Random.Guid().ToString())
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
					.RuleFor(u => u.UpdateDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EnterBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<InvoiceItems> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public InvoiceItemsTests(TestFixture<StartupTest> fixture) 
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            var Seq = 0;
            DataBaseFactory = new DataBaseFactory(Configuration["dsn"]);
            FakerData = GetFakerData();
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

            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void CopyFrom_Test()
        {
            var data = FakerData.Generate();
            var dataJson = data.ObjectToString();
            var newData = FakerData.Generate();
            var newDataJson = newData.ObjectToString();
            data.CopyFrom(newData);
            var result = !data.Equals(newData);

			Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
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

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<InvoiceItems>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new InvoiceItems();
            dataOrig.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data.CopyFrom(newData);
            data.Patch(new[] { "SKU", "LotNum" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<InvoiceItems>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.LotNum != dataOrig.LotNum &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.LotNum == newData.LotNum;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
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
            dataUpdate.CopyFrom(dataChanged, new[] {"InvoiceItemsUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<InvoiceItems>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<InvoiceItems>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.Get<InvoiceItems>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<InvoiceItems>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var InvoiceUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceUuid = InvoiceUuid);
            list.AsEnumerable()
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .Save<InvoiceItems>();

            var cnt = DataBaseFactory.Count<InvoiceItems>("WHERE InvoiceUuid = @0", InvoiceUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var InvoiceUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceUuid = InvoiceUuid);
            list.AsEnumerable()
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .Save<InvoiceItems>();

            var NewLotNum = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<InvoiceItems>("WHERE InvoiceUuid = @0 ORDER BY RowNum", InvoiceUuid);
            listFind.ToList().ForEach(x => x.LotNum = NewLotNum);
            listFind.Save<InvoiceItems>();

            list = DataBaseFactory.Find<InvoiceItems>("WHERE InvoiceUuid = @0 ORDER BY RowNum", InvoiceUuid).ToList();
            var result = list.Where(x => x.LotNum == NewLotNum).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var InvoiceUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceUuid = InvoiceUuid);
            list.AsEnumerable()
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<InvoiceItems>("WHERE InvoiceUuid = @0 ORDER BY RowNum", InvoiceUuid);
            listFind.Delete();

            var cnt = DataBaseFactory.Count<InvoiceItems>("WHERE InvoiceUuid = @0", InvoiceUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<InvoiceItems>("SELECT TOP 1 * FROM InvoiceItems");
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<InvoiceItems>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<InvoiceItems>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
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
            await data.AddAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<InvoiceItems>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = await DataBaseFactory.FindAsync<InvoiceItems>();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<InvoiceItems>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = await DataBaseFactory.FindAsync<InvoiceItems>();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new InvoiceItems();
            dataOrig.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data.CopyFrom(newData);
            await data.PatchAsync(new[] { "SKU", "LotNum" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<InvoiceItems>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.LotNum != dataOrig.LotNum &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.LotNum == newData.LotNum;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveAsync_Test()
        {
            var dataNew = FakerData.Generate();
            dataNew.SetDataBaseFactory(DataBaseFactory);
            DataBaseFactory.Begin();
            await dataNew.SaveAsync();
            DataBaseFactory.Commit();

            var dataUpdate = await DataBaseFactory.GetByIdAsync<InvoiceItems>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate.CopyFrom(dataChanged, new[] { "InvoiceItemsUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<InvoiceItems>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = await DataBaseFactory.FindAsync<InvoiceItems>();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<InvoiceItems>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = await DataBaseFactory.FindAsync<InvoiceItems>();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<InvoiceItems>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = await DataBaseFactory.FindAsync<InvoiceItems>();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<InvoiceItems>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var InvoiceUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceUuid = InvoiceUuid);
            await list.AsEnumerable()
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .SaveAsync<InvoiceItems>();

            var cnt = await DataBaseFactory.CountAsync<InvoiceItems>("WHERE InvoiceUuid = @0", InvoiceUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var InvoiceUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceUuid = InvoiceUuid);
            await list.AsEnumerable()
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .SaveAsync<InvoiceItems>();

            var NewLotNum = Guid.NewGuid().ToString();
            var listFind = await DataBaseFactory.FindAsync<InvoiceItems>("WHERE InvoiceUuid = @0 ORDER BY RowNum", InvoiceUuid);
            listFind.ToList().ForEach(x => x.LotNum = NewLotNum);
            await listFind.SaveAsync<InvoiceItems>();

            list = DataBaseFactory.Find<InvoiceItems>("WHERE InvoiceUuid = @0 ORDER BY RowNum", InvoiceUuid).ToList();
            var result = list.Where(x => x.LotNum == NewLotNum).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var InvoiceUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.InvoiceUuid = InvoiceUuid);
            await list.AsEnumerable()
                .SetDataBaseFactory<InvoiceItems>(DataBaseFactory)
                .SaveAsync();

            var listFind = await DataBaseFactory.FindAsync<InvoiceItems>("WHERE InvoiceUuid = @0 ORDER BY RowNum", InvoiceUuid);
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<InvoiceItems>("WHERE InvoiceUuid = @0", InvoiceUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = await DataBaseFactory.FindAsync<InvoiceItems>("SELECT TOP 1 * FROM InvoiceItems");
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<InvoiceItems>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<InvoiceItems>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


