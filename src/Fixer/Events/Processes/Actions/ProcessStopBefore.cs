using Fixer.Processes.Configurations;

namespace Fixer.Events.Processes.Actions
{
    internal class ProcessStopBefore : IEvent
    {
        public IProcessConfiguration ProcessConfiguration { get; private set; }

        public ProcessStopBefore(IProcessConfiguration processConfiguration)
        {
            ProcessConfiguration = processConfiguration;
        }
    }
}
