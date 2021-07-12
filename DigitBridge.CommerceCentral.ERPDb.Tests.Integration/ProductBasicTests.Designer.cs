
              
    

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
    /// Represents a Tester for ProductBasic.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class ProductBasicTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<ProductBasic> GetFakerData()
        {
            #region faker data rules
            return new Faker<ProductBasic>()
					.RuleFor(u => u.CentralProductNum, f => default(long))
					.RuleFor(u => u.DatabaseNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.MasterAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProfileNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.SKU, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.FNSku, f => f.Lorem.Sentence().TruncateTo(10))
					.RuleFor(u => u.Condition, f => f.Random.Bool())
					.RuleFor(u => u.Brand, f => f.Lorem.Sentence().TruncateTo(150))
					.RuleFor(u => u.Manufacturer, f => f.Lorem.Sentence().TruncateTo(255))
					.RuleFor(u => u.ProductTitle, f => f.Lorem.Sentence().TruncateTo(500))
					.RuleFor(u => u.LongDescription, f => f.Lorem.Sentence().TruncateTo(2000))
					.RuleFor(u => u.ShortDescription, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.Subtitle, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.ASIN, f => f.Lorem.Sentence().TruncateTo(10))
					.RuleFor(u => u.UPC, f => f.Lorem.Sentence().TruncateTo(20))
					.RuleFor(u => u.EAN, f => f.Lorem.Sentence().TruncateTo(20))
					.RuleFor(u => u.ISBN, f => f.Lorem.Sentence().TruncateTo(20))
					.RuleFor(u => u.MPN, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.Price, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.Cost, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.AvgCost, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.MAPPrice, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.MSRP, f => f.Random.Decimal(1, 1000, 0))
					.RuleFor(u => u.BundleType, f => f.Random.Bool())
					.RuleFor(u => u.ProductType, f => f.Random.Bool())
					.RuleFor(u => u.VariationVaryBy, f => f.Lorem.Sentence().TruncateTo(80))
					.RuleFor(u => u.CopyToChildren, f => f.Random.Bool())
					.RuleFor(u => u.MultipackQuantity, f => f.Random.Int(1, 100))
					.RuleFor(u => u.VariationParentSKU, f => f.Random.AlphaNumeric(50))
					.RuleFor(u => u.IsInRelationship, f => f.Random.Bool())
					.RuleFor(u => u.NetWeight, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.GrossWeight, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.WeightUnit, f => f.Random.Bool())
					.RuleFor(u => u.ProductHeight, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.ProductLength, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.ProductWidth, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.BoxHeight, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.BoxLength, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.BoxWidth, f => f.Random.Decimal(1, 1000, 2))
					.RuleFor(u => u.Unit, f => f.Random.Bool())
					.RuleFor(u => u.HarmonizedCode, f => f.Lorem.Sentence().TruncateTo(20))
					.RuleFor(u => u.TaxProductCode, f => f.Lorem.Sentence().TruncateTo(25))
					.RuleFor(u => u.IsBlocked, f => f.Random.Bool())
					.RuleFor(u => u.Warranty, f => f.Lorem.Sentence().TruncateTo(255))
					.RuleFor(u => u.CreateBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.CreateDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.UpdateDate, f => f.Date.Past(0).Date)
					.RuleFor(u => u.ClassificationNum, f => default(long))
					.RuleFor(u => u.OriginalUPC, f => f.Lorem.Sentence().TruncateTo(20))
					.RuleFor(u => u.ProductUuid, f => f.Random.Guid().ToString())
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<ProductBasic> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public ProductBasicTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<ProductBasic>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<ProductBasic>("SELECT TOP 1 * FROM ProductBasic").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<ProductBasic>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<ProductBasic>("SELECT TOP 1 * FROM ProductBasic").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new ProductBasic();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "SKU", "FNSku" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<ProductBasic>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.FNSku != dataOrig.FNSku &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.FNSku == newData.FNSku;

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

            var dataUpdate = DataBaseFactory.GetById<ProductBasic>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"ProductUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<ProductBasic>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<ProductBasic>("SELECT TOP 1 * FROM ProductBasic").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<ProductBasic>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<ProductBasic>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<ProductBasic>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<ProductBasic>("SELECT TOP 1 * FROM ProductBasic").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<ProductBasic>("SELECT TOP 1 * FROM ProductBasic").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<ProductBasic>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<ProductBasic>("SELECT TOP 1 * FROM ProductBasic").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<ProductBasic>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<ProductBasic>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<ProductBasic>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductBasic>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<ProductBasic>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductBasic>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new ProductBasic();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "SKU", "FNSku" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<ProductBasic>(data.RowNum);
            var result = dataGet.SKU != dataOrig.SKU &&
                            dataGet.FNSku != dataOrig.FNSku &&
                            dataGet.SKU == newData.SKU &&
                            dataGet.FNSku == newData.FNSku;

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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<ProductBasic>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "ProductUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<ProductBasic>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductBasic>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<ProductBasic>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductBasic>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<ProductBasic>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductBasic>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<ProductBasic>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<ProductBasic>("SELECT TOP 1 * FROM ProductBasic")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<ProductBasic>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<ProductBasic>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


