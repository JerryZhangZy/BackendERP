using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.WebApi
{
    [Obsolete]
    public class BaseApi : IFunctionInvocationFilter,IFunctionExceptionFilter
    {
        public Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
        {
            // todo unhandle exception
            throw new NotImplementedException();
        }

        public Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            // todo 
            throw new NotImplementedException();
        }

        public Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            // todo Authorize
            throw new NotImplementedException();
        }
    }  
}
