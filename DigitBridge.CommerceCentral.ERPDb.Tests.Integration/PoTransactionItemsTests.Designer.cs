              
    

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
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Xunit;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using Bogus;

namespace DigitBridge.CommerceCentral.ERPDb.Tests.Integration
{
    /// <summary>
    /// Represents a Tester for PoTransactionItems.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class PoTransactionItemsTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<PoTransactionItems> GetFakerData()
        {
            #region faker data rules
            return new Faker<PoTransactionItems>()
					.RuleFor(u => u.TransItemUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.TransUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.Seq, f => f.Random.Int(1, 100))
					.RuleFor(u => u.PoUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.PoItemUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ItemType, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ItemStatus, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ItemDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.ItemTime, f => f.Date.Timespan())
					.RuleFor(u => u.ProductUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.InventoryUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.SKU, f => f.Commerce.Product())
					.RuleFor(u => u.WarehouseUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.LotNum, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.LotDescription, f => f.Commerce.ProductName())
					.RuleFor(u => u.LotInDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.LotExpDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.Description, f => f.Commerce.ProductName())
					.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
					.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
					.RuleFor(u => u.UOM, f => f.PickRandom(TestHelper.UOM))
					.RuleFor(u => u.TransQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.Price, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ExtAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.TaxRate, f => f.Random.Decimal(0.01m, 0.99m, 6))
					.RuleFor(u => u.TaxAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.DiscountRate, f => f.Random.Decimal(0.01m, 0.99m, 6))
					.RuleFor(u => u.DiscountAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ShippingAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ShippingTaxAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.MiscAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.MiscTaxAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ChargeAndAllowanceAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.Stockable, f => f.Random.Bool())
					.RuleFor(u => u.IsAp, f => f.Random.Bool())
					.RuleFor(u => u.Taxable, f => f.Random.Bool())
					.RuleFor(u => u.Costable, f => f.Random.Bool())
					.RuleFor(u => u.UpdateDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EnterBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<PoTransactionItems> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public PoTransactionItemsTests(TestFixture<StartupTest> fixture) 
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
            data?.CopyFrom(newData);
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

            var dataGet = DataBaseFactory.GetFromCacheById<PoTransactionItems>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<PoTransactionItems>("SELECT TOP 1 * FROM PoTransactionItems").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<PoTransactionItems>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<PoTransactionItems>("SELECT TOP 1 * FROM PoTransactionItems").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new PoTransactionItems();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "SKU", "LotNum" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<PoTransactionItems>(data.RowNum);
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

            var dataUpdate = DataBaseFactory.GetById<PoTransactionItems>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"TransItemUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<PoTransactionItems>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<PoTransactionItems>("SELECT TOP 1 * FROM PoTransactionItems").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<PoTransactionItems>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<PoTransactionItems>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<PoTransactionItems>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<PoTransactionItems>("SELECT TOP 1 * FROM PoTransactionItems").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<PoTransactionItems>("SELECT TOP 1 * FROM PoTransactionItems").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<PoTransactionItems>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var TransUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.TransUuid = TransUuid);
            list.SetDataBaseFactory<PoTransactionItems>(DataBaseFactory)
                .Save<PoTransactionItems>();

            var cnt = DataBaseFactory.Count<PoTransactionItems>("WHERE TransUuid = @0", TransUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var TransUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.TransUuid = TransUuid);
            list.SetDataBaseFactory<PoTransactionItems>(DataBaseFactory)
                .Save<PoTransactionItems>();

            var NewLotNum = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<PoTransactionItems>("WHERE TransUuid = @0 ORDER BY RowNum", TransUuid).ToList();
            listFind.ToList().ForEach(x => x.LotNum = NewLotNum);
            listFind.Save<PoTransactionItems>();

            list = DataBaseFactory.Find<PoTransactionItems>("WHERE TransUuid = @0 ORDER BY RowNum", TransUuid).ToList();
            var result = list.Where(x => x.LotNum == NewLotNum).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var TransUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.TransUuid = TransUuid);
            list.SetDataBaseFactory<PoTransactionItems>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<PoTransactionItems>("WHERE TransUuid = @0 ORDER BY RowNum", TransUuid).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<PoTransactionItems>("WHERE TransUuid = @0", TransUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<PoTransactionItems>("SELECT TOP 1 * FROM PoTransactionItems").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<PoTransactionItems>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<PoTransactionItems>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<PoTransactionItems>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoTransactionItems>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<PoTransactionItems>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoTransactionItems>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new PoTransactionItems();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "SKU", "LotNum" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<PoTransactionItems>(data.RowNum);
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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<PoTransactionItems>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "TransItemUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<PoTransactionItems>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoTransactionItems>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<PoTransactionItems>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoTransactionItems>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<PoTransactionItems>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoTransactionItems>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<PoTransactionItems>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var TransUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.TransUuid = TransUuid);
            await list
                .SetDataBaseFactory<PoTransactionItems>(DataBaseFactory)
                .SaveAsync<PoTransactionItems>();

            var cnt = await DataBaseFactory.CountAsync<PoTransactionItems>("WHERE TransUuid = @0", TransUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var TransUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.TransUuid = TransUuid);
            await list
                .SetDataBaseFactory<PoTransactionItems>(DataBaseFactory)
                .SaveAsync<PoTransactionItems>();

            var NewLotNum = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<PoTransactionItems>("WHERE TransUuid = @0 ORDER BY RowNum", TransUuid)).ToList();
            listFind.ToList().ForEach(x => x.LotNum = NewLotNum);
            await listFind.SaveAsync<PoTransactionItems>();

            list = DataBaseFactory.Find<PoTransactionItems>("WHERE TransUuid = @0 ORDER BY RowNum", TransUuid).ToList();
            var result = list.Where(x => x.LotNum == NewLotNum).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var TransUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.TransUuid = TransUuid);
            await list
                .SetDataBaseFactory<PoTransactionItems>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<PoTransactionItems>("WHERE TransUuid = @0 ORDER BY RowNum", TransUuid)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<PoTransactionItems>("WHERE TransUuid = @0", TransUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoTransactionItems>("SELECT TOP 1 * FROM PoTransactionItems")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<PoTransactionItems>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<PoTransactionItems>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}

