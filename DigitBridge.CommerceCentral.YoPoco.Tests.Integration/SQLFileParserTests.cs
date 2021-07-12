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
                "C:\\DigitBridge.CommerceCentral.ERP\\centralerp\\DigitBridge.CommerceCentral.ERPDatabase\\inventory";
            var files = "ProductBasic,ProductExt,ProductExtAttributes,Inventory,InventoryAttributes";
            var processName = "Inventory";
            var structureName = $"{processName}Data";
            var structure = new StructureInfo(structureName,
                    new StructureTable() { Name = "ProductBasic", MainTable = true, OneToOne = true },
                    new StructureTable() { Name = "ProductExt", ParentName = "ProductBasic", OneToOne = true, LoadByColumn = "ProductUuid" },
                    new StructureTable() { Name = "ProductExtAttributes", ParentName = "ProductBasic", OneToOne = true, LoadByColumn = "ProductUuid" },
                    new StructureTable() { Name = "Inventory", ParentName = "ProductBasic", DetailTable = true, OneToOne = false, LoadByColumn = "ProductUuid", oneToOneChildrenName = "InventoryAttributes" },
                    new StructureTable() { Name = "InventoryAttributes", ParentName = "Inventory", OneToOne = false, LoadByColumn = "ProductUuid" }
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
