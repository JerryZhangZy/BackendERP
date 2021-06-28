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
                "C:\\digit\\JerryZhang\\DigitBridgeCommerceCentralSln\\DigitBridge.CommerceCentral.CommerceCentralDatabase\\ar";
            var files = "InvoiceHeader,InvoiceHeaderInfo,InvoiceHeaderAttributes,InvoiceItems,InvoiceItemsAttributes";
            var structure = new StructureInfo("InvoiceData",
                new StructureTable() { Name = "InvoiceHeader", MainTable = true, oneToOneChildrenName = "InvoiceHeaderInfo,InvoiceHeaderAttributes" },
                new StructureTable() { Name = "InvoiceHeaderInfo", ParentName = "InvoiceHeader", OneToOne = true, LoadByColumn = "InvoiceId" },
                new StructureTable() { Name = "InvoiceHeaderAttributes", ParentName = "InvoiceHeader", OneToOne = true, LoadByColumn = "InvoiceId" },
                new StructureTable() { Name = "InvoiceItems", ParentName = "InvoiceHeader", OneToOne = false, LoadByColumn = "InvoiceId", oneToOneChildrenName = "InvoiceItemsAttributes" },
                new StructureTable() { Name = "InvoiceItemsAttributes", ParentName = "InvoiceItems", OneToOne = false, LoadByColumn = "InvoiceId" }
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
