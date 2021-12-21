
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
    /// Represents a SalesOrderIOManager Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public partial class SalesOrderIOManagerTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Manager Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public SalesOrderIOManagerTests(TestFixture<StartupTest> fixture)
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
        public async Task ImportCsvAsync_Test()
        {
            var fileName = "c:\\temp\\Dto_2.csv";
            var service = new SalesOrderIOManager(DataBaseFactory);
            IList<SalesOrderDataDto> data;

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
            var fileName = "c:\\temp\\Dto_5.csv";
            var service = new SalesOrderIOManager(DataBaseFactory);
            IList<SalesOrderDataDto> data;

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
            var fileName = "c:\\temp\\Dto_4.csv";
            var service = new SalesOrderIOManager(DataBaseFactory);
            IList<SalesOrderDataDto> dtos = new List<SalesOrderDataDto>();
            AddSalesOrderDto(dtos, "9027ebbd-a022-434e-965d-adb20cc27dac");
            AddSalesOrderDto(dtos, "117227d4-43cc-476c-9257-29695d08fdd1");
            AddSalesOrderDto(dtos, "22da3dbb-6d02-4852-a2b8-04c380b36dd6");

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
            var fileName = "c:\\temp\\Dto_5.csv";
            var service = new SalesOrderIOManager(DataBaseFactory);
            IList<SalesOrderDataDto> dtos = new List<SalesOrderDataDto>();

            AddSalesOrderDto(dtos, "9027ebbd-a022-434e-965d-adb20cc27dac");
            AddSalesOrderDto(dtos, "117227d4-43cc-476c-9257-29695d08fdd1");
            AddSalesOrderDto(dtos, "22da3dbb-6d02-4852-a2b8-04c380b36dd6");

            try
            {
                using (var b = new Benchmark("ExportAsync_Test"))
                {
                    var dataArray = await service.ExportAllColumnsAsync(dtos);
                    File.WriteAllBytesAsync(fileName, dataArray);
                }

                Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception e)
            {
                throw;
            }

        }

        protected void AddSalesOrderDto(IList<SalesOrderDataDto> dtos, string salesOrderUuid)
        {
            var SalesOrderService = new SalesOrderService(DataBaseFactory);
            if (SalesOrderService.List(salesOrderUuid))
            {
                SalesOrderService.Data.SalesOrderHeader.OrderNumber = "Import-" + Guid.NewGuid().ToString("N");
                SalesOrderService.Data.SalesOrderHeader.SalesOrderUuid = Guid.NewGuid().ToString();
                dtos.Add(SalesOrderService.ToDto());
            }

        }
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ImportAsync_Test()
        {

            var service = new SalesOrderIOManager(DataBaseFactory);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = 10002,
                ProfileNum = 10003,
                ImportUuid = "salesorder-a1d5ec0e-01e4-4f99-a1b0-636b2cef29ba",
            };
            var success = await service.ImportAsync(payload);
            Assert.True(success, service.Messages.ObjectToString());
        }
    }
}

