
    

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
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.CommerceCentral.AzureStorage;
using Azure.Data.Tables;
using DigitBridge.QuickBooks.Integration.Model;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class AzureTableUniversalTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory dataBaseFactory { get; set; }

        private string connString = "UseDevelopmentStorage=true";

        public AzureTableUniversalTests(TestFixture<StartupTest> fixture) 
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

        //[Fact()]
        //public async Task Add_Test()
        //{
        //    var log = new OAuthMapInfo()
        //    {
        //        ProfileNum = 10001,
        //        MasterAccountNum = 10001,
        //        RequestState = Guid.NewGuid().ToString()
        //    };
        //    var tableUniversal =await AzureTableUniversal<OAuthMapInfo>.CreateAsync(typeof(OAuthMapInfo).Name, connString);
        //    await tableUniversal.AddOrUpdateAsync(log);

        //    var getentity =await tableUniversal.GetByRowKey(log.RowKey);
        //    Assert.True(getentity != null, "succ");
        //    await tableUniversal.DeleteAsync(getentity); 
        //    getentity = await tableUniversal.GetByRowKey(log.RowKey);
        //    Assert.True(getentity == null, "succ");
        //}

    }
}

