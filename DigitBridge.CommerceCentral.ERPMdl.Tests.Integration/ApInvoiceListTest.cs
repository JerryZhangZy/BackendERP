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
using Newtonsoft.Json;
using DigitBridge.CommerceCentral.ERPMdl.po;
namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
 
    public partial class ApInvoiceListTest : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }
        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;
        public ApInvoiceListTest(TestFixture<StartupTest> fixture)
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
                DataBaseFactory = YoPoco.DataBaseFactory.CreateDefault(Configuration["dsn"].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Dispose()
        {
        }


        #region sync methods

        #endregion sync methods

        #region async methods
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetPoReceiveListAsync_Test()
        {
            //PoReceiveList
            var apInvoiceData = await ApInvoiceListDataTests.SaveFakerPoReceive(this.DataBaseFactory);
            ApInvoiceService apInvoiceService = new ApInvoiceService(this.DataBaseFactory);
          

            var payload = new ApInvoicePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum
            };
            payload.LoadAll = true;
            payload.Filter = new JObject()
            {
                {"ApInvoiceUuid",  $"{apInvoiceData.ApInvoiceHeader.ApInvoiceUuid}"},
                {"ApInvoiceNum",  $"{apInvoiceData.ApInvoiceHeader.ApInvoiceNum }"},
                {"ApInvoiceType",  $"{apInvoiceData.ApInvoiceHeader.ApInvoiceType }"},
                {"ApInvoiceStatus",  $"{apInvoiceData.ApInvoiceHeader.ApInvoiceStatus }"},

                {"ApInvoiceDateFrom",  $"{apInvoiceData.ApInvoiceHeader.ApInvoiceDate}"},
                {"ApInvoiceDateTo",  $"{apInvoiceData.ApInvoiceHeader.ApInvoiceDate}"},
                {"VendorUuid",  $"{apInvoiceData.ApInvoiceHeader.VendorUuid}"},
                {"VendorCode",  $"{apInvoiceData.ApInvoiceHeader.VendorCode}"},
                {"VendorName",  $"{apInvoiceData.ApInvoiceHeader.VendorName}"},
                {"VendorInvoiceNum",  $"{apInvoiceData.ApInvoiceHeader.VendorInvoiceNum}"},
                {"VendorInvoiceDateFrom",  $"{apInvoiceData.ApInvoiceHeader.VendorInvoiceDate}"},
                {"VendorInvoiceDateTo",  $"{apInvoiceData.ApInvoiceHeader.VendorInvoiceDate}"},
                //{"DueDateFrom",  $"{apInvoiceData.ApInvoiceHeader.DueDate}"},
                //{"DueDateTo",  $"{apInvoiceData.ApInvoiceHeader.DueDate}"},
                {"PoUuid",  $"{apInvoiceData.ApInvoiceHeader.PoUuid}"},
                {"PoNum",  $"{apInvoiceData.ApInvoiceHeader.PoNum}"}



            };

            var listService = new ApInvoiceList(this.DataBaseFactory);
            await listService.GetApInvoiceListAsync(payload);

            //make sure query is correct.
            Assert.True(payload.Success, listService.Messages.ObjectToString());

            //make sure result is matched.
            Assert.Equal(1, payload.ApInvoiceListCount);

            var rowNum_Actual = JArray.Parse(payload.ApInvoiceList.ToString())[0].Value<long>("rowNum");
            //make sure result data is matched.
            Assert.Equal(apInvoiceData.ApInvoiceHeader.RowNum, rowNum_Actual);
        }
        #endregion async methods 
    }
}
