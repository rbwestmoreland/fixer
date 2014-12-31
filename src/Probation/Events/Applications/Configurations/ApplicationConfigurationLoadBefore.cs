using System;

namespace Probation.Events.Applications.Configurations
{
    internal class ApplicationConfigurationLoadBefore : IEvent
    {
        public string Path { get; private set; }

        public ApplicationConfigurationLoadBefore(string path)
        {
            Path = path;
        }
    }
}
