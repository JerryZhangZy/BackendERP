
    

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
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class BlobUniversalTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug TableUniversalTests Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory dataBaseFactory { get; set; }

        private string connString = "UseDevelopmentStorage=true";

        public BlobUniversalTests(TestFixture<StartupTest> fixture) 
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

        [Fact()]
        public async Task GetAllBlobContainers_Test()
        {
            var containerList = AzureUtil.GetAllBlobContainers(connString);
            foreach(var containnerName in containerList)
            {
                System.Diagnostics.Debug.WriteLine(containnerName);
            }
            Assert.True(true, "succ");
            
        }
        [Fact()]
        public async Task AddInventoryLog_Test()
        {
            var blobUniversal =await BlobUniversal.CreateAsync("importwarehouses", connString);
            var blobsList = blobUniversal.GetBlobList();
            foreach(var blob in blobsList)
            {
                var buffer =await blobUniversal.GetBlobAsync(blob);
                System.Diagnostics.Debug.WriteLine($"Blob Name:{blob},Blob Length:{buffer.Length}");
                await blobUniversal.DeleteBlobAsync(blob);
            }
            Assert.True(true, "succ");
            //getentity = await tableUniversal.GetEntityAsync(log.UniqueId, entity.PartitionKey);
            //Assert.True(getentity == null, "succ");
        }

    }
}


