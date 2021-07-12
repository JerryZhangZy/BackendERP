
    

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
    public partial class InventoryServiceTests
    {

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public void AddDto_Test()
		{
            var srv = new InventoryService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            var id = data.UniqueId;

            srv.Add(dto);

            var srvGet = new InventoryService(DataBaseFactory);
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

            var id = DataBaseFactory.GetValue<ProductBasic, string>(@"
SELECT TOP 1 ins.ProductUuid 
FROM ProductBasic ins 
INNER JOIN (
    SELECT it.ProductUuid, COUNT(1) AS cnt FROM Inventory it GROUP BY it.ProductUuid
) itm ON (itm.ProductUuid = ins.ProductUuid)
WHERE itm.cnt > 0
");


            var srv = new InventoryService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.ProductBasic.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.ProductBasic.RowNum = rowNum;
            dto.ProductBasic.ProductUuid = id;

            srv.Clear();
            srv.Update(dto);

            var srvGet = new InventoryService(DataBaseFactory);
            srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

        [Fact()]
		//[Fact(Skip = SkipReason)]
		public async Task AddDtoAsync_Test()
		{
            var srv = new InventoryService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            var id = data.UniqueId;

            await srv.AddAsync(dto);

            var srvGet = new InventoryService(DataBaseFactory);
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

            var id = await DataBaseFactory.GetValueAsync<ProductBasic, string>(@"
SELECT TOP 1 ins.ProductUuid 
FROM ProductBasic ins 
INNER JOIN (
    SELECT it.ProductUuid, COUNT(1) AS cnt FROM Inventory it GROUP BY it.ProductUuid
) itm ON (itm.ProductUuid = ins.ProductUuid)
WHERE itm.cnt > 0
");


            var srv = new InventoryService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.ProductBasic.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.ProductBasic.RowNum = rowNum;
            dto.ProductBasic.ProductUuid = id;

            srv.Clear();
            await srv.UpdateAsync(dto);

            var srvGet = new InventoryService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

			Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
		}

    }
}



