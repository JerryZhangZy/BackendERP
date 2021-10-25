
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
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;

namespace DigitBridge.QuickBooks.Integration.Tests
{
    public partial class QboPaymentServiceTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const int MasterAccountNum = 10001;
        protected const int ProfileNum = 10001;

        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }

        public QboPaymentServiceTests(TestFixture<StartupTest> fixture)
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
        protected async Task<(string, int)> GetErpInvoiceNumberAndTranNumAsync()
        {
            var paymentData = await InvoicePaymentDataTests.SaveFakerInvoicePayment(DataBaseFactory);
            return (paymentData.InvoiceTransaction.InvoiceNumber, paymentData.InvoiceTransaction.TransNum);
        }

        protected async Task<(string, string)> GetErpInvoiceUuidAndTransUuidAsync()
        {
            var paymentData = await InvoicePaymentDataTests.SaveFakerInvoicePayment(DataBaseFactory);

            var trans = paymentData.InvoiceTransaction;

            return (trans.InvoiceUuid, trans.TransUuid);
        }
    }
}


