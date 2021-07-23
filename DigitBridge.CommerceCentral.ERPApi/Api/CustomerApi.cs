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
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "CustomerCode", Description = "CustomerCode = ProfileNumber-CustomerCode ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<CustomerDataDto>), Example = typeof(CustomerDataDto), Description = "The OK response")]
        public static async Task<IActionResult> GetCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "customers/{CustomerCode?}")] HttpRequest req,
            string CustomerCode,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0; ;
            var dbFactory = MyAppHelper.GetDatabase(masterAccountNum);
            var spilterIndex = CustomerCode.IndexOf("-");
            var customerCode = CustomerCode;
            if (spilterIndex > 0)
            {
                profileNum = CustomerCode.Substring(0, spilterIndex).ToInt();
                customerCode = CustomerCode.Substring(spilterIndex + 1);
            }
            var svc = new CustomerService(dbFactory);
            var data = svc.GetCustomerByCode(profileNum, customerCode);
            return new Response<CustomerDataDto>(data);
        }

        [FunctionName(nameof(DeleteCustomer))]
        [OpenApiOperation(operationId: "DeleteCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "CustomerCode", Description = "CustomerCode = ProfileNumber-CustomerCode ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> DeleteCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "customers/{CustomerCode?}")] HttpRequest req,
            string CustomerCode,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0; ;
            var dbFactory = MyAppHelper.GetDatabase(masterAccountNum);
            var spilterIndex = CustomerCode.IndexOf("-");
            var customerCode = CustomerCode;
            if (spilterIndex > 0)
            {
                profileNum = CustomerCode.Substring(0, spilterIndex).ToInt();
                customerCode = CustomerCode.Substring(spilterIndex + 1);
            }
            var svc = new CustomerService(dbFactory);
            var result = svc.DeleteByCode(profileNum, customerCode);
            return new Response<string>("Delete customer result", result);
        }
        [FunctionName(nameof(AddCustomer))]
        [OpenApiOperation(operationId: "AddCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerDataDto), Description = "CustomerDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> AddCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "customers")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
                var profileNum = req.GetHeaderData<int>("profileNum") ?? 0; ;
                var dbFactory = MyAppHelper.GetDatabase(masterAccountNum);
                var svc = new CustomerService(dbFactory);

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var dto = JsonConvert.DeserializeObject<CustomerDataDto>(requestBody);

                var addresult = await svc.AddAsync(dto);
                return new OkObjectResult("Add customer success");
                //return new Response<string>("Delete customer result", addresult);
            }
            catch (System.Exception ex)
            {
                if (MySingletonAppSetting.DebugMode)
                {
                    return new ContentResult()
                    {
                        Content = ex.ObjectToString(),
                        ContentType = "application/json",
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                }
                else
                    return new BadRequestObjectResult("Server Internal Error");

            }
        }

        [FunctionName(nameof(UpdateCustomer))]
        [OpenApiOperation(operationId: "UpdateCustomer", tags: new[] { "Customers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomerDataDto), Description = "CustomerDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> UpdateCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "PATCH", Route = "customers")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0; ;
            var dbFactory = MyAppHelper.GetDatabase(masterAccountNum);
            var svc = new CustomerService(dbFactory);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var dto = JsonConvert.DeserializeObject<CustomerDataDto>(requestBody);

            var updateresult = svc.Update(dto);
            DbUtility.CloseConnection();
            return new Response<string>("Update customer result", updateresult);
        }
    }
}

