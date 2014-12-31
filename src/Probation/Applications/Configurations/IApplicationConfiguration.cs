using System;
using System.Collections.Generic;

namespace Probation.Applications.Configurations
{
    interface IApplicationConfiguration
    {
        string Path { get; }
        IEnumerable<string> Files { get; }
    }
}
