
              
    

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
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPDb.Tests.Integration
{
    /// <summary>
    /// Represents a Tester for OrderShipmentService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class OrderShipmentDataDtoTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected OrderShipmentData GetFakerData()
        {
            return OrderShipmentDataTests.GetFakerData();
        }

        protected List<OrderShipmentData> GetFakerData(int count)
        {
            return OrderShipmentDataTests.GetFakerData(count);
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public OrderShipmentDataDtoTests(TestFixture<StartupTest> fixture) 
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

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void ReadAndWrite_Test()
		{
            var mapper = new OrderShipmentDataDtoMapperDefault();

            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            var data2 = new OrderShipmentData();
            mapper.ReadDto(data2, dto);


            var result = data.Equals(data2);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

    }
}


