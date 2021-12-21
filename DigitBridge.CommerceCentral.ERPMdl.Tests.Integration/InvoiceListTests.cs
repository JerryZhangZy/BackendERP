


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
         
        #endregion sync methods

        #region async methods
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetInvoiceListAsync_Simple_Test()
        {
            var payload = new InvoicePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                LoadAll = false,
            };

            var listService = new InvoiceList(this.DataBaseFactory);

            try
            {
                using (var b = new Benchmark("GetSalesOrderDatasAsync_Test"))
                {
                    await listService.GetInvoiceListAsync(payload);
                }

                //make sure query is correct.
                Assert.True(payload.Success, listService.Messages.ObjectToString());
            }
            catch (Exception ex)
            {
                //Cannot open server 'bobotestsql' requested by the login. Client with IP address '174.81.9.150' is not allowed to access the server.
                //To enable access, use the Windows Azure Management Portal or run sp_set_firewall_rule on the master database to create a firewall rule
                //for this IP address or address range.  It may take up to five minutes for this change to take effect.
                throw;
            }

        } 

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task GetInvoiceListAsync_Full_Test()
        {
            var invoice = await InvoiceDataTests.GetInvoiceFromDBAsync(this.DataBaseFactory);
            var header = invoice.InvoiceHeader;
            var headerInfo = invoice.InvoiceHeaderInfo;

            var payload = new InvoicePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                LoadAll = true,
            };
            var filters = new JObject()
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

            var filterList = filters.Properties();
            foreach (var filter in filterList)
            {
                payload.Filter = new JObject() { filter };
                await TestFilter(payload, header.RowNum);
            }

            //test all
            payload.Filter = filters;
            await TestFilter(payload, header.RowNum);
        }

        private async Task TestFilter(InvoicePayload payload, long expectedRowNum)
        {
            var listService = new InvoiceList(this.DataBaseFactory);
            await listService.GetInvoiceListAsync(payload);

            //make sure query is correct.
            Assert.True(payload.Success, listService.Messages.ObjectToString());

            //make sure InvoiceListCount is matched.
            Assert.True(payload.InvoiceListCount >= 1, $"TestFilter error. filter data from record by rownum:{expectedRowNum},filter is :{payload.Filter}");

            var queryResult = JArray.Parse(payload.InvoiceList.ToString());
            var rowNumMatchedCount = queryResult.Count(i => i.Value<long>("rowNum") == expectedRowNum);
            //make sure result data is matched.
            Assert.Equal(1, rowNumMatchedCount);
        }

        #endregion async methods 
    }
}


