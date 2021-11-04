


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
    public partial class MiscInvoiceListTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }
        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;
        public MiscInvoiceListTests(TestFixture<StartupTest> fixture)
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
        //[Fact()]
        ////[Fact(Skip = SkipReason)]
        //public void GetMiscInvoiceList_Test()
        //{
        //    var MiscInvoice = await SaveMiscInvoice();
        //    var header = MiscInvoice.MiscInvoiceHeader;
        //    var headerInfo = MiscInvoice.MiscInvoiceHeaderInfo;

        //    var payload = new MiscInvoicePayload()
        //    {
        //        MasterAccountNum = MasterAccountNum,
        //        ProfileNum = ProfileNum
        //    };
        //    payload.LoadAll = true;
        //    payload.Filter = new JObject()
        //    {
        //        {"MiscInvoiceUuid",  $"{header.MiscInvoiceUuid}"},
        //        {"QboDocNumber",  $"{header.QboDocNumber}"},
        //        {"MiscInvoiceNumberFrom",  $"{header.MiscInvoiceNumber}"},
        //        {"MiscInvoiceNumberTo",  $"{header.MiscInvoiceNumber}"},
        //        {"MiscInvoiceDateFrom",  $"{header.MiscInvoiceDate}"},
        //        {"MiscInvoiceDateTo",  $"{header.MiscInvoiceDate}"},
        //        {"DueDateFrom",  $"{header.DueDate}"},
        //        {"DueDateTo",  $"{header.DueDate}"},
        //        {"MiscInvoiceType",   header.MiscInvoiceType},
        //        {"MiscInvoiceStatus",   header.MiscInvoiceStatus },
        //        {"CustomerCode",  $"{header.CustomerCode}"},
        //        {"CustomerName",  $"{header.CustomerName}"},

        //        {"OrderShipmentNum",  $"{headerInfo.OrderShipmentNum}"},
        //        {"ShippingCarrier", $"{headerInfo.ShippingCarrier}"},
        //        {"DistributionCenterNum", $"{headerInfo.DistributionCenterNum}"},
        //        {"CentralOrderNum",  $"{headerInfo.CentralOrderNum}"},
        //        {"ChannelNum",  $"{headerInfo.ChannelNum}"},
        //        {"ChannelAccountNum", $"{headerInfo.ChannelAccountNum}"},
        //        {"ChannelOrderID", $"{headerInfo.ChannelOrderID}"},
        //        {"WarehouseCode",  $"{headerInfo.WarehouseCode}"},
        //        {"RefNum",  $"{headerInfo.RefNum}"},
        //        {"CustomerPoNum",  $"{headerInfo.CustomerPoNum}"},
        //        {"ShipToName", $"{headerInfo.ShipToName}"},
        //        {"ShipToState",  $"{headerInfo.ShipToState}"},
        //        {"ShipToPostalCode",  $"{headerInfo.ShipToPostalCode}"},
        //    };

        //    var listService = new MiscInvoiceList(this.DataBaseFactory);
        //    listService.GetMiscInvoiceList(payload);

        //    //make sure query is correct.
        //    Assert.True(payload.Success, listService.Messages.ObjectToString());

        //    //make sure result is matched.
        //    Assert.Equal(1, payload.MiscInvoiceListCount);

        //}
        #endregion sync methods

        #region async methods
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetMiscInvoiceListAsync_Test()
        {
            var MiscInvoice = MiscInvoiceDataTests.SaveFakerMiscInvoice(this.DataBaseFactory);
            var header = MiscInvoice.MiscInvoiceHeader;

            var payload = new MiscInvoicePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum
            };
            payload.LoadAll = true;
            payload.Filter = new JObject()
            {
                {"MiscInvoiceUuid",  $"{header.MiscInvoiceUuid}"},
                {"QboDocNumber",  $"{header.QboDocNumber}"},
                {"MiscInvoiceNumberFrom",  $"{header.MiscInvoiceNumber}"},
                {"MiscInvoiceNumberTo",  $"{header.MiscInvoiceNumber}"},
                {"MiscInvoiceDateFrom",  $"{header.MiscInvoiceDate}"},
                {"MiscInvoiceDateTo",  $"{header.MiscInvoiceDate}"},
                {"MiscInvoiceType",   header.MiscInvoiceType},
                {"MiscInvoiceStatus",   header.MiscInvoiceStatus },
                {"CustomerCode",  $"{header.CustomerCode}"},
                {"CustomerName",  $"{header.CustomerName}"},
                {"BankAccountCode",  $"{header.BankAccountCode}"},
            };

            var listService = new MiscInvoiceList(this.DataBaseFactory);
            await listService.GetMiscInvoiceListAsync(payload);

            //make sure query is correct.
            Assert.True(payload.Success, listService.Messages.ObjectToString());

            //make sure result is matched.
            Assert.Equal(1, payload.MiscInvoiceListCount);

            var rowNum_Actual = JArray.Parse(payload.MiscInvoiceList.ToString())[0].Value<long>("rowNum");
            //make sure result data is matched.
            Assert.Equal(header.RowNum, rowNum_Actual);
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetMiscInvoiceListAsync_EachFilter_Test()
        {
            var MiscInvoice = MiscInvoiceDataTests.SaveFakerMiscInvoice(this.DataBaseFactory);
            var header = MiscInvoice.MiscInvoiceHeader;

            var payload = new MiscInvoicePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                LoadAll = true,
            };
            var filters = new JObject()
            {
                {"MiscInvoiceUuid",  $"{header.MiscInvoiceUuid}"},
                {"QboDocNumber",  $"{header.QboDocNumber}"},
                {"MiscInvoiceNumberFrom",  $"{header.MiscInvoiceNumber}"},
                {"MiscInvoiceNumberTo",  $"{header.MiscInvoiceNumber}"},
                {"MiscInvoiceDateFrom",  $"{header.MiscInvoiceDate}"},
                {"MiscInvoiceDateTo",  $"{header.MiscInvoiceDate}"},
                {"MiscInvoiceType",   header.MiscInvoiceType},
                {"MiscInvoiceStatus",   header.MiscInvoiceStatus },
                {"CustomerCode",  $"{header.CustomerCode}"},
                {"CustomerName",  $"{header.CustomerName}"},
                {"BankAccountCode",  $"{header.BankAccountCode}"},
            };

            var filterList = filters.Properties();
            foreach (var filter in filterList)
            {
                payload.Filter = new JObject() { filter };
                await TestEachFilter(payload, header.RowNum);
            }

            //test all
            payload.Filter = filters;
            await TestEachFilter(payload, header.RowNum);
        }

        private async Task TestEachFilter(MiscInvoicePayload payload, long expectedRowNum)
        {
            var listService = new MiscInvoiceList(this.DataBaseFactory);
            await listService.GetMiscInvoiceListAsync(payload);

            //make sure query is correct.
            Assert.True(payload.Success, listService.Messages.ObjectToString());

            //make sure MiscInvoiceListCount is matched.
            Assert.True(payload.MiscInvoiceListCount >= 1, "No data found." + payload.Filter.ToString());
            
            var queryResult = JArray.Parse(payload.MiscInvoiceList.ToString());
            var rowNumMatchedCount = queryResult.Count(i => i.Value<long>("rowNum") == expectedRowNum);
            //make sure result data is matched.
            Assert.Equal(1, rowNumMatchedCount);
        }

        #endregion async methods 
    }
}

