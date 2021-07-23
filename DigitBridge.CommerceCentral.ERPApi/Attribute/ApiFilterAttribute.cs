
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]

    public class ApiFilterAttribute : Attribute, IFunctionInvocationFilter, IFunctionExceptionFilter
    {
        /// <summary>
        /// get caller type
        /// </summary>
        private Type _currentType;
        public ApiFilterAttribute(Type currentType)
        {
            this._currentType = currentType;
        }
        /// <summary>
        /// mark exception handled 
        /// todo:write log 
        /// </summary>
        /// <param name="exceptionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
        {
            var isInvalidParameterException = exceptionContext.Exception.InnerException is InvalidParameterException;
            if (!isInvalidParameterException)
            {
                //todo record
            }

            ((RecoverableException)exceptionContext.ExceptionDispatchInfo.SourceException).Handled = true;
            if (MySingletonAppSetting.DebugMode)
            {
                Exception ex = exceptionContext.Exception;
                exceptionContext.Logger.Log<object>(LogLevel.Debug, 1, null, exceptionContext.Exception,
                    (object obj, Exception ex) => ex.ObjectToString()
                );

                //var text = "Some text"; // text variable
                //HttpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //httpContextAccessor.HttpContext.Response.ContentType = "text/html; charset=UTF-8"; // Missing part
                //httpContextAccessor.HttpContext.Response.Headers.Append("MyHeader", "Foo");
                //await httpContextAccessor.HttpContext.Response.WriteAsync(text);
                //httpContextAccessor.HttpContext.Response.ContentLength = text.Length; // Missing part            }
            }
        }

        /// <summary>
        /// override exception respone
        /// </summary>
        /// <param name="executedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            if (executedContext.FunctionResult.Exception != null)
            {
                var data = new ResponseResult<Exception>(executedContext.FunctionResult.Exception);
                var req = executedContext.GetContext<HttpRequest>();
                await req.HttpContext.Response.Output(data, data.StatusCode);
            }
        }

        public async Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            var methodInfo = _currentType.GetMethod(executingContext.FunctionName);
            var openApiParameterAttributes = methodInfo?.GetCustomAttributes<OpenApiParameterAttribute>().Where(i => i.Required);
            if (openApiParameterAttributes != null)
            {
                var req = executingContext.GetContext<HttpRequest>();
                foreach (var item in openApiParameterAttributes)
                {
                    var parameterValue = req.GetData(item.Name);
                    if (parameterValue == null)
                    {
                        var message = $"parameter {item.Name} is required";
                        await req.HttpContext.Response.Output(message);
                        throw new InvalidParameterException(message);
                    }
                }
            } 
            // todo Authorize  
        }
    }
}
