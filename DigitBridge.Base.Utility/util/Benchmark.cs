using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Utility
{
    public partial class Util
    {
        public class Benchmark : IDisposable
        {
            public string _name;
            protected System.Diagnostics.Stopwatch watch;

            public Benchmark(string name)
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
}
