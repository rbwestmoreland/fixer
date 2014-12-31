using System;

namespace Fixer.Processes.Configurations.Factories
{
    internal interface IProcessConfigurationFactory
    {
        IProcessConfiguration Create(string path);
    }
}
