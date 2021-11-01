
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
    public partial class MiscInvoicePaymentServiceTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }
        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;

        public MiscInvoicePaymentServiceTests(TestFixture<StartupTest> fixture)
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
        public async Task AddMiscPayment_Test()
        {
            var miscInvoiceData = SaveMiscInvoice();
            var invoiceData = SaveInvoice();
            var presalesAmount = new Random().Next();
            var service = new MiscInvoicePaymentService(DataBaseFactory);

            var success = await service.AddMiscPayment(miscInvoiceData.UniqueId, invoiceData.UniqueId, presalesAmount);

            Assert.True(success, "AddMiscPayment error:" + service.Messages.ObjectToString());
        }
        #endregion async methods

        #region data prepare

        protected MiscInvoiceTransactionDataDto GetFakerPaymentDto(string MiscInvoiceNumber = null)
        {
            var data = MiscInvoiceTransactionDataTests.GetFakerData();
            //data.MiscInvoiceTransaction.MasterAccountNum = MasterAccountNum;
            //data.MiscInvoiceTransaction.ProfileNum = ProfileNum;
            data.MiscInvoiceTransaction.MiscInvoiceNumber = MiscInvoiceNumber;
            var mapper = new MiscInvoiceTransactionDataDtoMapperDefault();
            return mapper.WriteDto(data, null);
        }

        protected MiscInvoiceData SaveMiscInvoice()
        {
            var data = MiscInvoiceDataTests.GetFakerData(); 
            var mapper = new MiscInvoiceDataDtoMapperDefault();
            var dto = mapper.WriteDto(data, null);
            var service = new MiscInvoiceService(DataBaseFactory);
            var success = service.Add(dto);
            Assert.True(success, "SaveMiscInvoice error:" + service.Messages.ObjectToString());
            return service.Data;
        }

        protected InvoiceData SaveInvoice()
        {
            var data = InvoiceDataTests.GetFakerData();
            var mapper = new InvoiceDataDtoMapperDefault();
            var dto = mapper.WriteDto(data, null);
            var service = new InvoiceService(DataBaseFactory);
            var success = service.Add(dto);
            Assert.True(success, "SaveInvoice error:" + service.Messages.ObjectToString());
            return service.Data;
        }
        #endregion
    }
}


