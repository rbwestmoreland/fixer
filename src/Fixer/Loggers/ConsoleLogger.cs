using Fixer.Events;
using Fixer.Events.Applications;
using Fixer.Events.Applications.Configurations;
using Fixer.Events.Common;
using Fixer.Events.Processes.Actions;
using Fixer.Events.Processes.Conditions;
using Fixer.Events.Processes.Configurations;
using System;

namespace Fixer.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger()
        {
            Console.CursorVisible = false;
            ListenToCommonEvents();
            ListenToAppictionEvents();
            ListenToProcessEvents();
            ListenToNotificationEvents();
        }

        private void OutputLine(string type, string area, string messageFormat, params object[] args)
        {
            Console.WriteLine();

            var message = string.Format(messageFormat, args);

            if (message.Length > 59)
            {
                message = message.Substring(0, 59);
            }

            var output = string.Format("{0,-6} │ {1,-8} │ {2}", type, area, message);

            Console.Write(output);
        }

        private void OutputDivider()
        {
            Console.WriteLine();
            var output = new string('─', Console.WindowWidth - 1);
            Console.Write(output);
        }

        private void ListenToCommonEvents()
        {
            EventBus.Register<ExceptionThrown>(e =>
            {
                OutputLine("Error", "global", e.Exception.Message);
            });
        }

        private void ListenToAppictionEvents()
        {
            EventBus.Register<ApplicationConfigurationLoadBefore>(e =>
            {
                OutputLine("Info", "App", "Loading Application Configuration... ");
            });

            EventBus.Register<ApplicationConfigurationLoadAfter>(e =>
            {
                if (e.ApplicationConfiguration == null)
                {
                    var path = e.Path.Length > 40 ? e.Path.Substring(e.Path.Length - 40, 40) : e.Path;
                    OutputLine("Info", "App", "Error Loading... {0} ", path);
                }
                else
                {
                    Console.Write("success.");
                }
            });

            EventBus.Register<ApplicationStartBefore>(e =>
            {
                OutputLine("Info", "App", "Starting...");
            });

            EventBus.Register<ApplicationStartAfter>(e =>
            {
                OutputDivider();
            });

            EventBus.Register<ApplicationStopBefore>(e =>
            {
                OutputLine("Info", "App", "Stopping...");
            });

            EventBus.Register<ApplicationStopAfter>(e =>
            {
            });

            EventBus.Register<ApplicationPollBefore>(e =>
            {
            });

            EventBus.Register<ApplicationPollAfter>(e =>
            {
                OutputDivider();
            });
        }

        private void ListenToProcessEvents()
        {
            EventBus.Register<ProcessStartBefore>(e =>
            {
                OutputLine("Info", "Process", "  └ Starting '{0}'", e.ProcessConfiguration.Description);
            });

            EventBus.Register<ProcessStartAfter>(e =>
            {
            });

            EventBus.Register<ProcessStopBefore>(e =>
            {
                OutputLine("Info", "Process", "  └ Stopping '{0}'", e.ProcessConfiguration.Description);
            });

            EventBus.Register<ProcessStopAfter>(e =>
            {
            });

            EventBus.Register<ProcessConditionMet>(e =>
            {
                OutputLine("Info", "Process", "  └ condition '{0}' triggered '{1}'", e.Condition.Decription, e.Condition.Action);
            });

            EventBus.Register<ProcessConfigurationLoadBefore>(e =>
            {
                var path = e.Path.Length > 45 ? e.Path.Substring(e.Path.Length - 45, 45) : e.Path;
                OutputLine("Info", "Process", "Loading... {0}", path);
            });

            EventBus.Register<ProcessConfigurationLoadAfter>(e =>
            {
                if (e.ProcessConfiguration != null)
                    OutputLine("Info", "Process", "└ {0}", e.ProcessConfiguration.Description);
            });
        }

        private void ListenToNotificationEvents()
        {

        }
    }
}
