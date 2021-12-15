using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
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

namespace DigitBridge.CommerceCentral.ERP.Integration.Api
{
    [ApiFilter(typeof(EventApi))]
    public static class EventApi
    {
        [FunctionName(nameof(AddQuickBooksInvoiceEvent))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "AddQuickBooksInvoiceEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(NewEvent),
            Description = "NewEvent ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        #endregion Swagger Docs
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksInvoiceEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksInvoice")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>();
            payload.NewEvent = await req.GetBodyObjectAsync<NewEvent>();

            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);

            payload.Success = await svc.AddAsync(payload, ErpEventType.InvoiceToQboInvoice);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddQuickBooksInvoiceVoidEvent))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "AddQuickBooksInvoiceVoidEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(NewEvent),
            Description = "NewEvent ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        #endregion Swagger Docs
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksInvoiceVoidEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksInvoiceVoid")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>();
            payload.NewEvent = await req.GetBodyObjectAsync<NewEvent>();

            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);

            payload.Success = await svc.AddAsync(payload, ErpEventType.VoidQboInvoice);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddQuickBooksReturnEvent))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "AddQuickBooksReturnEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(NewEvent),
            Description = "NewEvent ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        #endregion Swagger Docs
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksReturnEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksReturn")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>();
            payload.NewEvent = await req.GetBodyObjectAsync<NewEvent>();

            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);

            payload.Success = await svc.AddAsync(payload, ErpEventType.InvoiceRetrunToQboRefund);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddQuickBooksReturnDeleteEvent))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "AddQuickBooksReturnDeleteEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(NewEvent),
            Description = "NewEvent ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        #endregion Swagger Docs
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksReturnDeleteEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksReturnDelete")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>();
            payload.NewEvent = await req.GetBodyObjectAsync<NewEvent>();

            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);

            payload.Success = await svc.AddAsync(payload, ErpEventType.DeleteQboRefund);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddQuickBooksPaymentEvent))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "AddQuickBooksPaymentEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(NewEvent),
            Description = "NewEvent ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        #endregion Swagger Docs
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksPaymentEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksPayment")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>();
            payload.NewEvent = await req.GetBodyObjectAsync<NewEvent>();

            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);

            payload.Success = await svc.AddAsync(payload, ErpEventType.InvoicePaymentToQboPayment);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddCreateSalesOrderByCentralOrderEvent))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "AddCreateSalesOrderByCentralOrderEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(NewEvent),
            Description = "NewEvent ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        #endregion Swagger Docs
        public static async Task<JsonNetResponse<EventERPPayload>> AddCreateSalesOrderByCentralOrderEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addCreateSalesOrderByCentralOrder")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>();
            payload.NewEvent = await req.GetBodyObjectAsync<NewEvent>();

            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);

            payload.Success = await svc.AddAsync(payload, ErpEventType.CentralOrderToSalesOrder);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddCreateInvoiceByOrderShipmentEvent))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "AddCreateInvoiceByOrderShipmentEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(NewEvent),
            Description = "NewEvent ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        #endregion Swagger Docs
        public static async Task<JsonNetResponse<EventERPPayload>> AddCreateInvoiceByOrderShipmentEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addCreateInvoiceByOrderShipment")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>();
            payload.NewEvent = await req.GetBodyObjectAsync<NewEvent>();

            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);

            payload.Success = await svc.AddAsync(payload, ErpEventType.ShipmentToInvoice);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(AddQuickBooksPaymentDeleteEvent))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "AddQuickBooksPaymentDeleteEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(NewEvent),
            Description = "NewEvent ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadAdd))]
        #endregion Swagger Docs
        public static async Task<JsonNetResponse<EventERPPayload>> AddQuickBooksPaymentDeleteEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/addQuicksBooksPaymentDelete")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>();
            payload.NewEvent = await req.GetBodyObjectAsync<NewEvent>();

            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);

            payload.Success = await svc.AddAsync(payload, ErpEventType.DeleteQboPayment);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(UpdateEventERP))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "UpdateEventERP", tags: new[] { "EventERPs" })]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateEventDto),
            Description = "UpdateEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadUpdate))]
        #endregion Swagger Docs
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
            payload.Success = await svc.UpdateAsync(payload);
            if (payload.Success)
                payload.Event = svc.Data.Event_ERP;
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }

        /// <summary>
        /// Load erpevent list
        /// </summary>
        [FunctionName(nameof(EventERPsList))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "EventERPsList", tags: new[] { "EventERPs" },
            Summary = "Load erpevent list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "MasterAccountNum", Description = "From login profile",
            Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventERPPayloadFind),
            Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadFind))]
        #endregion Swagger Docs
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
        #region Swagger Docs
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
        #endregion Swagger Docs
        public static async Task<JsonNetResponse<EventERPPayloadFind>> Sample_EventERP_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/erpevents/find")]
            HttpRequest req)
        {
            return new JsonNetResponse<EventERPPayloadFind>(EventERPPayloadFind.GetSampleData());
        }



        [FunctionName(nameof(ReSendEvent))]
        #region Swagger Docs
        [OpenApiOperation(operationId: "ReSendEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventERPPayloadResendRequest), Required = true, Description = "Array of eventUuid.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadUpdate))]
        #endregion Swagger Docs
        public static async Task<JsonNetResponse<EventERPPayload>> ReSendEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/resend")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            payload.Success = await svc.ResendEventsAsync(payload);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<EventERPPayload>(payload);
        }
    }
}