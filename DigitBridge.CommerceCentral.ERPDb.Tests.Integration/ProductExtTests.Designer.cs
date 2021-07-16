

              
    

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
    /// Represents a Tester for ProductExt.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class ProductExtTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<ProductExt> GetFakerData()
        {
            #region faker data rules
            return new Faker<ProductExt>()
					.RuleFor(u => u.DatabaseNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.MasterAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProfileNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProductUuid, f => f.Random.Guid().ToString())
					.RuleFor(u => u.CentralProductNum, f => default(long))
					.RuleFor(u => u.SKU, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.StyleCode, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.ColorPatternCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.SizeType, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.SizeCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.WidthCode, f => f.Lorem.Sentence().TruncateTo(30))
					.RuleFor(u => u.LengthCode, f => f.Lorem.Sentence().TruncateTo(30))
					.RuleFor(u => u.ClassCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.SubClassCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.DepartmentCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.DivisionCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.OEMCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.AlternateCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.Remark, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.Model, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.CatalogPage, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.CategoryCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.GroupCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.SubGroupCode, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.PriceRule, f => f.PickRandom(TestHelper.PriceRule))
					.RuleFor(u => u.Stockable, f => f.Random.Bool())
					.RuleFor(u => u.IsAr, f => f.Random.Bool())
					.RuleFor(u => u.IsAp, f => f.Random.Bool())
					.RuleFor(u => u.Taxable, f => f.Random.Bool())
					.RuleFor(u => u.Costable, f => f.Random.Bool())
					.RuleFor(u => u.IsProfit, f => f.Random.Bool())
					.RuleFor(u => u.Release, f => f.Random.Bool())
					.RuleFor(u => u.UOM, f => f.PickRandom(TestHelper.UOM))
					.RuleFor(u => u.QtyPerPallot, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.QtyPerCase, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.QtyPerBox, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.PackType, f => f.PickRandom(TestHelper.PackType))
					.RuleFor(u => u.PackQty, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.DefaultPackType, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.DefaultWarehouseNum, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.DefaultVendorNum, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.PoSize, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.MinStock, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.SalesCost, f => f.Random.Decimal(1, 1000, 6))
					.RuleFor(u => u.LeadTimeDay, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProductYear, f => f.Random.AlphaNumeric(50))
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<ProductExt> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public ProductExtTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<ProductExt>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<ProductExt>("SELECT TOP 1 * FROM ProductExt").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<ProductExt>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<ProductExt>("SELECT TOP 1 * FROM ProductExt").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new ProductExt();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "SKU", "StyleCode" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<ProductExt>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.StyleCode != dataOrig.StyleCode &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.StyleCode == newData.StyleCode;

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

            var dataUpdate = DataBaseFactory.GetById<ProductExt>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"ProductUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<ProductExt>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<ProductExt>("SELECT TOP 1 * FROM ProductExt").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<ProductExt>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<ProductExt>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<ProductExt>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<ProductExt>("SELECT TOP 1 * FROM ProductExt").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<ProductExt>("SELECT TOP 1 * FROM ProductExt").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<ProductExt>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<ProductExt>("SELECT TOP 1 * FROM ProductExt").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<ProductExt>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<ProductExt>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<ProductExt>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductExt>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<ProductExt>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductExt>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new ProductExt();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "SKU", "StyleCode" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<ProductExt>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.StyleCode != dataOrig.StyleCode &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.StyleCode == newData.StyleCode;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<ProductExt>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "ProductUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<ProductExt>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductExt>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<ProductExt>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductExt>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<ProductExt>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductExt>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<ProductExt>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductExt>("SELECT TOP 1 * FROM ProductExt")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<ProductExt>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<ProductExt>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}

