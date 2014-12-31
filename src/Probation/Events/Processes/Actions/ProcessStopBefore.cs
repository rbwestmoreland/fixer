using Probation.Processes.Configurations;

namespace Probation.Events.Processes.Actions
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
