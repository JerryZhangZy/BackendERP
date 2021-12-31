using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPApi.OpenApiModel;
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
 

        [FunctionName(nameof(ExportCustomer))]
        [OpenApiOperation(operationId: "ExportCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "CustomerUuid", Description = "CustomerUuid", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerPayloadGetSingle), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportCustomer(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "customers/export/{CustomerUuid}")] HttpRequest req,string CustomerUuid = null)
        {
            var payload = await req.GetParameters<CustomerPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var customerIOManager = new CustomerIOManager(dbFactory);
            var customerService = new CustomerService(dbFactory);

            if (!await customerService.GetCustomerByCustomerUuidAsync(payload, CustomerUuid))
            {
                customerService.AddError("Get Customer datas error");
                return null;
            }
            var customerDtos = new List<CustomerDataDto>();
            customerDtos.Add(customerService.ToDto());
            var fileBytes = await customerIOManager.ExportAllColumnsAsync(customerDtos);
            var downfile = new FileContentResult(fileBytes, "text/csv");
            downfile.FileDownloadName = "export-customer.csv";
            return downfile;
        }



        [FunctionName(nameof(ImportCustomer))]
        [OpenApiOperation(operationId: "ImportCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayload))]
        public static async Task<CustomerPayload> ImportCustomer(
     [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "customers/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new CustomerManager(dbFactory);
            var customerIOManager = new CustomerIOManager(dbFactory);
            var customerDtos= await customerIOManager.ImportAllColumnsAsync(files[0].OpenReadStream());
            
            payload.Customer = customerDtos[0];
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }
 




        /// <summary>
        /// ExistCustomerCode
        /// </summary>
        /// <param name="req"></param>
        /// <param name="CustomerCode"></param>
        /// <returns></returns>

        [FunctionName(nameof(ExistCustomerCode))]
        [OpenApiOperation(operationId: "ExistCustomerCode", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "CustomerCode", Description = "CustomerCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExistCustomerCodePayload))]
        public static async Task<JsonNetResponse<ExistCustomerCodePayload>> ExistCustomerCode(
  [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "customers/existCustomerCode/{CustomerCode}")] HttpRequest req,
  string CustomerCode = null)
        {
            var payload = await req.GetParameters<ExistCustomerCodePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomerService(dbFactory);

            var spilterIndex = CustomerCode.IndexOf("-");
            var customerCode = CustomerCode;
            if (spilterIndex > 0 && customerCode.StartsWith(payload.ProfileNum.ToString()))
            {
                customerCode = CustomerCode.Substring(spilterIndex + 1);
            }
            if (await svc.GetCustomerByCustomerCodeAsync(new CustomerPayload() { MasterAccountNum = payload.MasterAccountNum, ProfileNum = payload.ProfileNum }, customerCode))
                payload.IsExistCustomerCode = svc.ToDto() != null;
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<ExistCustomerCodePayload>(payload);

        }




        [FunctionName(nameof(GetCustomer))]
        [OpenApiOperation(operationId: "GetCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "CustomerCode", Description = "CustomerCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadGetSingle))]
        public static async Task<JsonNetResponse<CustomerPayload>> GetCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "customers/{CustomerCode}")] HttpRequest req,
            string CustomerCode = null)
        {
            var payload = await req.GetParameters<CustomerPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomerService(dbFactory);

            var spilterIndex = CustomerCode.IndexOf("-");
            var customerCode = CustomerCode;
            if (spilterIndex > 0 && customerCode.StartsWith(payload.ProfileNum.ToString()))
            {
                customerCode = CustomerCode.Substring(spilterIndex + 1);
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
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCodes", In = ParameterLocation.Query, Required = true, Type = typeof(List<string>), Summary = "CustomerCodes", Description = "CustomerCode Array", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadGetMultiple))]
        public static async Task<JsonNetResponse<CustomerPayload>> GetMultiCustomers(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "customers")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new CustomerService(dbFactory);
            payload = await svc.GetCustomersByCodeArrayAsync(payload);
            return new JsonNetResponse<CustomerPayload>(payload);

        }

        [FunctionName(nameof(DeleteCustomer))]
        [OpenApiOperation(operationId: "DeleteCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "CustomerCode", Description = "CustomerCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadDelete), Description = "The OK response")]
        public static async Task<JsonNetResponse<CustomerPayload>> DeleteCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "customers/{CustomerCode}")] HttpRequest req,
            string CustomerCode)
        {
            var payload = await req.GetParameters<CustomerPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomerService(dbFactory);
            var spilterIndex = CustomerCode.IndexOf("-");
            var customerCode = CustomerCode;
            if (spilterIndex > 0 && customerCode.StartsWith(payload.ProfileNum.ToString()))
            {
                customerCode = CustomerCode.Substring(spilterIndex + 1);
            }
            payload.CustomerCodes.Add(customerCode);
            if (await svc.DeleteByCodeAsync(payload, customerCode))
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
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
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
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
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
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadFind))]
        public static async Task<JsonNetResponse<CustomerPayload>> CustomersList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new CustomerList(dataBaseFactory, new CustomerQuery());
            await srv.GetCustomerListAsync(payload);
            return new JsonNetResponse<CustomerPayload>(payload);
        }

        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(CustomerDataList))]
        [OpenApiOperation(operationId: "CustomerDataList", tags: new[] { "Customers" }, Summary = "Load customer list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadFind))]
        public static async Task<JsonNetResponse<CustomerPayload>> CustomerDataList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers/finddata")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new CustomerList(dataBaseFactory, new CustomerQuery());
            await srv.GetExportJsonListAsync(payload);
            return new JsonNetResponse<CustomerPayload>(payload);
        }

        /// <summary>
        /// Add customer
        /// </summary>
        [FunctionName(nameof(Sample_Customer_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "CustomerAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of customer")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadAdd))]
        public static async Task<JsonNetResponse<CustomerPayloadAdd>> Sample_Customer_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/customers")] HttpRequest req)
        {
            return new JsonNetResponse<CustomerPayloadAdd>(CustomerPayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find customer
        /// </summary>
        [FunctionName(nameof(Sample_Customer_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "CustomerFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of customer find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadFind))]
        public static async Task<JsonNetResponse<CustomerPayloadFind>> Sample_Customer_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/customers/find")] HttpRequest req)
        {
            return new JsonNetResponse<CustomerPayloadFind>(CustomerPayloadFind.GetSampleData());
        }

       

 

        /// <summary>
        /// Get sales order summary by search criteria
        /// </summary>
        /// <param name="req"></param> 
        [FunctionName(nameof(CustomerSummary))]
        [OpenApiOperation(operationId: "CustomerSummary", tags: new[] { "Customers" }, Summary = "Get customers summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayload))]
        public static async Task<JsonNetResponse<CustomerPayload>> CustomerSummary(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "customers/Summary")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new CustomerSummaryInquiry(dataBaseFactory, new CustomerSummaryQuery());
            await srv.GetCustomerSummaryAsync(payload);
            return new JsonNetResponse<CustomerPayload>(payload);
        }










        /// <summary>
        /// Add CustomerAdress
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddCustomerAdress))]
        [OpenApiOperation(operationId: "AddCustomerAdress", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerAddressPayloadAdd), Description = "CustomerAddressDataDto")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerAddressPayloadAdd))]
        public static async Task<JsonNetResponse<CustomerAddressPayload>> AddCustomerAdress(
         [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "customers/address")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerAddressPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomerService(dbFactory);
            if (await svc.AddCustomerAddressAsync(payload))
            {
                payload.CustomerAddress = svc.ToCustomerAddressDto();
            }
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<CustomerAddressPayload>(payload);
        }

        /// <summary>
        /// CustomerAdress
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateCustomerAdress))]
        [OpenApiOperation(operationId: "UpdateCustomerAdress", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerAddressPayloadUpdate), Description = "CustomerAddressDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerAddressPayloadUpdate))]
        public static async Task<JsonNetResponse<CustomerAddressPayload>> UpdateCustomerAdress(
[HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "customers/address")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerAddressPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomerService(dbFactory);
            if (await svc.UpdateCustomerAddressAsync(payload))
                payload.CustomerAddress = svc.ToCustomerAddressDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<CustomerAddressPayload>(payload);
        }


        /// <summary>
        /// DeleteCustomerAdress
        /// </summary>
        /// <param name="req"></param>
        /// <param name="addressUuid"></param>
        /// <returns></returns>

        [FunctionName(nameof(DeleteCustomerAdress))]
        [OpenApiOperation(operationId: "DeleteCustomerAdress", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "customerCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "customerCode", Description = "customerCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "addressCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "addressCode", Description = "addressCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerAddressPayloadDelete), Description = "The OK response")]
        public static async Task<JsonNetResponse<CustomerAddressPayload>> DeleteCustomerAdress(
    [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "customers/address/{customerCode}/{addressCode}")] HttpRequest req,
    string customerCode, string addressCode)
        {
            var payload = await req.GetParameters<CustomerAddressPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new CustomerService(dataBaseFactory);
            payload.Success = await srv.DeleteCustomerAddressAsync(payload.MasterAccountNum,payload.ProfileNum,customerCode, addressCode);
            if (payload.Success)
                payload.CustomerAddress = srv.ToCustomerAddressDto();
            payload.Messages = srv.Messages;
            return new JsonNetResponse<CustomerAddressPayload>(payload);
        }



        /// <summary>
        /// Add CustomerAddress Sample
        /// </summary>
        [FunctionName(nameof(Sample_CustomerAddress_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "CustomerAddressSample", tags: new[] { "Sample" }, Summary = "Get new sample of Customer address")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerAddressPayloadAdd))]
        public static async Task<JsonNetResponse<CustomerAddressPayloadAdd>> Sample_CustomerAddress_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/customerAddress")] HttpRequest req)
        {
            return new JsonNetResponse<CustomerAddressPayloadAdd>(CustomerAddressPayloadAdd.GetSampleData());
        }



        [FunctionName(nameof(CustomerListSummary))]
        [OpenApiOperation(operationId: "CustomerListSummary", tags: new[] { "Customers" }, Summary = "Load Customers list summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerPayloadFind))]
        public static async Task<JsonNetResponse<CustomerPayload>> CustomerListSummary(
          [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers/find/summary")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<CustomerPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new CustomerList(dataBaseFactory, new CustomerQuery());
            await srv.GetCustomerListSummaryAsync(payload);
            return new JsonNetResponse<CustomerPayload>(payload);
        }


    }
}

