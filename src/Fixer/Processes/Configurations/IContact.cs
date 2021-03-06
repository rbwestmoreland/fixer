﻿using System.Collections.Generic;

namespace Fixer.Processes.Configurations
{
    internal interface IContact
    {
        string Name { get; }
        IEnumerable<string> Groups { get; }
        IDictionary<string, string> Addresses { get; }
    }
}
