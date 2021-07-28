using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using Xunit;
namespace DigitBridge.CommerceCentral.ERPApi.Tests.Integration
{

    public class ProductExApiTests
    {
        [Fact]
        public async void AddProductEx_Test()
        {
            var data= InventoryDataTests.GetFakerData();
            var mapper = new InventoryDataDtoMapperDefault();
            var reqestInfo = new RequestInfo<ProductExPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = 1,
                    MasterAccountNum = 1
                },
                RequestBody = new ProductExPayload
                {
                    InventoryData = mapper.WriteDto(data, null)
                }
            };
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await ProductExtApi.AddProductExt(req);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasInventoryData);
        }

        [Fact]
        public async void GetProductEx_Test()
        {
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var inventory = dbFactory.Db.Query<ProductExt>(@"
SELECT TOP 1 * 
FROM ProductExt 
", System.Data.CommandType.Text).First();
            var reqestInfo = new RequestInfo<ProductExPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = inventory.ProfileNum,
                    MasterAccountNum = inventory.MasterAccountNum
                },
                RequestBody = new ProductExPayload()
            };
            reqestInfo.RequestBody.Skus.Add(inventory.SKU);
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await ProductExtApi.GetProductExt(req,inventory.SKU);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasInventoryDatas);
        }

        [Fact]
        public async void DeleteProductEx_Test()
        {
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var inventory = dbFactory.Db.Query<ProductExt>(@"
SELECT TOP 1 * 
FROM ProductExt 
", System.Data.CommandType.Text).First();
            var reqestInfo = new RequestInfo<ProductExPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = inventory.ProfileNum,
                    MasterAccountNum = inventory.MasterAccountNum
                },
                RequestBody = new ProductExPayload()
            };
            reqestInfo.RequestBody.Skus.Add(inventory.SKU);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await ProductExtApi.DeleteProductExt(req, inventory.SKU);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasInventoryData);
        }

        [Fact]
        public async void UpdateProductEx_Test()
        {
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var inventory = dbFactory.Db.Query<Inventory>(@"
SELECT TOP 1 * 
FROM Inventory 
", System.Data.CommandType.Text).First();
            var svc = new InventoryService(dbFactory);
            svc.GetData(inventory.RowNum);
            var dto = svc.ToDto();
            dto.ProductExt.Remark = Guid.NewGuid().ToString("N");
            var reqestInfo = new RequestInfo<ProductExPayload>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = inventory.ProfileNum,
                    MasterAccountNum = inventory.MasterAccountNum
                },
                RequestBody = new ProductExPayload() { InventoryData=dto}
            };
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await ProductExtApi.UpdateProductExt(req);
            var payload = await response.GetBodyObjectAsync();
            Assert.True(payload.HasInventoryData);
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
