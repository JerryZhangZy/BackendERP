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

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class InvoiceManagerTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Manager Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public InvoiceManagerTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;

            InitForTest();

            CreateInvoice_Test();
        }
        protected void InitForTest()
        {
            var Seq = 0;
            DataBaseFactory = new DataBaseFactory(Configuration["DBConnectionString"]);
        }
        public void Dispose()
        {
        }


        #region sync methods
        [Fact]
        //[Fact(Skip = SkipReason)]
        public void CreateInvoice_Test()
        {
            var uuids = new List<string> {
            "ac1e7f54-e5a2-4a45-80bd-a04a38b105b3",
            "11cb3ad7-80b1-48ba-a7a7-02a1ddb238b7",
            "ffb46bc8-a1f0-4245-9f7f-11f641411e11",
            "0221637b-7c9d-45e4-aec1-c7f1235912b2",
            "d9ec860f-8a9e-4f61-a9a4-9cedbd887961",
            "f13f3fd4-715f-4772-8c39-e68494ec87c3",
            "ed239ada-6526-4fed-99f2-e9a4fe7ad0f3",
            "736bf01f-31a2-49b5-a14c-f6235e88e096",
            "f9973f32-5d61-4e7c-8862-3f98f73da427"
            };
            InvoiceManager invoiceManager = new InvoiceManager(DataBaseFactory);
            bool result = true;
            foreach (var uuid in uuids)
            {
                try
                {
                    string invoiceNumber = "";
                    (result, invoiceNumber) = invoiceManager.CreateInvoiceByOrderShipmentIdAsync(uuid).Result;
                    if (result)
                        Assert.True(result);
                    else
                        Assert.False(result);
                }
                catch (Exception ex)
                {
                    Assert.False(false, ex.Message);

                }
            }

        }

        #endregion sync methods

        #region async methods
        #endregion async methods

    }
}
