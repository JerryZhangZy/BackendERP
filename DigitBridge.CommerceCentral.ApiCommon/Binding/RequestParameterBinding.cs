using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    /// <summary>
    /// Binding Provider
    /// </summary>
    //public class RequestParameterBindingProvider : IBindingProvider
    //{
    //    public Task<IBinding> TryCreateAsync(BindingProviderContext context)
    //    {
    //        IBinding binding = new RequestParameterBinding(context.Parameter.ParameterType);
    //        return Task.FromResult(binding);
    //    }
    //}

    //public class RequestParameterBinding : IBinding
    //{
    //    private readonly Type _instanceType;
    //    public RequestParameterBinding(Type type) => _instanceType = type;

    //    public Task<IValueProvider> BindAsync(BindingContext context)
    //    {
    //        // Get the HTTP request
    //        var request = context.BindingData["req"] as HttpRequest;

    //        return Task.FromResult<IValueProvider>(new RequestParameterValueProvider(request, _instanceType));
    //    }

    //    public bool FromAttribute => true;


    //    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
    //    {
    //        return null;
    //    }

    //    public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor();
    //}
    //public class RequestParameterValueProvider : IValueProvider
    //{
    //    private readonly HttpRequest _request;
    //    private readonly Type _instanceType;
    //    public Type Type => _instanceType ?? typeof(object);

    //    public RequestParameterValueProvider(HttpRequest request, Type instanceType)
    //    {
    //        _request = request;
    //        _instanceType = instanceType;
    //    }
    //    public async Task<object> GetValueAsync()
    //    {
    //        try { return _request.GetRequestParameter(_instanceType); }
    //        catch(Exception ex)
    //        {
    //            throw new Exception("Get parameter error", ex);
    //        }
            
    //    }
    //    public string ToInvokeString() => string.Empty;
    //}


}
