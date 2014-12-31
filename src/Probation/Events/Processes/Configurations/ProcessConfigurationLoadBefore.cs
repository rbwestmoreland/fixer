using System;

namespace Probation.Events.Processes.Configurations
{
    internal class ProcessConfigurationLoadBefore : IEvent
    {
        public string Path { get; private set; }

        public ProcessConfigurationLoadBefore(string path)
        {
            Path = path;
        }
    }
}
