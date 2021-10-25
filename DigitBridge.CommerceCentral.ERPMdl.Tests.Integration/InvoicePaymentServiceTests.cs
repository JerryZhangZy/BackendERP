


//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
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
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class InvoicePaymentServiceTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }
        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;

        public InvoicePaymentServiceTests(TestFixture<StartupTest> fixture)
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


        #region sync methods

        #endregion sync methods

        #region async methods
        [Fact()]
        public async Task AddPaymentsAsync_Test()
        {
            //get 10 invociedatas saved in db.
            var invoiceDatas = await GenerateInvoiceList();

            var paymentPayload = new InvoicePaymentPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                InvoiceTransaction = GetFakerPaymentDto(),
                ApplyInvoices = await PrepareApplyInvoiceAsync(invoiceDatas, ProcessingMode.Add)
            };

            var paymentService = new InvoicePaymentService(DataBaseFactory);
            var success = await paymentService.AddAsync(paymentPayload);
            Assert.True(success, paymentService.Messages.ObjectToString());
        }

        [Fact()]
        public async Task UpdatePaymentsAsync_Test()
        {
            //get 10 invociedatas saved in db.
            var invoiceDatas = await GenerateInvoiceList();

            var paymentPayload_Add = new InvoicePaymentPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                InvoiceTransaction = GetFakerPaymentDto(),
                ApplyInvoices = await PrepareApplyInvoiceAsync(invoiceDatas, ProcessingMode.Add)
            };

            var paymentService = new InvoicePaymentService(DataBaseFactory);
            var success = await paymentService.AddAsync(paymentPayload_Add);
            Assert.True(success, "Add payments error:" + paymentService.Messages.ObjectToString());


            var paymentPayload_Update = new InvoicePaymentPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                InvoiceTransaction = GetFakerPaymentDto(),
                ApplyInvoices = await PrepareApplyInvoiceAsync(invoiceDatas, ProcessingMode.Edit)
            };

            success = await paymentService.UpdateAsync(paymentPayload_Update);
            Assert.True(success, "Update payments error:" + paymentService.Messages.ObjectToString());
        }

        #endregion async methods

        #region invoice data prepare   

        protected async Task<List<InvoiceData>> GenerateInvoiceList(int count = 10)
        {
            var invoiceDataList = new List<InvoiceData>();
            for (int i = 0; i < count; i++)
            {
                var invoiceData = await InvoiceDataTests.SaveFakerInvoice(DataBaseFactory);
                invoiceDataList.Add(invoiceData);
            }
            return invoiceDataList;
        }
        #endregion

        #region invoice payment data prepare

        protected InvoiceTransactionDataDto GetFakerPaymentDto(string invoiceNumber = null)
        {
            var data = InvoiceTransactionDataTests.GetFakerData();
            //data.InvoiceTransaction.MasterAccountNum = MasterAccountNum;
            //data.InvoiceTransaction.ProfileNum = ProfileNum;
            data.InvoiceTransaction.InvoiceNumber = invoiceNumber;
            var mapper = new InvoiceTransactionDataDtoMapperDefault();
            return mapper.WriteDto(data, null);
        }
        protected async Task<IList<ApplyInvoice>> PrepareApplyInvoiceAsync(IList<InvoiceData> invoiceDatas, ProcessingMode processingMode)
        {
            var applyInvoices = new List<ApplyInvoice>();
            foreach (var invoiceData in invoiceDatas)
            {
                var transUuid = processingMode == ProcessingMode.Edit ? await GetLatestPaymentRowNum(invoiceData.InvoiceHeader.InvoiceNumber) : 0;
                var applyInvoice = new ApplyInvoice()
                {
                    InvoiceNumber = invoiceData.InvoiceHeader.InvoiceNumber,
                    InvoiceUuid = invoiceData.InvoiceHeader.InvoiceUuid,
                    PaidAmount = new Random().Next(1, 100),
                    TransRowNum = transUuid,
                };
                applyInvoices.Add(applyInvoice);
            }
            return applyInvoices;
        }

        protected async Task<long> GetLatestPaymentRowNum(string invoiceNumber)
        {
            var paymentService = new InvoicePaymentService(DataBaseFactory);
            var queryPayload = new InvoicePaymentPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
            };
            await paymentService.GetPaymentWithInvoiceHeaderAsync(queryPayload, invoiceNumber);

            Assert.True(queryPayload.Success, "Get payments by invoiceNumber error:" + paymentService.Messages.ObjectToString());

            Assert.True(queryPayload.InvoiceTransactions.Count > 0, $"no payment trans in db for invoice {invoiceNumber}");

            return queryPayload.InvoiceTransactions.OrderByDescending(i => i.InvoiceTransaction.TransNum).FirstOrDefault().InvoiceTransaction.RowNum.ToLong();
        }
        #endregion
    }
}


