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
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class InvoicePaymentManagerTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Manager Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public InvoicePaymentManagerTests(TestFixture<StartupTest> fixture)
        {
            Fixture = fixture;
            Configuration = fixture.Configuration;
            InitForTest();
        }
        protected void InitForTest()
        {
            var Seq = 0;
            DataBaseFactory = YoPoco.DataBaseFactory.CreateDefault(Configuration["dsn"].ToString());
        }
        public void Dispose()
        {
        }


        #region sync methods 

        #endregion sync methods

        #region async methods

        [Fact]
        public async Task AddPaymentFromPrepayment_Test()
        {
            var miscInvoiceData = MiscInvoiceDataTests.SaveFakerMiscInvoice(DataBaseFactory);
            var invoiceData = await InvoiceDataTests.SaveFakerInvoiceAsync(DataBaseFactory);
            var prepaymentAmount = new Random().Next(1, 100);
            var srv = new InvoicePaymentManager(DataBaseFactory);
            var success = await srv.AddPaymentFromPrepayment(miscInvoiceData.UniqueId, invoiceData.UniqueId, prepaymentAmount);

            Assert.True(success, srv.Messages.ObjectToString());

            var srv_misInvoice = new MiscInvoiceService(DataBaseFactory);
            success = await srv_misInvoice.GetDataByIdAsync(miscInvoiceData.UniqueId);
            Assert.True(success, srv_misInvoice.Messages.ObjectToString());
            var misInvoiceHeader = srv_misInvoice.Data.MiscInvoiceHeader;
            var payAmount = miscInvoiceData.MiscInvoiceHeader.Balance - misInvoiceHeader.Balance;

            var miscPaymentTrasn = DataBaseFactory.GetBy<MiscInvoiceTransaction>($@"
SELECT *
FROM MiscInvoiceTransaction 
WHERE ProfileNum={misInvoiceHeader.ProfileNum} 
And MasterAccountNum={misInvoiceHeader.MasterAccountNum}
And MiscInvoiceNumber='{misInvoiceHeader.MiscInvoiceNumber}'
And TotalAmount={payAmount}
");
            Assert.True(miscPaymentTrasn != null && miscPaymentTrasn.RowNum > 0, "miscPaymentTrasn is null");


            var invoiceHeader = invoiceData.InvoiceHeader;
            var paymentTrasn = DataBaseFactory.GetBy<InvoiceTransaction>($@"
SELECT *
FROM InvoiceTransaction 
WHERE ProfileNum={invoiceHeader.ProfileNum} 
And MasterAccountNum={invoiceHeader.MasterAccountNum}
And InvoiceNumber='{invoiceHeader.InvoiceNumber}'
And TotalAmount={payAmount}
");
            Assert.True(paymentTrasn != null && paymentTrasn.RowNum > 0, "paymentTrasn is null");
        }

        #endregion async methods

    }
}
