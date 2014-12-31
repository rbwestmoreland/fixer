using System;

namespace Probation.Processes.Configurations
{
    internal interface IScript
    {
        string Language { get; }
        string Source { get; }
    }
}
