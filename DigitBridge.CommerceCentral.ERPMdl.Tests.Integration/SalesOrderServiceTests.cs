


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
using Microsoft.Extensions.Configuration;
using Xunit;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.ERPDb;
using Bogus;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class SalesOrderServiceTests
    {

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddDto_Test()
        {
            var srv = new SalesOrderService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);


            srv.Add(dto);

            var id = dto.SalesOrderHeader.SalesOrderUuid;

            var srvGet = new SalesOrderService(DataBaseFactory);
            //srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void UpdateDto_Test()
        {
            SaveData_Test();

            var id = DataBaseFactory.GetValue<SalesOrderHeader, string>(@"
SELECT TOP 1 ins.SalesOrderUuid 
FROM SalesOrderHeader ins 
INNER JOIN (
    SELECT it.SalesOrderUuid, COUNT(1) AS cnt FROM SalesOrderItems it GROUP BY it.SalesOrderUuid
) itm ON (itm.SalesOrderUuid = ins.SalesOrderUuid)
WHERE itm.cnt > 0
");


            var srv = new SalesOrderService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.SalesOrderHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.SalesOrderHeader.RowNum = rowNum;
            dto.SalesOrderHeader.SalesOrderUuid = id;

            srv.Clear();
            srv.Update(dto);

            var srvGet = new SalesOrderService(DataBaseFactory);
            srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddDtoAsync_Test()
        {
            var srv = new SalesOrderService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);

            await srv.AddAsync(dto);

            var id = dto.SalesOrderHeader.SalesOrderUuid;

            var srvGet = new SalesOrderService(DataBaseFactory);
            srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdateDtoAsync_Test()
        {
            await SaveDataAsync_Test();

            var id = await DataBaseFactory.GetValueAsync<SalesOrderHeader, string>(@"
SELECT TOP 1 ins.SalesOrderUuid 
FROM SalesOrderHeader ins 
INNER JOIN (
    SELECT it.SalesOrderUuid, COUNT(1) AS cnt FROM SalesOrderItems it GROUP BY it.SalesOrderUuid
) itm ON (itm.SalesOrderUuid = ins.SalesOrderUuid)
WHERE itm.cnt > 0
");


            var srv = new SalesOrderService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.SalesOrderHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.SalesOrderHeader.RowNum = rowNum;
            dto.SalesOrderHeader.SalesOrderUuid = id;

            srv.Clear();
            await srv.UpdateAsync(dto);

            var srvGet = new SalesOrderService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddPayload_Test()
        {
            var srv = new SalesOrderService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);


            var payload = new SalesOrderPayload();
            payload.SalesOrder = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            srv.Add(payload);
            var id = dto.SalesOrderHeader.SalesOrderUuid;
            var srvGet = new SalesOrderService(DataBaseFactory);
            //srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void UpdatePayload_Test()
        {
            SaveData_Test();

            var id = DataBaseFactory.GetValue<SalesOrderHeader, string>(@"
SELECT TOP 1 ins.SalesOrderUuid 
FROM SalesOrderHeader ins 
INNER JOIN (
    SELECT it.SalesOrderUuid, COUNT(1) AS cnt FROM SalesOrderItems it GROUP BY it.SalesOrderUuid
) itm ON (itm.SalesOrderUuid = ins.SalesOrderUuid)
WHERE itm.cnt > 0
");


            var srv = new SalesOrderService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.SalesOrderHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.SalesOrderHeader.RowNum = rowNum;
            dto.SalesOrderHeader.SalesOrderUuid = id;

            var payload = new SalesOrderPayload();
            payload.SalesOrder = dto;
            payload.MasterAccountNum = srv.Data.SalesOrderHeader.MasterAccountNum;
            payload.ProfileNum = srv.Data.SalesOrderHeader.ProfileNum;
            payload.DatabaseNum = srv.Data.SalesOrderHeader.DatabaseNum;

            srv.Clear();
            srv.Update(payload);

            var srvGet = new SalesOrderService(DataBaseFactory);
            srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddPayloadAsync_Test()
        {
            var srv = new SalesOrderService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);

            var payload = new SalesOrderPayload();
            payload.SalesOrder = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            await srv.AddAsync(payload);

            var id = dto.SalesOrderHeader.SalesOrderUuid;

            var srvGet = new SalesOrderService(DataBaseFactory);
            srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdatePayloadAsync_Test()
        {
            await SaveDataAsync_Test();

            var id = await DataBaseFactory.GetValueAsync<SalesOrderHeader, string>(@"
SELECT TOP 1 ins.SalesOrderUuid 
FROM SalesOrderHeader ins 
INNER JOIN (
    SELECT it.SalesOrderUuid, COUNT(1) AS cnt FROM SalesOrderItems it GROUP BY it.SalesOrderUuid
) itm ON (itm.SalesOrderUuid = ins.SalesOrderUuid)
WHERE itm.cnt > 0
");


            var srv = new SalesOrderService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.SalesOrderHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.SalesOrderHeader.RowNum = rowNum;
            dto.SalesOrderHeader.SalesOrderUuid = id;

            var payload = new SalesOrderPayload();
            payload.SalesOrder = dto;
            payload.MasterAccountNum = srv.Data.SalesOrderHeader.MasterAccountNum;
            payload.ProfileNum = srv.Data.SalesOrderHeader.ProfileNum;
            payload.DatabaseNum = srv.Data.SalesOrderHeader.DatabaseNum;

            srv.Clear();
            await srv.UpdateAsync(payload);

            var srvGet = new SalesOrderService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddPrepaymentAmountAsync_Test()
        {
            //Save salesorder
            var salesOrderData = GetFakerData();
            salesOrderData.SalesOrderHeader.MiscInvoiceUuid = string.Empty;
            salesOrderData.SalesOrderHeader.DepositAmount = 0;

            var srv_Save = new SalesOrderService(DataBaseFactory);
            srv_Save.Add();
            srv_Save.AttachData(salesOrderData);
            var success = await srv_Save.SaveDataAsync();
            Assert.True(success, "Save salesorder error:" + srv_Save.Messages.ObjectToString());

            //Get salesorder
            var srv_Get = new SalesOrderService(DataBaseFactory);
            success = await srv_Get.GetDataByIdAsync(srv_Save.Data.UniqueId);
            Assert.True(success, "Get salesorder error:" + srv_Get.Messages.ObjectToString());

            var salesOrderHeader_FromDB = srv_Get.Data.SalesOrderHeader;
            //Add Pre sales amount 
            var srv_AddPrepayment = new SalesOrderService(DataBaseFactory);
            var salesOrder_Payload = new SalesOrderPayload()
            {
                MasterAccountNum = salesOrderHeader_FromDB.MasterAccountNum,
                ProfileNum = salesOrderHeader_FromDB.ProfileNum,
            };
            var prepaymentAmount = new Random().Next();
            success = await srv_AddPrepayment.AddPrepaymentAsync(salesOrder_Payload, salesOrderHeader_FromDB.OrderNumber, prepaymentAmount);
            Assert.True(success, "Add Pre sales amount  error:" + srv_AddPrepayment.Messages.ObjectToString());

            var miscInvoiceUuid = srv_AddPrepayment.Data.SalesOrderHeader.MiscInvoiceUuid;
            var misSrv_Get = new MiscInvoiceService(DataBaseFactory);
            success = await misSrv_Get.GetDataByIdAsync(miscInvoiceUuid);
            Assert.True(success, "Get mis invoice  error:" + misSrv_Get.Messages.ObjectToString());
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdateStatusAsync_Test()
        {
            //Save salesorder
            var rowNum = (long)12741;
            var status = SalesOrderStatus.Approved;

            var srv = new SalesOrderService(DataBaseFactory);
            try
            {
                using (var b = new Benchmark("FindNotExistSkuWarehouseAsync_Test"))
                {
                    var success = await srv.UpdateStatusAsync(rowNum, status);
                }

                Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdateWithIgnoreAsync_Test()
        {
            //Save salesorder
            var rowNum = (long)12742;
            var srv = new SalesOrderService(DataBaseFactory);
            try
            {
                using (var b = new Benchmark("FindNotExistSkuWarehouseAsync_Test"))
                {
                    await srv.EditAsync(rowNum);
                    srv.Data.SalesOrderItems[0].ShipPack = 0;
                    srv.Data.SalesOrderItems[0].ShipQty = 0;
                    await srv.SaveDataAsync();
                }

                Assert.True(true, "This is a generated tester, please report any tester bug to team leader.");
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdateShippedQtyAsync_Test()
        {
            //var salesOrderUuid = SalesOrderDataTests.GetSalesOrderUuid(DataBaseFactory);
            var salesOrderUuid = await MakeRealtionForSalesOrderAndSalesOrder();

            var service = new SalesOrderService(DataBaseFactory);
            var success = await service.UpdateShippedQtyAsync(salesOrderUuid);
            Assert.True(success, service.Messages.ObjectToString());

        }

        protected async Task<string> MakeRealtionForSalesOrderAndSalesOrder()
        {
            var service = new OrderShipmentService(DataBaseFactory);
            service.Edit();

            var salesOrderData = await GetSalesOrderData();
            var shipmentData = await GetShipmentData();

            int index = 0;

            foreach (var shipPackage in shipmentData.OrderShipmentPackage)
            {
                foreach (var shippedItem in shipPackage.OrderShipmentShippedItem)
                {
                    if (index >= salesOrderData.SalesOrderItems.Count) continue;
                    shippedItem.SalesOrderItemsUuid = salesOrderData.SalesOrderItems[index].SalesOrderItemsUuid;
                    index++;
                }
            }
            shipmentData.OrderShipmentHeader.SalesOrderUuid = salesOrderData.SalesOrderHeader.SalesOrderUuid;

            service.AttachData(shipmentData);

            var success = await service.SaveDataAsync();
            Assert.True(success, service.Messages.ObjectToString());

            return salesOrderData.SalesOrderHeader.SalesOrderUuid;
        }

        protected async Task<SalesOrderData> GetSalesOrderData()
        {
            var salesOrderUuid = SalesOrderDataTests.GetSalesOrderUuid(DataBaseFactory);
            var service = new SalesOrderService(DataBaseFactory);
            var success = await service.GetDataByIdAsync(salesOrderUuid);
            Assert.True(success, service.Messages.ObjectToString());
            return service.Data;
        }
        protected async Task<OrderShipmentData> GetShipmentData()
        {
            var shipmentUuid = OrderShipmentDataTests.GetOrderShipmentUuid(DataBaseFactory);
            var service = new OrderShipmentService(DataBaseFactory);
            var success = await service.GetDataByIdAsync(shipmentUuid);
            Assert.True(success, service.Messages.ObjectToString());
            return service.Data;
        }
    }
}



