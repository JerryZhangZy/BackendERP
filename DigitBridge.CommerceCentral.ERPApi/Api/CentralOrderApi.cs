using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb.so;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process salesorder
    /// </summary> 
    [ApiFilter(typeof(CentralOrderApi))]
    public static class CentralOrderApi
    {


        /// <summary>
        /// Get one sales order by orderNumber
        /// </summary>
        /// <param name="req"></param>
        /// <param name="centralOrderNumber"></param>
        [FunctionName(nameof(CentralOrderReference))]
        [OpenApiOperation(operationId: "CentralOrderReference", tags: new[] { "CentralOrders" }, Summary = "Get  CentralOrders info")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "centralOrderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "centralOrderNumber", Description = "centralOrderNumber. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CentralOrderReferencePayload))]
        public static async Task<JsonNetResponse<CentralOrderReferencePayload>> CentralOrderReference(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "centralOrders/{centralOrderNumber}")] HttpRequest req,
            long centralOrderNumber)
        {
            var payload = await req.GetParameters<CentralOrderReferencePayload>();
            payload.CentralOrderNum = centralOrderNumber;
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new SalesOrderManager(dataBaseFactory);
            await srv.CentralOrderReferenceAsync(payload);

            return new JsonNetResponse<CentralOrderReferencePayload>(payload);
        }

    }
}

