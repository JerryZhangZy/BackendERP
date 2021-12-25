//using DigitBridge.Base.Utility;
//using DigitBridge.CommerceCentral.ApiCommon;
//using DigitBridge.CommerceCentral.ERPDb;
//using DigitBridge.CommerceCentral.ERPApiSDK;
//using DigitBridge.CommerceCentral.ERPMdl;
//using DigitBridge.Log;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
//using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Threading.Tasks;

//namespace DigitBridge.CommerceCentral.ERPBroker
//{
//    [ApiFilter(typeof(SalesOrderBroker))]
//    public static class SalesOrderBroker
//    {
//        [FunctionName("CreateSalesOrderByCentralOrder")]
//        public static async Task CreateSalesOrderByCentralOrder([QueueTrigger("erp-create-salesorder-by-centralorder")] string myQueueItem, ILogger log)
//        {
//            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
//            var salesOrderClient = new CommerceCentralOrderClient();
//            var eventDto = new UpdateErpEventDto();
//            try
//            {
//                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
//                var payload = new SalesOrderOpenListPayload()
//                {
//                    MasterAccountNum = message.MasterAccountNum,
//                    ProfileNum = message.ProfileNum
//                };
//                eventDto.EventUuid = message.EventUuid;
//                eventDto.MasterAccountNum = message.MasterAccountNum;
//                eventDto.ProfileNum = message.ProfileNum;
//                var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
//                var svc = new SalesOrderManager(dbFactory);
//                (bool ret, List<string> salesOrderUuids) = await svc.CreateSalesOrderByChannelOrderIdAsync(message.ProcessUuid);

//                eventDto.ActionStatus = ret ? 0 : 1;
//                eventDto.EventMessage = svc.Messages.ObjectToString();
//            }
//            catch (Exception e)
//            {
//                eventDto.ActionStatus = 1;
//                eventDto.EventMessage = e.ObjectToString();
//                var reqInfo = new Dictionary<string, object>
//                {
//                    { "QueueFunctionName", "CreateSalesOrderByCentralOrder" },
//                    { "QueueMessage", myQueueItem }
//                };
//                LogCenter.CaptureException(e, reqInfo);
//            }
//            finally
//            {
//                await salesOrderClient.SendActionResultAsync(eventDto);
//            }
//        }

//        ///// <summary>
//        ///// Load sales order list
//        ///// </summary>
//        //[FunctionName(nameof(SalesOrdersOpenList))]
//        //[OpenApiOperation(operationId: "SalesOrdersOpenList", tags: new[] { "SalesOrders" }, Summary = "Load open sales order list data")]
//        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
//        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
//        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
//        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderOpenListPayload), Description = "Request Body in json format")]
//        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderOpenListPayload))]
//        //public static async Task<JsonNetResponse<SalesOrderOpenListPayload>> SalesOrdersOpenList(
//        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "salesOrders/find")] HttpRequest req)
//        //{
//        //    var payload = await req.GetParameters<SalesOrderOpenListPayload>(true);
//        //    var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
//        //    var srv = new SalesOrderOpenList(dataBaseFactory, new SalesOrderOpenQuery());
//        //    await srv.GetSalesOrdersOpenListAsync(payload);
//        //    return new JsonNetResponse<SalesOrderOpenListPayload>(payload);
//        //}

//        //[FunctionName(nameof(Sample_SalesOrder_Find))]
//        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
//        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
//        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
//        //[OpenApiOperation(operationId: "SalesOrderFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of salesorder find")]
//        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderOpenListPayloadFind))]
//        //public static async Task<JsonNetResponse<SalesOrderOpenListPayloadFind>> Sample_SalesOrder_Find(
//        //   [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/salesOrders/find")] HttpRequest req)
//        //{
//        //    return new JsonNetResponse<SalesOrderOpenListPayloadFind>(SalesOrderOpenListPayloadFind.GetSampleData());
//        //}
//    }
//}
