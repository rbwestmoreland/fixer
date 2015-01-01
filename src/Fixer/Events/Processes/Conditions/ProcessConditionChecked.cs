using Fixer.Processes.Configurations;
using Fixer.Processes.States;

namespace Fixer.Events.Processes.Conditions
{
    internal class ProcessConditionChecked : IEvent
    {
        public IProcessConfiguration ProcessConfiguration { get; private set; }
        public IProcessState ProcessState { get; private set; }
        public ICondition Condition { get; private set; }
        public bool Occurred { get; private set; }
        public int TimesOccurred { get; private set; }

        public ProcessConditionChecked(IProcessConfiguration processConfiguration, IProcessState processState, ICondition condition, bool occurred, int timesOccurred)
        {
            ProcessConfiguration = processConfiguration;
            ProcessState = processState;
            Condition = condition;
            Occurred = occurred;
            TimesOccurred = timesOccurred;
        }
    }
}
