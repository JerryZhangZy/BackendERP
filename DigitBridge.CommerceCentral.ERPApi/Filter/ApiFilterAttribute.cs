
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
        /// Mark exception handled 
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
                var req = executedContext.GetContext<HttpRequest>();
                var hasException = !(exception is InvalidParameterException
                    || exception is NoContentException
                    || exception is InvalidRequestException);
                var needLog = hasException && !MySingletonAppSetting.DebugMode;
                var logMessage = string.Empty;
                if (needLog)
                {
                    logMessage = await WriteLog(executedContext.FunctionName, exception, req);
                }

                var data = needLog ? logMessage : (hasException ? exception.ObjectToString() : exception.Message);

                // anyway write response
                await req.HttpContext.Response.Output(data);
            }
        }

        public async Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            // todo Authorize  

            var methodInfo = _currentType.GetMethod(executingContext.FunctionName);
            var openApiParameterAttributes = methodInfo?.GetCustomAttributes<OpenApiParameterAttribute>().Where(i => i.Required);
            var messages = new List<string>();
            var req = executingContext.GetContext<HttpRequest>();
            if (openApiParameterAttributes != null)
            {
                foreach (var item in openApiParameterAttributes)
                {
                    var parameterValue = req.GetData(item.Name, item.In);
                    if (parameterValue == null)
                    {
                        messages.Add($"Parameter {item.Name} is required in {item.In}");
                    }
                    else if((item.Type == typeof(int) && parameterValue.ToInt() <= 0)
                        || (item.Type == typeof(long) && parameterValue.ToLong() <= 0)
                        || (item.Type == typeof(IList<string>) && parameterValue.SplitTo<string>().Count() == 0)
                        )
                    {
                        messages.Add($"Parameter {item.Name} is invalid");
                    }
                }
            }
            if (messages.Count > 0)
            {
                await req.HttpContext.Response.Output(messages.ObjectToString());
                //interrupt function call
                throw new InvalidParameterException();
            }
        }
        private async Task<string> WriteLog(string functionName, Exception exception, HttpRequest req)
        {
            //var methodInfo = _currentType.GetMethod(executedContext.FunctionName);
            //var bodyType = methodInfo?.GetCustomAttribute<OpenApiRequestBodyAttribute>()?.BodyType; 
            //var parameters = await req.ToDictionary(executedContext.FunctionName, bodyType);

            // write log to log center
            var methodInfo = _currentType.GetMethod(functionName);
            var parmeters = methodInfo?.GetCustomAttributes<OpenApiParameterAttribute>();
            var reqInfo = await LogHelper.GetRequestInfo(req, functionName, parmeters);
            var logID = LogCenter.CaptureException(exception, reqInfo);
            return $"A general error occured. Error ID: {logID}";
        }
    }
}
