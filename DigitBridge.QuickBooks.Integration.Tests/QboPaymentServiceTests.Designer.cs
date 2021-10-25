
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
        protected InvoiceTransactionData GetFakerPaymentData(string invoiceNumber)
        {
            var data = InvoiceTransactionDataTests.GetFakerData();
            data.InvoiceTransaction.MasterAccountNum = MasterAccountNum;
            data.InvoiceTransaction.ProfileNum = ProfileNum;
            data.InvoiceTransaction.InvoiceNumber = invoiceNumber;
            return data;
        }
        protected InvoiceData GetFakerInvoiceData()
        {
            var data = InvoiceDataTests.GetFakerData();
            data.InvoiceHeader.MasterAccountNum = MasterAccountNum;
            data.InvoiceHeader.ProfileNum = ProfileNum;

            foreach (var item in data.InvoiceItems)
            {
                item.DiscountAmount = 0;
            }
            data.InvoiceHeader.InvoiceNumber = NumberGenerate.Generate();
            return data;
        }
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

        private string GetErpInvoiceNumber()
        {
            var srv = new InvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerInvoiceData();
            var dto = mapper.WriteDto(data, null);
            srv.Add(dto);
            return srv.Data.InvoiceHeader.InvoiceNumber;
        }


        protected (string, int) GetErpInvoiceNumberAndTranNum()
        {
            var invoiceNumber = GetErpInvoiceNumber();

            var srv = new InvoicePaymentService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerPaymentData(invoiceNumber);
            var dto = mapper.WriteDto(data, null);
            srv.Add(dto);
            return (srv.Data.InvoiceTransaction.InvoiceNumber, srv.Data.InvoiceTransaction.TransNum);
        }

        protected (string, string) GetErpInvoiceUuidAndTransUuid()
        {
            var invoiceNumber = GetErpInvoiceNumber();

            var srv = new InvoicePaymentService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerPaymentData(invoiceNumber);
            var dto = mapper.WriteDto(data, null);
            srv.Add(dto);

            var trans = srv.Data.InvoiceTransaction;

            return (trans.InvoiceUuid, trans.TransUuid);
        }
    }
}

