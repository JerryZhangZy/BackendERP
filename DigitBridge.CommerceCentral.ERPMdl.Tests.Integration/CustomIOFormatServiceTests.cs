    

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
    public partial class CustomIOFormatServiceTests
    {

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task AddDtoAsync_Test()
		{
            var srv = new CustomIOFormatService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null); 

            await srv.AddAsync(dto);

            var id = dto.CustomIOFormat.CustomIOFormatUuid;

            var srvGet = new CustomIOFormatService(DataBaseFactory);
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

            var id = await DataBaseFactory.GetValueAsync<CustomIOFormat, string>(@"
SELECT TOP 1 ins.CustomIOFormatUuid 
FROM CustomIOFormat ins 
");


            var srv = new CustomIOFormatService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.CustomIOFormat.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.CustomIOFormat.RowNum = rowNum;
            dto.CustomIOFormat.CustomIOFormatUuid = id;

            srv.Clear();
            await srv.UpdateAsync(dto);

            var srvGet = new CustomIOFormatService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task AddPayloadAsync_Test()
		{
            var srv = new CustomIOFormatService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            
            var payload = new CustomIOFormatPayload();
            payload.CustomIOFormat = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            await srv.AddAsync(payload);

            var id = dto.CustomIOFormat.CustomIOFormatUuid;

            var srvGet = new CustomIOFormatService(DataBaseFactory);
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

            var id = await DataBaseFactory.GetValueAsync<CustomIOFormat, string>(@"
SELECT TOP 1 ins.CustomIOFormatUuid 
FROM CustomIOFormat ins 
");


            var srv = new CustomIOFormatService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.CustomIOFormat.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.CustomIOFormat.RowNum = rowNum;
            dto.CustomIOFormat.CustomIOFormatUuid = id;

            var payload = new CustomIOFormatPayload();
            payload.CustomIOFormat = dto;
            payload.MasterAccountNum = srv.Data.CustomIOFormat.MasterAccountNum;
            payload.ProfileNum = srv.Data.CustomIOFormat.ProfileNum;
            payload.DatabaseNum = srv.Data.CustomIOFormat.DatabaseNum;

            srv.Clear();
            await srv.UpdateAsync(payload);

            var srvGet = new CustomIOFormatService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

    }
}


