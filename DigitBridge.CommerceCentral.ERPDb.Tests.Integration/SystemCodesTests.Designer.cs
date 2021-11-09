              
    

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
    /// Represents a Tester for SystemCodes.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class SystemCodesTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static Faker<SystemCodes> GetFakerData()
        {
            #region faker data rules
            return new Faker<SystemCodes>()
					.RuleFor(u => u.DatabaseNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.MasterAccountNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.ProfileNum, f => f.Random.Int(1, 100))
					.RuleFor(u => u.SystemCodeUuid, f => f.Lorem.Word())
					.RuleFor(u => u.SystemCodeName, f => f.Company.CompanyName())
					.RuleFor(u => u.Description, f => f.Commerce.ProductName())
					.RuleFor(u => u.InActive, f => f.Random.Bool())
					.RuleFor(u => u.EffectiveStart, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EffectiveEnd, f => f.Date.Past(0).Date)
					.RuleFor(u => u.Fields, (f, u) => u.Fields.SetValues(f.Random.JObject()))
					.RuleFor(u => u.UpdateDateUtc, f => f.Date.Past(0).Date)
					.RuleFor(u => u.EnterBy, f => f.Lorem.Sentence().TruncateTo(100))
					.RuleFor(u => u.UpdateBy, f => f.Lorem.Sentence().TruncateTo(100))
					;
            #endregion faker data rules
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public Faker<SystemCodes> FakerData { get; set; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public SystemCodesTests(TestFixture<StartupTest> fixture) 
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

            var dataGet = DataBaseFactory.GetFromCacheById<SystemCodes>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Put_Test()
        {
            var list = DataBaseFactory.Find<SystemCodes>("SELECT TOP 1 * FROM SystemCodes").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Put();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<SystemCodes>(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Patch_Test()
        {
            var list = DataBaseFactory.Find<SystemCodes>("SELECT TOP 1 * FROM SystemCodes").ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new SystemCodes();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            data.Patch(new[] { "SystemCodeName", "Description" });
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCache<SystemCodes>(data.RowNum);
            var result = dataGet.SystemCodeName != dataOrig.SystemCodeName &&
                            dataGet.Description != dataOrig.Description &&
                            dataGet.SystemCodeName == newData.SystemCodeName &&
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

            var dataUpdate = DataBaseFactory.GetById<SystemCodes>(dataNew.UniqueId);
			var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] {"SystemCodeUuid"});

            DataBaseFactory.Begin();
            dataUpdate.Save();
            DataBaseFactory.Commit();

            var dataGet = DataBaseFactory.GetFromCacheById<SystemCodes>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        { 
            var list = DataBaseFactory.Find<SystemCodes>("SELECT TOP 1 * FROM SystemCodes").ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<SystemCodes>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            //var list = DataBaseFactory.Find<SystemCodes>().ToList();
            //var listData = list.FirstOrDefault();
            //var data = DataBaseFactory.Get<SystemCodes>(listData.RowNum);
            //var result = data.Equals(listData);

            var list = DataBaseFactory.Find<SystemCodes>("SELECT TOP 1 * FROM SystemCodes").ToList();
            var listData = list.FirstOrDefault(); 
            var result = listData!=null;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetById_Test()
        {
            var list = DataBaseFactory.Find<SystemCodes>("SELECT TOP 1 * FROM SystemCodes").ToList();
            var listData = list.FirstOrDefault();
            var data = DataBaseFactory.GetById<SystemCodes>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetFromCacheById_Test()
        {
            var list = DataBaseFactory.Find<SystemCodes>("SELECT TOP 1 * FROM SystemCodes").ToList();
            var data = list.FirstOrDefault();
            var data1 = DataBaseFactory.GetFromCacheById<SystemCodes>(data.UniqueId);
            var data2 = DataBaseFactory.GetFromCacheById<SystemCodes>(data.UniqueId);

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

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<SystemCodes>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PutAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SystemCodes>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PutAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<SystemCodes>(data.UniqueId);
            var result = data.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task PatchAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SystemCodes>()).ToList();

            DataBaseFactory.Begin();
            var data = list.FirstOrDefault();
            var dataOrig = new SystemCodes();
            dataOrig?.CopyFrom(data);

            data.SetDataBaseFactory(DataBaseFactory);
            var newData = FakerData.Generate();
            data?.CopyFrom(newData);
            await data.PatchAsync(new[] { "SystemCodeName", "Description" });
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheAsync<SystemCodes>(data.RowNum);
            var result = dataGet.SystemCodeName != dataOrig.SystemCodeName &&
                            dataGet.Description != dataOrig.Description &&
                            dataGet.SystemCodeName == newData.SystemCodeName &&
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

            var dataUpdate = await DataBaseFactory.GetByIdAsync<SystemCodes>(dataNew.UniqueId);
            var dataChanged = FakerData.Generate();
            dataUpdate?.CopyFrom(dataChanged, new[] { "SystemCodeUuid" });

            DataBaseFactory.Begin();
            await dataUpdate.SaveAsync();
            DataBaseFactory.Commit();

            var dataGet = await DataBaseFactory.GetFromCacheByIdAsync<SystemCodes>(dataUpdate.UniqueId);
            var result = dataUpdate.Equals(dataGet);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SystemCodes>()).ToList();
            var data = list.FirstOrDefault();

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = await DataBaseFactory.ExistUniqueIdAsync<SystemCodes>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SystemCodes>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetAsync<SystemCodes>(listData.RowNum);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SystemCodes>()).ToList();
            var listData = list.FirstOrDefault();
            var data = await DataBaseFactory.GetByIdAsync<SystemCodes>(listData.UniqueId);
            var result = data.Equals(listData);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetFromCacheByIdAsync_Test()
        {
            var list = (await DataBaseFactory.FindAsync<SystemCodes>("SELECT TOP 1 * FROM SystemCodes")).ToList();
            var data = list.FirstOrDefault();
            var data1 = await DataBaseFactory.GetFromCacheByIdAsync<SystemCodes>(data.UniqueId);
            var data2 = await DataBaseFactory.GetFromCacheByIdAsync<SystemCodes>(data.UniqueId);

            var result = data1 == data2;

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


