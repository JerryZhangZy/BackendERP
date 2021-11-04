


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
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class InvoicePaymentListTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }
        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;
        public InvoicePaymentListTests(TestFixture<StartupTest> fixture)
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
        public async Task GetInvoicePaymentListAsync_Simple_Test()
        {
            var payload = new InvoicePaymentPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                LoadAll = true,
            };
            var listService = new InvoicePaymentList(this.DataBaseFactory);
            await listService.GetInvoicePaymentListAsync(payload);

            //make sure query is correct.
            Assert.True(payload.Success, listService.Messages.ObjectToString());
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetInvoicePaymentListAsync_Full_Test()
        {
            var paymentData = await InvoicePaymentDataTests.GetInvoicePaymentFromDBAsync(this.DataBaseFactory);
            var trans = paymentData.InvoiceTransaction;

            var invoiceData = await InvoiceDataTests.GetInvoiceFromDBAsync(DataBaseFactory, paymentData.InvoiceTransaction.InvoiceUuid);
            var header = invoiceData.InvoiceHeader;
            var headerInfo = invoiceData.InvoiceHeaderInfo;


            var payload = new InvoicePaymentPayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                LoadAll = true,
            };
            var filters = new JObject()
            {
                {"TransUuid",  $"{trans.TransUuid}"},
                {"TransDateFrom",  $"{trans.TransDate }"},
                {"TransDateTo",  $"{trans.TransDate}"},
                {"TransType",  $"{trans.TransType}"},
                {"TransStatus",  $"{trans.TransStatus}"},
                {"PaidBy",  $"{trans.PaidBy}"},

                {"InvoiceUuid",  $"{header.InvoiceUuid}"},
                {"QboDocNumber",  $"{header.QboDocNumber}"},
                {"InvoiceNumberFrom",  $"{header.InvoiceNumber}"},
                {"InvoiceNumberTo",  $"{header.InvoiceNumber}"},
                //{"InvoiceDateFrom",  $"{header.InvoiceDate}"},
                //{"InvoiceDateTo",  $"{header.InvoiceDate}"},
                {"DueDateFrom",  $"{header.DueDate}"},
                {"DueDateTo",  $"{header.DueDate}"},
                {"InvoiceType",   header.InvoiceType},
                {"InvoiceStatus",   header.InvoiceStatus },
                {"CustomerCode",  $"{header.CustomerCode}"},
                {"CustomerName",  $"{header.CustomerName}"},

                {"OrderShipmentNum",  $"{headerInfo.OrderShipmentNum}"},
                {"ShippingCarrier", $"{headerInfo.ShippingCarrier}"},
                {"DistributionCenterNum", $"{headerInfo.DistributionCenterNum}"},
                {"CentralOrderNum",  $"{headerInfo.CentralOrderNum}"},
                {"ChannelNum",  $"{headerInfo.ChannelNum}"},
                {"ChannelAccountNum", $"{headerInfo.ChannelAccountNum}"},
                {"ChannelOrderID", $"{headerInfo.ChannelOrderID}"},
                {"WarehouseCode",  $"{headerInfo.WarehouseCode}"},
                {"RefNum",  $"{headerInfo.RefNum}"},
                {"CustomerPoNum",  $"{headerInfo.CustomerPoNum}"},
                {"ShipToName", $"{headerInfo.ShipToName}"},
                {"ShipToState",  $"{headerInfo.ShipToState}"},
                {"ShipToPostalCode",  $"{headerInfo.ShipToPostalCode}"},
            };

            var filterList = filters.Properties();
            foreach (var filter in filterList)
            {
                payload.Filter = new JObject() { filter };
                await TestFilter(payload, trans.RowNum);
            }

            //test all
            payload.Filter = filters;
            await TestFilter(payload, trans.RowNum);
        }

        private async Task TestFilter(InvoicePaymentPayload payload, long expectedRowNum)
        {
            var listService = new InvoicePaymentList(this.DataBaseFactory);
            await listService.GetInvoicePaymentListAsync(payload);

            //make sure query is correct.
            Assert.True(payload.Success, listService.Messages.ObjectToString());

            //make sure InvoiceListCount is matched.
            Assert.True(payload.InvoiceTransactionListCount >= 1, $"TestFilter error. filter data from record by rownum:{expectedRowNum},filter is :{payload.Filter}");

            var queryResult = JArray.Parse(payload.InvoiceTransactionList.ToString());
            var rowNumMatchedCount = queryResult.Count(i => i.Value<long>("rowNum") == expectedRowNum);
            //make sure result data is matched.
            Assert.Equal(1, rowNumMatchedCount);
        }
        #endregion async methods 
    }
}


