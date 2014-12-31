using System;

namespace Probation.Processes.Configurations.Factories
{
    internal interface IProcessConfigurationFactory
    {
        IProcessConfiguration Create(string path);
    }
}
