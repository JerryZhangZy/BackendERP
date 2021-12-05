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
            var fileName = "c:\\temp\\salesOrderDto_1.csv";
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


    }
}
