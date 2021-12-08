    
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
    /// Represents a InvoiceTransactionIOManager Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public partial class InvoiceTransactionIOManagerTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Manager Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public InvoiceTransactionIOManagerTests(TestFixture<StartupTest> fixture)
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
            var fileName = "c:\\temp\\Dto_2.csv";
            var service = new InvoiceTransactionIOManager(DataBaseFactory);
            IList<InvoiceTransactionDataDto> data;

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
            var fileName = "c:\\temp\\Dto_3.csv";
            var service = new InvoiceTransactionIOManager(DataBaseFactory);
            IList<InvoiceTransactionDataDto> data;

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
            var service = new InvoiceTransactionIOManager(DataBaseFactory);
            IList<InvoiceTransactionDataDto> dtos = new List<InvoiceTransactionDataDto>();
            var InvoiceTransactionService = new InvoiceTransactionService(DataBaseFactory);
            if (InvoiceTransactionService.List("179280c8-1795-466a-a1f9-93d17628ed42"))
                dtos.Add(InvoiceTransactionService.ToDto());
            if (InvoiceTransactionService.List("d20324f6-72b6-4d96-905d-3a280ba8b44b"))
                dtos.Add(InvoiceTransactionService.ToDto());
            if (InvoiceTransactionService.List("a9997f83-0d3e-441c-b122-b9a002aa4670"))
                dtos.Add(InvoiceTransactionService.ToDto());


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
            var fileName = "c:\\temp\\Dto_3.csv";
            var service = new InvoiceTransactionIOManager(DataBaseFactory);
            IList<InvoiceTransactionDataDto> dtos = new List<InvoiceTransactionDataDto>();
            var InvoiceTransactionService = new InvoiceTransactionService(DataBaseFactory);
            if (InvoiceTransactionService.List("9027ebbd-a022-434e-965d-adb20cc27dac"))
                dtos.Add(InvoiceTransactionService.ToDto());
            if (InvoiceTransactionService.List("117227d4-43cc-476c-9257-29695d08fdd1"))
                dtos.Add(InvoiceTransactionService.ToDto());
            if (InvoiceTransactionService.List("22da3dbb-6d02-4852-a2b8-04c380b36dd6"))
                dtos.Add(InvoiceTransactionService.ToDto());

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

    }
}

