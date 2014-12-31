using System;

namespace Fixer.Processes.States
{
    internal class ProcessState : IProcessState
    {
        public static readonly ProcessState Default = new ProcessState(null, 0, 0, TimeSpan.Zero, DateTime.UtcNow);

        public int? Id { get; private set; }
        public bool IsRunning { get { return Id.HasValue; } }
        public double CpuUsage { get; private set; }
        public long MemoryUsage { get; private set; }
        public TimeSpan Uptime { get; private set; }
        public DateTime Timestamp { get; private set; }

        public ProcessState(int? id, double cpuUsage, long memoryUsage, TimeSpan uptime, DateTime timestamp)
        {
            Id = id;
            CpuUsage = cpuUsage;
            MemoryUsage = memoryUsage;
            Uptime = uptime;
            Timestamp = timestamp;
        }
    }
}
