﻿using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using DigitBridge.CommerceCentral.WebApi;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;

[assembly: WebJobsStartup(typeof(Startup))]

namespace DigitBridge.CommerceCentral.WebApi
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            
            //Register the extension
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts =>
            {
                opts.SpecVersion = OpenApiSpecVersion.OpenApi3_0;
                opts.AddCodeParameter = false;
                opts.PrependOperationWithRoutePrefix = false;
                opts.XmlPath = "DigitBridge.CommerceCentral.WebApi.xml";
                opts.Documents = new[]
                {
                    new SwaggerDocument
                    {
                        Name = "v1",
                        Title = "Swagger document",
                        Description = "Swagger test document",
                        Version = "v2"
                    },
                    new SwaggerDocument
                    {
                        Name = "v2",
                        Title = "Swagger document 2",
                        Description = "Swagger test document 2",
                        Version = "v2"
                    }
                };
                opts.Title = "Swagger Test";
                //opts.OverridenPathToSwaggerJson = new Uri("http://localhost:7071/api/Swagger/json");
                opts.ConfigureSwaggerGen = x =>
                {
                    x.OperationFilter<RequiredHeaderParameter>();
                    //custom operation example
                    x.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
                        ? methodInfo.Name
                        : new Guid().ToString());

                    //custom filter example
                    //x.DocumentFilter<RemoveSchemasFilter>();
                    x.IncludeXmlComments("DigitBridge.CommerceCentral.ERPDb.xml", true);
                    //oauth2
                    x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri("https://your.idserver.net/connect/authorize"),
                                Scopes = new Dictionary<string, string>
                                {
                                    { "api.read", "Access read operations" },
                                    { "api.write", "Access write operations" }
                                }
                            }
                        }
                    });
                };

                // set up your client ID if your API is protected
                opts.ClientId = "your.client.id";
                opts.OAuth2RedirectPath = "http://localhost:7071/api/swagger/oauth2-redirect";

            });
        }
    }
}