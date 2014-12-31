using System;
using System.Diagnostics;
using System.Threading;

namespace ProbationProcess
{
    class Program
    {
        static void Main()
        {
            var utilization = 3;

            Console.WriteLine("Utilizing {0}% of CPU resources.", utilization);

            utilization = utilization * Environment.ProcessorCount;
            var stopwatch = Stopwatch.StartNew();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > utilization)
                {
                    Thread.Sleep(100 - utilization);
                    stopwatch.Reset();
                    stopwatch.Start();
                }
            }
        }
    }
}
