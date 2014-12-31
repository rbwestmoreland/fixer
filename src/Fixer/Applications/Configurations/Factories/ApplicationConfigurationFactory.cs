using Fixer.Events;
using Fixer.Events.Applications.Configurations;

namespace Fixer.Applications.Configurations.Factories
{
    internal class ApplicationConfigurationFactory : IApplicationConfigurationFactory
    {
        public IApplicationConfiguration Create(string path)
        {
            var applicationConfiguration = default(IApplicationConfiguration);

            EventBus.Raise(new ApplicationConfigurationLoadBefore(path));

            V1.ApplicationConfigurationV1 v1;
            if(V1.ApplicationConfigurationV1.TryLoad(path, out v1))
            {
                var files = v1.Files;

                applicationConfiguration = new ApplicationConfiguration(path, files);
            }

            EventBus.Raise(new ApplicationConfigurationLoadAfter(path, applicationConfiguration));

            return applicationConfiguration;
        }
    }
}
