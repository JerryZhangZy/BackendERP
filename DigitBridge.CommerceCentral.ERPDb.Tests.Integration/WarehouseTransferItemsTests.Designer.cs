              
    

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
    /// Represents a Tester for WarehouseTransferItems.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class WarehouseTransferItemsTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<WarehouseTransferItems> GetFakerData()
        {
            #region faker data rules
            return new Faker<WarehouseTransferItems>()
					.RuleFor(u => u.WarehouseTransferItemsUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ReferWarehouseTransferItemsUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.WarehouseTransferUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.Seq, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ItemDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.ItemTime, f => f.Date.Timespan())
					.RuleFor(u => u.SKU, f => f.Commerce.Product())
					.RuleFor(u => u.ProductUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.FromInventoryUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.FromWarehouseUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.FromWarehouseCode, f => f.Lorem.Word())
					.RuleFor(u => u.LotNum, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.ToInventoryUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ToWarehouseUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ToWarehouseCode, f => f.Lorem.Word())
					.RuleFor(u => u.ToLotNum, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.Description, f => f.Commerce.ProductName())
					.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
					.RuleFor(u => u.UOM, f => f.PickRandom(TestHelper.UOM))
					.RuleFor(u => u.PackType, f => f.PickRandom(TestHelper.PackType))
					.RuleFor(u => u.PackQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.TransferPack, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.TransferQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.FromBeforeInstockPack, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.FromBeforeInstockQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ToBeforeInstockPack, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ToBeforeInstockQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.UnitCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.AvgCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.LotCost, f => f.Random.Decimal(1, 1000, 6))
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
        public Faker<WarehouseTransferItems> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public WarehouseTransferItemsTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<WarehouseTransferItems>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<WarehouseTransferItems>("SELECT TOP 1 * FROM WarehouseTransferItems").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<WarehouseTransferItems>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<WarehouseTransferItems>("SELECT TOP 1 * FROM WarehouseTransferItems").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new WarehouseTransferItems();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "SKU", "FromWarehouseCode" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<WarehouseTransferItems>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.FromWarehouseCode != dataOrig.FromWarehouseCode &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.FromWarehouseCode == newData.FromWarehouseCode;

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

            var dataUpdate = DataBaseFactory.GetById<WarehouseTransferItems>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"WarehouseTransferItemsUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<WarehouseTransferItems>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<WarehouseTransferItems>("SELECT TOP 1 * FROM WarehouseTransferItems").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<WarehouseTransferItems>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<WarehouseTransferItems>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<WarehouseTransferItems>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<WarehouseTransferItems>("SELECT TOP 1 * FROM WarehouseTransferItems").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<WarehouseTransferItems>("SELECT TOP 1 * FROM WarehouseTransferItems").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<WarehouseTransferItems>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var ReferWarehouseTransferItemsUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ReferWarehouseTransferItemsUuid = ReferWarehouseTransferItemsUuid);
            list.SetDataBaseFactory<WarehouseTransferItems>(DataBaseFactory)
                .Save<WarehouseTransferItems>();

            var cnt = DataBaseFactory.Count<WarehouseTransferItems>("WHERE ReferWarehouseTransferItemsUuid = @0", ReferWarehouseTransferItemsUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var ReferWarehouseTransferItemsUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ReferWarehouseTransferItemsUuid = ReferWarehouseTransferItemsUuid);
            list.SetDataBaseFactory<WarehouseTransferItems>(DataBaseFactory)
                .Save<WarehouseTransferItems>();

            var NewFromWarehouseCode = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<WarehouseTransferItems>("WHERE ReferWarehouseTransferItemsUuid = @0 ORDER BY RowNum", ReferWarehouseTransferItemsUuid).ToList();
            listFind.ToList().ForEach(x => x.FromWarehouseCode = NewFromWarehouseCode);
            listFind.Save<WarehouseTransferItems>();

            list = DataBaseFactory.Find<WarehouseTransferItems>("WHERE ReferWarehouseTransferItemsUuid = @0 ORDER BY RowNum", ReferWarehouseTransferItemsUuid).ToList();
            var result = list.Where(x => x.FromWarehouseCode == NewFromWarehouseCode).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var ReferWarehouseTransferItemsUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ReferWarehouseTransferItemsUuid = ReferWarehouseTransferItemsUuid);
            list.SetDataBaseFactory<WarehouseTransferItems>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<WarehouseTransferItems>("WHERE ReferWarehouseTransferItemsUuid = @0 ORDER BY RowNum", ReferWarehouseTransferItemsUuid).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<WarehouseTransferItems>("WHERE ReferWarehouseTransferItemsUuid = @0", ReferWarehouseTransferItemsUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<WarehouseTransferItems>("SELECT TOP 1 * FROM WarehouseTransferItems").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<WarehouseTransferItems>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<WarehouseTransferItems>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<WarehouseTransferItems>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferItems>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<WarehouseTransferItems>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferItems>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new WarehouseTransferItems();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "SKU", "FromWarehouseCode" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<WarehouseTransferItems>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.FromWarehouseCode != dataOrig.FromWarehouseCode &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.FromWarehouseCode == newData.FromWarehouseCode;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<WarehouseTransferItems>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "WarehouseTransferItemsUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<WarehouseTransferItems>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferItems>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<WarehouseTransferItems>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferItems>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<WarehouseTransferItems>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferItems>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<WarehouseTransferItems>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var ReferWarehouseTransferItemsUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ReferWarehouseTransferItemsUuid = ReferWarehouseTransferItemsUuid);
            await list
                .SetDataBaseFactory<WarehouseTransferItems>(DataBaseFactory)
                .SaveAsync<WarehouseTransferItems>();

            var cnt = await DataBaseFactory.CountAsync<WarehouseTransferItems>("WHERE ReferWarehouseTransferItemsUuid = @0", ReferWarehouseTransferItemsUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var ReferWarehouseTransferItemsUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ReferWarehouseTransferItemsUuid = ReferWarehouseTransferItemsUuid);
            await list
                .SetDataBaseFactory<WarehouseTransferItems>(DataBaseFactory)
                .SaveAsync<WarehouseTransferItems>();

            var NewFromWarehouseCode = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<WarehouseTransferItems>("WHERE ReferWarehouseTransferItemsUuid = @0 ORDER BY RowNum", ReferWarehouseTransferItemsUuid)).ToList();
            listFind.ToList().ForEach(x => x.FromWarehouseCode = NewFromWarehouseCode);
            await listFind.SaveAsync<WarehouseTransferItems>();

            list = DataBaseFactory.Find<WarehouseTransferItems>("WHERE ReferWarehouseTransferItemsUuid = @0 ORDER BY RowNum", ReferWarehouseTransferItemsUuid).ToList();
            var result = list.Where(x => x.FromWarehouseCode == NewFromWarehouseCode).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var ReferWarehouseTransferItemsUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ReferWarehouseTransferItemsUuid = ReferWarehouseTransferItemsUuid);
            await list
                .SetDataBaseFactory<WarehouseTransferItems>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<WarehouseTransferItems>("WHERE ReferWarehouseTransferItemsUuid = @0 ORDER BY RowNum", ReferWarehouseTransferItemsUuid)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<WarehouseTransferItems>("WHERE ReferWarehouseTransferItemsUuid = @0", ReferWarehouseTransferItemsUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferItems>("SELECT TOP 1 * FROM WarehouseTransferItems")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<WarehouseTransferItems>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<WarehouseTransferItems>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


