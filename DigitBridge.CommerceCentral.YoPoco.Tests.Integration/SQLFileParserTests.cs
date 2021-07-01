using DigitBridge.CommerceCentral.XUnit.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.YoPoco.Tests.Integration
{
    public class SQLFileParserTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }


        public SQLFileParserTests(TestFixture<StartupTest> fixture) 
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();
        }
        protected void InitForTest()
        {
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetTableFiles_Test()
        {
            var folder =
                @"D:\cuijunxian\Project\erp\DigitBridge.CommerceCentral.ERPDatabase\customer";
            var files = "Customer,CustomerAddress,CustomerAttributes";
            var structure = new StructureInfo("CustomerData",
           new StructureTable() { Name = "Customer", MainTable = true, OneToOne = true },
           new StructureTable() { Name = "CustomerAddress", ParentName = "Customer", OneToOne = false, LoadByColumn = "CustomerId" },
           new StructureTable() { Name = "CustomerAttributes", ParentName = "Customer", OneToOne = true, LoadByColumn = "CustomerId" }
       );
            var parser = new SQLFileParser(folder, files, structure);
            parser.Parse(); 
            Assert.True(true, "This test is a debug helper");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
