﻿using System;
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
            "6fd3afc5-954a-4500-8e1b-8605d1dd5365",
            "D7FB3599-D264-4055-A586-D0CFD11D55E1",
            "4E807A78-4331-41C4-BEF7-6E8A26163A04",
            "C5FBE495-3D3D-4069-A841-4DD7D5932C68"
            };
            SalesOrderManager soManager = new SalesOrderManager(DataBaseFactory, "Tester");
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
