using Probation.Applications.Configurations;

namespace Probation.Events.Applications.Configurations
{
    internal class ApplicationConfigurationLoadAfter : IEvent
    {
        public string Path { get; private set; }
        public IApplicationConfiguration ApplicationConfiguration { get; private set; }

        public ApplicationConfigurationLoadAfter(string path, IApplicationConfiguration applicationConfiguration)
        {
            Path = path;
            ApplicationConfiguration = applicationConfiguration;
        }
    }
}
