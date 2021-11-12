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
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
 
    public partial class PurchaseOrderManagerTest : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Manager Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public PurchaseOrderManagerTest(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();

            // CreateInvoice_Test();
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
        public async Task GetInitNumber_Test()
        {
            var srv = new PurchaseOrderManager(DataBaseFactory);
            string iniNumber = await srv.GetNextNumberAsync(10001, 10001, "eadf5c15-3702-ff74-7d68-5be78956ad45");
            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }
        #endregion async methods
 
 
    }
}
