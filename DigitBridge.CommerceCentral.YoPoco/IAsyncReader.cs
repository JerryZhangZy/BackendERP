using System;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public interface IAsyncReader<out T> : IDisposable
    {
        T Poco { get; }

        Task<bool> ReadAsync();
    }
}