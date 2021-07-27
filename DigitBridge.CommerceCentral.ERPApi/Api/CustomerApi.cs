using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPApi
{
    [ApiFilter(typeof(CustomerApi))]
    public static class CustomerApi
    {
        [FunctionName(nameof(GetCustomer))]
        [OpenApiOperation(operationId: "GetCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCodes", In = ParameterLocation.Query, Required = false, Type = typeof(List<string>), Summary = "CustomerCodes", Description = "CustomerCode Array", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<List<CustomerDataDto>>), Example = typeof(List<CustomerDataDto>), Description = "The OK response")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<CustomerPayload>), Example = typeof(CustomerPayload), Description = "The OK response")]
        public static async Task<JsonNetResponse<CustomerPayload>> GetCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "customers/{CustomerCode?}")] HttpRequest req,
            string CustomerCode,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var payload =await req.GetParameters<CustomerPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new CustomerService(dbFactory);

            if (!string.IsNullOrEmpty(CustomerCode))
            {
                var spilterIndex = CustomerCode.IndexOf("-");
                var customerCode = CustomerCode;
                if (spilterIndex > 0)
                {
                    customerCode = CustomerCode.Substring(spilterIndex + 1);
                }
                payload.CustomerCodes.Add(customerCode);
            }
            var result = svc.GetCustomersByCodeArray(payload);
            return new JsonNetResponse<CustomerPayload>(result);

        }

        [FunctionName(nameof(DeleteCustomer))]
        [OpenApiOperation(operationId: "DeleteCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "CustomerCode", Description = "CustomerCode = ProfileNumber-CustomerCode ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayload), Example = typeof(CustomerPayload), Description = "The OK response")]
        public static async Task<JsonNetResponse<CustomerPayload>> DeleteCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "customers/{CustomerCode}")] HttpRequest req,
            string CustomerCode,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var payload = await req.GetParameters<CustomerPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new CustomerService(dbFactory);
            var spilterIndex = CustomerCode.IndexOf("-");
            var customerCode = CustomerCode;
            if (spilterIndex > 0)
            {
                customerCode = CustomerCode.Substring(spilterIndex + 1);
            }
            payload.CustomerCodes.Add(customerCode);

            if( svc.DeleteByCode(payload))
                payload.Customer = svc.ToDto();
            return new JsonNetResponse<CustomerPayload>(payload);
        }
        [FunctionName(nameof(AddCustomer))]
        [OpenApiOperation(operationId: "AddCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerDataDto), Description = "CustomerDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayload), Example = typeof(CustomerPayload), Description = "The OK response")]
        public static async Task<JsonNetResponse<CustomerPayload>> AddCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "customers")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var payload = await req.GetParameters<CustomerPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new CustomerService(dbFactory);

            if (await svc.AddAsync(payload.Customer))
                payload.Customer = svc.ToDto();
            return new JsonNetResponse<CustomerPayload>(payload);
        }

        [FunctionName(nameof(UpdateCustomer))]
        [OpenApiOperation(operationId: "UpdateCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerDataDto), Description = "CustomerDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayload), Example = typeof(CustomerPayload), Description = "The OK response")]
        public static async Task<JsonNetResponse<CustomerPayload>> UpdateCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "PATCH", Route = "customers")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var payload = await req.GetParameters<CustomerPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new CustomerService(dbFactory);

            if (svc.Update(payload.Customer))
                payload.Customer = svc.ToDto();
            return new JsonNetResponse<CustomerPayload>(payload);
        }
    }
}

