using System;

namespace Probation.Processes.Configurations
{
    internal interface IStart
    {
        string File { get; }
        string Arguments { get; }
    }
}
