using DigitBridge.CommerceCentral.WebApi;
using Microsoft.Azure.Functions.Extensions.DependencyInjection; 

[assembly: FunctionsStartup(typeof(Startup))]
namespace DigitBridge.CommerceCentral.WebApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //todo init data or DI
        }
    }
}