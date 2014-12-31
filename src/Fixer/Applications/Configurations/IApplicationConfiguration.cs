using System;
using System.Collections.Generic;

namespace Fixer.Applications.Configurations
{
    interface IApplicationConfiguration
    {
        string Path { get; }
        IEnumerable<string> Files { get; }
    }
}
