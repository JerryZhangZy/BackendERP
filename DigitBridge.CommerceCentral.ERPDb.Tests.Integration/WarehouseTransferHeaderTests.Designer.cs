              
    

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
    /// Represents a Tester for WarehouseTransferHeader.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class WarehouseTransferHeaderTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<WarehouseTransferHeader> GetFakerData()
        {
            #region faker data rules
            return new Faker<WarehouseTransferHeader>()
					.RuleFor(u => u.DatabaseNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.MasterAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProfileNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.WarehouseTransferUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.BatchNumber, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.WarehouseTransferType, f => f.Random.Int(1, 100))
					.RuleFor(u => u.WarehouseTransferStatus, f => f.Random.Int(1, 100))
					.RuleFor(u => u.TransferDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.TransferTime, f => f.Date.Timespan())
					.RuleFor(u => u.Processor, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.ReceiveDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.ReceiveTime, f => f.Date.Timespan())
					.RuleFor(u => u.ReceiveProcessor, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.FromWarehouseUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.FromWarehouseCode, f => f.Lorem.Word())
					.RuleFor(u => u.ToWarehouseUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ToWarehouseCode, f => f.Lorem.Word())
					.RuleFor(u => u.ReferenceType, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ReferenceUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ReferenceNum, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.WarehouseTransferSourceCode, f => f.Lorem.Word())
					.RuleFor(u => u.UpdateDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EnterBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<WarehouseTransferHeader> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public WarehouseTransferHeaderTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<WarehouseTransferHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<WarehouseTransferHeader>("SELECT TOP 1 * FROM WarehouseTransferHeader").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<WarehouseTransferHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<WarehouseTransferHeader>("SELECT TOP 1 * FROM WarehouseTransferHeader").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new WarehouseTransferHeader();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "BatchNumber", "Processor" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<WarehouseTransferHeader>(data.RowNum);
            var result = dataGet.BatchNumber != dataOrig.BatchNumber &&
                            dataGet.Processor != dataOrig.Processor &&
                            dataGet.BatchNumber == newData.BatchNumber &&
                            dataGet.Processor == newData.Processor;

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

            var dataUpdate = DataBaseFactory.GetById<WarehouseTransferHeader>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"WarehouseTransferUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<WarehouseTransferHeader>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<WarehouseTransferHeader>("SELECT TOP 1 * FROM WarehouseTransferHeader").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<WarehouseTransferHeader>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<WarehouseTransferHeader>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<WarehouseTransferHeader>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<WarehouseTransferHeader>("SELECT TOP 1 * FROM WarehouseTransferHeader").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<WarehouseTransferHeader>("SELECT TOP 1 * FROM WarehouseTransferHeader").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<WarehouseTransferHeader>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var FromWarehouseUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.FromWarehouseUuid = FromWarehouseUuid);
            list.SetDataBaseFactory<WarehouseTransferHeader>(DataBaseFactory)
                .Save<WarehouseTransferHeader>();

            var cnt = DataBaseFactory.Count<WarehouseTransferHeader>("WHERE FromWarehouseUuid = @0", FromWarehouseUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var FromWarehouseUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.FromWarehouseUuid = FromWarehouseUuid);
            list.SetDataBaseFactory<WarehouseTransferHeader>(DataBaseFactory)
                .Save<WarehouseTransferHeader>();

            var NewProcessor = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<WarehouseTransferHeader>("WHERE FromWarehouseUuid = @0 ORDER BY RowNum", FromWarehouseUuid).ToList();
            listFind.ToList().ForEach(x => x.Processor = NewProcessor);
            listFind.Save<WarehouseTransferHeader>();

            list = DataBaseFactory.Find<WarehouseTransferHeader>("WHERE FromWarehouseUuid = @0 ORDER BY RowNum", FromWarehouseUuid).ToList();
            var result = list.Where(x => x.Processor == NewProcessor).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var FromWarehouseUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.FromWarehouseUuid = FromWarehouseUuid);
            list.SetDataBaseFactory<WarehouseTransferHeader>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<WarehouseTransferHeader>("WHERE FromWarehouseUuid = @0 ORDER BY RowNum", FromWarehouseUuid).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<WarehouseTransferHeader>("WHERE FromWarehouseUuid = @0", FromWarehouseUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<WarehouseTransferHeader>("SELECT TOP 1 * FROM WarehouseTransferHeader").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<WarehouseTransferHeader>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<WarehouseTransferHeader>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<WarehouseTransferHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferHeader>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<WarehouseTransferHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferHeader>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new WarehouseTransferHeader();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "BatchNumber", "Processor" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<WarehouseTransferHeader>(data.RowNum);
            var result = dataGet.BatchNumber != dataOrig.BatchNumber &&
                            dataGet.Processor != dataOrig.Processor &&
                            dataGet.BatchNumber == newData.BatchNumber &&
                            dataGet.Processor == newData.Processor;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<WarehouseTransferHeader>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "WarehouseTransferUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<WarehouseTransferHeader>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferHeader>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<WarehouseTransferHeader>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferHeader>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<WarehouseTransferHeader>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferHeader>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<WarehouseTransferHeader>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var FromWarehouseUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.FromWarehouseUuid = FromWarehouseUuid);
            await list
                .SetDataBaseFactory<WarehouseTransferHeader>(DataBaseFactory)
                .SaveAsync<WarehouseTransferHeader>();

            var cnt = await DataBaseFactory.CountAsync<WarehouseTransferHeader>("WHERE FromWarehouseUuid = @0", FromWarehouseUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var FromWarehouseUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.FromWarehouseUuid = FromWarehouseUuid);
            await list
                .SetDataBaseFactory<WarehouseTransferHeader>(DataBaseFactory)
                .SaveAsync<WarehouseTransferHeader>();

            var NewProcessor = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<WarehouseTransferHeader>("WHERE FromWarehouseUuid = @0 ORDER BY RowNum", FromWarehouseUuid)).ToList();
            listFind.ToList().ForEach(x => x.Processor = NewProcessor);
            await listFind.SaveAsync<WarehouseTransferHeader>();

            list = DataBaseFactory.Find<WarehouseTransferHeader>("WHERE FromWarehouseUuid = @0 ORDER BY RowNum", FromWarehouseUuid).ToList();
            var result = list.Where(x => x.Processor == NewProcessor).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var FromWarehouseUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.FromWarehouseUuid = FromWarehouseUuid);
            await list
                .SetDataBaseFactory<WarehouseTransferHeader>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<WarehouseTransferHeader>("WHERE FromWarehouseUuid = @0 ORDER BY RowNum", FromWarehouseUuid)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<WarehouseTransferHeader>("WHERE FromWarehouseUuid = @0", FromWarehouseUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<WarehouseTransferHeader>("SELECT TOP 1 * FROM WarehouseTransferHeader")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<WarehouseTransferHeader>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<WarehouseTransferHeader>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


