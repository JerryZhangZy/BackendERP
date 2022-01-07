              
    

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
    /// Represents a Tester for PaidbyMapService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class PaidbyMapDataDtoTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected PaidbyMapData GetFakerData()
        {
            return PaidbyMapDataTests.GetFakerData();
        }

        protected List<PaidbyMapData> GetFakerData(int count)
        {
            return PaidbyMapDataTests.GetFakerData(count);
        }

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public PaidbyMapDataDtoTests(TestFixture<StartupTest> fixture) 
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
            var mapper = new PaidbyMapDataDtoMapperDefault();

            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            var data2 = new PaidbyMapData();
            mapper.ReadDto(data2, dto);


            var result = data.Equals(data2);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}
        
        [Fact()]
        public void ExportCsv_Test()
        {
            var mapper = new PaidbyMapDataDtoMapperDefault();

            var data = GetFakerData(100);
            var dtolist = new List<PaidbyMapDataDto>();
            data.ForEach(x =>
            {
                dtolist.Add(mapper.WriteDto(x, null));
            });
            //var dto = mapper.WriteDto(data, null);
            var fileName = "c:\\temp\\_PaidbyMapDataDto.csv";
            var csvHelper = new PaidbyMapDataDtoCsv();

            try
            {
                csvHelper.Export(dtolist, fileName);
            }
            catch (Exception e)
            {
                throw;
            }

            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void ImportCsv_Test()
        {
            var fileName = "c:\\temp\\_PaidbyMapDataDto.csv";
            var csvHelper = new PaidbyMapDataDtoCsv();
            IList<PaidbyMapDataDto> data;

            try
            {
                data = csvHelper.Import(fileName).ToList();
            }
            catch (Exception e)
            {
                throw;
            }

            Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
        }
    }
}


