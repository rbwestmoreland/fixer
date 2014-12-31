using System;
using System.Collections.Generic;

namespace Fixer.Processes.Configurations
{
    internal interface ICondition
    {
        string Decription { get; }
        TimeSpan Interval { get; }
        IEnumerable<int> Ratio { get; }
        IScript Script { get; }
        string Action { get; }
        IEnumerable<string> Contact { get; }
    }
}
