using System;

namespace Probation.Processes.Configurations
{
    internal interface IStop
    {
        TimeSpan Duration { get; }
    }
}
