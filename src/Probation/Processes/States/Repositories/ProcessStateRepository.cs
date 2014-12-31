using Probation.Processes.Common;
using Probation.Processes.Configurations;
using System;
using System.Diagnostics;
using System.Linq;

namespace Probation.Processes.States.Repositories
{
    internal class ProcessStateRepository : ProcessServiceBase, IProcessStateRepository
    {
        public IProcessState GetState(IProcessConfiguration processConfiguration)
        {
            var process = FindMatchingProcesses(processConfiguration).FirstOrDefault();
            var processState = GetProcessState(process);

            return processState;
        }

        private IProcessState GetProcessState(Process process)
        {
            var processId = default(int?);
            var cpuUsage = 0d;
            var memoryUsage = 0L;
            var uptime = TimeSpan.Zero;
            var timestamp = DateTime.UtcNow;

            if (process != null)
            {
                processId = process.Id;
                cpuUsage = process.GetCpuUtilization();
                memoryUsage = process.GetMemoryUsage();
                uptime = process.GetUptime();
            }

            var processState = new ProcessState(processId, cpuUsage, memoryUsage, uptime, timestamp);
            return processState;
        }
    }
}
