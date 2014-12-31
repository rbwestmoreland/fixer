using Probation.Processes.Configurations;

namespace Probation.Events.Processes.Actions
{
    internal class ProcessStartAfter : IEvent
    {
        public IProcessConfiguration ProcessConfiguration { get; private set; }
        public int? ProcessId {get; private set;}

        public ProcessStartAfter(IProcessConfiguration processConfiguration, int? processId)
        {
            ProcessConfiguration = processConfiguration;
            ProcessId = processId;
        }
    }
}
