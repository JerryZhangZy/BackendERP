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
            //"9E590AE7-F15E-42CD-A575-B101C39AC536",
            //"C267FD30-2B58-4A32-B856-10B71300F928",
            //"19E20861-0BFA-4235-8DA9-1B5F1FF055E6",
            //"9a119d2f-ba69-4d75-97d8-2ad4ef1af436",
            //"5834b632-a23c-4518-91ed-e13c96ff152a",
            //"9c521408-bb76-4be5-a02d-946225857eff",
            //"C5FBE495-3D3D-4069-A841-4DD7D5932C68"
            "0bebe247-2afb-4b47-9511-5287fff7405d"
            };
            SalesOrderManager soManager = new SalesOrderManager(DataBaseFactory);
            bool result = true;
            foreach (var uuid in uuids)
            {
                try
                {
                    result = soManager.CreateSalesOrderByChannelOrderIdAsync(uuid).Result;

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
