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

    public partial class VenderSelectListTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory dataBaseFactory { get; set; }

        public VenderSelectListTests(TestFixture<StartupTest> fixture)
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
        public async Task vender_area_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "vender_area",
                Term = "Amet dolor cum illum",
                Top = 20,
            };
            using (var b = new Benchmark("vender_area_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }
        [Fact()]
        public async Task vender_businessType_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "vender_businessType",
                Term = "hfvtcurqrek674f80ewousuyegmdt0lqhim73uso92ffxjf88m",
                Top = 20,
            };
            using (var b = new Benchmark("vender_businessType_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }
        [Fact()]
        public async Task vender_classCode_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "vender_classCode",
                Term = "aperiam",
                Top = 20,
            };
            using (var b = new Benchmark("vender_classCode_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }






        [Fact()]
        public async Task vender_departmentCode_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "vender_departmentCode",
                Term = "et",
                Top = 20,
            };
            using (var b = new Benchmark("vender_departmentCode_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        public async Task vender_email_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "vender_email",
                Term = "Malachi_Streich24@yahoo.com",
                Top = 20,
            };
            using (var b = new Benchmark("vender_email_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]

        public async Task vender_phone1_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "vender_phone1",
                Term = "(370) 630-6844 x139",
                Top = 20,
            };
            using (var b = new Benchmark("vender_phone1_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");

        }

        [Fact()]
        public async Task vender_vendorCode_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "vender_vendorCode",
                Term = "voluptas",
                Top = 20,
            };
            using (var b = new Benchmark("vender_vendorCode_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");



        }
        [Fact()]
        public async Task vender_vendorName_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "vender_vendorName",
                Term = "Satterfield - Hand",
                Top = 20,
            };
            using (var b = new Benchmark("vender_vendorName_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");



        }

        [Fact()]
        public async Task vender_vendorStatus_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "vender_vendorStatus",
                Term = "79",
                Top = 20,
            };
            using (var b = new Benchmark("vender_vendorStatus_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");
 
        }
        [Fact()]
        public async Task vender_vendorType_Test()
        {
            var payload = new SelectListPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                LoadAll = false,
                Name = "vender_vendorType",
                Term = "11",
                Top = 20,
            };
            using (var b = new Benchmark("vender_vendorType_Test"))
            {
                var factory = new SelectListFactory(dataBaseFactory);
                var result = await factory.GetSelectListAsync(payload);
            }
            Assert.True(payload.Success, "This is a generated tester, please report any tester bug to team leader.");

        }

       

 
        #endregion async methods
    }
}


