using Microsoft.Azure.WebJobs.Host;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Abstractions;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ApiCommon;

namespace DigitBridge.CommerceCentral.ERPApi
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]

    public class CommonAttribute : Attribute, IFunctionInvocationFilter, IFunctionExceptionFilter
    {
        /// <summary>
        /// mark exception handled 
        /// todo:write log 
        /// </summary>
        /// <param name="exceptionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
        {
            //todo record
            ((RecoverableException)exceptionContext.ExceptionDispatchInfo.SourceException).Handled = true;
        }

        /// <summary>
        /// override exception respone
        /// </summary>
        /// <param name="executedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            var req = executedContext.GetContext<HttpRequest>();
            if (executedContext.FunctionResult.Exception != null && req != null)
            {
                var data = new ResponseResult<Exception>(executedContext.FunctionResult.Exception);
                var jsonData = JsonConvert.SerializeObject(data);
                var response = req.HttpContext.Response;
                response.ContentType = "application/json;charset=utf-8;";
                response.StatusCode = (int)data.StatusCode;
                var bytes = Encoding.UTF8.GetBytes(jsonData);
                req.HttpContext.Response.ContentLength = bytes.Length;
                await response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        public Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        { 
            return Task.CompletedTask;
            // todo Authorize  
        }
    }
}
