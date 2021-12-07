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
            DataBaseFactory = new DataBaseFactory(Configuration["DBConnectionString"]);
        }
        public void Dispose()
        {
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ImportCsvAsync_Test()
        {
            var fileName = "c:\\temp\\salesOrderDto_2.csv";
            var service = new SalesOrderIOManager();
            IList<SalesOrderDataDto> data;

            try
            {
                data = await service.ImportAsync(fileName);

                Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ExportCsvAsync_Test()
        {
            var fileName = "c:\\temp\\salesOrderDto_2.csv";
            var service = new SalesOrderIOManager();
            IList<SalesOrderDataDto> dtos = new List<SalesOrderDataDto>();
            var soService = new SalesOrderService(DataBaseFactory);
            if (soService.List("9027ebbd-a022-434e-965d-adb20cc27dac"))
                dtos.Add(soService.ToDto());
            if (soService.List("117227d4-43cc-476c-9257-29695d08fdd1"))
                dtos.Add(soService.ToDto());
            if (soService.List("22da3dbb-6d02-4852-a2b8-04c380b36dd6"))
                dtos.Add(soService.ToDto());


            try
            {
                var dataArray = await service.ExportAsync(dtos);
                File.WriteAllBytesAsync(fileName, dataArray);

                Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception e)
            {
                throw;
            }

        }


    }
}
