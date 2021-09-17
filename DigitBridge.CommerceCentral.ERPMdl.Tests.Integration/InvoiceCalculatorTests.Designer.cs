              
    

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
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    /// <summary>
    /// Represents a Tester for SalesOrderService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class InvoiceCalculatorTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected InvoiceData GetFakerData()
        {
            return InvoiceDataTests.GetFakerData();
        } 
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public InvoiceCalculatorTests(TestFixture<StartupTest> fixture) 
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

        private InvoiceData SaveData(InvoiceData data)
        {
            var success = true;
            var service = new InvoiceService(DataBaseFactory);
            success = success && service.Add();
            service.AttachData(data);
            //srv.Calculate();
            success = success && service.SaveData();
            var rowNum = service.Data.InvoiceHeader.RowNum;
            service.List();
            success = success && service.GetData(rowNum);
            Assert.False(success == false, "Init data failed.");

            var items = service.Data.InvoiceItems;
            success = items != null && items.Count > 0;
            Assert.False(success == false, "InvoiceItems not found.");
            return service.Data;
        }
    }
}


