using System;

namespace Fixer.Processes.Configurations
{
    internal interface IStop
    {
        TimeSpan Duration { get; }
    }
}
