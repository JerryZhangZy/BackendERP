
    

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
using DigitBridge.CommerceCentral.ERPDb;
using Bogus;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class CustomerHelperTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public CustomerHelperTests(TestFixture<StartupTest> fixture) 
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

        #endregion sync methods

        #region async methods

        #endregion async methods

        [Fact()]
        public void RandomRepeatRead_Test()
        {
            var idlist = DataBaseFactory.Db.Query<string>("select CustomerUuid from Customer").ToList();
            var dataList = new List<CustomerData>();
            var service = new CustomerService(DataBaseFactory);
            using (var b = new Benchmark("RandomRepeatRead_Test"))
            {
                for (var i = 0; i < 1000; i++)
                {
                    foreach (var uuid in idlist)
                    {
                        if (service.GetDataById(uuid))
                            dataList.Add(service.Data);
                    }
                }
            }
            Assert.True(dataList.Count > 1000, "");
        }

        [Fact()]
        public void RandomRepeatReadWithIdCache_Test()
        {
            var idlist = DataBaseFactory.Db.Query<string>("select CustomerUuid from Customer").ToList();
            var dataList = new List<CustomerData>();
            var service = new CustomerService(DataBaseFactory);
            using (var b = new Benchmark("RandomRepeatReadWithIdCache_Test"))
            {
                for (var i = 0; i < 1000; i++)
                {
                    foreach (var uuid in idlist)
                    {
                        var dt = service.GetCacheById(uuid);
                        if (dt != null)
                            dataList.Add(service.Data);
                    }
                }
            }
            Assert.True(dataList.Count > 1000, "");
        }

        [Fact()]
        public void RandomRepeatReadWithRowNumCache_Test()
        {
            var idlist = DataBaseFactory.Db.Query<long>("select RowNum from Customer").ToList();
            var dataList = new List<CustomerData>();
            var service = new CustomerService(DataBaseFactory);
            using (var b = new Benchmark("RandomRepeatReadWithRowNumCache_Test"))
            {
                for (var i = 0; i < 1000; i++)
                {
                    foreach (var uuid in idlist)
                    {
                        var dt = service.GetCacheByRowNum(uuid);
                        if (dt != null)
                            dataList.Add(service.Data);
                    }
                }
            }
            Assert.True(dataList.Count > 1000, "");
        }

    }
}


