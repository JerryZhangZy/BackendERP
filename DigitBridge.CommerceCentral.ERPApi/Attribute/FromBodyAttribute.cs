using DigitBridge.CommerceCentral.ApiCommon;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Host.Protocols;
using System;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// register FromBodyAttribute  
    /// </summary>
    public class BindingExtensionProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {

            // Creates a rule that links the attribute to the binding
            context.AddBindingRule<FromBodyBindingAttribute>().Bind(new FromBodyBindingProvider());
            //todo bind to specified type
            //context.AddBindingRule<FromBodyBindingAttribute>().BindToCollector(attribute => new FromBodyBindingProvider(attribute.InstanceType));

        }
    }

    /// <summary>
    /// Binding Provider
    /// </summary>
    public class FromBodyBindingProvider : IBindingProvider
    {
        private readonly Type _instanceType;
        public FromBodyBindingProvider(Type type) => _instanceType = type;
        public FromBodyBindingProvider() { }
        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            //var parameter = context.Parameter;
            IBinding binding = new FromBodyBinding(_instanceType);
            return Task.FromResult(binding);
        }
    }

    public class FromBodyBinding : IBinding
    {
        private readonly Type _instanceType;
        public FromBodyBinding(Type type) => _instanceType = type;

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            // Get the HTTP request
            var request = context.BindingData["req"] as HttpRequest;

            return Task.FromResult<IValueProvider>(new FromBodyValueProvider(request, _instanceType));
        }

        public bool FromAttribute => true;


        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            return null;
        }

        public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor();
    }

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Binding]
    public sealed class FromBodyBindingAttribute : Attribute
    {
        public Type InstanceType { get; }
        public FromBodyBindingAttribute(Type type) => InstanceType = type;
        public FromBodyBindingAttribute() { }
    }

    public class FromBodyValueProvider : IValueProvider
    {
        private readonly HttpRequest _request;
        private readonly Type _instanceType;
        public Type Type => _instanceType ?? typeof(object);

        public FromBodyValueProvider(HttpRequest request, Type instanceType)
        {
            _request = request;
            _instanceType = instanceType;
        }

        public async Task<object> GetValueAsync()
        {
            try
            {
                return _request.GetBodyObjectAsync<object>();
            }
            catch (Exception ex)
            {
                throw new Exception("Deserialize http request body to object error", ex);
            }
        }
        public string ToInvokeString() => string.Empty;
    }


}
