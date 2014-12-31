using System;

namespace Fixer.Processes.States
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
