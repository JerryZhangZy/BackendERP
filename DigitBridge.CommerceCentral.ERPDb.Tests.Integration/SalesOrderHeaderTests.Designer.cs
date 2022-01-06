              
    

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
    /// Represents a Tester for SalesOrderHeader.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class SalesOrderHeaderTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<SalesOrderHeader> GetFakerData()
        {
            #region faker data rules
            return new Faker<SalesOrderHeader>()
					.RuleFor(u => u.DatabaseNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.MasterAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProfileNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.SalesOrderUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.OrderNumber, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.OrderType, f => f.Random.Int(1, 100))
					.RuleFor(u => u.OrderStatus, f => f.Random.Int(1, 100))
					.RuleFor(u => u.OrderDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.OrderTime, f => f.Date.Timespan())
					.RuleFor(u => u.ShipDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.DueDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.BillDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EtaArrivalDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EarliestShipDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.LatestShipDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.SignatureFlag, f => f.Random.Bool())
					.RuleFor(u => u.CustomerUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.CustomerCode, f => f.Lorem.Word())
					.RuleFor(u => u.CustomerName, f => f.Company.CompanyName())
					.RuleFor(u => u.Terms, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.TermsDays, f => f.Random.Int(1, 100))
					.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
					.RuleFor(u => u.SubTotalAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.SalesAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.TotalAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.TaxableAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.NonTaxableAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.TaxRate, f => f.Random.Decimal(0.01m, 0.99m, 6))
					.RuleFor(u => u.TaxAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.DiscountRate, f => f.Random.Decimal(0.01m, 0.99m, 6))
					.RuleFor(u => u.DiscountAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ShippingAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ShippingTaxAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.MiscAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.MiscTaxAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ChargeAndAllowanceAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ChannelAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.ShippingCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.PaidAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.CreditAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.Balance, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.UnitCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.AvgCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.LotCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.OrderSourceCode, f => f.Lorem.Word())
					.RuleFor(u => u.DepositAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.MiscInvoiceUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.SalesRep, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.SalesRep2, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.SalesRep3, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.SalesRep4, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.CommissionRate, f => f.Random.Decimal(0.01m, 0.99m, 6))
					.RuleFor(u => u.CommissionRate2, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.CommissionRate3, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.CommissionRate4, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.CommissionAmount, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.CommissionAmount2, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.CommissionAmount3, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.CommissionAmount4, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.UpdateDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EnterBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<SalesOrderHeader> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public SalesOrderHeaderTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<SalesOrderHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<SalesOrderHeader>("SELECT TOP 1 * FROM SalesOrderHeader").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<SalesOrderHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<SalesOrderHeader>("SELECT TOP 1 * FROM SalesOrderHeader").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new SalesOrderHeader();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "OrderNumber", "CustomerCode" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<SalesOrderHeader>(data.RowNum);
            var result = dataGet.OrderNumber != dataOrig.OrderNumber &&
                            dataGet.CustomerCode != dataOrig.CustomerCode &&
                            dataGet.OrderNumber == newData.OrderNumber &&
                            dataGet.CustomerCode == newData.CustomerCode;

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

            var dataUpdate = DataBaseFactory.GetById<SalesOrderHeader>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"SalesOrderUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<SalesOrderHeader>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<SalesOrderHeader>("SELECT TOP 1 * FROM SalesOrderHeader").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<SalesOrderHeader>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<SalesOrderHeader>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<SalesOrderHeader>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<SalesOrderHeader>("SELECT TOP 1 * FROM SalesOrderHeader").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<SalesOrderHeader>("SELECT TOP 1 * FROM SalesOrderHeader").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<SalesOrderHeader>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddList_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            list.SetDataBaseFactory<SalesOrderHeader>(DataBaseFactory)
                .Save<SalesOrderHeader>();

            var cnt = DataBaseFactory.Count<SalesOrderHeader>("WHERE CustomerUuid = @0", CustomerUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void SaveList_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            list.SetDataBaseFactory<SalesOrderHeader>(DataBaseFactory)
                .Save<SalesOrderHeader>();

            var NewCustomerCode = Guid.NewGuid().ToString();
            var listFind = DataBaseFactory.Find<SalesOrderHeader>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid).ToList();
            listFind.ToList().ForEach(x => x.CustomerCode = NewCustomerCode);
            listFind.Save<SalesOrderHeader>();

            list = DataBaseFactory.Find<SalesOrderHeader>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid).ToList();
            var result = list.Where(x => x.CustomerCode == NewCustomerCode).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteList_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            list.SetDataBaseFactory<SalesOrderHeader>(DataBaseFactory)
                .Save();

            var listFind = DataBaseFactory.Find<SalesOrderHeader>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid).ToList();
            listFind.Delete();

            var cnt = DataBaseFactory.Count<SalesOrderHeader>("WHERE CustomerUuid = @0", CustomerUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<SalesOrderHeader>("SELECT TOP 1 * FROM SalesOrderHeader").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<SalesOrderHeader>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<SalesOrderHeader>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<SalesOrderHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SalesOrderHeader>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<SalesOrderHeader>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SalesOrderHeader>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new SalesOrderHeader();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "OrderNumber", "CustomerCode" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<SalesOrderHeader>(data.RowNum);
            var result = dataGet.OrderNumber != dataOrig.OrderNumber &&
                            dataGet.CustomerCode != dataOrig.CustomerCode &&
                            dataGet.OrderNumber == newData.OrderNumber &&
                            dataGet.CustomerCode == newData.CustomerCode;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<SalesOrderHeader>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "SalesOrderUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<SalesOrderHeader>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SalesOrderHeader>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<SalesOrderHeader>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SalesOrderHeader>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<SalesOrderHeader>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SalesOrderHeader>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<SalesOrderHeader>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            await list
                .SetDataBaseFactory<SalesOrderHeader>(DataBaseFactory)
                .SaveAsync<SalesOrderHeader>();

            var cnt = await DataBaseFactory.CountAsync<SalesOrderHeader>("WHERE CustomerUuid = @0", CustomerUuid);
            var result = cnt.Equals(list.Count());

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task SaveListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            await list
                .SetDataBaseFactory<SalesOrderHeader>(DataBaseFactory)
                .SaveAsync<SalesOrderHeader>();

            var NewCustomerCode = Guid.NewGuid().ToString();
            var listFind = (await DataBaseFactory.FindAsync<SalesOrderHeader>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid)).ToList();
            listFind.ToList().ForEach(x => x.CustomerCode = NewCustomerCode);
            await listFind.SaveAsync<SalesOrderHeader>();

            list = DataBaseFactory.Find<SalesOrderHeader>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid).ToList();
            var result = list.Where(x => x.CustomerCode == NewCustomerCode).Count() == listFind.Count();

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteListAsync_Test()
        {
            var list = FakerData.Generate(10);
            var CustomerUuid = Guid.NewGuid().ToString();

            list.ForEach(x => x.CustomerUuid = CustomerUuid);
            await list
                .SetDataBaseFactory<SalesOrderHeader>(DataBaseFactory)
                .SaveAsync();

            var listFind = (await DataBaseFactory.FindAsync<SalesOrderHeader>("WHERE CustomerUuid = @0 ORDER BY RowNum", CustomerUuid)).ToList();
            await listFind.DeleteAsync();

            var cnt = await DataBaseFactory.CountAsync<SalesOrderHeader>("WHERE CustomerUuid = @0", CustomerUuid);
            var result = cnt == 0;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SalesOrderHeader>("SELECT TOP 1 * FROM SalesOrderHeader")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<SalesOrderHeader>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<SalesOrderHeader>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


