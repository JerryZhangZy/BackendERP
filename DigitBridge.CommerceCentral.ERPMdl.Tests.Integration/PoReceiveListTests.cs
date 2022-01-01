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
 
    public partial class PoReceiveListTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }
        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;
        public PoReceiveListTests(TestFixture<StartupTest> fixture)
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
            var poReceiveData = await PoReceiveListDataTests.SaveFakerPoReceive(this.DataBaseFactory);
         
            
            var payload = new PoTransactionPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum
            };
            payload.LoadAll = true;
            payload.Filter = new JObject()
            {
                {"TransUuid",  $"{poReceiveData.PoTransaction.TransUuid}"},
                {"TransNum",  $"{poReceiveData.PoTransaction.TransNum }"},
                {"PoUuid",  $"{poReceiveData.PoTransaction.PoUuid }"},
                {"PoNum",  $"{poReceiveData.PoTransaction.PoNum }"},
                {"TransType",  $"{poReceiveData.PoTransaction.TransType }"},
                {"TransStatus",  $"{poReceiveData.PoTransaction.TransStatus }"},
                {"VendorUuid",  $"{poReceiveData.PoTransaction.VendorUuid }"},

                {"VendorInvoiceNum",  $"{poReceiveData.PoTransaction.VendorInvoiceNum }"},
                {"VendorInvoiceDate",  $"{poReceiveData.PoTransaction.VendorInvoiceDate }"},
                {"DueDate",  $"{poReceiveData.PoTransaction.DueDate }"}
            };

            var listService = new PoReceiveList(this.DataBaseFactory);
            await listService.GetPoReceiveListAsync(payload);

            //make sure query is correct.
            Assert.True(payload.Success, listService.Messages.ObjectToString());

            //make sure result is matched.
            Assert.Equal(1, payload.PoTransactionListCount);

            var rowNum_Actual = JArray.Parse(payload.PoTransactionList.ToString())[0].Value<long>("rowNum");
            //make sure result data is matched.
            Assert.Equal(poReceiveData.PoTransaction.RowNum, rowNum_Actual);
        }
        #endregion async methods 
    }
}
