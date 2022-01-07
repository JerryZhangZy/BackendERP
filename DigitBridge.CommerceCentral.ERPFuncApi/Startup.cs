using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPFuncApi;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(Startup))]

namespace DigitBridge.CommerceCentral.ERPFuncApi
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<BindingExtensionProvider>();
        }
    }
}