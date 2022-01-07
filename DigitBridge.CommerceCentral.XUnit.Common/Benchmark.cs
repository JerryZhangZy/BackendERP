using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class Benchmark : IDisposable
    {
        public string _name;
        protected System.Diagnostics.Stopwatch watch;

        public                  Benchmark(string name) 
        {
            _name = name;
            System.Diagnostics.Debug.WriteLine($"{_name}-start");
            watch = System.Diagnostics.Stopwatch.StartNew();
        }

        void IDisposable.Dispose()
        {
            watch.Stop();
            var ms = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"{_name}-stop- {ms} ms");
        }
    }
}