﻿


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
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class PoSelectListTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory dataBaseFactory { get; set; }

        public PoSelectListTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            try
            {
                var Seq = 0;
                dataBaseFactory = DataBaseFactory.CreateDefault(Configuration["dsn"].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Dispose()
        {
        }


        #region async methods

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task po_poNum_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "po_poNum",
                Term = "20",
                Top = 20,
            };
            using (var b = new Benchmark("po_poNum_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }
  

     
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task po_vendorName_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "po_vendorName",
                Term = "Modify",
                Top = 20,
            };
            using (var b = new Benchmark("po_vendorName_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }
  
 
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task po_vendorCode_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "po_vendorCode",
                Term = "5",
                Top = 20,
            };
            using (var b = new Benchmark("po_vendorCode_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }
 

     
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task po_centralOrderNum_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "po_centralOrderNum",
                Term = "cus",
                Top = 20,
            };
            using (var b = new Benchmark("po_centralOrderNum_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }

     

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task po_channelAccountNum_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "po_channelAccountNum",
                Term = "81",
                Top = 20,
            };
            using (var b = new Benchmark("po_channelAccountNum_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task po_channelNum_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "po_channelNum",
                Term = "95",
                Top = 20,
            };
            using (var b = new Benchmark("po_channelNum_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }



        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task poHeaderInfo_channelOrderID_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "po_channelOrderID",
                Term = "52",
                Top = 20,
            };
            using (var b = new Benchmark("po_channelOrderID_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task po_customerPoNum_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "po_customerPoNum",
                Term = "295",
                Top = 20,
            };
            using (var b = new Benchmark("po_customerPoNum_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task po_refNum_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "po_refNum",
                Term = "32",
                Top = 20,
            };
            using (var b = new Benchmark("po_refNum_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }
        #endregion
    }
}



