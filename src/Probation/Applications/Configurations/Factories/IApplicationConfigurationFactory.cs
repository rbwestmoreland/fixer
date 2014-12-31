using System;

namespace Probation.Applications.Configurations.Factories
{
    internal interface IApplicationConfigurationFactory
    {
        IApplicationConfiguration Create(string path);
    }
}
