using System;

namespace Fixer.Processes.Configurations
{
    internal interface IStart
    {
        string File { get; }
        string Arguments { get; }
    }
}
