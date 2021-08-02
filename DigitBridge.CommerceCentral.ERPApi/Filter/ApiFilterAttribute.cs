
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
            if (MySingletonAppSetting.DebugMode)
            {
                exceptionContext.Logger.LogInformation(exceptionContext.Exception.ObjectToString());
            }
            ((RecoverableException)exceptionContext.ExceptionDispatchInfo.SourceException).Handled = true;
        }
        //Dictionary<Guid, HttpRequest> exceptionReqs = new Dictionary<Guid, HttpRequest>();

        /// <summary>
        /// override exception respone
        /// </summary>
        /// <param name="executedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            var exception = executedContext.FunctionResult.Exception;
            if (exception != null)
            {
                //exceptionReqs.Add(executedContext.FunctionInstanceId, executedContext.GetContext<HttpRequest>()); 

                var req = executedContext.GetContext<HttpRequest>();
                var isInvalidParameterException = exception.InnerException is InvalidParameterException;
                if (!isInvalidParameterException)
                {

                    //var methodInfo = _currentType.GetMethod(executedContext.FunctionName);
                    //var bodyType = methodInfo?.GetCustomAttribute<OpenApiRequestBodyAttribute>()?.BodyType; 
                    //var parameters = await req.ToDictionary(executedContext.FunctionName, bodyType);

                    // write log to log center
                    var methodInfo = _currentType.GetMethod(executedContext.FunctionName);
                    var parmeters = methodInfo?.GetCustomAttributes<OpenApiParameterAttribute>();  
                    var reqInfo = await LogHelper.GetRequestInfo(req, executedContext.FunctionName, parmeters);
                    var excepitonMessageID = LogCenter.CaptureException(exception, reqInfo);
                    var data = new ResponseResult<string>($"A general error occured. Error ID: {excepitonMessageID}", false);
                    await req.HttpContext.Response.Output(data);
                }
                else
                {
                    var data = new ResponseResult<Exception>(exception, false);
                    await req.HttpContext.Response.Output(data);
                }

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
                    var parameterValue = req.GetData(item.Name, item.In);
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
