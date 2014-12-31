using System;
using System.Collections.Generic;

namespace Probation.Processes.Configurations
{
    internal interface IProcessConfiguration
    {
        string Path { get; }
        string Description { get; }
        IEnumerable<string> Groups { get; }
        IStart Start { get; }
        IStop Stop { get; }
        IEnumerable<IContact> Contacts { get; }
        IEnumerable<ICondition> Conditions { get; }
    }
}
