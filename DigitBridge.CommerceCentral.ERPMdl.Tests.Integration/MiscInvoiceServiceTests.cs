

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
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class MiscInvoiceServiceTests
    {

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddDto_Test()
        {
            var srv = new MiscInvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);


            var result = srv.Add(dto);

            //var id = dto.MiscInvoiceHeader.MiscInvoiceUuid;

            //var srvGet = new MiscInvoiceService(DataBaseFactory);
            ////srvGet.Edit();
            //srvGet.GetDataById(id);
            //var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void UpdateDto_Test()
        {
            SaveData_Test();

            //            var id = DataBaseFactory.GetValue<MiscInvoiceHeader, string>(@"
            //SELECT TOP 1 ins.MiscInvoiceUuid 
            //FROM MiscInvoiceHeader ins 
            //INNER JOIN (
            //    SELECT it.MiscInvoiceUuid, COUNT(1) AS cnt FROM MiscInvoiceTransaction it GROUP BY it.MiscInvoiceUuid
            //) itm ON (itm.MiscInvoiceUuid = ins.MiscInvoiceUuid)
            //WHERE itm.cnt > 0
            //");
            var id = DataBaseFactory.GetValue<MiscInvoiceHeader, string>(@"
SELECT TOP 1 ins.MiscInvoiceUuid 
FROM MiscInvoiceHeader ins 
");


            var srv = new MiscInvoiceService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.MiscInvoiceHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.MiscInvoiceHeader.RowNum = rowNum;
            dto.MiscInvoiceHeader.MiscInvoiceUuid = id;

            srv.Clear();
            var result = srv.Update(dto);

            //var srvGet = new MiscInvoiceService(DataBaseFactory);
            //srvGet.Edit();
            //srvGet.GetDataById(id);
            //var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddDtoAsync_Test()
        {
            var srv = new MiscInvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);

            var result = await srv.AddAsync(dto);

            //var id = dto.MiscInvoiceHeader.MiscInvoiceUuid;

            //var srvGet = new MiscInvoiceService(DataBaseFactory);
            //srvGet.Edit();
            //await srvGet.GetDataByIdAsync(id);
            //srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdateDtoAsync_Test()
        {
            await SaveDataAsync_Test();

            //            var id = await DataBaseFactory.GetValueAsync<MiscInvoiceHeader, string>(@"
            //SELECT TOP 1 ins.MiscInvoiceUuid 
            //FROM MiscInvoiceHeader ins 
            //INNER JOIN (
            //    SELECT it.MiscInvoiceUuid, COUNT(1) AS cnt FROM MiscInvoiceTransaction it GROUP BY it.MiscInvoiceUuid
            //) itm ON (itm.MiscInvoiceUuid = ins.MiscInvoiceUuid)
            //WHERE itm.cnt > 0
            //");
            var id = await DataBaseFactory.GetValueAsync<MiscInvoiceHeader, string>(@"
SELECT TOP 1 ins.MiscInvoiceUuid 
FROM MiscInvoiceHeader ins 
");


            var srv = new MiscInvoiceService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.MiscInvoiceHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.MiscInvoiceHeader.RowNum = rowNum;
            dto.MiscInvoiceHeader.MiscInvoiceUuid = id;

            srv.Clear();
            var result = await srv.UpdateAsync(dto);

            //var srvGet = new MiscInvoiceService(DataBaseFactory);
            ////srvGet.Edit();
            //await srvGet.GetDataByIdAsync(id);
            //srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddPayload_Test()
        {
            var srv = new MiscInvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);


            var payload = new MiscInvoicePayload();
            payload.MiscInvoice = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            var result = srv.Add(payload);
            //var id = dto.MiscInvoiceHeader.MiscInvoiceUuid;
            //var srvGet = new MiscInvoiceService(DataBaseFactory);
            ////srvGet.Edit();
            //srvGet.GetDataById(id);
            //srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void UpdatePayload_Test()
        {
            SaveData_Test();

            //            var id = await DataBaseFactory.GetValueAsync<MiscInvoiceHeader, string>(@"
            //SELECT TOP 1 ins.MiscInvoiceUuid 
            //FROM MiscInvoiceHeader ins 
            //INNER JOIN (
            //    SELECT it.MiscInvoiceUuid, COUNT(1) AS cnt FROM MiscInvoiceTransaction it GROUP BY it.MiscInvoiceUuid
            //) itm ON (itm.MiscInvoiceUuid = ins.MiscInvoiceUuid)
            //WHERE itm.cnt > 0
            //");

            var id = DataBaseFactory.GetValue<MiscInvoiceHeader, string>(@"
            SELECT TOP 1 ins.MiscInvoiceUuid 
            FROM MiscInvoiceHeader ins 
            ");


            var srv = new MiscInvoiceService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.MiscInvoiceHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.MiscInvoiceHeader.RowNum = rowNum;
            dto.MiscInvoiceHeader.MiscInvoiceUuid = id;

            var payload = new MiscInvoicePayload();
            payload.MiscInvoice = dto;
            payload.MasterAccountNum = srv.Data.MiscInvoiceHeader.MasterAccountNum;
            payload.ProfileNum = srv.Data.MiscInvoiceHeader.ProfileNum;
            payload.DatabaseNum = srv.Data.MiscInvoiceHeader.DatabaseNum;

            srv.Clear();
            var result = srv.Update(payload);

            //var srvGet = new MiscInvoiceService(DataBaseFactory);
            //srvGet.Edit();
            //srvGet.GetDataById(id);
            //srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddPayloadAsync_Test()
        {
            var srv = new MiscInvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);

            var payload = new MiscInvoicePayload();
            payload.MiscInvoice = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            var result = await srv.AddAsync(payload);

            //var id = dto.MiscInvoiceHeader.MiscInvoiceUuid;

            //var srvGet = new MiscInvoiceService(DataBaseFactory);
            //srvGet.Edit();
            //await srvGet.GetDataByIdAsync(id);
            //srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdatePayloadAsync_Test()
        {
            await SaveDataAsync_Test();

            //            var id = await DataBaseFactory.GetValueAsync<MiscInvoiceHeader, string>(@"
            //SELECT TOP 1 ins.MiscInvoiceUuid 
            //FROM MiscInvoiceHeader ins 
            //INNER JOIN (
            //    SELECT it.MiscInvoiceUuid, COUNT(1) AS cnt FROM MiscInvoiceTransaction it GROUP BY it.MiscInvoiceUuid
            //) itm ON (itm.MiscInvoiceUuid = ins.MiscInvoiceUuid)
            //WHERE itm.cnt > 0
            //");

            var id = await DataBaseFactory.GetValueAsync<MiscInvoiceHeader, string>(@"
            SELECT TOP 1 ins.MiscInvoiceUuid 
            FROM MiscInvoiceHeader ins 
            ");
            var srv = new MiscInvoiceService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.MiscInvoiceHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.MiscInvoiceHeader.RowNum = rowNum;
            dto.MiscInvoiceHeader.MiscInvoiceUuid = id;

            var payload = new MiscInvoicePayload();
            payload.MiscInvoice = dto;
            payload.MasterAccountNum = srv.Data.MiscInvoiceHeader.MasterAccountNum;
            payload.ProfileNum = srv.Data.MiscInvoiceHeader.ProfileNum;
            payload.DatabaseNum = srv.Data.MiscInvoiceHeader.DatabaseNum;

            srv.Clear();
            var result = await srv.UpdateAsync(payload);

            //var srvGet = new MiscInvoiceService(DataBaseFactory);
            ////srvGet.Edit();
            //await srvGet.GetDataByIdAsync(id);
            //srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddFromSalesOrderAsync_Test()
        {
            //prepare salesorder data
            var salesOrderData = SalesOrderDataTests.GetFakerData();

            //Add misc invoice from sales order.
            var miscInvoiceService = new MiscInvoiceService(DataBaseFactory);
            var success = await miscInvoiceService.AddFromSalesOrderAsync(salesOrderData.SalesOrderHeader);
            Assert.True(success, "creat miscinvoice by AddFromSalesOrderAsync:" + miscInvoiceService.Messages.ObjectToString());

            //get misc invoice from db
            var miscInvoiceService_Get = new MiscInvoiceService(DataBaseFactory);
            success = await miscInvoiceService_Get.GetDataByIdAsync(miscInvoiceService.Data.UniqueId);
            Assert.True(success, "Get salesorder after create misinvoice error:" + miscInvoiceService_Get.Messages.ObjectToString());

        }

        //[Fact()]
        ////[Fact(Skip = SkipReason)]
        //public async Task WithdrawAsync_Test()
        //{
        //    var misInvoiceData = MiscInvoiceDataTests.SaveFakerMiscInvoice(DataBaseFactory);

        //    var service = new MiscInvoiceService(DataBaseFactory);
        //    var banlance = new Random().Next(1, 100);
        //    var success = await service.WithdrawAsync(misInvoiceData.UniqueId, banlance);
        //    Assert.True(success, service.Messages.ObjectToString());


        //    var service_Get = new MiscInvoiceService(DataBaseFactory);
        //    success = await service_Get.GetDataByIdAsync(misInvoiceData.UniqueId);
        //    Assert.True(success, service_Get.Messages.ObjectToString());

        //    //Assert.Equal(banlance, service_Get.Data.MiscInvoiceHeader.Balance);
        //}

    }
}



