using System;

namespace Probation.Processes.States
{
    interface IProcessState
    {
        double CpuUsage { get; }
        int? Id { get; }
        bool IsRunning { get; }
        long MemoryUsage { get; }
        DateTime Timestamp { get; }
        TimeSpan Uptime { get; }
    }
}
