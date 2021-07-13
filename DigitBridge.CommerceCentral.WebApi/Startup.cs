using DigitBridge.CommerceCentral.WebApi;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace DigitBridge.CommerceCentral.WebApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //todo init data or DI
            //builder.Services.AddSwaggerGen(options =>
            //{
            //    options.OperationFilter<AddSwaggerBizParametersFilters>();
            //    //options.SchemaFilter<SwaggerExcludeFilter>();
            //    // UseFullTypeNameInSchemaIds replacement for .NET Core
            //    //options.CustomSchemaIds(x => x.FullName);
            //    //options.DescribeAllEnumsAsStrings();
            //});
        }
    }
}