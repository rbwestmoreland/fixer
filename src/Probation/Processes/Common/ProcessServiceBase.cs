using Probation.Processes.Configurations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace Probation.Processes.Common
{
    internal abstract class ProcessServiceBase
    {
        protected IEnumerable<Process> FindMatchingProcesses(IProcessConfiguration processConfiguration)
        {
            var processes = new List<Process>();
            var processIds = FindMatchingProcessIds(processConfiguration);

            foreach (var processId in processIds)
            {

                try
                {
                    var process = Process.GetProcessById(processId);
                    processes.Add(process);
                }
                catch { }
            }

            return processes;
        }

        protected IEnumerable<int> FindMatchingProcessIds(IProcessConfiguration processConfiguration)
        {
            var processIds = new List<int>();

            var command = string.Format("\"{0}\" {1}", processConfiguration.Start.File, processConfiguration.Start.Arguments);
            var wmiQuery = string.Format("select ProcessId, Name, CommandLine from Win32_Process");
            var searcher = new ManagementObjectSearcher(wmiQuery);

            foreach (ManagementObject obj in searcher.Get())
            {
                var processId = (int)(UInt32)obj["ProcessId"];
                var commandLine = (string)obj["CommandLine"];

                if (command.Equals(commandLine))
                    processIds.Add(processId);
            }

            return processIds;
        }
    }
}
