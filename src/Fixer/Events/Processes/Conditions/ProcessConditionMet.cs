using Fixer.Processes.Configurations;
using Fixer.Processes.States;

namespace Fixer.Events.Processes.Conditions
{
    internal class ProcessConditionMet : IEvent
    {
        public IProcessConfiguration ProcessConfiguration { get; private set; }
        public IProcessState ProcessState { get; private set; }
        public ICondition Condition { get; private set; }

        public ProcessConditionMet(IProcessConfiguration processConfiguration, IProcessState processState, ICondition condition)
        {
            ProcessConfiguration = processConfiguration;
            ProcessState = processState;
            Condition = condition;
        }
    }
}
