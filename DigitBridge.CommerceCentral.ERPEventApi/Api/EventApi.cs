using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPApi;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.EventERPApi
{
    [ERPEventApi.ApiFilter(typeof(EventApi))]
    public static class EventApi
    {
        [FunctionName(nameof(AddQuickBooksInvoiceEvent))]
        [OpenApiOperation(operationId: "AddQuickBooksInvoiceEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AddEventDto),
            Description = "AddEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksInvoiceEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksInvoice")]
            HttpRequest req)
        {
            var eventdata = await req.GetBodyObjectAsync<AddEventDto>();
            var payload = new EventERPPayload()
            {
                MasterAccountNum = eventdata.MasterAccountNum,
                ProfileNum = eventdata.ProfileNum,
                EventERP = eventdata.ToEventERPDataDto(ErpEventType.InvoiceToQboInvoice)
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.AddAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddQuickBooksInvoiceVoidEvent))]
        [OpenApiOperation(operationId: "AddQuickBooksInvoiceVoidEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AddEventDto),
            Description = "AddEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksInvoiceVoidEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksInvoiceVoid")]
            HttpRequest req)
        {
            var eventdata = await req.GetBodyObjectAsync<AddEventDto>();
            var payload = new EventERPPayload()
            {
                MasterAccountNum = eventdata.MasterAccountNum,
                ProfileNum = eventdata.ProfileNum,
                EventERP = eventdata.ToEventERPDataDto(ErpEventType.VoidQboInvoice)
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.AddAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddQuickBooksReturnEvent))]
        [OpenApiOperation(operationId: "AddQuickBooksReturnEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AddEventDto),
            Description = "AddEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksReturnEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksReturn")]
            HttpRequest req)
        {
            var eventdata = await req.GetBodyObjectAsync<AddEventDto>();
            var payload = new EventERPPayload()
            {
                MasterAccountNum = eventdata.MasterAccountNum,
                ProfileNum = eventdata.ProfileNum,
                EventERP = eventdata.ToEventERPDataDto(ErpEventType.InvoiceRetrunToQboRefund)
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.AddAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddQuickBooksReturnDeleteEvent))]
        [OpenApiOperation(operationId: "AddQuickBooksReturnDeleteEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AddEventDto),
            Description = "AddEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksReturnDeleteEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksReturnDelete")]
            HttpRequest req)
        {
            var eventdata = await req.GetBodyObjectAsync<AddEventDto>();
            var payload = new EventERPPayload()
            {
                MasterAccountNum = eventdata.MasterAccountNum,
                ProfileNum = eventdata.ProfileNum,
                EventERP = eventdata.ToEventERPDataDto(ErpEventType.DeleteQboRefund)
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.AddAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddQuickBooksPaymentEvent))]
        [OpenApiOperation(operationId: "AddQuickBooksPaymentEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AddEventDto),
            Description = "AddEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksPaymentEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksPayment")]
            HttpRequest req)
        {
            var eventdata = await req.GetBodyObjectAsync<AddEventDto>();
            var payload = new EventERPPayload()
            {
                MasterAccountNum = eventdata.MasterAccountNum,
                ProfileNum = eventdata.ProfileNum,
                EventERP = eventdata.ToEventERPDataDto(ErpEventType.InvoicePaymentToQboPayment)
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.AddAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddCreateSalesOrderByCentralOrderEvent))]
        [OpenApiOperation(operationId: "AddCreateSalesOrderByCentralOrderEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AddEventDto),
            Description = "AddEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        public static async Task<JsonNetResponse<EventERPPayload>> AddCreateSalesOrderByCentralOrderEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addCreateSalesOrderByCentralOrder")]
            HttpRequest req)
        {
            var eventdata = await req.GetBodyObjectAsync<AddEventDto>();
            var payload = new EventERPPayload()
            {
                MasterAccountNum = eventdata.MasterAccountNum,
                ProfileNum = eventdata.ProfileNum,
                EventERP = eventdata.ToEventERPDataDto(ErpEventType.CentralOrderToSalesOrder)
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.AddAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddCreateInvoiceByOrderShipmentEvent))]
        [OpenApiOperation(operationId: "AddCreateInvoiceByOrderShipmentEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AddEventDto),
            Description = "AddEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        public static async Task<JsonNetResponse<EventERPPayload>> AddCreateInvoiceByOrderShipmentEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addCreateInvoiceByOrderShipment")]
            HttpRequest req)
        {
            var eventdata = await req.GetBodyObjectAsync<AddEventDto>();
            var payload = new EventERPPayload()
            {
                MasterAccountNum = eventdata.MasterAccountNum,
                ProfileNum = eventdata.ProfileNum,
                EventERP = eventdata.ToEventERPDataDto(ErpEventType.ShipmentToInvoice)
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.AddAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddQuickBooksPaymentDeleteEvent))]
        [OpenApiOperation(operationId: "AddQuickBooksPaymentDeleteEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AddEventDto),
            Description = "AddEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksPaymentDeleteEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksPaymentDelete")]
            HttpRequest req)
        {
            var eventdata = await req.GetBodyObjectAsync<AddEventDto>();
            var payload = new EventERPPayload()
            {
                MasterAccountNum = eventdata.MasterAccountNum,
                ProfileNum = eventdata.ProfileNum,
                EventERP = eventdata.ToEventERPDataDto(ErpEventType.DeleteQboPayment)
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.AddAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }

            return new JsonNetResponse<EventERPPayload>(payload);
        }


        [FunctionName(nameof(UpdateEventERP))]
        [OpenApiOperation(operationId: "UpdateEventERP", tags: new[] { "EventERPs" })]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateEventDto),
            Description = "UpdateEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadUpdate))]
        public static async Task<JsonNetResponse<EventERPPayload>> UpdateEventERP(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "erpevents")]
            HttpRequest req)
        {
            var eventdata = await req.GetBodyObjectAsync<UpdateEventDto>();
            var payload = new EventERPPayload()
            {
                MasterAccountNum = eventdata.MasterAccountNum,
                ProfileNum = eventdata.ProfileNum,
                EventERP = eventdata.ToEventERPDataDto()
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.UpdateAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        /// <summary>
        /// Load erpevent list
        /// </summary>
        [FunctionName(nameof(EventERPsList))]
        [OpenApiOperation(operationId: "EventERPsList", tags: new[] { "EventERPs" },
            Summary = "Load erpevent list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "MasterAccountNum", Description = "From login profile",
            Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventERPPayloadFind),
            Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadFind))]
        public static async Task<JsonNetResponse<EventERPPayload>> EventERPsList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "erpevents/find")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new EventERPList(dataBaseFactory, new EventERPQuery());
            await srv.GetEventERPListAsync(payload);
            return new JsonNetResponse<EventERPPayload>(payload);
        }

        ///// <summary>
        ///// Add erpevent
        ///// </summary>
        //[FunctionName(nameof(Sample_EventERP_Post))]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiOperation(operationId: "EventERPAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of erpevent")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(EventERPPayloadAdd))]
        //public static async Task<JsonNetResponse<EventERPPayloadAdd>> Sample_EventERP_Post(
        //    [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/erpevents")] HttpRequest req)
        //{
        //    return new JsonNetResponse<EventERPPayloadAdd>(EventERPPayloadAdd.GetSampleData());
        //}

        /// <summary>
        /// find erpevent
        /// </summary>
        [FunctionName(nameof(Sample_EventERP_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "MasterAccountNum", Description = "From login profile",
            Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "EventERPFindSample", tags: new[] { "Sample" },
            Summary = "Get new sample of erpevent find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadFind))]
        public static async Task<JsonNetResponse<EventERPPayloadFind>> Sample_EventERP_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/erpevents/find")]
            HttpRequest req)
        {
            return new JsonNetResponse<EventERPPayloadFind>(EventERPPayloadFind.GetSampleData());
        }



        [FunctionName(nameof(ReSendEvent))]
        [OpenApiOperation(operationId: "ReSendEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "eventUuids", In = ParameterLocation.Query, Required = true, Type = typeof(IList<string>), Summary = "eventUuids", Description = "Array of eventUuid.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadUpdate))]
        public static async Task<JsonNetResponse<EventERPPayload>> ReSendEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/resend")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            payload.Success = await svc.ResendEventsAsync(payload);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }
    }
}