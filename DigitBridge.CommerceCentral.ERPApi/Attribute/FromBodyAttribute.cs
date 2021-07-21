﻿using DigitBridge.CommerceCentral.ApiCommon;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Newtonsoft.Json;
using System;
using System.IO;
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
        }
    }

    /// <summary>
    /// Binding Provider
    /// </summary>
    public class FromBodyBindingProvider : IBindingProvider
    {
        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            IBinding binding = new FromBodyBinding(context.Parameter.ParameterType);
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
                return await _request.GetBodyObjectAsync(_instanceType);
            }
            catch (Exception ex)
            {
                throw new Exception("Deserialize http request body to object error", ex);
            }
        }
        public string ToInvokeString() => string.Empty;
    }


}
