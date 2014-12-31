using Fixer.Events;
using Fixer.Events.Common;
using Fixer.Events.Processes.Actions;
using Fixer.Processes.Common;
using Fixer.Processes.Configurations;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Fixer.Processes.Actions.Services
{
    internal class ProcessActionService : ProcessServiceBase, IProcessActionService
    {
        public void PerformAction(IProcessConfiguration processConfiguration, string action)
        {
            if (string.Equals(action, "start", StringComparison.OrdinalIgnoreCase))
                Start(processConfiguration);

            if (string.Equals(action, "restart", StringComparison.OrdinalIgnoreCase))
                Restart(processConfiguration);

            if (string.Equals(action, "stop", StringComparison.OrdinalIgnoreCase))
                Stop(processConfiguration);
        }

        private void Start(IProcessConfiguration processConfiguration)
        {
            EventBus.Raise(new ProcessStartBefore(processConfiguration));

            var processId = default(int?);

            try
            {
                var processes = FindMatchingProcesses(processConfiguration);

                if (!processes.Any())
                {
                    var process = Process.Start(processConfiguration.Start.File, processConfiguration.Start.Arguments);
                    processId = process.Id;
                }
            }
            catch (Exception ex)
            {
                EventBus.Raise(new ExceptionThrown(ex));
            }

            EventBus.Raise(new ProcessStartAfter(processConfiguration, processId));
        }

        private void Restart(IProcessConfiguration processConfiguration)
        {
            Stop(processConfiguration);
            Start(processConfiguration);
        }

        private void Stop(IProcessConfiguration processConfiguration)
        {
            EventBus.Raise(new ProcessStopBefore(processConfiguration));

            var success = false;
            var processes = FindMatchingProcesses(processConfiguration);

            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                    Thread.Sleep(processConfiguration.Stop.Duration);
                    success = true;
                }
                catch (Exception ex)
                {
                    EventBus.Raise(new ExceptionThrown(ex));
                }
            }

            EventBus.Raise(new ProcessStopAfter(processConfiguration, success));
        }
    }
}
