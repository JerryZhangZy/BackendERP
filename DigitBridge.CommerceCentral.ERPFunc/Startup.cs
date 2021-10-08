using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPFunc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(Startup))]

namespace DigitBridge.CommerceCentral.ERPFunc
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            //builder.AddExtension<BindingExtensionProvider>();
        }
    }
}