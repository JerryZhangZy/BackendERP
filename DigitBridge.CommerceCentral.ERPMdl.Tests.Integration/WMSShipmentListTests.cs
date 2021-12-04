


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
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using Newtonsoft.Json.Linq;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class WMSShipmentListTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory dataBaseFactory { get; set; }

        public WMSShipmentListTests(TestFixture<StartupTest> fixture)
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
        public async Task GetSalesOrderListAsync_Test()
        {
            var wmsShipmentList = new WMSShipmentList(dataBaseFactory);
            var shipmentIDs = new List<string>() { "113-10000001139", "113-10000001140" };
            var result = await wmsShipmentList.GetWMSShipmentListAsync(10002, 10003, shipmentIDs);
            Assert.True(!result.ToString().IsZero(), "This is a generated tester, please report any tester bug to team leader.");
        }


        #endregion async methods

    }
}


