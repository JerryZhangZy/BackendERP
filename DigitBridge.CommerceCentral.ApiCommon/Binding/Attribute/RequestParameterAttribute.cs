using Microsoft.Azure.WebJobs.Description;
using System;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Binding]
    public sealed class RequestParameterAttribute : Attribute
    {
    }

}
