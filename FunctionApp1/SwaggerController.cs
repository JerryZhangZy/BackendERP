using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Function1
{
    public class AddRequiredHeaderParameter : IOperationFilter,IDocumentFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            //var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            //if (descriptor != null && !descriptor.ControllerName.StartsWith("Weather"))
            //{
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "timestamp",
                In = ParameterLocation.Query,
                Description = "The timestamp of now",
                Required = true
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "nonce",
                In = ParameterLocation.Query,
                Description = "The random value",
                Required = true
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "sign",
                In = ParameterLocation.Query,
                Description = "The signature",
                Required = true
            });
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            throw new System.NotImplementedException();
        }
        //}
    }
    public static class SwaggerController
    {
        [SwaggerIgnore]
        [FunctionName("SwaggerJson")]
        public static Task<HttpResponseMessage> SwaggerJson(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Swagger/json")]
            HttpRequestMessage req,
            [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return Task.FromResult(swashBuckleClient.CreateSwaggerJsonDocumentResponse(req));
        }

        [SwaggerIgnore]
        [FunctionName("SwaggerYaml")]
        public static Task<HttpResponseMessage> SwaggerYaml(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Swagger/yaml")]
            HttpRequestMessage req,
            [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return Task.FromResult(swashBuckleClient.CreateSwaggerYamlDocumentResponse(req));
        }

        [SwaggerIgnore]
        [FunctionName("SwaggerUi")]
        public static Task<HttpResponseMessage> SwaggerUi(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Swagger/ui")]
            HttpRequestMessage req,
            [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return Task.FromResult(swashBuckleClient.CreateSwaggerUIResponse(req, "swagger/json"));
        }

        /// <summary>
        /// This is only needed for OAuth2 client. This redirecting document is normally served
        /// as a static content. Functions don't provide this out of the box, so we serve it here.
        /// Don't forget to set OAuth2RedirectPath configuration option to reflect this route.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="swashBuckleClient"></param>
        /// <returns></returns>
        [SwaggerIgnore]
        [FunctionName("SwaggerOAuth2Redirect")]
        public static Task<HttpResponseMessage> SwaggerOAuth2Redirect(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger/oauth2-redirect")]
            HttpRequestMessage req,
            [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return Task.FromResult(swashBuckleClient.CreateSwaggerOAuth2RedirectResponse(req));
        }
    }
}