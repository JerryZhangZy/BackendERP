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
            var list = new List<InventoryLog>();
            for(var i = 0;i<10;i++)
            {
                list.Add(InventoryLogTests.GetFakerData());
            }
            var logUuid = Guid.NewGuid().ToString("N");
            list.ForEach(x => x.LogUuid = logUuid);
            var mapper = new InventoryLogDataDtoMapperDefault();
            var reqestInfo = new RequestInfo<InventoryLogPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = 1,
                    MasterAccountNum = 1
                },
                RequestBody = new InventoryLogPayload
                {
                    InventoryLogs = mapper.WriteInventoryLogDtoList(list,null)
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
            var uuids = dbFactory.GetValue<InventoryLog, string>(@"
SELECT TOP 1 ins.LogUuid 
FROM InventoryLog ins 
");
            var reqestInfo = new RequestInfo<InventoryLogPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = 12,
                    MasterAccountNum = 20
                },
                RequestBody = new InventoryLogPayload()
            };
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await InventoryLogApi.GetInventoryLogs(req, uuids);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasInventoryLogs);
        }

        [Fact]
        public async void DeleteProductEx_Test()
        {
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var uuids = dbFactory.GetValue<InventoryLog, string>(@"
SELECT TOP 1 ins.LogUuid 
FROM InventoryLog ins 
");
            var reqestInfo = new RequestInfo<InventoryLogPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = 12,
                    MasterAccountNum = 20
                },
                RequestBody = new InventoryLogPayload()
            };
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await InventoryLogApi.DeleteInventoryLogs(req, uuids);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasInventoryLogs);
        }

        [Fact]
        public async void UpdateProductEx_Test()
        {
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var customer = dbFactory.Db.Query<Customer>(@"
SELECT TOP 1 * 
FROM Customer 
", System.Data.CommandType.Text).First();
            var svc = new CustomerService(dbFactory);
            svc.GetData(customer.RowNum);
            var dto = svc.ToDto();
            dto.Customer.Contact = Guid.NewGuid().ToString("N");
            var reqestInfo = new RequestInfo<CustomerPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = customer.ProfileNum,
                    MasterAccountNum = customer.MasterAccountNum
                },
                RequestBody = new CustomerPayload() { Customer=dto}
            };
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await CustomerApi.UpdateCustomer(req);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasCustomer);
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
