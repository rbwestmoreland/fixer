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
            ListenToCommonEvents();
            ListenToAppictionEvents();
            ListenToProcessEvents();
            ListenToNotificationEvents();
        }

        private void Output(string type, string area, string messageFormat, params object[] args)
        {
            var message = string.Format(messageFormat, args);

            if (message.Length > 50)
            {
                message = message.Substring(0, 50);
            }

            var output = string.Format("{0,-9} │ {1,-9} │ {2}", type, area, message);

            Console.WriteLine(output);
        }

        private void ListenToCommonEvents()
        {
            EventBus.Register<ExceptionThrown>(e =>
            {
                Output("Error", "global", e.Exception.Message);
            });
        }

        private void ListenToAppictionEvents()
        {
            EventBus.Register<ApplicationConfigurationLoadBefore>(e =>
            {
                Output("Info", "App", "Loading... {0} ", e.Path);
            });

            EventBus.Register<ApplicationConfigurationLoadAfter>(e =>
            {

            });

            EventBus.Register<ApplicationStartBefore>(e =>
            {
                Output("Info", "App", "Starting...");
            });

            EventBus.Register<ApplicationStartAfter>(e =>
            {
                Output("Info", "App", "Started");
            });

            EventBus.Register<ApplicationStopBefore>(e =>
            {
                Output("Info", "App", "Stopping...");
            });

            EventBus.Register<ApplicationStopAfter>(e =>
            {
                Output("Info", "App", "Stopped");
            });

            EventBus.Register<ApplicationPollBefore>(e =>
            {
                Output("Info", "App", "Polling...");
            });

            EventBus.Register<ApplicationPollAfter>(e =>
            {
                Output("Info", "App", "Polling complete.");
            });
        }

        private void ListenToProcessEvents()
        {
            EventBus.Register<ProcessStartBefore>(e =>
            {
                Output("Info", "Process", "Starting '{0}'", e.ProcessConfiguration.Description);
            });

            EventBus.Register<ProcessStartAfter>(e =>
            {
            });

            EventBus.Register<ProcessStopBefore>(e =>
            {
                Output("Info", "Process", "Stopping '{0}'", e.ProcessConfiguration.Description);
            });

            EventBus.Register<ProcessStopAfter>(e =>
            {
            });

            EventBus.Register<ProcessConditionMet>(e =>
            {
                Output("Info", "Process", "Process '{0}' condition '{0}' triggered '{1}'", e.ProcessConfiguration.Description, e.Condition.Decription, e.Condition.Action);
            });

            EventBus.Register<ProcessConfigurationLoadBefore>(e =>
            {
                Output("Info", "Process", "Loading '{0}'", e.Path);
            });

            EventBus.Register<ProcessConfigurationLoadAfter>(e =>
            {

            });
        }

        private void ListenToNotificationEvents()
        {

        }
    }
}
