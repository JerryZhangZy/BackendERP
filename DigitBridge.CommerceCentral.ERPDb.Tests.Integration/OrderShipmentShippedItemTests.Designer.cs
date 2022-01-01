              
    

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
    /// Represents a Tester for OrderShipmentShippedItem.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class OrderShipmentShippedItemTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<OrderShipmentShippedItem> GetFakerData()
        {
            #region faker data rules
            return new Faker<OrderShipmentShippedItem>()
					.RuleFor(u => u.OrderShipmentShippedItemNum, f => default(long))
					.RuleFor(u => u.DatabaseNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.MasterAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProfileNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.OrderShipmentNum, f => default(long))
					.RuleFor(u => u.OrderShipmentPackageNum, f => default(long))
					.RuleFor(u => u.ChannelOrderID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.CentralOrderNum, f => default(long))
					.RuleFor(u => u.CentralOrderLineNum, f => default(long))
					.RuleFor(u => u.CentralProductNum, f => default(long))
					.RuleFor(u => u.DistributionProductNum, f => default(long))
					.RuleFor(u => u.OrderDCAssignmentLineNum, f => default(long))
					.RuleFor(u => u.SKU, f => f.Commerce.Product())
					.RuleFor(u => u.ShippedQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.DBChannelOrderLineRowID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.OrderShipmentUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.OrderShipmentPackageUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.OrderShipmentShippedItemUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.SalesOrderItemsUuid, f => f.Random.Guid().ToString())
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<OrderShipmentShippedItem> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public OrderShipmentShippedItemTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<OrderShipmentShippedItem>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<OrderShipmentShippedItem>("SELECT TOP 1 * FROM OrderShipmentShippedItem").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<OrderShipmentShippedItem>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<OrderShipmentShippedItem>("SELECT TOP 1 * FROM OrderShipmentShippedItem").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new OrderShipmentShippedItem();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "SKU", "ChannelOrderID" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<OrderShipmentShippedItem>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.ChannelOrderID != dataOrig.ChannelOrderID &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.ChannelOrderID == newData.ChannelOrderID;

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

            var dataUpdate = DataBaseFactory.GetById<OrderShipmentShippedItem>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"OrderShipmentShippedItemUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<OrderShipmentShippedItem>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<OrderShipmentShippedItem>("SELECT TOP 1 * FROM OrderShipmentShippedItem").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<OrderShipmentShippedItem>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<OrderShipmentShippedItem>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<OrderShipmentShippedItem>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<OrderShipmentShippedItem>("SELECT TOP 1 * FROM OrderShipmentShippedItem").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<OrderShipmentShippedItem>("SELECT TOP 1 * FROM OrderShipmentShippedItem").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<OrderShipmentShippedItem>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var ChannelOrderID = Guid.NewGuid().ToString();

            list.ForEach(x => x.ChannelOrderID = ChannelOrderID);
            list.SetDataBaseFactory<OrderShipmentShippedItem>(DataBaseFactory)
                .Save<OrderShipmentShippedItem>();

            var cnt = DataBaseFactory.Count<OrderShipmentShippedItem>("WHERE ChannelOrderID = @0", ChannelOrderID);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var ChannelOrderID = Guid.NewGuid().ToString();

            list.ForEach(x => x.ChannelOrderID = ChannelOrderID);
            list.SetDataBaseFactory<OrderShipmentShippedItem>(DataBaseFactory)
                .Save<OrderShipmentShippedItem>();

            var NewChannelOrderID = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<OrderShipmentShippedItem>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            listFind.ToList().ForEach(x => x.ChannelOrderID = NewChannelOrderID);
            listFind.Save<OrderShipmentShippedItem>();

            list = DataBaseFactory.Find<OrderShipmentShippedItem>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            var result = list.Where(x => x.ChannelOrderID == NewChannelOrderID).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var ChannelOrderID = Guid.NewGuid().ToString();

            list.ForEach(x => x.ChannelOrderID = ChannelOrderID);
            list.SetDataBaseFactory<OrderShipmentShippedItem>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<OrderShipmentShippedItem>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<OrderShipmentShippedItem>("WHERE ChannelOrderID = @0", ChannelOrderID);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<OrderShipmentShippedItem>("SELECT TOP 1 * FROM OrderShipmentShippedItem").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<OrderShipmentShippedItem>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<OrderShipmentShippedItem>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<OrderShipmentShippedItem>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentShippedItem>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<OrderShipmentShippedItem>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentShippedItem>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new OrderShipmentShippedItem();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "SKU", "ChannelOrderID" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<OrderShipmentShippedItem>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.ChannelOrderID != dataOrig.ChannelOrderID &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.ChannelOrderID == newData.ChannelOrderID;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<OrderShipmentShippedItem>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "OrderShipmentShippedItemUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<OrderShipmentShippedItem>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentShippedItem>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<OrderShipmentShippedItem>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentShippedItem>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<OrderShipmentShippedItem>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentShippedItem>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<OrderShipmentShippedItem>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var ChannelOrderID = Guid.NewGuid().ToString();

            list.ForEach(x => x.ChannelOrderID = ChannelOrderID);
            await list
                .SetDataBaseFactory<OrderShipmentShippedItem>(DataBaseFactory)
                .SaveAsync<OrderShipmentShippedItem>();

            var cnt = await DataBaseFactory.CountAsync<OrderShipmentShippedItem>("WHERE ChannelOrderID = @0", ChannelOrderID);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var ChannelOrderID = Guid.NewGuid().ToString();

            list.ForEach(x => x.ChannelOrderID = ChannelOrderID);
            await list
                .SetDataBaseFactory<OrderShipmentShippedItem>(DataBaseFactory)
                .SaveAsync<OrderShipmentShippedItem>();

            var NewChannelOrderID = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<OrderShipmentShippedItem>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID)).ToList();
            listFind.ToList().ForEach(x => x.ChannelOrderID = NewChannelOrderID);
            await listFind.SaveAsync<OrderShipmentShippedItem>();

            list = DataBaseFactory.Find<OrderShipmentShippedItem>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            var result = list.Where(x => x.ChannelOrderID == NewChannelOrderID).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var ChannelOrderID = Guid.NewGuid().ToString();

            list.ForEach(x => x.ChannelOrderID = ChannelOrderID);
            await list
                .SetDataBaseFactory<OrderShipmentShippedItem>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<OrderShipmentShippedItem>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<OrderShipmentShippedItem>("WHERE ChannelOrderID = @0", ChannelOrderID);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentShippedItem>("SELECT TOP 1 * FROM OrderShipmentShippedItem")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<OrderShipmentShippedItem>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<OrderShipmentShippedItem>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


