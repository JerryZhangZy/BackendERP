using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace FunctionApp1
{
    public class AddSwaggerBizParametersFilters : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

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
    }
}
