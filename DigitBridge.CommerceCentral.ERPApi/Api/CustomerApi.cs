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
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "CustomerCode", Description = "CustomerCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadGetSingle))]
        public static async Task<JsonNetResponse<CustomerPayload>> GetCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "customers/{CustomerCode}")] HttpRequest req,
            string CustomerCode = null)
        {
            var payload = await req.GetParameters<CustomerPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomerService(dbFactory);

            var customerCode = CustomerCode;
            if (!string.IsNullOrEmpty(CustomerCode))
            {
                var spilterIndex = CustomerCode.IndexOf("-");
                if (spilterIndex > 0)
                {
                    customerCode = CustomerCode.Substring(spilterIndex + 1);
                }
                payload.CustomerCodes.Add(customerCode);
            }
            if (await svc.GetCustomerByCustomerCodeAsync(payload, customerCode))
                payload.Customer = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<CustomerPayload>(payload);

        }
        [FunctionName(nameof(GetMultiCustomers))]
        [OpenApiOperation(operationId: "GetMultiCustomers", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCodes", In = ParameterLocation.Query, Required = false, Type = typeof(List<string>), Summary = "CustomerCodes", Description = "CustomerCode Array", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadGetMultiple))]
        public static async Task<JsonNetResponse<CustomerPayload>> GetMultiCustomers(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "customers")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new CustomerService(dbFactory);
            payload =await svc.GetCustomersByCodeArrayAsync(payload);
            return new JsonNetResponse<CustomerPayload>(payload);

        }

        [FunctionName(nameof(DeleteCustomer))]
        [OpenApiOperation(operationId: "DeleteCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "CustomerCode", Description = "CustomerCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadDelete),Description = "The OK response")]
        public static async Task<JsonNetResponse<CustomerPayload>> DeleteCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "customers/{CustomerCode}")] HttpRequest req,
            string CustomerCode)
        {
            var payload = await req.GetParameters<CustomerPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomerService(dbFactory);
            var spilterIndex = CustomerCode.IndexOf("-");
            var customerCode = CustomerCode;
            if (spilterIndex > 0)
            {
                customerCode = CustomerCode.Substring(spilterIndex + 1);
            }
            payload.CustomerCodes.Add(customerCode);
            if (await svc.DeleteByCodeAsync(payload,customerCode))
                payload.Customer = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<CustomerPayload>(payload);
        }

        [FunctionName(nameof(AddCustomer))]
        [OpenApiOperation(operationId: "AddCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerPayloadAdd), Description = "CustomerDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadAdd))]
        public static async Task<JsonNetResponse<CustomerPayload>> AddCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "customers")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomerService(dbFactory);
            if (await svc.AddAsync(payload))
                payload.Customer = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<CustomerPayload>(payload);
        }

        [FunctionName(nameof(UpdateCustomer))]
        [OpenApiOperation(operationId: "UpdateCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerPayloadUpdate), Description = "CustomerDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadUpdate))]
        public static async Task<JsonNetResponse<CustomerPayload>> UpdateCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "customers")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomerService(dbFactory);
            if (await svc.UpdateAsync(payload))
                payload.Customer = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<CustomerPayload>(payload);
        }

        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(CustomersList))]
        [OpenApiOperation(operationId: "CustomersList", tags: new[] { "Customers" }, Summary = "Load customer list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadFind))]
        public static async Task<JsonNetResponse<CustomerPayload>> CustomersList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new CustomerList(dataBaseFactory, new CustomerQuery());
            payload = await srv.GetCustomerListAsync(payload);
            return new JsonNetResponse<CustomerPayload>(payload);
        }

        /// <summary>
        /// Add customer
        /// </summary>
        [FunctionName(nameof(CustomerAddSample))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "CustomerAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of customer")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadAdd))]
        public static async Task<JsonNetResponse<CustomerPayloadAdd>> CustomerAddSample(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Sample/post:customer")] HttpRequest req)
        {
            return new JsonNetResponse<CustomerPayloadAdd>(CustomerPayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find customer
        /// </summary>
        [FunctionName(nameof(CustomerFindSample))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "CustomerFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of customer find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadFind))]
        public static async Task<JsonNetResponse<CustomerPayloadFind>> CustomerFindSample(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Sample/find:customer")] HttpRequest req)
        {
            return new JsonNetResponse<CustomerPayloadFind>(CustomerPayloadFind.GetSampleData());
        }
    }
}

