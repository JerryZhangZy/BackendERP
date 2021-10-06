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
    public partial class SalesOrderManagerTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Manager Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public SalesOrderManagerTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();

            CreateSalesOrder_Test();
        }
        protected void InitForTest()
        {
            var Seq = 0;
            DataBaseFactory = new DataBaseFactory(Configuration["DBConnectionString"]);
        }
        public void Dispose()
        {
        }


        #region sync methods
        [Fact]
        //[Fact(Skip = SkipReason)]
        public void CreateSalesOrder_Test()
        {
            var uuids = new List<string> {
            "815E5528-5CD8-48E6-9FFD-EDBD0EA58394",
            "8265ED1E-DAC9-4B03-BB08-AB5FF7430405",
            "F82BCD20-BA46-4A55-AF95-248BF6634E56",
            "72EBF750-3C86-4865-BD41-4B1E5CF54FFD",
            "984A5EE4-6002-4955-A754-C27BED59E78E",
            "97fdd43b-3413-42c3-ba5c-284e8a3524c0",
            "8240c857-f491-4895-801a-71c4c9d5fd45",
            "dd999d94-74f5-4f6c-af5c-7bec45e03b58",
            "a792a6ed-87c3-4cfe-9dd4-15174b7290f8"
            };
            SalesOrderManager soManager = new SalesOrderManager(DataBaseFactory);
            bool result = true;
            List<string> salesOrderNums = new List<string>();
            foreach (var uuid in uuids)
            {
                try
                {
                    (result, salesOrderNums) = soManager.CreateSalesOrderByChannelOrderIdAsync(uuid).Result;

                    Assert.True(result);

                }
                catch (Exception ex)
                {
                    Assert.False(false, ex.Message);

                }
            }

        }

        #endregion sync methods

        #region async methods
        #endregion async methods

    }
}
