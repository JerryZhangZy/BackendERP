using Microsoft.Azure.WebJobs.Host;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    //public static class FunctionFilterContextExtensions
    //{
    //    public static T GetContext<T>(this FunctionFilterContext executedContext) where T : class
    //    {
    //        foreach (var arg in executedContext.Arguments)
    //        {
    //            if (arg.Value.GetType() == typeof(T) || arg.Value.GetType().BaseType == typeof(T))
    //            {
    //                return arg.Value as T;
    //            }
    //        }
    //        return null;
    //    }
    //}
    public static class FunctionInvocationContextExtensions
    {
        public static T GetContext<T>(this FunctionInvocationContext executedContext) where T : class
        {
            foreach (var arg in executedContext.Arguments)
            {
                if (arg.Value.GetType() == typeof(T) || arg.Value.GetType().BaseType == typeof(T))
                {
                    return arg.Value as T;
                }
            }
            return null;
        }
    }
}
