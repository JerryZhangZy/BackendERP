              
    

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
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    /// <summary>
    /// Represents a Tester for InitNumbersService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class InitNumbersServiceTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected InitNumbersData GetFakerData()
        {
            return InitNumbersDataTests.GetFakerData();
        }

        protected List<InitNumbersData> GetFakerData(int count)
        {
            return InitNumbersDataTests.GetFakerData(count);
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public InitNumbersServiceTests(TestFixture<StartupTest> fixture) 
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

        #region sync methods

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void SaveData_Test()
		{
            var srv = new InitNumbersService(DataBaseFactory);
            srv.Add();
            srv.AttachData(GetFakerData());
            srv.Calculate();
			srv.SaveData();

            var srvGet = new InitNumbersService(DataBaseFactory);
            srvGet.Edit();
            srvGet.GetDataById(srv.Data.UniqueId);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void GetData_Test()
        {
            SaveData_Test();

            var id = DataBaseFactory.GetValue<InitNumbers, string>(@"
SELECT TOP 1 ins.InitNumbersUuid 
FROM InitNumbers ins 
");


            var srv = new InitNumbersService(DataBaseFactory);
            srv.Edit();
            srv.GetDataById(id);
            var rowNum = srv.Data.InitNumbers.RowNum;

            var dataUpdate = GetFakerData();
            srv.Data?.CopyFrom(dataUpdate);
            srv.Calculate();
            srv.SaveData();

            var srvGetById = new InitNumbersService(DataBaseFactory);
            srvGetById.List();
            srvGetById.GetDataById(id);

            var srvGet = new InitNumbersService(DataBaseFactory);
            srvGet.List();
            srvGet.GetData(rowNum);

            var result = srv.Data.Equals(srvGet.Data) && srvGet.Data.Equals(srvGetById.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void DeleteData_Test()
        {
            SaveData_Test();

            var id = DataBaseFactory.GetValue<InitNumbers, string>(@"
SELECT TOP 1 ins.InitNumbersUuid 
FROM InitNumbers ins 
");


            var srv = new InitNumbersService(DataBaseFactory);
            srv.Delete();
            srv.GetDataById(id);
            srv.DeleteData();

            var result = DataBaseFactory.ExistUniqueId<InitNumbers>(srv.Data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion sync methods

        #region async methods

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task SaveDataAsync_Test()
		{
            var srv = new InitNumbersService(DataBaseFactory);
            srv.Add();
            srv.AttachData(GetFakerData());
            srv.Calculate();
			await srv.SaveDataAsync();

            var srvGet = new InitNumbersService(DataBaseFactory);
            srvGet.Edit();
            await srvGet.GetDataByIdAsync(srv.Data.UniqueId);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetDataAsync_Test()
        {
            await SaveDataAsync_Test();

            var id = await DataBaseFactory.GetValueAsync<InitNumbers, string>(@"
SELECT TOP 1 ins.InitNumbersUuid 
FROM InitNumbers ins 
");


            var srv = new InitNumbersService(DataBaseFactory);
            srv.Edit();
            await srv.GetDataByIdAsync(id);
            var rowNum = srv.Data.InitNumbers.RowNum;

            var dataUpdate = GetFakerData();
            srv.Data?.CopyFrom(dataUpdate);
            srv.Calculate();
            await srv.SaveDataAsync();

            var srvGetById = new InitNumbersService(DataBaseFactory);
            srvGetById.List();
            await srvGetById.GetDataByIdAsync(id);

            var srvGet = new InitNumbersService(DataBaseFactory);
            srvGet.List();
            await srvGet.GetDataAsync(rowNum);

            var result = srv.Data.Equals(srvGet.Data) && srvGet.Data.Equals(srvGetById.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task DeleteDataAsync_Test()
        {
            await SaveDataAsync_Test();

            var id = await DataBaseFactory.GetValueAsync<InitNumbers, string>(@"
SELECT TOP 1 ins.InitNumbersUuid 
FROM InitNumbers ins 
");


            var srv = new InitNumbersService(DataBaseFactory);
            srv.Delete();
            await srv.GetDataByIdAsync(id);
            await srv.DeleteDataAsync();

            var result = DataBaseFactory.ExistUniqueId<InitNumbers>(srv.Data.UniqueId);

            Assert.True(!result, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion async methods

    }
}


