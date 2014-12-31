using Probation.Processes.Configurations;

namespace Probation.Events.Processes.Actions
{
    internal class ProcessStartBefore : IEvent
    {
        public IProcessConfiguration ProcessConfiguration { get; private set; }

        public ProcessStartBefore(IProcessConfiguration processConfiguration)
        {
            ProcessConfiguration = processConfiguration;
        }
    }
}
