using Fixer.Processes.Configurations;

namespace Fixer.Events.Processes.Actions
{
    internal class ProcessStopAfter : IEvent
    {
        public IProcessConfiguration ProcessConfiguration { get; private set; }
        public bool Success {get; private set;}

        public ProcessStopAfter(IProcessConfiguration processConfiguration, bool success)
        {
            ProcessConfiguration = processConfiguration;
            Success = success;
        }
    }
}
