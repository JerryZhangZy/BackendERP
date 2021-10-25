
    

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
using Microsoft.Extensions.Configuration;
using Xunit;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.ERPDb;
using Bogus;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class CompanySummaryInquiryTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public CompanySummaryInquiryTests(TestFixture<StartupTest> fixture)
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


        #region sync methods

        #endregion sync methods

        #region async methods

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetCompaySummaryAsync_Test()
        {
            var srv = new CompanySummaryInquiry(DataBaseFactory);
            var payload = new CompanySummaryPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                Filters = new SummaryInquiryFilter()
                {
                    MasterAccountNum = 10001,
                    ProfileNum = 10001,
                    DateFrom = new DateTime(DateTime.Today.Year, 1, 1),
                    DateTo = DateTime.Today
                }
            };
            await srv.GetCompaySummaryAsync(payload);

            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }

        #endregion async methods

    }
}


