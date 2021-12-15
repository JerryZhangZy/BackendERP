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
namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process salesorder
    /// </summary> 
    [ApiFilter(typeof(ImportFilesApi))]
    public static class ImportFilesApi
    {

        [FunctionName(nameof(ImportCustomer))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportSalesOrder", tags: new[] { "SalesOrders" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportFilesPayload>> ImportCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/customer")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.ImportFiles = req.Form.Files;
            //payload.Options = req.Form["options"];
            var svc = new SalesOrderManager(dbFactory);

            //await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return new JsonNetResponse<ImportFilesPayload>(payload);
        }

    }
}

