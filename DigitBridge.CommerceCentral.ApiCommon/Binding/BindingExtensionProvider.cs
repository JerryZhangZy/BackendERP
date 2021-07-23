using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.Azure.WebJobs.Host.Config;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    /// <summary>
    /// register RequestParameterAttribute  
    /// </summary>

    public class BindingExtensionProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            // Creates a rule that links the attribute to the binding
            context.AddBindingRule<FromBodyBindingAttribute>().Bind(new FromBodyBindingProvider());
            context.AddBindingRule<RequestParameterAttribute>().Bind(new RequestParameterBindingProvider());
        }
    }
}
