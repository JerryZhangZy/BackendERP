    

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
    public partial class ApTransactionServiceTests
    {

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void AddDto_Test()
		{
            var srv = new ApTransactionService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            

            srv.Add(dto);

            var id = dto.ApInvoiceTransaction.TransUuid;

            var srvGet = new ApTransactionService(DataBaseFactory);
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

            var id = DataBaseFactory.GetValue<ApInvoiceTransaction, string>(@"
SELECT TOP 1 ins.TransUuid 
FROM ApInvoiceTransaction ins 
");


            var srv = new ApTransactionService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.ApInvoiceTransaction.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.ApInvoiceTransaction.RowNum = rowNum;
            dto.ApInvoiceTransaction.TransUuid = id;

            srv.Clear();
            srv.Update(dto);

            var srvGet = new ApTransactionService(DataBaseFactory);
            srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task AddDtoAsync_Test()
		{
            var srv = new ApTransactionService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null); 

            await srv.AddAsync(dto);

            var id = dto.ApInvoiceTransaction.TransUuid;

            var srvGet = new ApTransactionService(DataBaseFactory);
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

            var id = await DataBaseFactory.GetValueAsync<ApInvoiceTransaction, string>(@"
SELECT TOP 1 ins.TransUuid 
FROM ApInvoiceTransaction ins 
");


            var srv = new ApTransactionService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.ApInvoiceTransaction.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.ApInvoiceTransaction.RowNum = rowNum;
            dto.ApInvoiceTransaction.TransUuid = id;

            srv.Clear();
            await srv.UpdateAsync(dto);

            var srvGet = new ApTransactionService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}


        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void AddPayload_Test()
		{
            var srv = new ApTransactionService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            

            var payload = new ApTransactionPayload();
            payload.ApTransaction = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            srv.Add(payload);
            var id = dto.ApInvoiceTransaction.TransUuid;
            var srvGet = new ApTransactionService(DataBaseFactory);
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

            var id = DataBaseFactory.GetValue<ApInvoiceTransaction, string>(@"
SELECT TOP 1 ins.TransUuid 
FROM ApInvoiceTransaction ins 
");


            var srv = new ApTransactionService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.ApInvoiceTransaction.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.ApInvoiceTransaction.RowNum = rowNum;
            dto.ApInvoiceTransaction.TransUuid = id;

            var payload = new ApTransactionPayload();
            payload.ApTransaction = dto;
            payload.MasterAccountNum = srv.Data.ApInvoiceTransaction.MasterAccountNum;
            payload.ProfileNum = srv.Data.ApInvoiceTransaction.ProfileNum;
            payload.DatabaseNum = srv.Data.ApInvoiceTransaction.DatabaseNum;

            srv.Clear();
            srv.Update(payload);

            var srvGet = new ApTransactionService(DataBaseFactory);
            srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task AddPayloadAsync_Test()
		{
            var srv = new ApTransactionService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            
            var payload = new ApTransactionPayload();
            payload.ApTransaction = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            await srv.AddAsync(payload);

            var id = dto.ApInvoiceTransaction.TransUuid;

            var srvGet = new ApTransactionService(DataBaseFactory);
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

            var id = await DataBaseFactory.GetValueAsync<ApInvoiceTransaction, string>(@"
SELECT TOP 1 ins.TransUuid 
FROM ApInvoiceTransaction ins 
");


            var srv = new ApTransactionService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.ApInvoiceTransaction.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.ApInvoiceTransaction.RowNum = rowNum;
            dto.ApInvoiceTransaction.TransUuid = id;

            var payload = new ApTransactionPayload();
            payload.ApTransaction = dto;
            payload.MasterAccountNum = srv.Data.ApInvoiceTransaction.MasterAccountNum;
            payload.ProfileNum = srv.Data.ApInvoiceTransaction.ProfileNum;
            payload.DatabaseNum = srv.Data.ApInvoiceTransaction.DatabaseNum;

            srv.Clear();
            await srv.UpdateAsync(payload);

            var srvGet = new ApTransactionService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

    }
}



