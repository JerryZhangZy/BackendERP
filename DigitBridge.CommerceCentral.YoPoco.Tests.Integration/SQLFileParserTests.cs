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
                "D:\\ERPNEWWork\\DigitBridge.CommerceCentral.ERPDatabase\\ap";
            var files = "ApInvoiceHeader,ApInvoiceHeaderInfo,ApInvoiceHeaderAttributes,ApInvoiceItems";
            var processName = "ApInvoice";
            var structureName = $"{processName}Data";
            var structure = new StructureInfo(structureName,
                  new StructureTable() { Name = "ApInvoiceHeader", AlliesName = "apih", MainTable = true, OneToOne = true },
            new StructureTable() { Name = "ApInvoiceHeaderInfo", AlliesName = "apihi", ParentName = "ApInvoiceHeader", OneToOne = true, LoadByColumn = "ApInvoiceUuid" },
            new StructureTable() { Name = "ApInvoiceHeaderAttributes", AlliesName = "apiha", ParentName = "ApInvoiceHeader", OneToOne = true, LoadByColumn = "ApInvoiceUuid" },
            new StructureTable() { Name = "ApInvoiceItems", AlliesName = "apii", ParentName = "ApInvoiceHeader", DetailTable = true, OneToOne = false, LoadByColumn = "ApInvoiceUuid"} 
             
                );

            var parser = new SQLFileParser(folder, files, structure);
            try
            {
                parser.Parse();
            }
            catch (Exception ex)
            {
                throw;
            }

            Assert.True(true, "This test is a debug helper");
        }

        public void Dispose()
        {
        }
    }
}
