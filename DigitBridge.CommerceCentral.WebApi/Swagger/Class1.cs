using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitBridge.CommerceCentral.WebApi
{
    public class AddSwaggerBizParametersFilters : IOperationFilter
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
        //}
    }

    //public class AddSwaggerBizParametersFilters : IOperationFilter
    //{
    //    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    //    {
    //        if (operation.Parameters == null)
    //        {
    //            operation.Parameters = new List<OpenApiParameter>();
    //        }

    //        var declareTypeAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true);
    //        var actionAttributes = context.MethodInfo.GetCustomAttributes(true);
    //        var totalAttributes = declareTypeAttributes.Concat(actionAttributes);
    //        ApplySwaggerOperationAttribute(operation, totalAttributes);

    //        var commonQueryParams = new List<string>() {
    //        "countrycode",
    //        "companycode"
    //        };
    //        commonQueryParams.ForEach(cqp => operation.Parameters.Add(new OpenApiParameter
    //        {
    //            Name = cqp,
    //            In = ParameterLocation.Query,
    //            Required = true,
    //            Description = cqp,
    //            Schema = new OpenApiSchema { Type = "string" }
    //        }));

    //        RemoveFixedParams(operation, context);
    //    }

    //    private void ApplySwaggerOperationAttribute(OpenApiOperation operation,
    //        IEnumerable<object> actionAttributes)
    //    {
    //        var swaggerOperationAttribute = actionAttributes
    //            .OfType<CustomSwaggerParamsAttribute>();

    //        if (swaggerOperationAttribute != null && swaggerOperationAttribute.EnumerableAny())
    //        {

    //            var paramType = new List<string>() { "header", "query" };

    //            var customerParams = swaggerOperationAttribute.First().CustomParams;
    //            customerParams.ForEach(cp =>
    //            {
    //                var paramsKv = cp.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
    //                if (paramsKv.Length == 2 && paramType.Contains(paramsKv[0].ToLower()))
    //                {
    //                    var paramsInStr = paramsKv[0].ToLower();
    //                    paramsInStr = paramsInStr.Substring(0, 1).ToUpper() + paramsInStr.Substring(1);
    //                    object paramsIn = ParameterLocation.Query;
    //                    Enum.TryParse(typeof(ParameterLocation), paramsInStr, out paramsIn);

    //                    operation.Parameters.Add(new OpenApiParameter
    //                    {
    //                        Name = paramsKv[1],
    //                        In = (ParameterLocation)paramsIn,
    //                        Schema = new OpenApiSchema { Type = "string" },
    //                        Description = paramsKv[1],
    //                        Required = true
    //                    });
    //                }
    //            });

    //        }
    //    }

    //    private void RemoveFixedParams(OpenApiOperation operation, OperationFilterContext context)
    //    {
    //        var declareTypeAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true);
    //        var methodAttributes = context.MethodInfo.GetCustomAttributes(true);

    //        var totalAttributes = declareTypeAttributes.Concat(methodAttributes);
    //        var removeParams = new List<string>();

    //        foreach (var attribute in totalAttributes.OfType<ExcludeFixedParameterAttribute>())
    //        {
    //            attribute.CustomParams.ForEach(dtap =>
    //            {
    //                var matchParam = operation.Parameters
    //                    .FirstOrDefault(p => p.Name.Equals(dtap, StringComparison.OrdinalIgnoreCase));
    //                if (matchParam != null)
    //                {
    //                    operation.Parameters.Remove(matchParam);
    //                }
    //            });
    //        }
    //    }
    //}
}
