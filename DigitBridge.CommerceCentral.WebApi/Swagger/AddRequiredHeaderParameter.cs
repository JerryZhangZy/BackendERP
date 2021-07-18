using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace DigitBridge.CommerceCentral.WebApi
{
    public class RequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "masterAccountNum",
                In = ParameterLocation.Header,
                Description = "From login masterAccountNum",
                Required = true,
                AllowEmptyValue = false
                //Schema = new OpenApiSchema()
                //{
                //    Type = typeof(int).ToString(),
                //    //Nullable = false, 
                //}
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "profileNum",
                In = ParameterLocation.Header,
                Description = "From login profileNum",
                Required = true,
                AllowEmptyValue = false
                //Schema = new OpenApiSchema()
                //{
                //    Type = typeof(int).ToString(),
                //    Minimum=1
                //    //Nullable = false
                //}
            });
        }
    }
}
