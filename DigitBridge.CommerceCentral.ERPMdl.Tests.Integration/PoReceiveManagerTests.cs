


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
    public partial class PoReceiveManagerTests : IDisposable, IClassFixture<TestFixture<StartupTest>>
    {
        protected const string SkipReason = "Debug Helper Function";

        protected TestFixture<StartupTest> Fixture { get; }
        public IConfiguration Configuration { get; }
        public IDataBaseFactory DataBaseFactory { get; set; }
        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;

        public PoReceiveManagerTests(TestFixture<StartupTest> fixture)
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

        #region async methods
        [Fact()]
        public async Task AddPoTransAsync_Test()
        {


            var payload = new PoReceivePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                WMSPoReceiveItems = await GetWMSPoReceiveItems(),
            };

            var service = new PoReceiveManager(DataBaseFactory);
            var success = await service.AddPoTransAsync(payload);
            Assert.True(success, service.Messages.ObjectToString());
        }

        protected async Task<List<WMSPoReceiveItem>> GetWMSPoReceiveItems(List<string> excludeVendorCodes = null)
        {
            string sql;
            if (excludeVendorCodes != null && excludeVendorCodes.Count() > 0)
                sql = $@" 
	select top 1 VendorCode,count(1) as itemCount into #tmp
	from PoHeader poh
	join PoItems item on item.PoUuid=poh.PoUuid
    where VendorCode not in ('{string.Join("',", excludeVendorCodes)}')
    AND poh.MasterAccountNum =@masterAccountNum
    AND poh.ProfileNum =@profileNum	
    group by VendorCode  
	order by itemCount desc ";
            else
            {
                sql = $@" 
	select top 1 VendorCode,count(1) as itemCount into #tmp
	from PoHeader poh
	join PoItems item on item.PoUuid=poh.PoUuid 
    where poh.MasterAccountNum =@masterAccountNum
    AND poh.ProfileNum =@profileNum
	group by VendorCode  
	order by itemCount desc ";
            }

            sql += $@"
select 
 poi.PoItemUuid
,poh.PoUuid
,poi.SKU
,poh.VendorCode
,poi.WarehouseCode 

from PoHeader poh
JOIN PoItems  poi on poh.PoUuid = poi.PoUuid 
join #tmp t on t.VendorCode=poh.VendorCode
where poh.MasterAccountNum =@masterAccountNum
AND poh.ProfileNum =@profileNum ";

            var WMSBatchNum = Guid.NewGuid().ToString();

            using (var tx = new ScopedTransaction(DataBaseFactory))
            {
                var items = await SqlQuery.ExecuteAsync(sql
                   , (string poItemUuid, string poUuid, string sku, string vendorCode, string warehouseCode) => new WMSPoReceiveItem()
                   {
                       PoUuid = poUuid,
                       PoItemUuid = poItemUuid,
                       Qty = new Random().ToInt(),
                       SKU = sku,
                       VendorCode = vendorCode,
                       WarehouseCode = warehouseCode.IsZero() ? "Main" : warehouseCode,
                       WMSBatchNum = WMSBatchNum
                   }
                   , MasterAccountNum.ToSqlParameter("masterAccountNum")
                   , ProfileNum.ToSqlParameter("profileNum")
                   );

                return items;
            }
        }


        [Fact()]
        public async Task AddWMSPoReceiveToQueueAsync_Test()
        {
            var batchNum1 = Guid.NewGuid().ToString();
            var batchNum2 = Guid.NewGuid().ToString();
            var payload = new PoReceivePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,

                WMSPoReceiveItems = new List<WMSPoReceiveItem>()
                {
                 new WMSPoReceiveItem(){WMSBatchNum=batchNum1, PoUuid="de4a8888-9f12-4e8f-8f96-0a1c67c9bdea" ,PoItemUuid="1a8e53ca-4a0c-4317-952e-445584eee520", Qty=1,SKU="sku1" ,WarehouseCode="WarehouseCode1" ,VendorCode="VendorCode1"},
                 new WMSPoReceiveItem(){WMSBatchNum=batchNum1,PoUuid="de4a8888-9f12-4e8f-8f96-0a1c67c9bdea" , PoItemUuid="fc2d34b8-e3f0-443f-89b4-2309f66729ad", Qty=2 ,SKU="sku2" ,WarehouseCode="WarehouseCode2",VendorCode="VendorCode1"},
                 new WMSPoReceiveItem(){WMSBatchNum=batchNum1,PoUuid="de4a8888-9f12-4e8f-8f96-0a1c67c9bdea" , Qty=3 ,SKU="sku3" ,WarehouseCode="WarehouseCode3",VendorCode="VendorCode1"},

                 new WMSPoReceiveItem(){WMSBatchNum=batchNum1,PoUuid="e4c06215-5b0c-41f7-9df9-9f4b04f6e10a" ,PoItemUuid="94043d6f-e6bc-4428-8585-0ef133745c2c", Qty=21,SKU="sku1" ,WarehouseCode="WarehouseCode1",VendorCode="VendorCode1"},
                 new WMSPoReceiveItem(){WMSBatchNum=batchNum2,PoUuid="e4c06215-5b0c-41f7-9df9-9f4b04f6e10a" ,PoItemUuid="94043d6f-e6bc-4428-8585-0ef133745c2c", Qty=21,SKU="sku1" ,WarehouseCode="WarehouseCode1",VendorCode="VendorCode2"},

                }
            };

            var service = new PoReceiveManager(DataBaseFactory, MySingletonAppSetting.AzureWebJobsStorage);
            var result = await service.AddWMSPoReceiveToEventProcessAndQueueAsync(payload);
            var success = result != null && result.Count(i => !i.Success) <= 0;
            Assert.True(success, result.SelectMany(i => i.Messages).ObjectToString());
        }

        #endregion async methods

    }
}


