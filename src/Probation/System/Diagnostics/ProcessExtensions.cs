using System.Threading;

namespace System.Diagnostics
{
    internal static class ProcessExtensions
    {
        public static double GetCpuUtilization(this Process process)
        {
            if (process == null)
                return 0;

            try
            {
                var pid = process.Id;
                var duration = TimeSpan.FromMilliseconds(100);

                //todo: this is a rough approximation
                var totalProcessorTime1 = System.Diagnostics.Process.GetProcessById(pid).TotalProcessorTime;
                Thread.Sleep(duration);
                var totalProcessorTime2 = System.Diagnostics.Process.GetProcessById(pid).TotalProcessorTime;

                var totalProcessorTime = totalProcessorTime2 - totalProcessorTime1;
                var cpuUsage = totalProcessorTime.TotalMilliseconds / duration.TotalMilliseconds;
                cpuUsage /= Environment.ProcessorCount;
                cpuUsage *= 100;

                return cpuUsage;
            }
            catch
            {
                //process exited
                return 0;
            }
        }

        public static long GetMemoryUsage(this Process process)
        {
            if (process == null)
                return 0;

            try
            {
                var perfCounter = new PerformanceCounter();
                perfCounter.CategoryName = "Process";
                perfCounter.CounterName = "Working Set - Private";
                perfCounter.InstanceName = process.ProcessName;

                var bytes = (int)perfCounter.NextValue();
                return bytes;
            }
            catch
            {
                //process exited
                return 0;
            }
        }

        public static TimeSpan GetUptime(this Process process)
        {
            if (process == null)
                return TimeSpan.Zero;
            try
            {
                var utcStartTime = process.StartTime.ToUniversalTime();
                return DateTime.UtcNow - utcStartTime;
            }
            catch
            {
                //process exited
                return TimeSpan.Zero;
            }
        }
    }
}
