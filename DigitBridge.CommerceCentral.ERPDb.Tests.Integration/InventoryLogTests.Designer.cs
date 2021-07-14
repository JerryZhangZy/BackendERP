
              
    

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
    /// Represents a Tester for InventoryLog.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class InventoryLogTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<InventoryLog> GetFakerData()
        {
            #region faker data rules
            return new Faker<InventoryLog>()
					.RuleFor(u => u.DatabaseNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.MasterAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProfileNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.InventoryLogUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ProductUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.InventoryUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.BatchNum, f => f.Random.Long(1,1000))
					.RuleFor(u => u.LogType, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.LogUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.LogItemUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.LogStatus, f => f.Random.Int(1, 100))
					.RuleFor(u => u.LogDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.LogTime, f => f.Date.Timespan())
					.RuleFor(u => u.LogBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.SKU, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.Description, f => f.Lorem.Sentence().TruncateTo(200))
					.RuleFor(u => u.WarehouseUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.WhsDescription, f => f.Lorem.Sentence().TruncateTo(200))
					.RuleFor(u => u.LotNum, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.LotInDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.LotExpDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.LotDescription, f => f.Lorem.Sentence().TruncateTo(200))
					.RuleFor(u => u.LpnNum, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.LpnDescription, f => f.Lorem.Sentence().TruncateTo(200))
					.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
					.RuleFor(u => u.UOM, f => f.PickRandom(TestHelper.UOM))
					.RuleFor(u => u.LogQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.BeforeInstock, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.BeforeBaseCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.BeforeUnitCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.BeforeAvgCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.UpdateDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EnterBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<InventoryLog> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public InventoryLogTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<InventoryLog>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<InventoryLog>("SELECT TOP 1 * FROM InventoryLog").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<InventoryLog>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<InventoryLog>("SELECT TOP 1 * FROM InventoryLog").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new InventoryLog();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "LogType", "LogBy" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<InventoryLog>(data.RowNum);
            var result = dataGet.LogType != dataOrig.LogType &&
                            dataGet.LogBy != dataOrig.LogBy &&
                            dataGet.LogType == newData.LogType &&
                            dataGet.LogBy == newData.LogBy;

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

            var dataUpdate = DataBaseFactory.GetById<InventoryLog>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"InventoryLogUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<InventoryLog>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<InventoryLog>("SELECT TOP 1 * FROM InventoryLog").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<InventoryLog>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<InventoryLog>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<InventoryLog>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<InventoryLog>("SELECT TOP 1 * FROM InventoryLog").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<InventoryLog>("SELECT TOP 1 * FROM InventoryLog").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<InventoryLog>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var ProductUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ProductUuid = ProductUuid);
            list.SetDataBaseFactory<InventoryLog>(DataBaseFactory)
                .Save<InventoryLog>();

            var cnt = DataBaseFactory.Count<InventoryLog>("WHERE ProductUuid = @0", ProductUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var ProductUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ProductUuid = ProductUuid);
            list.SetDataBaseFactory<InventoryLog>(DataBaseFactory)
                .Save<InventoryLog>();

            var NewLogBy = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<InventoryLog>("WHERE ProductUuid = @0 ORDER BY RowNum", ProductUuid).ToList();
            listFind.ToList().ForEach(x => x.LogBy = NewLogBy);
            listFind.Save<InventoryLog>();

            list = DataBaseFactory.Find<InventoryLog>("WHERE ProductUuid = @0 ORDER BY RowNum", ProductUuid).ToList();
            var result = list.Where(x => x.LogBy == NewLogBy).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var ProductUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ProductUuid = ProductUuid);
            list.SetDataBaseFactory<InventoryLog>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<InventoryLog>("WHERE ProductUuid = @0 ORDER BY RowNum", ProductUuid).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<InventoryLog>("WHERE ProductUuid = @0", ProductUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<InventoryLog>("SELECT TOP 1 * FROM InventoryLog").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<InventoryLog>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<InventoryLog>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<InventoryLog>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InventoryLog>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<InventoryLog>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InventoryLog>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new InventoryLog();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "LogType", "LogBy" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<InventoryLog>(data.RowNum);
            var result = dataGet.LogType != dataOrig.LogType &&
                            dataGet.LogBy != dataOrig.LogBy &&
                            dataGet.LogType == newData.LogType &&
                            dataGet.LogBy == newData.LogBy;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<InventoryLog>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "InventoryLogUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<InventoryLog>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InventoryLog>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<InventoryLog>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InventoryLog>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<InventoryLog>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InventoryLog>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<InventoryLog>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var ProductUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ProductUuid = ProductUuid);
            await list
                .SetDataBaseFactory<InventoryLog>(DataBaseFactory)
                .SaveAsync<InventoryLog>();

            var cnt = await DataBaseFactory.CountAsync<InventoryLog>("WHERE ProductUuid = @0", ProductUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var ProductUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ProductUuid = ProductUuid);
            await list
                .SetDataBaseFactory<InventoryLog>(DataBaseFactory)
                .SaveAsync<InventoryLog>();

            var NewLogBy = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<InventoryLog>("WHERE ProductUuid = @0 ORDER BY RowNum", ProductUuid)).ToList();
            listFind.ToList().ForEach(x => x.LogBy = NewLogBy);
            await listFind.SaveAsync<InventoryLog>();

            list = DataBaseFactory.Find<InventoryLog>("WHERE ProductUuid = @0 ORDER BY RowNum", ProductUuid).ToList();
            var result = list.Where(x => x.LogBy == NewLogBy).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var ProductUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.ProductUuid = ProductUuid);
            await list
                .SetDataBaseFactory<InventoryLog>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<InventoryLog>("WHERE ProductUuid = @0 ORDER BY RowNum", ProductUuid)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<InventoryLog>("WHERE ProductUuid = @0", ProductUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<InventoryLog>("SELECT TOP 1 * FROM InventoryLog")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<InventoryLog>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<InventoryLog>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


