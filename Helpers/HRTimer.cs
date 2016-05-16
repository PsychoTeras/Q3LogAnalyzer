using System.Runtime.InteropServices;

namespace Q3LogAnalyzer.Helpers
{
    public sealed class HRTimer
    {

#region P/Invoke

        [DllImport("kernel32.dll")]
        public static extern bool QueryPerformanceCounter(out long count);

        [DllImport("kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(out long frequency);

#endregion

#region Private fields

        private long _start;
        private double _result;
        private readonly long _frequency;

#endregion

#region Properties

        public double Result
        {
            get { return _result; }
        }

#endregion

#region Ctor

        public HRTimer()
        {
            QueryPerformanceFrequency(out _frequency);
        }

#endregion

#region Static methods

        public static HRTimer CreateAndStart()
        {
            HRTimer hr = new HRTimer();
            hr.StartWatch();
            return hr;
        }

#endregion

#region Class methods

        public void StartWatch()
        {
            _result = 0;
            QueryPerformanceCounter(out _start);
        }

        public double StopWatch()
        {
            long stop;
            QueryPerformanceCounter(out stop);
            _result = ((double)(stop - _start))/_frequency * 1000;
            return _result;
        }

#endregion

    }
}
