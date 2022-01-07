using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.WebApi
{
    /// <summary>
    /// common function handler
    /// </summary> 
    public class BaseApi : IFunctionInvocationFilter, IFunctionExceptionFilter
    {

        public Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
        {
            // todo unhandle exception
            return Task.CompletedTask;
        }

        public Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            // todo 
            return Task.CompletedTask;
        }

        public Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            // todo Authorize
            return Task.CompletedTask;
        }
    }
}
