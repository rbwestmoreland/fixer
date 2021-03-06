﻿using Fixer.Processes.Configurations;
using System;

namespace Fixer.Events.Processes.Configurations
{
    internal class ProcessConfigurationLoadAfter : IEvent
    {
        public string Path { get; private set; }
        public IProcessConfiguration ProcessConfiguration { get; private set; }

        public ProcessConfigurationLoadAfter(string path, IProcessConfiguration processConfiguration)
        {
            Path = path;
            ProcessConfiguration = processConfiguration;
        }
    }
}
