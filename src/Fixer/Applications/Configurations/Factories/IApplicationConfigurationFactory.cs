using System;

namespace Fixer.Applications.Configurations.Factories
{
    internal interface IApplicationConfigurationFactory
    {
        IApplicationConfiguration Create(string path);
    }
}
