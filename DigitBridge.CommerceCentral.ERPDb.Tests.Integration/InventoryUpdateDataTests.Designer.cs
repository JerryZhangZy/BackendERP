              
    

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
    /// Represents a Tester for InventoryUpdateData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class InventoryUpdateDataTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        public static InventoryUpdateData GetFakerData()
        {
			var InventoryUpdateData = new InventoryUpdateData(); 
			InventoryUpdateData.InventoryUpdateHeader = InventoryUpdateHeaderTests.GetFakerData().Generate(); 
			InventoryUpdateData.InventoryUpdateItems = InventoryUpdateItemsTests.GetFakerData().Generate(10); 
			return InventoryUpdateData; 
        }

        public static List<InventoryUpdateData> GetFakerData(int count)
        {
			var InventoryUpdateDatas = new List<InventoryUpdateData>(); 
			for (int i = 0; i < count; i++) 
				InventoryUpdateDatas.Add(GetFakerData()); 
			return InventoryUpdateDatas; 
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public InventoryUpdateDataTests(TestFixture<StartupTest> fixture) 
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            var Seq = 0;
            DataBaseFactory = new DataBaseFactory(Configuration["dsn"]);
        }
        public void Dispose()
        {
        }

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void Clone_Test()
		{
            var id = DataBaseFactory.GetValue<InventoryUpdateHeader, string>(@"
SELECT TOP 1 ins.InventoryUpdateUuid 
FROM InventoryUpdateHeader ins 
INNER JOIN (
    SELECT it.InventoryUpdateUuid, COUNT(1) AS cnt FROM InventoryUpdateItems it GROUP BY it.InventoryUpdateUuid
) itm ON (itm.InventoryUpdateUuid = ins.InventoryUpdateUuid)
WHERE itm.cnt > 0
");


            var data = new InventoryUpdateData(DataBaseFactory);
            data.GetById(id);

            var dataClone = data.Clone();
            var result = !data.Equals(dataClone);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        #region sync methods

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void Save_Test()
		{
			var data = GetFakerData();
            data.SetDataBaseFactory(DataBaseFactory);
			data.Save();

            var dataGet = new InventoryUpdateData(DataBaseFactory);
            dataGet.GetById(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Get_Test()
        {
            Save_Test();

            var id = DataBaseFactory.GetValue<InventoryUpdateHeader, string>(@"
SELECT TOP 1 ins.InventoryUpdateUuid 
FROM InventoryUpdateHeader ins 
INNER JOIN (
    SELECT it.InventoryUpdateUuid, COUNT(1) AS cnt FROM InventoryUpdateItems it GROUP BY it.InventoryUpdateUuid
) itm ON (itm.InventoryUpdateUuid = ins.InventoryUpdateUuid)
WHERE itm.cnt > 0
");


            var data = new InventoryUpdateData(DataBaseFactory);
            data.GetById(id);
            var rowNum = data.InventoryUpdateHeader.RowNum;

            var dataUpdate = GetFakerData();
            dataUpdate.SetDataBaseFactory(DataBaseFactory);
            data?.CopyFrom(dataUpdate);
            data.Save();

            var dataGetById = new InventoryUpdateData(DataBaseFactory);
            dataGetById.GetById(id);

            var dataGet = new InventoryUpdateData(DataBaseFactory);
            dataGet.Get(rowNum);

            var result = data.Equals(dataGet) && dataGet.Equals(dataGetById);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void Delete_Test()
        {
            Save_Test();

            var id = DataBaseFactory.GetValue<InventoryUpdateHeader, string>(@"
SELECT TOP 1 ins.InventoryUpdateUuid 
FROM InventoryUpdateHeader ins 
INNER JOIN (
    SELECT it.InventoryUpdateUuid, COUNT(1) AS cnt FROM InventoryUpdateItems it GROUP BY it.InventoryUpdateUuid
) itm ON (itm.InventoryUpdateUuid = ins.InventoryUpdateUuid)
WHERE itm.cnt > 0
");


            var data = new InventoryUpdateData(DataBaseFactory);
            data.GetById(id);

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            data.Delete();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<InventoryUpdateHeader>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods


        #region async methods

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task SaveAsync_Test()
		{
			var data = GetFakerData();
            data.SetDataBaseFactory(DataBaseFactory);
			await data.SaveAsync();

            var dataGet = new InventoryUpdateData(DataBaseFactory);
            await dataGet.GetByIdAsync(data.UniqueId);
            var result = data.Equals(dataGet);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetAsync_Test()
        {
            await SaveAsync_Test();

            var id = await DataBaseFactory.GetValueAsync<InventoryUpdateHeader, string>(@"
SELECT TOP 1 ins.InventoryUpdateUuid 
FROM InventoryUpdateHeader ins 
INNER JOIN (
    SELECT it.InventoryUpdateUuid, COUNT(1) AS cnt FROM InventoryUpdateItems it GROUP BY it.InventoryUpdateUuid
) itm ON (itm.InventoryUpdateUuid = ins.InventoryUpdateUuid)
WHERE itm.cnt > 0
");


            var data = new InventoryUpdateData(DataBaseFactory);
            await data.GetByIdAsync(id);
            var rowNum = data.InventoryUpdateHeader.RowNum;

            var dataUpdate = GetFakerData();
            dataUpdate.SetDataBaseFactory(DataBaseFactory);
            data?.CopyFrom(dataUpdate);
            await data.SaveAsync();

            var dataGetById = new InventoryUpdateData(DataBaseFactory);
            await dataGetById.GetByIdAsync(id);

            var dataGet = new InventoryUpdateData(DataBaseFactory);
            await dataGet.GetAsync(rowNum);

            var result = data.Equals(dataGet) && dataGet.Equals(dataGetById);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteAsync_Test()
        {
            await SaveAsync_Test();

            var id = await DataBaseFactory.GetValueAsync<InventoryUpdateHeader, string>(@"
SELECT TOP 1 ins.InventoryUpdateUuid 
FROM InventoryUpdateHeader ins 
INNER JOIN (
    SELECT it.InventoryUpdateUuid, COUNT(1) AS cnt FROM InventoryUpdateItems it GROUP BY it.InventoryUpdateUuid
) itm ON (itm.InventoryUpdateUuid = ins.InventoryUpdateUuid)
WHERE itm.cnt > 0
");


            var data = new InventoryUpdateData(DataBaseFactory);
            await data.GetByIdAsync(id);

            DataBaseFactory.Begin();
            data.SetDataBaseFactory(DataBaseFactory);
            await data.DeleteAsync();
            DataBaseFactory.Commit();

            var result = DataBaseFactory.ExistUniqueId<InventoryUpdateHeader>(data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

    }
}


