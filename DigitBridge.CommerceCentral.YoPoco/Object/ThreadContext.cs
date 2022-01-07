//using System.Runtime.Remoting.Messaging;

using System.Collections.Concurrent;
using System.Threading;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    /// Create a thread safe static object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct ThreadContext<T>
    {
        public ThreadContext(string name) { Name = name; }
        public ThreadContext(string name, T value) { Name = name; Set(value); }
        public string       Name { get; } 
        public T            Value => (T)CallContext.LogicalGetData(Name);
        public T            Set(T value) { CallContext.LogicalSetData(Name, value); return value; }
        public void         Clear() => CallContext.LogicalSetData(Name, null);
    }

    /// <summary>
    /// Cause CallContext is not exist in .net core
    /// create the following static  class mimicking the CallContext API
    /// </summary>
    public sealed class CallContext
    {
        static readonly ConcurrentDictionary<string, AsyncLocal<object>> state = new ConcurrentDictionary<string, AsyncLocal<object>>();

        public static object HostContext { get; set; }
        public static void LogicalSetData(string name, object data) =>
            state.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;
        public static object LogicalGetData(string name) =>
          state.TryGetValue(name, out var data) ? data.Value : null;

        public static void SetData(string name, object data) =>
           state.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;
        public static object GetData(string name) =>
          state.TryGetValue(name, out var data) ? data.Value : null;

    }
}