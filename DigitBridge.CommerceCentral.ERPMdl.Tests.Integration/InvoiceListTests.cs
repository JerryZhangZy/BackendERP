


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

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class InvoiceListTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }
        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;
        public InvoiceListTests(TestFixture<StartupTest> fixture)
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
        //public void GetInvoiceList_Test()
        //{
        //    var invoice = await SaveInvoice();
        //    var header = invoice.InvoiceHeader;
        //    var headerInfo = invoice.InvoiceHeaderInfo;

        //    var payload = new InvoicePayload()
        //    {
        //        MasterAccountNum = MasterAccountNum,
        //        ProfileNum = ProfileNum
        //    };
        //    payload.LoadAll = true;
        //    payload.Filter = new JObject()
        //    {
        //        {"InvoiceUuid",  $"{header.InvoiceUuid}"},
        //        {"QboDocNumber",  $"{header.QboDocNumber}"},
        //        {"InvoiceNumberFrom",  $"{header.InvoiceNumber}"},
        //        {"InvoiceNumberTo",  $"{header.InvoiceNumber}"},
        //        {"InvoiceDateFrom",  $"{header.InvoiceDate}"},
        //        {"InvoiceDateTo",  $"{header.InvoiceDate}"},
        //        {"DueDateFrom",  $"{header.DueDate}"},
        //        {"DueDateTo",  $"{header.DueDate}"},
        //        {"InvoiceType",   header.InvoiceType},
        //        {"InvoiceStatus",   header.InvoiceStatus },
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

        //    var listService = new InvoiceList(this.DataBaseFactory);
        //    listService.GetInvoiceList(payload);

        //    //make sure query is correct.
        //    Assert.True(payload.Success, listService.Messages.ObjectToString());

        //    //make sure result is matched.
        //    Assert.Equal(1, payload.InvoiceListCount);

        //}
        #endregion sync methods

        #region async methods
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetInvoiceListAsync_Test()
        {
            var invoice = await SaveInvoice();
            var header = invoice.InvoiceHeader;
            var headerInfo = invoice.InvoiceHeaderInfo;

            var payload = new InvoicePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum
            };
            payload.LoadAll = true;
            payload.Filter = new JObject()
            {
                {"InvoiceUuid",  $"{header.InvoiceUuid}"},
                {"QboDocNumber",  $"{header.QboDocNumber}"},
                {"InvoiceNumberFrom",  $"{header.InvoiceNumber}"},
                {"InvoiceNumberTo",  $"{header.InvoiceNumber}"},
                {"InvoiceDateFrom",  $"{header.InvoiceDate}"},
                {"InvoiceDateTo",  $"{header.InvoiceDate}"},
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

            var listService = new InvoiceList(this.DataBaseFactory);
            await listService.GetInvoiceListAsync(payload);

            //make sure query is correct.
            Assert.True(payload.Success, listService.Messages.ObjectToString());

            //make sure result is matched.
            Assert.Equal(1, payload.InvoiceListCount);

        }
        #endregion async methods


        #region invoice data prepare  

        protected async Task<InvoiceData> SaveInvoice()
        {
            var srv = new InvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = await InvoiceDataTests.GetFakerInvoiceDataAsync(DataBaseFactory);
            var dto = mapper.WriteDto(data, null);
            var success = srv.Add(dto);

            Assert.True(success, srv.Messages.ObjectToString());

            return srv.Data;
        }
        #endregion
    }
}


