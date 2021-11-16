
    

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

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class InvoiceServiceTests
    {

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void AddDto_Test()
		{
            var srv = new InvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            

            srv.Add(dto);

            var id = dto.InvoiceHeader.InvoiceUuid;

            var srvGet = new InvoiceService(DataBaseFactory);
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

            var id = DataBaseFactory.GetValue<InvoiceHeader, string>(@"
SELECT TOP 1 ins.InvoiceUuid 
FROM InvoiceHeader ins 
INNER JOIN (
    SELECT it.InvoiceUuid, COUNT(1) AS cnt FROM InvoiceItems it GROUP BY it.InvoiceUuid
) itm ON (itm.InvoiceUuid = ins.InvoiceUuid)
WHERE itm.cnt > 0
");


            var srv = new InvoiceService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.InvoiceHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.InvoiceHeader.RowNum = rowNum;
            dto.InvoiceHeader.InvoiceUuid = id;

            srv.Clear();
            srv.Update(dto);

            var srvGet = new InvoiceService(DataBaseFactory);
            srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task AddDtoAsync_Test()
		{
            var srv = new InvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null); 

            await srv.AddAsync(dto);

            var id = dto.InvoiceHeader.InvoiceUuid;

            var srvGet = new InvoiceService(DataBaseFactory);
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

            var id = await DataBaseFactory.GetValueAsync<InvoiceHeader, string>(@"
SELECT TOP 1 ins.InvoiceUuid 
FROM InvoiceHeader ins 
INNER JOIN (
    SELECT it.InvoiceUuid, COUNT(1) AS cnt FROM InvoiceItems it GROUP BY it.InvoiceUuid
) itm ON (itm.InvoiceUuid = ins.InvoiceUuid)
WHERE itm.cnt > 0
");


            var srv = new InvoiceService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.InvoiceHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.InvoiceHeader.RowNum = rowNum;
            dto.InvoiceHeader.InvoiceUuid = id;

            srv.Clear();
            await srv.UpdateAsync(dto);

            var srvGet = new InvoiceService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}


        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void AddPayload_Test()
		{
            var srv = new InvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            

            var payload = new InvoicePayload();
            payload.Invoice = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            srv.Add(payload);
            var id = dto.InvoiceHeader.InvoiceUuid;
            var srvGet = new InvoiceService(DataBaseFactory);
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

            var id = DataBaseFactory.GetValue<InvoiceHeader, string>(@"
SELECT TOP 1 ins.InvoiceUuid 
FROM InvoiceHeader ins 
INNER JOIN (
    SELECT it.InvoiceUuid, COUNT(1) AS cnt FROM InvoiceItems it GROUP BY it.InvoiceUuid
) itm ON (itm.InvoiceUuid = ins.InvoiceUuid)
WHERE itm.cnt > 0
");


            var srv = new InvoiceService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.InvoiceHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.InvoiceHeader.RowNum = rowNum;
            dto.InvoiceHeader.InvoiceUuid = id;

            var payload = new InvoicePayload();
            payload.Invoice = dto;
            payload.MasterAccountNum = srv.Data.InvoiceHeader.MasterAccountNum;
            payload.ProfileNum = srv.Data.InvoiceHeader.ProfileNum;
            payload.DatabaseNum = srv.Data.InvoiceHeader.DatabaseNum;

            srv.Clear();
            srv.Update(payload);

            var srvGet = new InvoiceService(DataBaseFactory);
            srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task AddPayloadAsync_Test()
		{
            var srv = new InvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            
            var payload = new InvoicePayload();
            payload.Invoice = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            await srv.AddAsync(payload);

            var id = dto.InvoiceHeader.InvoiceUuid;

            var srvGet = new InvoiceService(DataBaseFactory);
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

            var id = await DataBaseFactory.GetValueAsync<InvoiceHeader, string>(@"
SELECT TOP 1 ins.InvoiceUuid 
FROM InvoiceHeader ins 
INNER JOIN (
    SELECT it.InvoiceUuid, COUNT(1) AS cnt FROM InvoiceItems it GROUP BY it.InvoiceUuid
) itm ON (itm.InvoiceUuid = ins.InvoiceUuid)
WHERE itm.cnt > 0
");


            var srv = new InvoiceService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.InvoiceHeader.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.InvoiceHeader.RowNum = rowNum;
            dto.InvoiceHeader.InvoiceUuid = id;

            var payload = new InvoicePayload();
            payload.Invoice = dto;
            payload.MasterAccountNum = srv.Data.InvoiceHeader.MasterAccountNum;
            payload.ProfileNum = srv.Data.InvoiceHeader.ProfileNum;
            payload.DatabaseNum = srv.Data.InvoiceHeader.DatabaseNum;

            srv.Clear();
            await srv.UpdateAsync(payload);

            var srvGet = new InvoiceService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact]
        public async Task InvoiceNumberExist_Test()
        {
            var srv = new InvoiceService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.InvoiceHeader.InvoiceNumber = "test-invoice-number";
            dto.InvoiceHeader.MasterAccountNum = 1001;
            dto.InvoiceHeader.ProfileNum = 1000;
            srv.Add(dto);

            var result = await srv.ExistInvoiceNumber("test-invoice-number", 1001, 1000);

            Assert.True(result);

        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task NewReturnAsync_Test()
        {
            var payload = new InvoiceReturnPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
            };
            var invoiceNumber = "aqaaec3me2s9qnekr2w7kldxa0y137f8it27iyvqk51xoc8cxc";

            var srv = new InvoiceReturnService(DataBaseFactory);
            var result = await srv.NewReturnAsync(payload, invoiceNumber);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

    }
}



