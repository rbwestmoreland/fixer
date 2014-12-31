using System;

namespace Fixer.Processes.Configurations
{
    internal interface IScript
    {
        string Language { get; }
        string Source { get; }
    }
}
