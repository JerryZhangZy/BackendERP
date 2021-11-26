


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
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class IntegrationChannelOrderApiTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        private string _baseUrl = "http://localhost:7073";
        //private string _baseUrl = "https://digitbridge-erp-event-api-dev.azurewebsites.neterpevents";
        private string _code = "drZEGmRUVmGcitmCqyp3VZe5b4H8fSoy8rDUsEMkfG9U7UURXMtnrw==";

        protected const int MasterAccountNum = 10002;
        protected const int ProfileNum = 10003;
        public IntegrationChannelOrderApiTests(TestFixture<StartupTest> fixture)
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

        #region async methods
        [Fact()]
        public async Task ReSendChannelOrderToErpAsync_Test()
        {
            var client = new IntegrationCentralOrderApi(_baseUrl, _code);
            var centralOrderUuid = "c2dc72e4-6e74-49c3-9ab6-eb2a951d5622";
            var payload = new ChannelOrderPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum
            };
            var success = await client.ReSendChannelOrderToErpAsync(payload, centralOrderUuid);
            Assert.True(success, client.Messages.ObjectToString());
        }

        [Fact()]
        public async Task ReSendAllChannelOrderToErp_Test()
        {
            var client = new IntegrationCentralOrderApi(_baseUrl, _code);
            var payload = new ChannelOrderPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                LoadAll = true,
            };
            var success = await client.ReSendAllChannelOrderToErp(payload);
            Assert.True(success, client.Messages.ObjectToString());
        }
        #endregion async methods

    }
}


