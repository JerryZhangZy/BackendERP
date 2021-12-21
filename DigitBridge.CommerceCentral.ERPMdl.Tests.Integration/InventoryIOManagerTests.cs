    
//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
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
using System.IO;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    /// <summary>
    /// Represents a InventoryIOManager Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public partial class InventoryIOManagerTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Manager Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public InventoryIOManagerTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
            var Seq = 0;
            DataBaseFactory = new DataBaseFactory(Configuration["DBConnectionString"]);
        }
        public void Dispose()
        {
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ImportCsvAsync_Test()
        {
            var fileName = "c:\\temp\\inventory.csv";
            var service = new InventoryIOManager(DataBaseFactory);
            IList<InventoryDataDto> data;

            try
            {
                using (var b = new Benchmark("ImportCsvAsync_Test"))
                {
                    using (var reader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        data = await service.ImportAsync(reader);
                    }
                }

                Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ImportAllColumnsAsync_Test()
        {
            var fileName = "c:\\temp\\inventory.csv";
            var service = new InventoryIOManager(DataBaseFactory);
            IList<InventoryDataDto> data;

            try
            {
                using (var b = new Benchmark("ImportAllColumnsAsync_Test"))
                {
                    using (var reader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        data = await service.ImportAllColumnsAsync(reader);
                    }
                }

                Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ExportAsync_Test()
        {
            var fileName = "c:\\temp\\Dto_2.csv";
            var service = new InventoryIOManager(DataBaseFactory);
            IList<InventoryDataDto> dtos = new List<InventoryDataDto>();
            var InventoryService = new InventoryService(DataBaseFactory);
            if (InventoryService.List("9027ebbd-a022-434e-965d-adb20cc27dac"))
                dtos.Add(InventoryService.ToDto());
            if (InventoryService.List("117227d4-43cc-476c-9257-29695d08fdd1"))
                dtos.Add(InventoryService.ToDto());
            if (InventoryService.List("22da3dbb-6d02-4852-a2b8-04c380b36dd6"))
                dtos.Add(InventoryService.ToDto());


            try
            {
                using (var b = new Benchmark("ExportAsync_Test"))
                {
                    var dataArray = await service.ExportAsync(dtos);
                    File.WriteAllBytesAsync(fileName, dataArray);
                }

                Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ExportAllColumnsAsync_Test()
        {
            var fileName = "c:\\temp\\inventory.csv";
            var service = new InventoryIOManager(DataBaseFactory);
            IList<InventoryDataDto> dtos = new List<InventoryDataDto>();
            var InventoryService = new InventoryService(DataBaseFactory);
            if (InventoryService.List("869413f8-b532-50c6-fe64-8405f58522f1"))
                dtos.Add(InventoryService.ToDto());
            //if (InventoryService.List("117227d4-43cc-476c-9257-29695d08fdd1"))
            //    dtos.Add(InventoryService.ToDto());
            //if (InventoryService.List("22da3dbb-6d02-4852-a2b8-04c380b36dd6"))
            //    dtos.Add(InventoryService.ToDto());

            try
            {
                using (var b = new Benchmark("ExportAsync_Test"))
                {
                    var dataArray = await service.ExportAllColumnsAsync(dtos);
                    await File.WriteAllBytesAsync(fileName, dataArray);
                }

                Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception e)
            {
                throw;
            }

        }

    }
}

