using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;
namespace DigitBridge.CommerceCentral.ERPApi.Tests.Integration
{

    public class InventoryLogApiTests
    {
        [Fact]
        public async void AddInvenrotyList_Test()
        {
            var list = InventoryLogDataTests.GetFakerData(20);
            var logUuid = Guid.NewGuid().ToString("N");
            var dtolist = new List<InventoryLogDataDto>();
            var mapper = new InventoryLogDataDtoMapperDefault();
            foreach (var x in list)
            {
                x.InventoryLog.LogUuid = logUuid;
                dtolist.Add(mapper.WriteDto(x, null));
            }
            var reqestInfo = new RequestInfo<InventoryLogPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = 1,
                    MasterAccountNum = 1
                },
                RequestBody = new InventoryLogPayload
                {
                    InventoryLogs = dtolist
                }
            };
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await InventoryLogApi.AddInventoryLogs(req);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasInventoryLogs);
        }

        [Fact]
        public async void GetInventoryLogs_Test()
        {
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var log = dbFactory.Db.First<InventoryLog>(@"
SELECT TOP 1  *
FROM InventoryLog 
");
            var reqestInfo = new RequestInfo<InventoryLogPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = log.ProfileNum,
                    MasterAccountNum = log.MasterAccountNum
                },
                RequestBody = new InventoryLogPayload()
            };
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await InventoryLogApi.GetInventoryLogs(req, log.LogUuid);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasInventoryLogs);
        }

        [Fact]
        public async void DeleteProductEx_Test()
        {
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var log = dbFactory.Db.First<InventoryLog>(@"
SELECT TOP 1  *
FROM InventoryLog 
");
            var reqestInfo = new RequestInfo<InventoryLogPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = log.ProfileNum,
                    MasterAccountNum = log.MasterAccountNum
                },
                RequestBody = new InventoryLogPayload()
            };
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await InventoryLogApi.DeleteInventoryLogs(req, log.LogUuid);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasInventoryLogs);
        }

        [Fact]
        public async void UpdateProductEx_Test()
        {
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var customer = dbFactory.Db.First<InventoryLog>(@"
SELECT TOP 1 * 
FROM InventoryLog 
", System.Data.CommandType.Text);
            var svc = new InventoryLogService(dbFactory);
            svc.GetData(customer.RowNum);
            var dto = svc.ToDto();
            dto.InventoryLog.Description = Guid.NewGuid().ToString("N");
            var dtoList = new List<InventoryLogDataDto>();
            dtoList.Add(dto);
            var reqestInfo = new RequestInfo<InventoryLogPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = customer.ProfileNum,
                    MasterAccountNum = customer.MasterAccountNum
                },
                RequestBody = new InventoryLogPayload() { InventoryLogs= dtoList }
            };
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await InventoryLogApi.UpdateInventoryLogs(req);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasInventoryLogs);
        }

        //[Theory]
        //[MemberData(nameof(TestFactory.Data), MemberType = typeof(TestFactory))]
        //public async void Http_trigger_should_return_known_string_from_member_data(string queryStringKey, string queryStringValue)
        //{
        //    var request = TestFactory.CreateHttpRequest(queryStringKey, queryStringValue);
        //    var response = (OkObjectResult)await MyHttpTrigger.Run(request, logger);
        //    Assert.Equal($"Hello, {queryStringValue}. This HTTP triggered function executed successfully.", response.Value);
        //}

        //[Fact]
        //public void Timer_should_log_message()
        //{
        //    var logger = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);
        //    MyTimerTrigger.Run(null, logger);
        //    var msg = logger.Logs[0];
        //    Assert.Contains("C# Timer trigger function executed at", msg);
        //}

    }
}
