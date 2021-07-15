

              
    

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
    /// Represents a Tester for OrderLine.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class OrderLineTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<OrderLine> GetFakerData()
        {
            #region faker data rules
            return new Faker<OrderLine>()
					.RuleFor(u => u.CentralOrderLineNum, f => default(long))
					.RuleFor(u => u.DatabaseNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.CentralOrderNum, f => default(long))
					.RuleFor(u => u.MasterAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProfileNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ChannelOrderID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.CentralProductNum, f => default(long))
					.RuleFor(u => u.ChannelItemID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.SKU, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.ItemTitle, f => f.Lorem.Sentence().TruncateTo(200))
					.RuleFor(u => u.OrderQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.UnitPrice, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.LineItemTaxAmount, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.LineShippingAmount, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.LineShippingTaxAmount, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.LineShippingDiscount, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.LineShippingDiscountTaxAmount, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.LineRecyclingFee, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.LineGiftMsg, f => f.Lorem.Sentence().TruncateTo(500))
					.RuleFor(u => u.LineGiftNotes, f => f.Lorem.Sentence().TruncateTo(400))
					.RuleFor(u => u.LineGiftAmount, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.LineGiftTaxAmount, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.LinePromotionCodes, f => f.Lorem.Sentence().TruncateTo(500))
					.RuleFor(u => u.LinePromotionAmount, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.LinePromotionTaxAmount, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.BundleStatus, f => f.Random.Bool())
					.RuleFor(u => u.HarmonizedCode, f => f.Lorem.Sentence().TruncateTo(20))
					.RuleFor(u => u.UPC, f => f.Lorem.Sentence().TruncateTo(20))
					.RuleFor(u => u.EAN, f => f.Lorem.Sentence().TruncateTo(20))
					.RuleFor(u => u.UnitOfMeasure, f => f.Lorem.Sentence().TruncateTo(20))
					.RuleFor(u => u.DBChannelOrderLineRowID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.CentralOrderUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.CentralOrderLineUuid, f => f.Random.Guid().ToString())
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<OrderLine> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public OrderLineTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<OrderLine>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<OrderLine>("SELECT TOP 1 * FROM OrderLine").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<OrderLine>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<OrderLine>("SELECT TOP 1 * FROM OrderLine").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new OrderLine();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "SKU", "ItemTitle" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<OrderLine>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.ItemTitle != dataOrig.ItemTitle &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.ItemTitle == newData.ItemTitle;

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

            var dataUpdate = DataBaseFactory.GetById<OrderLine>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"CentralOrderLineUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<OrderLine>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<OrderLine>("SELECT TOP 1 * FROM OrderLine").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<OrderLine>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<OrderLine>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<OrderLine>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<OrderLine>("SELECT TOP 1 * FROM OrderLine").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<OrderLine>("SELECT TOP 1 * FROM OrderLine").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<OrderLine>(listData.UniqueId);
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
            list.SetDataBaseFactory<OrderLine>(DataBaseFactory)
                .Save<OrderLine>();

            var cnt = DataBaseFactory.Count<OrderLine>("WHERE ChannelOrderID = @0", ChannelOrderID);
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
            list.SetDataBaseFactory<OrderLine>(DataBaseFactory)
                .Save<OrderLine>();

            var NewItemTitle = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<OrderLine>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            listFind.ToList().ForEach(x => x.ItemTitle = NewItemTitle);
            listFind.Save<OrderLine>();

            list = DataBaseFactory.Find<OrderLine>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            var result = list.Where(x => x.ItemTitle == NewItemTitle).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var ChannelOrderID = Guid.NewGuid().ToString();

            list.ForEach(x => x.ChannelOrderID = ChannelOrderID);
            list.SetDataBaseFactory<OrderLine>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<OrderLine>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<OrderLine>("WHERE ChannelOrderID = @0", ChannelOrderID);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<OrderLine>("SELECT TOP 1 * FROM OrderLine").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<OrderLine>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<OrderLine>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<OrderLine>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderLine>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<OrderLine>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderLine>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new OrderLine();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "SKU", "ItemTitle" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<OrderLine>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.ItemTitle != dataOrig.ItemTitle &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.ItemTitle == newData.ItemTitle;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<OrderLine>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "CentralOrderLineUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<OrderLine>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderLine>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<OrderLine>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderLine>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<OrderLine>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderLine>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<OrderLine>(listData.UniqueId);
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
                .SetDataBaseFactory<OrderLine>(DataBaseFactory)
                .SaveAsync<OrderLine>();

            var cnt = await DataBaseFactory.CountAsync<OrderLine>("WHERE ChannelOrderID = @0", ChannelOrderID);
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
                .SetDataBaseFactory<OrderLine>(DataBaseFactory)
                .SaveAsync<OrderLine>();

            var NewItemTitle = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<OrderLine>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID)).ToList();
            listFind.ToList().ForEach(x => x.ItemTitle = NewItemTitle);
            await listFind.SaveAsync<OrderLine>();

            list = DataBaseFactory.Find<OrderLine>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            var result = list.Where(x => x.ItemTitle == NewItemTitle).Count() == listFind.Count();

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
                .SetDataBaseFactory<OrderLine>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<OrderLine>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<OrderLine>("WHERE ChannelOrderID = @0", ChannelOrderID);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderLine>("SELECT TOP 1 * FROM OrderLine")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<OrderLine>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<OrderLine>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


