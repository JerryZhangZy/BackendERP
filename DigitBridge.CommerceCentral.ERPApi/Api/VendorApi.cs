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
    [ApiFilter(typeof(VendorApi))]
    public static class VendorApi
    {
        [FunctionName(nameof(GetVendor))]
        [OpenApiOperation(operationId: "GetVendor", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "VendorCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "VendorCode", Description = "VendorCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorPayloadGetSingle))]
        public static async Task<JsonNetResponse<VendorPayload>> GetVendor(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "vendors/{VendorCode}")] HttpRequest req,
            string VendorCode = null)
        {
            var payload = await req.GetParameters<VendorPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new VendorService(dbFactory);

            var spilterIndex = VendorCode.IndexOf("-");
            var vendorCode = VendorCode;
            if (spilterIndex > 0 && vendorCode.StartsWith(payload.ProfileNum.ToString()))
            {
                vendorCode = VendorCode.Substring(spilterIndex + 1);
            }
            if (await svc.GetVendorByVendorCodeAsync(payload, vendorCode))
                payload.Vendor = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<VendorPayload>(payload);

        }

        [FunctionName(nameof(ExistVendorCode))]
        [OpenApiOperation(operationId: "ExistVendorCode", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "VendorCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "VendorCode", Description = "VendorCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExistVendorCodePayload))]
        public static async Task<JsonNetResponse<ExistVendorCodePayload>> ExistVendorCode(
    [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "vendors/existvendorCode/{VendorCode}")] HttpRequest req,
    string VendorCode = null)
        {
            var payload = await req.GetParameters<ExistVendorCodePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new VendorService(dbFactory);

            var spilterIndex = VendorCode.IndexOf("-");
            var vendorCode = VendorCode;
            if (spilterIndex > 0 && vendorCode.StartsWith(payload.ProfileNum.ToString()))
            {
                vendorCode = VendorCode.Substring(spilterIndex + 1);
            }
            if (await svc.GetVendorByVendorCodeAsync(new VendorPayload() { MasterAccountNum= payload.MasterAccountNum,ProfileNum=payload.ProfileNum }, vendorCode))
                payload.IsExistVendorCode =   svc.ToDto()!=null;
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<ExistVendorCodePayload>(payload);

        }



        [FunctionName(nameof(GetMultiVendors))]
        [OpenApiOperation(operationId: "GetMultiVendors", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "VendorCodes", In = ParameterLocation.Query, Required = true, Type = typeof(List<string>), Summary = "VendorCodes", Description = "VendorCode Array", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorPayloadGetMultiple))]
        public static async Task<JsonNetResponse<VendorPayload>> GetMultiVendors(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "vendors")] HttpRequest req)
        {
            var payload = await req.GetParameters<VendorPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new VendorService(dbFactory);
            payload = await svc.GetVendorsByCodeArrayAsync(payload);
            return new JsonNetResponse<VendorPayload>(payload);

        }

        [FunctionName(nameof(DeleteVendor))]
        [OpenApiOperation(operationId: "DeleteVendor", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "VendorCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "VendorCode", Description = "VendorCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorPayloadDelete), Description = "The OK response")]
        public static async Task<JsonNetResponse<VendorPayload>> DeleteVendor(
            [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "vendors/{VendorCode}")] HttpRequest req,
            string VendorCode)
        {
            var payload = await req.GetParameters<VendorPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new VendorService(dbFactory);
            var spilterIndex = VendorCode.IndexOf("-");
            var vendorCode = VendorCode;
            if (spilterIndex > 0 && vendorCode.StartsWith(payload.ProfileNum.ToString()))
            {
                vendorCode = VendorCode.Substring(spilterIndex + 1);
            }
            payload.VendorCodes.Add(vendorCode);
            if (await svc.DeleteByCodeAsync(payload, vendorCode))
                payload.Vendor = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<VendorPayload>(payload);
        }

        [FunctionName(nameof(AddVendor))]
        [OpenApiOperation(operationId: "AddVendor", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(VendorPayloadAdd), Description = "VendorDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorPayloadAdd))]
        public static async Task<JsonNetResponse<VendorPayload>> AddVendor(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "vendors")] HttpRequest req)
        {
            var payload = await req.GetParameters<VendorPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new VendorService(dbFactory);
            if (await svc.AddAsync(payload))
                payload.Vendor = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<VendorPayload>(payload);
        }

        [FunctionName(nameof(UpdateVendor))]
        [OpenApiOperation(operationId: "UpdateVendor", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(VendorPayloadUpdate), Description = "VendorDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorPayloadUpdate))]
        public static async Task<JsonNetResponse<VendorPayload>> UpdateVendor(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "vendors")] HttpRequest req)
        {
            var payload = await req.GetParameters<VendorPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new VendorService(dbFactory);
            if (await svc.UpdateAsync(payload))
                payload.Vendor = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<VendorPayload>(payload);
        }

        /// <summary>
        /// Load vendor list
        /// </summary>
        [FunctionName(nameof(VendorsList))]
        [OpenApiOperation(operationId: "VendorsList", tags: new[] { "Vendors" }, Summary = "Load vendor list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(VendorPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorPayloadFind))]
        public static async Task<JsonNetResponse<VendorPayload>> VendorsList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "vendors/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<VendorPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new VendorList(dataBaseFactory, new VendorQuery());
            await srv.GetVendorListAsync(payload);
            return new JsonNetResponse<VendorPayload>(payload);
        }

        /// <summary>
        /// Load vendor list
        /// </summary>
        [FunctionName(nameof(VendorDataList))]
        [OpenApiOperation(operationId: "VendorDataList", tags: new[] { "Vendors" }, Summary = "Load vendor list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(VendorPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorPayloadFind))]
        public static async Task<JsonNetResponse<VendorPayload>> VendorDataList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "vendors/finddata")] HttpRequest req)
        {
            var payload = await req.GetParameters<VendorPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new VendorList(dataBaseFactory, new VendorQuery());
            await srv.GetExportJsonListAsync(payload);
            return new JsonNetResponse<VendorPayload>(payload);
        }

        /// <summary>
        /// Add vendor
        /// </summary>
        [FunctionName(nameof(Sample_Vendor_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "VendorAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of vendor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorPayloadAdd))]
        public static async Task<JsonNetResponse<VendorPayloadAdd>> Sample_Vendor_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/vendors")] HttpRequest req)
        {
            return new JsonNetResponse<VendorPayloadAdd>(VendorPayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find vendor
        /// </summary>
        [FunctionName(nameof(Sample_Vendor_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "VendorFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of vendor find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorPayloadFind))]
        public static async Task<JsonNetResponse<VendorPayloadFind>> Sample_Vendor_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/vendors/find")] HttpRequest req)
        {
            return new JsonNetResponse<VendorPayloadFind>(VendorPayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportVendor))]
        [OpenApiOperation(operationId: "ExportVendor", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(VendorPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportVendor(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "vendors/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<VendorPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new VendorManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-vendor.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportVendor))]
        [OpenApiOperation(operationId: "ImportVendor", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorPayload))]
        public static async Task<VendorPayload> ImportVendor(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "vendors/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<VendorPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new VendorManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }


        /// <summary>
        /// AddVendorAdress
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddVendorAdress))]
        [OpenApiOperation(operationId: "AddVendorAdress", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(VendorAddressPayloadAdd), Description = "VendorAdressDataDto")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorAddressPayloadAdd))]
        public static async Task<JsonNetResponse<VendorAddressPayload>> AddVendorAdress(
         [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "vendors/address")] HttpRequest req)
        {
            var payload = await req.GetParameters<VendorAddressPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new VendorService(dbFactory);
            if (await svc.AddVendorAddressAsync(payload))
            {
                payload.VendorAddress = svc.ToVendorAddressDto();
            }
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<VendorAddressPayload>(payload);
        }

        [FunctionName(nameof(UpdateVendorAdress))]
        [OpenApiOperation(operationId: "UpdateVendorAdress", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(VendorAddressPayloadUpdate), Description = "VendorAdressDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorAddressPayloadUpdate))]
        public static async Task<JsonNetResponse<VendorAddressPayload>> UpdateVendorAdress(
    [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "vendors/address")] HttpRequest req)
        {
            var payload = await req.GetParameters<VendorAddressPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new VendorService(dbFactory);
            if (await svc.UpdateVendorAddressAsync(payload))
                payload.VendorAddress = svc.ToVendorAddressDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<VendorAddressPayload>(payload);
        }

        /// <summary>
        /// DeleteVendorAdress
        /// </summary>
        /// <param name="req"></param>
        /// <param name="addressUuid"></param>
        /// <returns></returns>

        [FunctionName(nameof(DeleteVendorAdress))]
        [OpenApiOperation(operationId: "DeleteVendorAdress", tags: new[] { "Vendors" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "vendorCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "vendorCode", Description = "vendorCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "addressCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "addressCode", Description = "addressCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorAddressPayloadDelete), Description = "The OK response")]
        public static async Task<JsonNetResponse<VendorAddressPayload>> DeleteVendorAdress(
    [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "vendors/address/{vendorCode}/{addressCode}")] HttpRequest req,
    string vendorCode,string addressCode)
        {
            var payload = await req.GetParameters<VendorAddressPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new VendorService(dataBaseFactory);
            payload.Success = await srv.DeleteVendorAddressAsync(payload.MasterAccountNum,payload.ProfileNum,vendorCode, addressCode);
            if (payload.Success)
                payload.VendorAddress = srv.ToVendorAddressDto();
            payload.Messages = srv.Messages;
            return new JsonNetResponse<VendorAddressPayload>(payload);
        }


        /// <summary>
        /// Add VendorAddress Sample
        /// </summary>
        [FunctionName(nameof(Sample_VendorAddress_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "VendorAddressSample", tags: new[] { "Sample" }, Summary = "Get new sample of vendor address")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VendorAddressPayloadAdd))]
        public static async Task<JsonNetResponse<VendorAddressPayloadAdd>> Sample_VendorAddress_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/vendorAddress")] HttpRequest req)
        {
            return new JsonNetResponse<VendorAddressPayloadAdd>(VendorAddressPayloadAdd.GetSampleData());
        }
    }
}

