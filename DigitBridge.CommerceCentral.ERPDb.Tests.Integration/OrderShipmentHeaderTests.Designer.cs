

              
    

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
    /// Represents a Tester for OrderShipmentHeader.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class OrderShipmentHeaderTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<OrderShipmentHeader> GetFakerData()
        {
            #region faker data rules
            return new Faker<OrderShipmentHeader>()
					.RuleFor(u => u.OrderShipmentNum, f => default(long))
					.RuleFor(u => u.DatabaseNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.MasterAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProfileNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.OrderDCAssignmentNum, f => default(long))
					.RuleFor(u => u.DistributionCenterNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.CentralOrderNum, f => default(long))
					.RuleFor(u => u.ChannelOrderID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ShipmentID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.WarehouseID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ShipmentType, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ShipmentReferenceID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ShipmentDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.ShippingCarrier, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.ShippingClass, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.ShippingCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.MainTrackingNumber, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.MainReturnTrackingNumber, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.BillOfLadingID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.TotalPackages, f => f.Random.Int(1, 100))
					.RuleFor(u => u.TotalShippedQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.TotalCanceledQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.TotalWeight, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.TotalVolume, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.WeightUnit, f => f.Random.Int(1, 100))
					.RuleFor(u => u.LengthUnit, f => f.Random.Int(1, 100))
					.RuleFor(u => u.VolumeUnit, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ShipmentStatus, f => f.Random.Int(1, 100))
					.RuleFor(u => u.DBChannelOrderHeaderRowID, f => f.Random.Guid().ToString())
					.RuleFor(u => u.ProcessStatus, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProcessDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.OrderShipmentUuid, f => f.Random.Guid().ToString())
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<OrderShipmentHeader> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public OrderShipmentHeaderTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<OrderShipmentHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<OrderShipmentHeader>("SELECT TOP 1 * FROM OrderShipmentHeader").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<OrderShipmentHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<OrderShipmentHeader>("SELECT TOP 1 * FROM OrderShipmentHeader").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new OrderShipmentHeader();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "ShippingCarrier", "ShippingClass" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<OrderShipmentHeader>(data.RowNum);
            var result = dataGet.ShippingCarrier != dataOrig.ShippingCarrier &&
                            dataGet.ShippingClass != dataOrig.ShippingClass &&
                            dataGet.ShippingCarrier == newData.ShippingCarrier &&
                            dataGet.ShippingClass == newData.ShippingClass;

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

            var dataUpdate = DataBaseFactory.GetById<OrderShipmentHeader>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"OrderShipmentUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<OrderShipmentHeader>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<OrderShipmentHeader>("SELECT TOP 1 * FROM OrderShipmentHeader").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<OrderShipmentHeader>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<OrderShipmentHeader>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<OrderShipmentHeader>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<OrderShipmentHeader>("SELECT TOP 1 * FROM OrderShipmentHeader").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<OrderShipmentHeader>("SELECT TOP 1 * FROM OrderShipmentHeader").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<OrderShipmentHeader>(listData.UniqueId);
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
            list.SetDataBaseFactory<OrderShipmentHeader>(DataBaseFactory)
                .Save<OrderShipmentHeader>();

            var cnt = DataBaseFactory.Count<OrderShipmentHeader>("WHERE ChannelOrderID = @0", ChannelOrderID);
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
            list.SetDataBaseFactory<OrderShipmentHeader>(DataBaseFactory)
                .Save<OrderShipmentHeader>();

            var NewShippingClass = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<OrderShipmentHeader>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            listFind.ToList().ForEach(x => x.ShippingClass = NewShippingClass);
            listFind.Save<OrderShipmentHeader>();

            list = DataBaseFactory.Find<OrderShipmentHeader>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            var result = list.Where(x => x.ShippingClass == NewShippingClass).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var ChannelOrderID = Guid.NewGuid().ToString();

            list.ForEach(x => x.ChannelOrderID = ChannelOrderID);
            list.SetDataBaseFactory<OrderShipmentHeader>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<OrderShipmentHeader>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<OrderShipmentHeader>("WHERE ChannelOrderID = @0", ChannelOrderID);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<OrderShipmentHeader>("SELECT TOP 1 * FROM OrderShipmentHeader").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<OrderShipmentHeader>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<OrderShipmentHeader>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<OrderShipmentHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentHeader>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<OrderShipmentHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentHeader>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new OrderShipmentHeader();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "ShippingCarrier", "ShippingClass" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<OrderShipmentHeader>(data.RowNum);
            var result = dataGet.ShippingCarrier != dataOrig.ShippingCarrier &&
                            dataGet.ShippingClass != dataOrig.ShippingClass &&
                            dataGet.ShippingCarrier == newData.ShippingCarrier &&
                            dataGet.ShippingClass == newData.ShippingClass;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<OrderShipmentHeader>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "OrderShipmentUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<OrderShipmentHeader>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentHeader>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<OrderShipmentHeader>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentHeader>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<OrderShipmentHeader>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentHeader>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<OrderShipmentHeader>(listData.UniqueId);
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
                .SetDataBaseFactory<OrderShipmentHeader>(DataBaseFactory)
                .SaveAsync<OrderShipmentHeader>();

            var cnt = await DataBaseFactory.CountAsync<OrderShipmentHeader>("WHERE ChannelOrderID = @0", ChannelOrderID);
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
                .SetDataBaseFactory<OrderShipmentHeader>(DataBaseFactory)
                .SaveAsync<OrderShipmentHeader>();

            var NewShippingClass = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<OrderShipmentHeader>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID)).ToList();
            listFind.ToList().ForEach(x => x.ShippingClass = NewShippingClass);
            await listFind.SaveAsync<OrderShipmentHeader>();

            list = DataBaseFactory.Find<OrderShipmentHeader>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID).ToList();
            var result = list.Where(x => x.ShippingClass == NewShippingClass).Count() == listFind.Count();

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
                .SetDataBaseFactory<OrderShipmentHeader>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<OrderShipmentHeader>("WHERE ChannelOrderID = @0 ORDER BY RowNum", ChannelOrderID)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<OrderShipmentHeader>("WHERE ChannelOrderID = @0", ChannelOrderID);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<OrderShipmentHeader>("SELECT TOP 1 * FROM OrderShipmentHeader")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<OrderShipmentHeader>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<OrderShipmentHeader>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}

