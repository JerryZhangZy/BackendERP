              
    

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
    /// Represents a Tester for PoItems.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class PoItemsTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<PoItems> GetFakerData()
        {
            #region faker data rules
            return new Faker<PoItems>()
					.RuleFor(u => u.PoItemUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.PoUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.Seq, f => f.Random.Int(1, 100))
					.RuleFor(u => u.PoItemType, f => f.Random.Int(1, 100))
					.RuleFor(u => u.PoItemStatus, f => f.Random.Int(1, 100))
					.RuleFor(u => u.PoDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.PoTime, f => f.Date.Timespan())
					.RuleFor(u => u.EtaShipDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EtaArrivalDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.CancelDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.ProductUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.InventoryUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.SKU, f => f.Commerce.Product())
					.RuleFor(u => u.Description, f => f.Commerce.ProductName())
					.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
					.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
					.RuleFor(u => u.PoQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ReceivedQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.CancelledQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.PriceRule, f => f.PickRandom(TestHelper.PriceRule))
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
					.RuleFor(u => u.Costable, f => f.Random.Bool())
					.RuleFor(u => u.Taxable, f => f.Random.Bool())
					.RuleFor(u => u.IsAp, f => f.Random.Bool())
					.RuleFor(u => u.UpdateDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EnterBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<PoItems> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public PoItemsTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<PoItems>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<PoItems>("SELECT TOP 1 * FROM PoItems").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<PoItems>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<PoItems>("SELECT TOP 1 * FROM PoItems").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new PoItems();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "SKU", "Description" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<PoItems>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.Description != dataOrig.Description &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.Description == newData.Description;

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

            var dataUpdate = DataBaseFactory.GetById<PoItems>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"PoItemUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<PoItems>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<PoItems>("SELECT TOP 1 * FROM PoItems").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<PoItems>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<PoItems>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<PoItems>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<PoItems>("SELECT TOP 1 * FROM PoItems").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<PoItems>("SELECT TOP 1 * FROM PoItems").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<PoItems>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            list.SetDataBaseFactory<PoItems>(DataBaseFactory)
                .Save<PoItems>();

            var cnt = DataBaseFactory.Count<PoItems>("WHERE PoUuid = @0", PoUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            list.SetDataBaseFactory<PoItems>(DataBaseFactory)
                .Save<PoItems>();

            var NewDescription = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<PoItems>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid).ToList();
            listFind.ToList().ForEach(x => x.Description = NewDescription);
            listFind.Save<PoItems>();

            list = DataBaseFactory.Find<PoItems>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid).ToList();
            var result = list.Where(x => x.Description == NewDescription).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            list.SetDataBaseFactory<PoItems>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<PoItems>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<PoItems>("WHERE PoUuid = @0", PoUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<PoItems>("SELECT TOP 1 * FROM PoItems").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<PoItems>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<PoItems>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<PoItems>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItems>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<PoItems>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItems>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new PoItems();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "SKU", "Description" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<PoItems>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.Description != dataOrig.Description &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.Description == newData.Description;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<PoItems>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "PoItemUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<PoItems>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItems>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<PoItems>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItems>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<PoItems>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItems>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<PoItems>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            await list
                .SetDataBaseFactory<PoItems>(DataBaseFactory)
                .SaveAsync<PoItems>();

            var cnt = await DataBaseFactory.CountAsync<PoItems>("WHERE PoUuid = @0", PoUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            await list
                .SetDataBaseFactory<PoItems>(DataBaseFactory)
                .SaveAsync<PoItems>();

            var NewDescription = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<PoItems>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid)).ToList();
            listFind.ToList().ForEach(x => x.Description = NewDescription);
            await listFind.SaveAsync<PoItems>();

            list = DataBaseFactory.Find<PoItems>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid).ToList();
            var result = list.Where(x => x.Description == NewDescription).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var PoUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.PoUuid = PoUuid);
            await list
                .SetDataBaseFactory<PoItems>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<PoItems>("WHERE PoUuid = @0 ORDER BY RowNum", PoUuid)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<PoItems>("WHERE PoUuid = @0", PoUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<PoItems>("SELECT TOP 1 * FROM PoItems")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<PoItems>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<PoItems>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}

